using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClassLibraryBase;
using DbDataModels;

using static ClassLibraryBase.CommandEnum;


namespace ClientTCP
{
    public partial class Authcs : Form
    {       

        Client client;

        public Authcs(Client client)
        {
            InitializeComponent();
            label7.Text = "";
            this.client = client;            

            client.AuthDataReceived += Client_AuthDataReceived;
        }

        //Вход
        private /*async*/ void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "")
            {
                User user = new User(textBox1.Text, textBox2.Text);

                string userData = JsonSerialization.Serialize(user);

                TransportObject transport = new TransportObject(ObjectType.Login, userData);

                string data = JsonSerialization.Serialize(transport);                

                client.Send(data);                
            }
            else
            {
                label7.Text = "Заполните все поля!";
            }
        }

        //Регистрация
        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox3.Text != "")
            {
                if (textBox4.Text == textBox5.Text)
                {
                    User user = new User(textBox3.Text, textBox4.Text);
                    string userData = JsonSerialization.Serialize(user);
                    TransportObject transport = new TransportObject(ObjectType.Registration, userData);

                    string data = JsonSerialization.Serialize(transport);
                    client.Send(data);                    
                }
                else
                {
                    label6.Text = "Пароли не совпадают";
                }
            }
            else
            {
                label6.Text = "Логин не должен быть пустым!";
            }
        }

        //Подключение
        private void button3_Click(object sender, EventArgs e)
        {
            client.ConnectClient();
        }

        public void UpdateAuthForm(TransportObject dataObj)
        {
            if (InvokeRequired)
            {                
                BeginInvoke(new Action<TransportObject>(UpdateAuthForm), dataObj);
            }
            else
            {
                switch (dataObj.ObjectType)
                {
                    case ObjectType.ErrorRegistration:                        
                        label6.Text = dataObj.Data;                        
                        break;

                    case ObjectType.ErrorLogin:
                        label7.Text = dataObj.Data;
                        break;

                    case ObjectType.Registration:
                        label6.Text = "Добро пожаловать!";
                        OpenMainMenu(dataObj);
                        break;

                    case ObjectType.Login:
                        label7.Text = "Добро пожаловать!";
                        OpenMainMenu(dataObj);
                        break;   
                        
                    case ObjectType.FirstMessage:
                        label7.Text = dataObj.Data;
                        break;                   
                }                
            }
        }

        

        private void Client_AuthDataReceived(object sender, TransportObject dataObj)
        {
            UpdateAuthForm(dataObj);
        }

        void OpenMainMenu(TransportObject dataObj)
        {
            User currentUser = JsonSerialization.Deserialize<User>(dataObj.Data);

            Form1 newForm = new Form1(client, currentUser);

            this.Hide();
            newForm.FormClosed += (sender, e) => { this.Close(); };
            newForm.Show();
        }
    }
}