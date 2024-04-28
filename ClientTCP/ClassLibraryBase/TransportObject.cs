using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbDataModels;
using static ClassLibraryBase.CommandEnum;

namespace ClassLibraryBase
{
    public class TransportObject
    {
        public ObjectType ObjectType { get; set; } 
        public string Data { get; set; }        

        public TransportObject()
        {            
        }
        public TransportObject(ObjectType objectType)
        {
            ObjectType = objectType;            
        }
        public TransportObject(ObjectType objectType, string data)
        {
            ObjectType = objectType;
            Data = data;           
        }        
    }
}
