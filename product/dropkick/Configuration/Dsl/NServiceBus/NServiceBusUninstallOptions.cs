namespace dropkick.Configuration.Dsl.NServiceBus
{
    public interface NServiceBusUninstallOptions
    {
        NServiceBusUninstallOptions Instance(string name);
        NServiceBusUninstallOptions LocatedAt(string location);
        NServiceBusUninstallOptions ServiceName(string serviceName);
    }
}