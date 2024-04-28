using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryBase
{
    public class CommandEnum
    {
        public enum ObjectType
        {
            ErrorRegistration = 0,
            ErrorLogin,
            Registration,
            Login,
            GetAllUsers,
            GetAllMessage,
            SendMessage,
            GetFile,
            FirstMessage
        }
    }
}
