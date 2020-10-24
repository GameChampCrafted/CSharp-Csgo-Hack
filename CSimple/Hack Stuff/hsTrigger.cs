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
    class hsTrigger
    {
        public static void Trigger_Loop()
        {
            while (true)
            {
                {
                    int Local = ReadMemory<int>((int)g_pClient + dwLocalPlayer);
                    int LocalTeam = ReadMemory<int>(Local + m_iTeamNum);
                    for (var i = 0; i <= 64; i++)
                    {
                        int EntBase = ReadMemory<int>((int)g_pClient + dwEntityList + i * 0x10);
                        if (EntBase == 0) continue;
                        int Dormant = ReadMemory<int>(EntBase + m_bDormant);
                        if (Dormant == 1) continue;
                        int Team = ReadMemory<int>(EntBase + m_iTeamNum);
                        var M8 = (Team == LocalTeam);
                        if (Globals.BotEnable == true && Globals.bTrigger == true)
                        {
                            var iCrosshairIndex = ReadMemory<int>(EntBase + m_iCrosshairId);
                            var iCrossBase = ReadMemory<int>((int)g_pClient + dwEntityList + (iCrosshairIndex - 1) * 0x10);
                            var iCrossTeam = ReadMemory<int>(iCrossBase + m_iTeamNum);
                            var HoldingKey = GetAsyncKeyState(Globals.iTriggerKey);
                            var M8onCross = (iCrossTeam == LocalTeam);
                            //65
                            if (((HoldingKey & 0x8000) > 0) && (iCrosshairIndex > 0 && iCrosshairIndex < 65) || Globals.autoFire == true && (iCrosshairIndex > 0 && iCrosshairIndex < 65))
                            {
                                if (M8onCross) continue;

                                Thread.Sleep(Globals.Delay); // delay before shoot in ms
                                WriteMemory<int>((int)g_pClient + dwForceAttack, 5);
                                Thread.Sleep(Globals.Delay); // delay between shoots in ms
                                WriteMemory<int>((int)g_pClient + dwForceAttack, 4);
                                Thread.Sleep(Globals.Delay); // delay after shoot in ms
                            }
                        }

                    }
                    //#endregion

                    Thread.Sleep(1); // to avoid huge usage CPU
                }
            }
        }

        private static void WriteMemory<T>(object p, T v)
        {
            throw new NotImplementedException();
        }
    }
}
