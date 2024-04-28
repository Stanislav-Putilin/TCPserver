using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClassLibraryBase;
using Microsoft.EntityFrameworkCore;
using DbDataModels;
using Microsoft.VisualBasic.Logging;
using System.Security.Cryptography.Xml;
using static ClassLibraryBase.CommandEnum;
using System.Net.Mail;


namespace ServerTCP
{
    class Server
    {
        string ip;
        int port;
        int numberOfClients;
        IPAddress ipAddr;
        EndPoint endPoint;
        Socket server;
        RichTextBox textBox;
        Form mainForm;

        List<ClientSocet> clientSocets = new List<ClientSocet>();

        const string msgHello = "\n[Server]: Вы подключены, введите логин и пароль.\n";
        public Server(string ip, int port, int numberOfClients, RichTextBox textBox, Form mainForm)
        {
            this.ip = ip;
            this.port = port;
            this.numberOfClients = numberOfClients;
            this.textBox = textBox;
            this.mainForm = mainForm;
            ipAddr = IPAddress.Parse(ip);
            endPoint = new IPEndPoint(ipAddr, port);
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
        }

        public void Start(ToolStripStatusLabel statusLabel)
        {
            server.Bind(endPoint);
            server.Listen(numberOfClients);
            statusLabel.Text = "Сервер запущен, ожидание пользователей!";

            server.BeginAccept(ClientAccept, server);
        }

        void ClientAccept(IAsyncResult result)
        {
            server.BeginAccept(ClientAccept, server);

            Socket serv = (Socket)result.AsyncState;
            Socket client = serv.EndAccept(result);

            TransportObject transport = new TransportObject(ObjectType.FirstMessage, msgHello);
            string data = JsonSerialization.Serialize(transport);
            byte[] buf = Encoding.UTF8.GetBytes(data);

            client.BeginSend(buf, 0, buf.Length, SocketFlags.None, SendMsgToClient, client);
        }

        async void SendMsgToClient(IAsyncResult result)
        {
            Socket cl = result.AsyncState as Socket;
            cl.EndSend(result);
            Thread.Sleep(1000);

            mainForm.Invoke(new Action(() => textBox.Text += $"\n[Server]: Client {cl.RemoteEndPoint} connected"));

            byte[] buf = new byte[20971520];
            string userResponse = "";
            int sizeUserResponse = 0;

            await LoginOrRegistration(cl, buf, sizeUserResponse, userResponse);

            foreach (var item in clientSocets)
            {
                mainForm.Invoke(new Action(() => textBox.Text += $"\nClient {item.Username}"));
            }

            try
            {
                do
                {
                    sizeUserResponse = cl.Receive(buf);
                    userResponse = Encoding.UTF8.GetString(buf, 0, sizeUserResponse);

                    TransportObject transport = JsonSerialization.Deserialize<TransportObject>(userResponse);

                    switch (transport.ObjectType)
                    {
                        case ObjectType.GetAllUsers:
                            using (ServerContext context = GetServerContext())
                            {
                                List<User> users = await context.Users.ToListAsync();

                                if (users != null)
                                {
                                    transport.ObjectType = ObjectType.GetAllUsers;

                                    string allUser = JsonSerialization.Serialize(users);

                                    transport.Data = allUser;

                                    string data = JsonSerialization.Serialize(transport);
                                    cl.Send(Encoding.UTF8.GetBytes(data));
                                }
                                else
                                {
                                    transport.ObjectType = ObjectType.ErrorLogin;

                                    string data = JsonSerialization.Serialize(transport);

                                    cl.Send(Encoding.UTF8.GetBytes(data));
                                }
                            }
                            break;

                        case ObjectType.GetAllMessage:

                            Clients clients = JsonSerialization.Deserialize<Clients>(transport.Data);
                            using (ServerContext context = GetServerContext())
                            {
                                User userSender = await context.Users.FirstOrDefaultAsync(u => u.UserName == clients.SenderName);
                                User userResiver = await context.Users.FirstOrDefaultAsync(u => u.UserName == clients.ReceiverName);

                                if (userSender != null && userResiver != null)
                                {
                                    await context.UserMessages.Include(u => u.Sender).Include(u => u.Receiver).LoadAsync();

                                    var allMessage = context.UserMessages.Local
                                                                         .Where(u => (u.Sender.Id == userSender.Id && u.Receiver.Id == userResiver.Id) ||
                                                                               (u.Sender.Id == userResiver.Id && u.Receiver.Id == userSender.Id))
                                                                         .OrderBy(u => u.TimeSent)
                                                                         .Select(u => new ClientMessage(u.TimeSent, u.Sender.UserName, u.Receiver.UserName, u.Content, u.HasAttachment, u.AttachmentFileName)).ToList();

                                    transport.ObjectType = ObjectType.GetAllMessage;

                                    string allMessageData = JsonSerialization.Serialize(allMessage);

                                    transport.Data = allMessageData;

                                    string data = JsonSerialization.Serialize(transport);
                                    cl.Send(Encoding.UTF8.GetBytes(data));
                                }
                                else
                                {
                                    transport.ObjectType = ObjectType.ErrorLogin;

                                    string data = JsonSerialization.Serialize(transport);

                                    cl.Send(Encoding.UTF8.GetBytes(data));
                                }
                            }
                            break;

                        case ObjectType.SendMessage:

                            ClientMessage clientMessage = JsonSerialization.Deserialize<ClientMessage>(transport.Data);

                            using (ServerContext context = GetServerContext())
                            {
                                User userSender = await context.Users.FirstOrDefaultAsync(u => u.UserName == clientMessage.SenderName);
                                User userResiver = await context.Users.FirstOrDefaultAsync(u => u.UserName == clientMessage.ReceiverName);
                                UserMessage message = null;

                                if (userSender != null && userResiver != null)
                                {
                                    if (clientMessage.HasAttachment)
                                    {
                                        string temp = clientMessage.AttachmentFileName;

                                        string currentDirectory = Directory.GetCurrentDirectory();

                                        string filePath = currentDirectory + "/files/" + temp;

                                        string filesDirectory = Path.Combine(currentDirectory, "files");

                                        if (!Directory.Exists(filesDirectory))
                                        {
                                            Directory.CreateDirectory(filesDirectory);
                                        }

                                        using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
                                        {
                                            fileStream.Write(clientMessage.AttachmentData, 0, clientMessage.AttachmentData.Length);
                                        }
                                        message = new UserMessage(clientMessage.Content, userSender, userResiver, clientMessage.HasAttachment, temp);
                                    }
                                    else
                                    {
                                        message = new UserMessage(clientMessage.Content, userSender, userResiver, clientMessage.HasAttachment);
                                    }

                                    context.Add(message);
                                    await context.SaveChangesAsync();

                                    await context.UserMessages.Include(u => u.Sender).Include(u => u.Receiver).LoadAsync();

                                    var allMessage = context.UserMessages.Local
                                                                         .Where(u => (u.Sender.Id == userSender.Id && u.Receiver.Id == userResiver.Id) ||
                                                                               (u.Sender.Id == userResiver.Id && u.Receiver.Id == userSender.Id))
                                                                         .OrderBy(u => u.TimeSent)
                                                                         .Select(u => new ClientMessage(u.TimeSent, u.Sender.UserName, u.Receiver.UserName, u.Content, u.HasAttachment, u.AttachmentFileName)).ToList();

                                    transport.ObjectType = ObjectType.SendMessage;

                                    string allMessageData = JsonSerialization.Serialize(allMessage);

                                    transport.Data = allMessageData;

                                    string data = JsonSerialization.Serialize(transport);
                                    cl.Send(Encoding.UTF8.GetBytes(data));

                                    ClientSocet clientWithUsername = clientSocets.FirstOrDefault(client => client.Username == userResiver.UserName);

                                    if (clientWithUsername != null)
                                    {
                                        clientWithUsername.Socket.Send(Encoding.UTF8.GetBytes(data));
                                    }
                                }
                                else
                                {
                                    transport.ObjectType = ObjectType.ErrorLogin;

                                    string data = JsonSerialization.Serialize(transport);

                                    cl.Send(Encoding.UTF8.GetBytes(data));
                                }
                            }
                            break;

                        case ObjectType.GetFile:

                            ClientMessage fileMessage = JsonSerialization.Deserialize<ClientMessage>(transport.Data);

                            string fileDirectory = Directory.GetCurrentDirectory();

                            string fileFullPath = fileDirectory + "/files/" + fileMessage.AttachmentFileName;

                            fileMessage.AttachmentData = File.ReadAllBytes(fileFullPath);

                            string fileMessageData = JsonSerialization.Serialize(fileMessage);

                            transport.Data = fileMessageData;

                            string dataFile = JsonSerialization.Serialize(transport);

                            cl.Send(Encoding.UTF8.GetBytes(dataFile));

                            break;
                    }
                } while (true);
            }
            catch
            {
                mainForm.Invoke(new Action(() => textBox.Text += $"\n[Server]: Client {cl.RemoteEndPoint} disconnected"));

                cl.Shutdown(SocketShutdown.Both);

                ClientSocet clientSocet = clientSocets.FirstOrDefault(client => client.Socket == cl);

                cl.Close();

                clientSocets.Remove(clientSocet);
            }           
        }

        private async Task LoginOrRegistration(Socket cl, byte[] buf, int sizeUserResponse, string userResponse)
        {
            bool login = false;

            do
            {
                sizeUserResponse = cl.Receive(buf);
                userResponse = Encoding.UTF8.GetString(buf, 0, sizeUserResponse);

                TransportObject transport = JsonSerialization.Deserialize<TransportObject>(userResponse);

                if (transport.ObjectType == ObjectType.Registration)
                {
                    User userCurrent = JsonSerialization.Deserialize<User>(transport.Data);
                    using (ServerContext context = GetServerContext())
                    {

                        User user = await context.Users.FirstOrDefaultAsync(u => u.UserName == userCurrent.UserName);

                        if (user != null)
                        {
                            transport.ObjectType = ObjectType.ErrorRegistration;
                            transport.Data = "Такой пользователь существует";
                            string data = JsonSerialization.Serialize(transport);
                            cl.Send(Encoding.UTF8.GetBytes(data));
                        }
                        else
                        {
                            context.Add(userCurrent);
                            await context.SaveChangesAsync();

                            cl.Send(Encoding.UTF8.GetBytes(userResponse));

                            clientSocets.Add(new ClientSocet(cl, userCurrent.UserName));

                            login = true;
                        }
                    }
                }
                else
                {
                    User userCurrent = JsonSerialization.Deserialize<User>(transport.Data);
                    using (ServerContext context = GetServerContext())
                    {
                        User user = await context.Users.FirstOrDefaultAsync(u => u.UserName == userCurrent.UserName);

                        if (user != null)
                        {
                            if (user.Password == userCurrent.Password)
                            {
                                transport.ObjectType = ObjectType.Login;
                                string data = JsonSerialization.Serialize(transport);
                                cl.Send(Encoding.UTF8.GetBytes(data));
                                clientSocets.Add(new ClientSocet(cl, userCurrent.UserName));
                                login = true;
                            }
                            else
                            {
                                transport.ObjectType = ObjectType.ErrorLogin;
                                transport.Data = "Не верный логин или пароль!";
                                string data = JsonSerialization.Serialize(transport);
                                cl.Send(Encoding.UTF8.GetBytes(data));
                            }
                        }
                        else
                        {
                            transport.ObjectType = ObjectType.ErrorLogin;
                            transport.Data = "Такого пользователя не существует!";
                            string data = JsonSerialization.Serialize(transport);
                            cl.Send(Encoding.UTF8.GetBytes(data));
                        }
                    }
                }
            } while (!login);
        }

        protected ServerContext GetServerContext()
        {
            MyServerContextFactory contextFactory = new MyServerContextFactory();
            return contextFactory.CreateDbContext(new string[] { });
        }
    }
}