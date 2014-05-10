using System;
using System.Messaging;

namespace Smessaging.Core
{
    internal static class MessageQueueExentensions
    {
        #region Receive            
        internal static MessageQueueReceiveValue TryReceive(this MessageQueue mq,
            TimeSpan timeout, MessageQueueTransaction transaction)
        {
            try {
                var result = mq.Receive(timeout, transaction);
                return new MessageQueueReceiveValue(result);
            } catch (MessageQueueException) {
                return new MessageQueueReceiveValue ();
            }
        }
        internal static MessageQueueReceiveValue TryReceive(this MessageQueue mq,
            int timeoutMilliseconds, MessageQueueTransaction transaction)
        {
            return MessageQueueExentensions.TryReceive(mq, 
                TimeSpan.FromMilliseconds(timeoutMilliseconds), transaction);
        }
        #endregion
    }
}

