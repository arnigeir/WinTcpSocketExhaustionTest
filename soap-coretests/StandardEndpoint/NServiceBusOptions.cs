namespace SNBEndpoint
{
    public class InfluxEnvKeysOptions
    {
        public string InfluxDatabase_EnvName { get; set; }
        public string InfluxPassword_EnvName { get; set; }
        public string InfluxUsername_EnvName { get; set; }
        public string InfluxUri_EnvName { get; set; }
    }

    public class NServiceBusOptions
    {
        public string EndpointName { get; set; }
        public string BusDBConnectionString { get; set; }
        public string TransportConnectionString { get; set; }
        public string ServiceControlMetricsAddress { get; set; }
        public string ServiceControlQueue { get; set; }
        public string ServiceControlMachineName { get; set; }
        public int? MessageProcessingConcurrency { get; set; }
        public int? ImmediateDelivery_NumberOfRetries { get; set; }
        public int? DelayedDelivery_NumberOfRetries { get; set; }
        public int? DelayedDelivery_TimeIncrease { get; set; }
        public bool? EnableInfluxMetrics { get; set; }
        public bool? LogAuditSagaChanges { get; set; }
        public bool? EnableMetrics { get; set; }
        public bool? EnableOutbox { get; set; }
        public int? Outbox_KeepDeduplicationDataFor { get; set; }
        public string ErrorQueueName { get; set; }
        public string AuditQueueName { get; set; }
        public InfluxEnvKeysOptions InfluxEnvKeysOptions { get; set; }
    }
}
