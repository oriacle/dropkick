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
namespace dropkick.Configuration.Dsl.Files
{
    using System;
    using CommandLine;
    using DeploymentModel;
    using FileSystem;
    using Tasks;

    public class ProtoEncryptWebConfigTask :
        BaseProtoTask
    {
        readonly Path _path;
        readonly string _where;

        public ProtoEncryptWebConfigTask(Path path, string whereIsTheConfig)
        {
            _where = ReplaceTokens(whereIsTheConfig);
            _path = path;
        }

        public override void RegisterRealTasks(PhysicalServer site)
        {
            var task = new ProtoCommandLineTask(@"aspnet_regiis");
            task.Args(@" -pe ""connectionStrings"" -app ""/MachineDPAPI"" -prov ""DataProtectionConfigurationProvider""");
            string winDir = Environment.GetEnvironmentVariable("WINDIR");
            task.ExecutableIsLocatedAt(_path.Combine(winDir, @"Microsoft.NET\Framework\v2.0.50727"));
            task.WorkingDirectory(_where);

            task.RegisterRealTasks(site);
        }
    }
}