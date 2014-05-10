using System;
using System.Messaging;

namespace Smessaging.Core
{
    #region Receive
    internal class MessageQueueReceiveValue
    {
        internal MessageQueueReceiveValue() : 
        this(false, null) { }
        internal MessageQueueReceiveValue(Message content) : 
        this(true, content) { }
        internal MessageQueueReceiveValue(bool received, Message content)
        {
            this.MessageReceived = received;
            this.Value = content;
        }

        internal Message Value { get; private set; }
        internal bool MessageReceived { get; private set;}
    }
    #endregion
}

