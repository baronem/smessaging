using System;
using System.Threading.Tasks;
using System.Threading;
using System.Messaging;

namespace Smessaging.Core
{
    internal class ListenerTaskFactory : IListenerTaskFactory
    {
        private readonly string _endpoint;
        private readonly int _timeout;
        private readonly CancellationTokenSource _tokenSource;

        public ListenerTaskFactory(string endpoint, int timeoutMilliseconds,
            CancellationTokenSource tokenSource) {

            _endpoint = endpoint;
            _timeout = timeoutMilliseconds;
            _tokenSource = tokenSource;
        }

        public Task Create(
            Action<Message, MessageQueueTransaction, 
                CancellationToken> processMsgAction, 
            Action<Task> faultAction) {

            // todo: further seperate by putting logic into another class
            //       that is taken in the factory's constructor?
            var token = _tokenSource.Token;
            var task = new Task(() => {

                var mode = QueueAccessMode.ReceiveAndAdmin;
                while (!token.IsCancellationRequested) {
                    using (var mq = new MessageQueue(_endpoint, mode)) {
                        using (var t = new MessageQueueTransaction()) {

                            var timeout = TimeSpan
                                .FromMilliseconds(_timeout);
                            var result = mq.TryReceive(timeout, t);
                            if (result.MessageReceived) {
                                processMsgAction(result.Value, t, token);
                            }
                        }
                    }
                }

                token.ThrowIfCancellationRequested();

            }, token, TaskCreationOptions.LongRunning);

            var options = TaskContinuationOptions.OnlyOnFaulted;
            task.ContinueWith(faultAction, CancellationToken.None, 
                options, TaskScheduler.Default);

            return task;
        }
    }
}

