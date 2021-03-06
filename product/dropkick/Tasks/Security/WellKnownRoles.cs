﻿// Copyright 2007-2010 The Apache Software Foundation.
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
namespace dropkick.Tasks.Security
{
    using System.Collections.Generic;
    using System.Security.AccessControl;
    using System.Security.Principal;

    public class WellKnownRoles
    {
        public static string Administrators = "Administrators";
        public static string Everyone = "Everyone";
        public static string Anonymous = "ANONYMOUS LOGON";

        public static string CurrentUser
        {
            get { return WindowsIdentity.GetCurrent().Name; }
        }

        static HashSet<string> _defaults = new HashSet<string>()
                                        {
                                            @"FHLB10\Prod Control",
                                            @"FHLB10\Prod Read",
                                            @"NT AUTHORITY\SYSTEM",
                                            @"FHLB10\Directory Admins"
                                        };

        public static bool IsDefault(string name)
        {
            return _defaults.Contains(name);
        }

        public static bool NotADefaultRule(FileSystemAccessRule rule)
        {
            return !_defaults.Contains(rule.IdentityReference.Value);
        }
        public static bool NotInherited(FileSystemAccessRule rule)
        {
            return !rule.IsInherited;
        }

        public static void AddDefaultGroupToIgnore(string name)
        {
            _defaults.Add(name);
        }
        public static void RemoveDefaultGroupToIgnore(string name)
        {
            _defaults.Remove(name);
        }
    }
}