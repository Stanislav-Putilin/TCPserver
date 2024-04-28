using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryBase
{
    public class Clients
    {
        public string SenderName { get; set; }
        public string ReceiverName { get; set; }

        public Clients() { }    

        public Clients(string senderName, string receiverName)
        {
            SenderName = senderName;
            ReceiverName = receiverName;
        }
    }
}
