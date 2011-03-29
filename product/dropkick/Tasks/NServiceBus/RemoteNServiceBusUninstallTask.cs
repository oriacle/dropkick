// Copyright 2007-2010 The Apache Software Foundation.
// 
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use 
// this file except in compliance with the License. You may obtain a copy of the 
// License at 
// 
//     http://www.apache.org/licenses/LICENSE-2.0 
// 
// Unless required by applicable law or agreed to in writing, software distributed 
// under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR 
// CONDITIONS OF ANY KIND, either express or implied. See the License for the 
// specific language governing permissions and limitations under the License.
namespace dropkick.Tasks.NServiceBus
{
    using System.Text;

    using CommandLine;
    using DeploymentModel;
    using Prompting;

    public class RemoteNServiceBusUninstallTask :
        BaseTask
    {
        readonly RemoteCommandLineTask _task;

        public RemoteNServiceBusUninstallTask(string exeName, string location, string instanceName, PhysicalServer site, string serviceName)
        {
            StringBuilder args = new StringBuilder("/uninstall ");


            if (!string.IsNullOrEmpty(instanceName)) args.AppendFormat(" /instance:{0}", instanceName);
            if (!string.IsNullOrEmpty(serviceName)) args.AppendFormat(" /serviceName:{0}", serviceName);

            _task = new RemoteCommandLineTask(exeName)
            {
                Args = args.ToString(),
                ExecutableIsLocatedAt = location,
                Machine = site.Name,
                WorkingDirectory = location
            };
        }

        public override string Name
        {
            get { return "[NServiceBus] remote Uninstalling"; }
        }

        public override DeploymentResult VerifyCanRun()
        {
            return _task.VerifyCanRun();
        }

        public override DeploymentResult Execute()
        {
            Logging.Coarse("[NServiceBus] Uninstalling a remote NServiceBus service");
            return _task.Execute();
        }
    }
}