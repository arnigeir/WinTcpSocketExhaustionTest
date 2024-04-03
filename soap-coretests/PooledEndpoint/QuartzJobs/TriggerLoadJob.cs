using NServiceBus;
using NServiceBus.Logging;
using SNBEndpoint.Messages;
using Quartz;

namespace SNBEndpoint
{

    public class TriggerLoadJob : IJob
    {
        private static readonly ILog log = LogManager.GetLogger<TriggerLoadJob>();
        private readonly IMessageSession _messageSession;
        private static readonly int _batchSize = 2000;

        public TriggerLoadJob(IMessageSession messageSession)
        {
            _messageSession = messageSession;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            //send a burst of messages on each trigger
            try
            {
                var triggerID = Guid.NewGuid().ToString();
                log.Info($"Send {_batchSize} messages");
                for (int i = 0; i < _batchSize; i++)
                {
                    await _messageSession.SendLocal<TriggerMessage>(m =>
                    {
                        m.TriggerId = triggerID;
                    }).ConfigureAwait(false);
                }
            }
            catch (Exception e)
            {
                log.Fatal($"Fatal error on executing job: {e.Message}");
                throw;
            }
        }
    }
}
