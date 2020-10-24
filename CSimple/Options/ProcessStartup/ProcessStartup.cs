using System;
using System.Drawing;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;
using static CSimple.SDK.Memory;
using System.Threading;
using static CSimple.SDK.Offsets;
using CSimple.SDK;
using static CSimple.SDK.Structs.GlowObject;
using CSimple.SDK.Structs;
using System.Globalization;

namespace CSimple.Options.Startup
{
    public class Startup
    {
        public String process = "csgo"; //Process
        public IntPtr Client; //Base Client Address
        public IntPtr Engine; //Base Engine Address

        public void LOAD()
        {
            try
            {
                var CSGO = Process.GetProcessesByName(process);
                if (CSGO.Length != 0)
                {
                    g_pProcess = CSGO[0];
                    g_pProcessHandle = OpenProcess(0x0008 | 0x0010 | 0x0020, false, g_pProcess.Id);
                    foreach (ProcessModule Module in g_pProcess.Modules)
                    {
                        if ((Module.ModuleName == "client_panorama.dll"))
                        {
                            g_pClient = Module.BaseAddress;
                            Client = g_pClient;
                        }
                        if ((Module.ModuleName == "engine.dll"))
                        {
                            g_pEngine = Module.BaseAddress;
                            Engine = g_pEngine;
                        }
                    }
                }
            }
            catch
            {
                Thread.Sleep(3000);
                var CSGO = Process.GetProcessesByName(process);
                if (CSGO.Length != 0)
                {
                    g_pProcess = CSGO[0];
                    g_pProcessHandle = OpenProcess(0x0008 | 0x0010 | 0x0020, false, g_pProcess.Id);
                    foreach (ProcessModule Module in g_pProcess.Modules)
                    {
                        if ((Module.ModuleName == "client_panorama.dll"))
                        {
                            g_pClient = Module.BaseAddress;
                            Client = g_pClient;
                        }
                        if ((Module.ModuleName == "engine.dll"))
                        {
                            g_pEngine = Module.BaseAddress;
                            Engine = g_pEngine;
                        }
                    }
                }
            }

        }
    }
}

