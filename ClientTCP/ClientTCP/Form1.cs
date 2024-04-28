using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using DbDataModels;
using ClassLibraryBase;

using static ClassLibraryBase.CommandEnum;
using System.Collections.Generic;

namespace ClientTCP
{
    public partial class Form1 : Form
    {
        bool change = false;

        byte[] currentFileData;
        string fileNameWithExtension;
        bool HasAttachment = false;

        byte[] bufR = new byte[1024];
        int sizeR = 0;
        Client client;
        User currentUser;
        List<User> users = new List<User>();

        public Form1(Client client, User currentUser)
        {
            InitializeComponent();
            this.FormClosing += Form1_FormClosing;
            this.currentUser = currentUser;
            this.client = client;           
            label4.Text = this.currentUser.UserName;
            dataGridView1.ReadOnly = true;

            client.MainDataReceived += Client_MainDataReceived;

            LoadUsersAsync();
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (client != null && client.clientSocket.Connected)
                {
                    client.clientSocket.Send(Encoding.UTF8.GetBytes("[Client]: End"));

                    client.clientSocket.Close();
                }
                else if (client?.clientSocket != null)
                {
                    client.clientSocket.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error while closing server socket: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text != "" && textBox1.Text != "")
            {
                ClientMessage clientMessage = null;

                if (!HasAttachment)
                {
                    clientMessage = new ClientMessage(textBox1.Text, currentUser.UserName, comboBox1.Text);
                }
                else
                {
                    clientMessage = new ClientMessage(currentUser.UserName, comboBox1.Text, textBox1.Text, HasAttachment, currentFileData, fileNameWithExtension);
                }

                string dataMessage = JsonSerialization.Serialize(clientMessage);

                TransportObject transport = new TransportObject(ObjectType.SendMessage, dataMessage);

                string data = JsonSerialization.Serialize(transport);

                client.Send(data);                

                HasAttachment = false;                
            }
            else
            {
                MessageBox.Show("Сообщение не должно быть пустым.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadUsersAsync()
        {
            GetUsers();            
        }

        public void GetUsers()
        {
            TransportObject transport = new TransportObject(ObjectType.GetAllUsers);

            string data = JsonSerialization.Serialize(transport);
            client.Send(data);            
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text != "" && change)
            {
                Clients clients = new Clients(currentUser.UserName, comboBox1.Text);

                string dataClients = JsonSerialization.Serialize(clients);

                TransportObject transport = new TransportObject(ObjectType.GetAllMessage, dataClients);

                string data = JsonSerialization.Serialize(transport);
                client.Send(data);                
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Title = "Выберите файл";
            openFileDialog.Filter = "Все файлы (*.*)|*.*";
            openFileDialog.FilterIndex = 1;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string selectedFilePath = openFileDialog.FileName;

                    currentFileData = File.ReadAllBytes(selectedFilePath);

                    fileNameWithExtension = Path.GetFileName(selectedFilePath);

                    HasAttachment = true;

                    MessageBox.Show("Файл успешно загружен!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при загрузке файла: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

                ClientMessage selectedMessage = selectedRow.DataBoundItem as ClientMessage;

                if (selectedMessage != null && selectedMessage.HasAttachment)
                {
                    string dataMessage = JsonSerialization.Serialize(selectedMessage);

                    TransportObject transport = new TransportObject(ObjectType.GetFile, dataMessage);

                    string data = JsonSerialization.Serialize(transport);
                    client.Send(data);                    
                }
                else
                {
                    MessageBox.Show("В этом письме нет вложений.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите только одну строку.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

                ClientMessage selectedMessage = selectedRow.DataBoundItem as ClientMessage;

                if (selectedMessage != null)
                {
                    richTextBox1.Text = selectedMessage.Content;
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            LoadUsersAsync();
        }

        public void UpdateMainForm(TransportObject dataObj)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action<TransportObject>(UpdateMainForm), dataObj);
            }
            else
            {
                List<ClientMessage> allMessage;
                switch (dataObj.ObjectType)
                {                    
                    case ObjectType.GetAllUsers:

                        List<User> usersAll = JsonSerialization.Deserialize<List<User>>(dataObj.Data);
                        comboBox1.DataSource = usersAll;
                        comboBox1.DisplayMember = "UserName";
                        change = true;
                        break;

                    case ObjectType.GetAllMessage:
                        allMessage = JsonSerialization.Deserialize<List<ClientMessage>>(dataObj.Data);
                        dataGridView1.DataSource = allMessage;
                        dataGridView1.Columns["AttachmentData"].Visible = false;
                        break;

                    case ObjectType.SendMessage:

                        allMessage = JsonSerialization.Deserialize<List<ClientMessage>>(dataObj.Data);

                        if(comboBox1.Text == allMessage[0].SenderName | comboBox1.Text == allMessage[0].ReceiverName)
                        {
                            dataGridView1.DataSource = allMessage;
                        }                        
                        break;

                    case ObjectType.GetFile:

                        ClientMessage fileMessage = JsonSerialization.Deserialize<ClientMessage>(dataObj.Data);

                        if (fileMessage.AttachmentData != null)
                        {
                            using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
                            {
                                folderDialog.Description = "Выберите папку для сохранения файла";

                                DialogResult result = folderDialog.ShowDialog();

                                if (result == DialogResult.OK)
                                {
                                    string selectedFolderPath = folderDialog.SelectedPath;

                                    selectedFolderPath += "/" + fileMessage.AttachmentFileName;

                                    using (FileStream fileStream = new FileStream(selectedFolderPath, FileMode.Create))
                                    {
                                        fileStream.Write(fileMessage.AttachmentData, 0, fileMessage.AttachmentData.Length);
                                    }
                                }
                            }
                        }
                        break;                    
                }
            }
        }

        private void Client_MainDataReceived(object sender, TransportObject dataObj)
        {
            UpdateMainForm(dataObj);
        }
    }
}