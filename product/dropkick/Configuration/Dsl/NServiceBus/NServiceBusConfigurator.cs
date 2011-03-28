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
    using dropkick.DeploymentModel;
    using dropkick.FileSystem;
    using dropkick.Tasks;
    using dropkick.Tasks.NServiceBus;

    public class NServiceBusConfigurator :
        BaseProtoTask,
        NServiceBusOptions
    {
        readonly Path _path;
        string _instanceName;
        string _location;
        string _exeName;
        string _password;
        string _username;
        string _displayName;
        string _serviceName;
        string _description;
        bool _startManualy;
        
        public NServiceBusConfigurator(Path path)
        {
            _path = path;
        }

        public void ExeName(string name)
        {
            _exeName = name;
        }

        public void Instance(string name)
        {
            _instanceName = name;
        }

        public void LocatedAt(string location)
        {
            _location = location;
        }

        public void PassCredentials(string username, string password)
        {
            _username = username;
            _password = password;
        }

        public void DisplayName(string displayName)
        {
            _displayName = displayName;
        }

        public void ServiceName(string serviceName)
        {
            _serviceName = serviceName;
        }

        public void Description(string description)
        {
            _description = description;
        }

        public void ManualStart(bool startManually)
        {
            _startManualy = startManually;
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
                site.AddTask(new LocalNServiceBusTask(_exeName, location, _instanceName, _username, _password, _serviceName, _displayName, _description, _startManualy));
            }
            else
            {
                site.AddTask(new RemoteNServiceBusTask(_exeName, location, _instanceName, site, _username, _password, _serviceName, _displayName, _description, _startManualy));
            }
        }
    }
}