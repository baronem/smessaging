using System;
using System.Messaging;
using System.Threading;

namespace Smessaging.Core
{
    public interface IMessageConsumer
    {
        void Consume(Message msg, MessageQueueTransaction t, 
            CancellationToken token);
    }

    public interface IMessageConsumer<TMsgBody>
        : IMessageConsumer { }
}

