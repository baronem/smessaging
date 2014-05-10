using System;
using System.Threading.Tasks;

namespace Smessaging.Core
{
    public class ListenerFaultHandler : IListenerFaultHandler
    {
        public void Handle(Task currentTask, IListener parentListener) {
            throw new NotImplementedException();
        }
    }
}

