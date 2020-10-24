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
    class hsHotKeys
    {
        public static void hotKeys()
        {
            while (true)
            {
                var HoldingKey = GetAsyncKeyState(Globals.iGlowEspKey);
                if ((HoldingKey & 0x8000) > 0 && Globals.bGlow)
                {
                    Thread.Sleep(100);
                    Globals.bGlow = false;
                }
                else if ((HoldingKey & 0x8000) > 0 && !Globals.bGlow)
                {
                    Thread.Sleep(100);
                    Globals.bGlow = true;
                }


                HoldingKey = GetAsyncKeyState(Globals.iTriggerToggleKey);
                if ((HoldingKey & 0x8000) > 0 && Globals.bTrigger)
                {
                    Thread.Sleep(100);
                    Globals.bTrigger = false;
                }
                else if ((HoldingKey & 0x8000) > 0 && !Globals.bTrigger)
                {
                    Thread.Sleep(100);
                    Globals.bTrigger = true;
                }
                HoldingKey = GetAsyncKeyState(Globals.iRadarKey);
                if ((HoldingKey & 0x8000) > 0 && Globals.bRadar)
                {
                    Thread.Sleep(100);
                    Globals.bRadar = false;
                }
                else
                {
                    Thread.Sleep(100);
                    Globals.bRadar = true;
                }
                HoldingKey = GetAsyncKeyState(Globals.iNoFlashKey);
                if ((HoldingKey & 0x8000) > 0 && Globals.bNoflash)
                {
                    Thread.Sleep(100);
                    Globals.bNoflash = false;
                }
                else if ((HoldingKey & 0x8000) > 0 && !Globals.bNoflash)
                {
                    Thread.Sleep(100);
                    Globals.bNoflash = true;
                }
            }

        }
    }
}
