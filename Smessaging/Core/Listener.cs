using System;
using System.Messaging;
using System.Threading.Tasks;
using System.Threading;

namespace Smessaging.Core
{
    public class Listener : IListener
	{
        // CONSTANTS
        private const int RECEIVE_TIMEOUT = 2000;

        // PRIVATE MEMBERS
        private bool _isListening;
		private Task _task;
        private CancellationTokenSource _tokenSource;
        private IListenerTaskFactory _ltFactory;
        private IMessageConsumer _consumer;

        // PROPERTIES
		public string Endpoint { get; private set; }
		public bool Transactional { get; private set; }
		public DateTime StartTimestamp { get; private set; }
		public long MessagesReceived { get; private set; }
		public long MessagedAccepted { get; private set; }

        // CONSTRUCTOR
        public Listener(IMessageConsumer msgConsumer, string queueEndpoint, 
            bool transactional) {

            _consumer = msgConsumer;
			this.Endpoint = queueEndpoint;
			this.Transactional = transactional;
		}

        // PUBLIC METHODS
		public void Start() {

            _isListening = true;

			this.StartTimestamp = DateTime.Now;
			this.MessagedAccepted = 0;
			this.MessagesReceived = 0;

            _tokenSource = new CancellationTokenSource();
            _ltFactory = new ListenerTaskFactory(this.Endpoint, 
                Listener.RECEIVE_TIMEOUT, _tokenSource);

            var faultHandler = new ListenerFaultHandler();
            _task = _ltFactory.Create(
                (msg, trans, token) => _consumer.Consume(msg, trans, token),
                (t) => faultHandler.Handle(t, this));
            _task.Start();
		}

		public void Stop() {

            if (!_isListening) return;
            if (_tokenSource == null) {
                throw new NullReferenceException ("CancellationTokenSource");
            }

            _isListening = false;
            _tokenSource.Cancel();
		}
	}
}

