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
namespace dropkick.Configuration.Dsl.NServiceBus
{
    using System;

    using dropkick.DeploymentModel;
    using dropkick.FileSystem;
    using dropkick.Tasks;
    using dropkick.Tasks.NServiceBus;

    public class ProtoNServiceBusInstallTask :
        BaseProtoTask,
        NServiceBusInstallOptions
    {
        readonly Path _path;
        private readonly string _exeName = "nservicebus.host.exe";

        string _instanceName;
        string _location;
        string _password;
        string _username;
        string _displayName;
        string _serviceName;
        string _description;
        bool _startManualy;
        
        public ProtoNServiceBusInstallTask(DotNetPath path, string location)
        {
            _path = path;
            _location = location;
        }

        public NServiceBusInstallOptions Instance(string name)
        {
            _instanceName = name;
            return this;
        }

        public NServiceBusInstallOptions LocatedAt(string location)
        {
            _location = location;
            return this;
        }

        public NServiceBusInstallOptions PassCredentials(string username, string password)
        {
            _username = username;
            _password = password;
            return this;
        }

        public NServiceBusInstallOptions DisplayName(string displayName)
        {
            _displayName = displayName;
            return this;
        }

        public NServiceBusInstallOptions ServiceName(string serviceName)
        {
            _serviceName = serviceName;
            return this;
        }

        public NServiceBusInstallOptions Description(string description)
        {
            _description = description;
            return this;
        }

        public NServiceBusInstallOptions ManualStart(bool startManually)
        {
            _startManualy = startManually;
            return this;
        }

        public override void RegisterRealTasks(PhysicalServer site)
        {
            string location;
            if (RemotePathHelper.IsUncPath(_location))
            {
                location = _path.ConvertUncShareToLocalPath(site, _location);
            }
            else
            {
                location = _location;
            }

            if (site.IsLocal)
            {
                site.AddTask(new LocalNServiceBusInstallTask(_exeName, location, _instanceName, _username, _password, _serviceName, _displayName, _description, _startManualy));
            }
            else
            {
                site.AddTask(new RemoteNServiceBusInstallTask(_exeName, location, _instanceName, site, _username, _password, _serviceName, _displayName, _description, _startManualy));
            }
        }
    }
}