using System;

namespace Smessaging.Core
{
    public interface IListener
    {
        string Endpoint { get; }
        bool Transactional { get; }
        DateTime StartTimestamp { get; }
        long MessagesReceived { get; }
        long MessagedAccepted { get; }

        void Start();
        void Stop();
    }
}

