using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DbDataModels;
using ClassLibraryBase;
using static ClassLibraryBase.CommandEnum;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace ClientTCP
{
    public class Client
    {
        public event EventHandler<TransportObject> AuthDataReceived;
        public event EventHandler<TransportObject> MainDataReceived;

        string ip = "127.0.0.1";
        int port = 700;
        byte[] bufR = new byte[20975520];
        int sizeR = 0;
        public Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP); 

        public Client()
        {            
            
        }

        public async void ConnectClient()
        {
            IPAddress iPAddress = IPAddress.Parse(ip);
            IPEndPoint endPoint = new IPEndPoint(iPAddress, port);

            await AcceptClientAsync(endPoint);
        }

        private async Task AcceptClientAsync(IPEndPoint endPoint)
        {
            try
            {
                await Task.Run(() => clientSocket.Connect(endPoint));
                if (clientSocket.Connected)
                {
                    StartReceivingThread();                   
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error accepting client: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void Send(string msg)
        {
            try
            {
                if (clientSocket.Connected)
                {
                    clientSocket.Send(Encoding.UTF8.GetBytes(msg));                    
                }                
            }
            catch (Exception ex)
            {                
                throw new Exception($"Error sending message: {ex.Message}", ex);
            }
        }

        public void StartReceivingThread()
        {
            Thread receivingThread = new Thread(Receive);
            receivingThread.IsBackground = true;
            receivingThread.Start();
        }

        private void Receive()
        {
            try
            {
                while (clientSocket.Connected)
                {
                    sizeR = clientSocket.Receive(bufR);                    

                    string data = Encoding.UTF8.GetString(bufR, 0, sizeR);

                    TransportObject dataObj = JsonSerialization.Deserialize<TransportObject>(data);                                      

                    switch (dataObj.ObjectType)
                    {
                        case ObjectType.ErrorRegistration:

                            AuthDataReceived?.Invoke(this, dataObj);
                            break;

                        case ObjectType.ErrorLogin:
                            AuthDataReceived?.Invoke(this, dataObj);
                            break;

                        case ObjectType.Registration:
                            AuthDataReceived?.Invoke(this, dataObj);
                            break;

                        case ObjectType.Login:
                            AuthDataReceived?.Invoke(this, dataObj);                            
                            break;

                        case ObjectType.GetAllUsers:
                            MainDataReceived?.Invoke(this, dataObj);
                            break;

                        case ObjectType.GetAllMessage:
                            MainDataReceived?.Invoke(this, dataObj);
                            break;

                        case ObjectType.SendMessage:
                            MainDataReceived?.Invoke(this, dataObj);
                            break;

                        case ObjectType.GetFile:
                            MainDataReceived?.Invoke(this, dataObj);
                            break;

                        case ObjectType.FirstMessage:
                            AuthDataReceived?.Invoke(this, dataObj);
                            break;
                    }
                }
            }
            catch
            {

            }
        }

    }
}