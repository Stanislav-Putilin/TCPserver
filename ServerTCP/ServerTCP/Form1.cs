using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ServerTCP
{
    public partial class Form1 : Form
    {
        Server server;

        string ip = "127.0.0.1";
        int port = 700;
        int numberOfClients = 10;

        public Form1()
        {
            InitializeComponent();
            this.FormClosing += Form1_FormClosing;
            server = new Server(ip, port, numberOfClients, richTextBox1, this);
            label4.Text = ip;
            label3.Text = port.ToString();            
            toolStripStatusLabel1.Text = "ќжидание запуска сервера!";
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            { 
                //if (serverSocket != null && serverSocket.Connected)
                //{
                //    serverSocket.Shutdown(SocketShutdown.Both);
                //    serverSocket.Close();
                //}
                //else if (serverSocket != null)
                //{
                //    serverSocket.Close();
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error while closing server socket: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }       

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {               
                button2.Enabled = false;
                server.Start(toolStripStatusLabel1);               
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error during server start: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                button2.Enabled = true;               
            }
        }        
    }
}