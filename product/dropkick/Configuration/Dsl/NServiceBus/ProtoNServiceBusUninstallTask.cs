namespace dropkick.Configuration.Dsl.NServiceBus
{
    using dropkick.DeploymentModel;
    using dropkick.FileSystem;
    using dropkick.Tasks;
    using dropkick.Tasks.NServiceBus;

    public class ProtoNServiceBusUninstallTask : BaseProtoTask, NServiceBusUninstallOptions
    {
        private readonly string exeName = "nservicebus.host.exe";
        private readonly Path path;
        private string serviceName;
        private string instance;
        private string location;

        public ProtoNServiceBusUninstallTask(Path path, string location)
        {
            this.path = path;
            this.location = location;
        }

        public override void RegisterRealTasks(PhysicalServer server)
        {
            string location;
            if (RemotePathHelper.IsUncPath(this.location))
            {
                location = path.ConvertUncShareToLocalPath(server, this.location);
            }
            else
            {
                location = this.location;
            }

            if (server.IsLocal)
            {
                server.AddTask(new LocalNServiceBusUninstallTask(exeName, location, instance, serviceName));
            }
            else
            {
                server.AddTask(new RemoteNServiceBusUninstallTask(exeName, location, instance, server, serviceName));
            }
        }
        
        public NServiceBusUninstallOptions Instance(string name)
        {
            this.instance = name;
            return this;
        }

        public NServiceBusUninstallOptions LocatedAt(string location)
        {
            this.location = location;
            return this;
        }

        public NServiceBusUninstallOptions ServiceName(string serviceName)
        {
            this.serviceName = serviceName;
            return this;
        }
    }
}