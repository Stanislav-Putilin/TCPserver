using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbDataModels
{
    public class UserMessage
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime TimeSent { get; set; }
        public virtual User Sender { get; set; }
        public virtual User Receiver { get; set; }

        public bool HasAttachment { get; set; }
        public string? AttachmentFileName { get; set; }

        public UserMessage()
        {

        }

        public UserMessage(string content, User Sender, User Receiver, bool HasAttachment)
        {
            Content = content;
            TimeSent = DateTime.Now;
            this.Sender = Sender;
            this.Receiver = Receiver;
        }

        public UserMessage(string content, User Sender, User Receiver, bool hasAttachment, string? attachmentFileName)
        {
            Content = content;
            TimeSent = DateTime.Now;
            this.Sender = Sender;
            this.Receiver = Receiver;

            HasAttachment = hasAttachment;
            AttachmentFileName = attachmentFileName;
        }
    }
}
