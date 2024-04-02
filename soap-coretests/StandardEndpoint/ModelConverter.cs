using ORServiceBus.Endpoint.Configuration.NetCore;

namespace SNBEndpoint
{
    public static class ModelConverter
    {
        public static NServiceBusConfig ToNServiceBusConfig(this NServiceBusOptions options)
        {
            var config = new NServiceBusConfig
            {
                EndpointName = options.EndpointName,
                BusDBConnectionString = options.BusDBConnectionString,
                TransportConnectionString = options.TransportConnectionString,
                ServiceControlMetricsAddress = options.ServiceControlMetricsAddress,
                ServiceControlQueue = options.ServiceControlQueue,
                ServiceControlMachineName = options.ServiceControlMachineName,
                AuditQueueName = options.AuditQueueName,
                ErrorQueueName = options.ErrorQueueName,
                ImmediateDelivery_NumberOfRetries = options.ImmediateDelivery_NumberOfRetries,
                DelayedDelivery_NumberOfRetries = options.DelayedDelivery_NumberOfRetries,
                DelayedDelivery_TimeIncrease = options.DelayedDelivery_TimeIncrease,
                EnableInfluxMetrics = options.EnableInfluxMetrics,
                MessageProcessingConcurrency = options.MessageProcessingConcurrency,
                EnableMetrics = options.EnableMetrics,
                EnableOutbox = options.EnableOutbox,
                Outbox_KeepDeduplicationDataFor = options.Outbox_KeepDeduplicationDataFor,
                LogAuditSagaChanges = options.LogAuditSagaChanges,
                InfluxOptions = new InfluxOptions
                {
                    InfluxDatabase_EnvName = options.InfluxEnvKeysOptions?.InfluxDatabase_EnvName,
                    InfluxUsername_EnvName = options.InfluxEnvKeysOptions?.InfluxUsername_EnvName,
                    InfluxPassword_EnvName = options.InfluxEnvKeysOptions?.InfluxPassword_EnvName,
                    InfluxUri_EnvName = options.InfluxEnvKeysOptions?.InfluxUri_EnvName,
                }
            };

            return config;
        }
    }
}
