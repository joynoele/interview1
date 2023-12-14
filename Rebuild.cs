[CliCommand("rebuildconfigurations", "Rebuilds all of the configurations for the tenant.")]
public async Task<string> RebuildConfigurations(
	[CliValueParameter("The guid id of the tenant (required)", "tenant id", Name = "tid")] Guid tenantId,
	ServiceBusTopicPublisher mainTopic,
	CancellationToken cancellationToken)
{
    var message = new RebuildTenantConfigurationsCommand
    {
        TenantId = tenantId,
        Timestamp = DateTime.UtcNow
    };

    await mainTopic.PublishMessageAsync(message, cancellationToken).ConfigureAwait(false);
    return "Message post complete.";
}
