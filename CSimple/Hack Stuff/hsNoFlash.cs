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

namespace CSimple.Hack_Stuff
{
    class hsNoFlash
    {
        public static void noFlash()
        {
            while (true)
            {
                int Local = ReadMemory<int>((int)g_pClient + dwLocalPlayer);
                for (var i = 0; i <= 64; i++)
                {

                    if (Globals.bNoflash && Globals.VisualsEnable)
                    {
                        WriteMemory<int>(Local + m_flFlashMaxAlpha, 0);
                        WriteMemory<int>(Local + m_flFlashDuration, 0);
                    }
                }
            }
        }
    }
}
