namespace NerdStore.Core.Messages
{
    public abstract class Message
    {
        protected Message()
        {
            MessageType = GetType().Name;
        }

        public string MessageType { get; private set; }
        public Guid AggregateId { get; set; }
    }
}
