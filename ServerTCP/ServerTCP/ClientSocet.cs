using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ServerTCP
{
    public class ClientSocet
    {
        public Socket Socket { get; set; }
        public string Username { get; set; }

        public ClientSocet(Socket socket, string username)
        {
            Socket = socket;
            Username = username;
        }
    }
}
