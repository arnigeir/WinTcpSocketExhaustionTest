using NServiceBus;

namespace SNBEndpoint.Messages
{
    public class TriggerMessage : ICommand
    {
        public string TriggerId { get; set; }
    }
}
