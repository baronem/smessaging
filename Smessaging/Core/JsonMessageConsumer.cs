using System;
using System.Messaging;
using System.Threading;
using Newtonsoft.Json;

namespace Smessaging.Core
{
    public class JsonMessageConsumer<TMsgBody> : IMessageConsumer<TMsgBody>
    {

         public void Consume(Message msg, MessageQueueTransaction t, 
            CancellationToken token) {

            token.ThrowIfCancellationRequested();

            TMsgBody value = JsonConvert.DeserializeObject<TMsgBody>((string)msg.Body);
        }
    }
}

