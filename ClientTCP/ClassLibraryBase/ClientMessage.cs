using DbDataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryBase
{
    public class ClientMessage
    {
        public DateTime TimeSent { get; set; }          
        public string SenderName { get; set; }
        public string ReceiverName { get; set; }
        public string Content { get; set; }
        public bool HasAttachment { get; set; }
        public string AttachmentFileName { get; set; }
        public byte[]? AttachmentData { get; set; }       

        public ClientMessage()
        {            
        }
        public ClientMessage(string content, string senderName, string receiverName)
        {
            Content = content;
            SenderName = senderName;
            ReceiverName = receiverName;
        }
        
        public ClientMessage(DateTime timeSent,  string senderName, string receiverName, string content)
        {
            Content = content;
            SenderName = senderName;
            ReceiverName = receiverName;
            TimeSent = timeSent;
        }

        public ClientMessage(DateTime timeSent, string senderName, string receiverName, string content, bool hasAttachment, string attachmentFileName)
        {
            SenderName = senderName;
            ReceiverName = receiverName;
            Content = content;
            HasAttachment = hasAttachment;
            AttachmentFileName = attachmentFileName;
            TimeSent = timeSent;
        }

        public ClientMessage(string senderName, string receiverName, string content, bool hasAttachment, byte[] attachmentData, string attachmentFileName)
        {            
            SenderName = senderName;
            ReceiverName = receiverName;
            Content = content;
            HasAttachment = hasAttachment;
            AttachmentData = attachmentData;
            AttachmentFileName = attachmentFileName;
        }

        
    }
}
