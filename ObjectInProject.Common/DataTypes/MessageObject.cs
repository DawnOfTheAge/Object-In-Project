using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectInProject.Common
{
    public class MessageObject
    {
        public MessageObject(IoAction action)
        {
            Action = action;
            Data = null;
        }

        public MessageObject(IoAction action, object data)
        {
            Action = action;
            Data = data;
        }

        public IoAction Action { get; set; }
        public object Data { get; set; }
    }
}
