namespace dropkick.Configuration.Dsl.NServiceBus
{
    public interface NServiceBusOptions
    {
        NServiceBusInstallOptions Install();
        NServiceBusUninstallOptions Uninstall();
    }
}