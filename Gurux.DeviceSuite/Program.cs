//
// --------------------------------------------------------------------------
//  Gurux Ltd
// 
//
//
// Filename:        $HeadURL$
//
// Version:         $Revision$,
//                  $Date$
//                  $Author$
//
// Copyright (c) Gurux Ltd
//
//---------------------------------------------------------------------------
//
//  DESCRIPTION
//
// This file is a part of Gurux Device Framework.
//
// Gurux Device Framework is Open Source software; you can redistribute it
// and/or modify it under the terms of the GNU General Public License 
// as published by the Free Software Foundation; version 2 of the License.
// Gurux Device Framework is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of 
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. 
// See the GNU General Public License for more details.
//
// This code is licensed under the GNU General Public License v2. 
// Full text may be retrieved at http://www.gnu.org/licenses/gpl-2.0.txt
//---------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Reflection;
using System.IO;
using System.Collections;
using Gurux.Device.Editor;
using Gurux.Device;
using Gurux.Common;

namespace Gurux.DeviceSuite
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(CurrentDomain_AssemblyResolve);
            AppDomain.CurrentDomain.TypeResolve += new ResolveEventHandler(CurrentDomain_TypeResolve);
            try
            {
                Gurux.Common.GXCommon.CheckFramework();
                CopyDebugFiles();                
                MainForm form = new MainForm();
                Application.Run(form);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.ToString());
            }
        }

        static Hashtable GetCommandLineParams()
        {
            Hashtable items = new Hashtable();
            string data = null;
            string[] Args = Environment.GetCommandLineArgs();
            data = string.Join(" ", Args);
            string[] sep = { " /", "-" };
            Args = data.Split(sep, StringSplitOptions.RemoveEmptyEntries);
            foreach (string str in Args)
            {
                if (str.Length == 0)
                {
                    continue;
                }
                int pos = str.IndexOf(' ');
                string key = str;
                data = "";
                if (pos != -1)
                {
                    key = str.Substring(0, pos).Trim().ToLower();
                    data = str.Substring(pos).Trim().ToLower();
                }
                items.Add(key, data);
            }
            return items;
        }

        /// <summary>
        /// Copy debug files to AddIn directory.
        /// </summary>
        /// <returns></returns>
        private static bool CopyDebugFiles()
        {
            Hashtable Args = GetCommandLineParams();
            List<Assembly> asms = new List<Assembly>();
            foreach (DictionaryEntry it in Args)
            {
                if (string.Compare(Convert.ToString(it.Key), "debug", true) == 0)
                {
                    List<string> files = new List<string>();
                    DirectoryInfo di = new DirectoryInfo(Directory.GetCurrentDirectory());
                    foreach (FileInfo file in di.GetFiles("*.dll"))
                    {
                        try
                        {
                            bool found = false;
                            string name = Path.GetFileNameWithoutExtension(file.Name);
                            foreach (AssemblyName asmName in Assembly.GetEntryAssembly().GetReferencedAssemblies())
                            {
                                if (Path.GetFileName(asmName.Name) == name)
                                {
                                    found = true;
                                    break;
                                }
                            }
                            if (!found)
                            {
                                Assembly asm = Assembly.LoadFile(file.FullName);
                                asms.Add(asm);
                            }
                        }
                        catch (Exception Ex)
                        {
                            System.Diagnostics.Debug.WriteLine(Ex.Message);
                            continue;
                        }
                    }
                    foreach (Assembly asm in asms)
                    {
                        foreach (Type type in asm.GetTypes())
                        {
                            if (type.IsSubclassOf(typeof(GXProtocolAddIn)))
                            {
                                GXProtocolAddIn protocol = Activator.CreateInstance(type) as GXProtocolAddIn;
                                if (!GXDeviceList.Protocols.ContainsKey(protocol.Name))
                                {
                                    GXDeviceList.Protocols.Add(protocol.Name, protocol);
                                }
                                break;
                            }
                        }
                    }
                    return true;
                }
            }
            return false;
        }

        static System.Reflection.Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            string[] tmp = args.Name.Split(',');
            if (tmp.Length == 4)
            {
                foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
                {
                    if (string.Compare(assembly.GetName().Name, tmp[0], true) == 0)
                    {
                        return assembly;
                    }
                }
            }
            foreach (Assembly it in AppDomain.CurrentDomain.GetAssemblies())
            {
                if (it.GetName().Name.Equals(args.Name))
                {
                    return it;
                }
            }
            string path = "";
            if (Environment.OSVersion.Platform == PlatformID.Unix)
            {
                path = Path.Combine("/usr", "lib");
                path = Path.Combine(path, tmp[0].ToLower().Replace(".", "-"));
                if (Directory.Exists(path))
                {
                    DirectoryInfo di = new DirectoryInfo(path);
                    foreach (FileInfo fi in di.GetFiles(tmp[0] + ".dll"))
                    {
                        System.Diagnostics.Trace.WriteLine("CurrentDomain_AssemblyResolve: Returning assembly from(3):" + fi.FullName);
                        Assembly assembly = Assembly.LoadFile(fi.FullName);
                        return assembly;
                    }
                }
            }
            /*
             * path = GXCommon.ProtocolAddInsPath;
            if (Directory.Exists(path))
            {
                foreach (string it in Directory.GetFiles(path, "*.dll"))
                {
                    Assembly asm = Assembly.LoadFile(it);
                    if (asm.GetName().ToString() == args.Name)
                    {
                        return asm;
                    }
                }
            }
            path = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.CommonProgramFiles), "Gurux");
            path = Path.Combine(path, "GXCom");
            if (Directory.Exists(path))
            {
                foreach (string it in Directory.GetFiles(path, "*.dll"))
                {
                    Assembly asm = Assembly.LoadFile(it);
                    if (asm.GetName().ToString() == args.Name)
                    {
                        return asm;
                    }
                }
            }

            path = Path.Combine(System.IO.Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath), "AddIns");
            if (Directory.Exists(path))
            {
                foreach (string it in Directory.GetFiles(path, "*.dll"))
                {
                    Assembly asm = Assembly.LoadFile(it);
                    if (asm.GetName().ToString() == args.Name)
                    {
                        return asm;
                    }
                }
            }
            path = Path.Combine(System.IO.Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath), "Medias");
            if (Directory.Exists(path))
            {
                foreach (string it in Directory.GetFiles(path, "*.dll"))
                {
                    Assembly asm = Assembly.LoadFile(it);
                    if (asm.GetName().ToString() == args.Name)
                    {
                        return asm;
                    }
                }
            }
             * */
            return null;
        }


        static Assembly CurrentDomain_TypeResolve(object sender, ResolveEventArgs args)
        {
            try
            {
                int pos = args.Name.LastIndexOf('.');
                if (pos != -1)
                {
                    string ns = args.Name.Substring(0, pos);
                    string className = args.Name.Substring(pos + 1);
                    foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
                    {
                        if (assembly.GetName().Name == ns)
                        {
                            if (assembly.GetType(args.Name, false, true) != null)
                            {
                                return assembly;
                            }
                        }
                    }
                }
                foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
                {
                    foreach (Type type in assembly.GetTypes())
                    {
                        if (type.Name == args.Name ||
                            type.FullName == args.Name)
                        {
                            return assembly;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            System.Diagnostics.Debug.WriteLine(args.Name);
            return null;

        }
    }
}
