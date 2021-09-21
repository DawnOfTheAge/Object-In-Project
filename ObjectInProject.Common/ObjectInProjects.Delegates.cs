using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectInProject.Common
{
    public delegate bool CreateUpdateSearchProjectReply(CrudAction crudAction, SearchProject searchProject, out string result);
    public delegate bool EventMessage(object message, out object reply, out string result);
}
