namespace dropkick.Configuration.Dsl.NServiceBus
{
    using System;

    using dropkick.DeploymentModel;
    using dropkick.FileSystem;
    using dropkick.Tasks;

    public class ProtoNServiceBusTask : NServiceBusOptions
    {
        private readonly ProtoServer protoServer;

        private readonly string location;

        public ProtoNServiceBusTask(ProtoServer protoServer, string location)
        {
            this.protoServer = protoServer;
            this.location = location;
        }

        public NServiceBusInstallOptions Install()
        {
            var protoInstallTask = new ProtoNServiceBusInstallTask(new DotNetPath(), this.location);
            protoServer.RegisterProtoTask(protoInstallTask);
            return protoInstallTask;
        }

        public NServiceBusUninstallOptions Uninstall()
        {
            var protoUninstallTask = new ProtoNServiceBusUninstallTask(new DotNetPath(), this.location);
            protoServer.RegisterProtoTask(protoUninstallTask);
            return protoUninstallTask;
        }
    }
}