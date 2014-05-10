using System;
using System.Threading.Tasks;
using System.Threading;
using System.Messaging;

namespace Smessaging.Core
{
    internal interface IListenerTaskFactory
    {
        Task Create(Action<Message, MessageQueueTransaction, 
            CancellationToken> processMsgAction, 
            Action<Task> faultContinuation);
    }
}

