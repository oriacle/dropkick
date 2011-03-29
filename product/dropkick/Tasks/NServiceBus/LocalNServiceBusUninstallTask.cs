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
    using FileSystem;
    using Prompting;

    public class LocalNServiceBusUninstallTask :
        BaseTask
    {
        readonly LocalCommandLineTask _task;

        public LocalNServiceBusUninstallTask(string exeName, string location, string instanceName, string serviceName)
        {
            StringBuilder args = new StringBuilder("/uninstall ");
       
            if (!string.IsNullOrEmpty(instanceName)) args.AppendFormat(" /instance:{0}", instanceName);
            if (!string.IsNullOrEmpty(serviceName)) args.AppendFormat(" /serviceName:{0}", serviceName);
            
            _task = new LocalCommandLineTask(new DotNetPath(), exeName)
                        {
                            Args = args.ToString(), 
                            ExecutableIsLocatedAt = location,
                            WorkingDirectory = location
                        };
        }

        public override string Name
        {
            get { return "[NServiceBus] local Uninstalling"; }
        }

        public override DeploymentResult VerifyCanRun()
        {
            return _task.VerifyCanRun();
        }

        public override DeploymentResult Execute()
        {
            Logging.Coarse("[NServiceBus] Uninstalling a local NServiceBus service.");
            return _task.Execute();
        }
    }
}