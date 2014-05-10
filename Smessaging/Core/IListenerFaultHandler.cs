using System;
using System.Threading.Tasks;

namespace Smessaging.Core
{
    public interface IListenerFaultHandler
    {
        void Handle(Task currentTask, IListener parentListener);
    }
}

