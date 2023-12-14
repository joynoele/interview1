[CliCommand("rebuildconfigurations", "Rebuilds all of the configurations for the tenant. If a configuration Id is given then only that configuration will be rebuilt.")]
public async Task<String> RebuildConfigurations(
	[CliValueParameter("The guid id of the tenant (required)", "tenant id", Name = "tid")] Guid tenantId,
			[CliValueParameter("An id for the configuration (optional)", "configuration id")] Guid? id, 
	ServiceBusTopicPublisher mainTopic,
    CancellationToken cancellationToken)
{
    if (id != new Guid())
    {
        var message = new RebuildConfigurationCommand
    {
        ConfigurationId = id,
	TenantId = tenantId,
        Timestamp = DateTimeOffset.UtcNow,
	CorrelationId = new Guid(),
    };
        await mainTopic.PublishMessageAsync(message, cancellationToken).ConfigureAwait(false);
    }
    else
    {
        var message = new RebuildTenantConfigurationsCommand
        {
	    TenantId = id,
            Timestamp = DateTime.UtcNow,
	    CorrelationId = new Guid()
        };
        await mainTopic.PublishMessageAsync(message, cancellationToken).ConfigureAwait(false);
    }
    
    return "Masssage post complete.";
}
