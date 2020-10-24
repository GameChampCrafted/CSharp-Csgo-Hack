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
    class hsBhopFOV
    {
        public static void Bunnyhop_Fov()
        {
            while (true)
            {

                int Local = ReadMemory<int>((int)g_pClient + dwLocalPlayer);
                int LocalTeam = ReadMemory<int>(Local + m_iTeamNum);
                int LocalFov = ReadMemory<int>(Local + (m_iFOVStart - 4));
                int LocalScope = ReadMemory<int>(Local + m_bIsScoped);
                for (var i = 0; i <= 64; i++)
                {
                    int EntBase = ReadMemory<int>((int)g_pClient + dwEntityList + i * 0x10);
                    if (EntBase == 0) continue;
                    int Dormant = ReadMemory<int>(EntBase + m_bDormant);
                    if (Dormant == 1) continue;
                    int Team = ReadMemory<int>(EntBase + m_iTeamNum);
                    var M8 = (Team == LocalTeam);
                    if (Globals.MiscEnable && Globals.BunnyhopEnable)
                    {

                        int aLocalPlayer = dwLocalPlayer;
                        int oFlags = m_fFlags;
                        int aJump = dwForceJump;

                        int fJump = (int)g_pClient + aJump;

                        aLocalPlayer = (int)g_pClient + aLocalPlayer;
                        int LocalPlayer = ReadMemory<int>(aLocalPlayer);

                        int aFlags = LocalPlayer + oFlags;
                        var HoldingKey = GetAsyncKeyState(Globals.BunnyKey);
                        if ((HoldingKey & 0x8000) > 0 && Globals.BunnyhopEnable || Globals.BunnyhopAlwaysOn && Globals.BunnyhopEnable)
                        {
                            if (Globals.BunnyhopAlwaysOn && Globals.BunnyhopEnable)
                            {
                                int Flags = ReadMemory<int>(aFlags);

                                if (Flags == 257)
                                {
                                    WriteMemory<int>(fJump, 5);
                                    Thread.Sleep(10);
                                    WriteMemory<int>(fJump, 4);
                                }
                            }
                            else
                            {
                                if (Globals.BunnyhopEnable == true)
                                {
                                    int Flags = ReadMemory<int>(aFlags);

                                    if (Flags == 257)
                                    {
                                        WriteMemory<int>(fJump, 5);
                                        Thread.Sleep(10);
                                        WriteMemory<int>(fJump, 4);
                                    }
                                }

                            }

                        }

                    }
                }
                if (Globals.bFov && Globals.VisualsEnable)
                {
                    if (LocalScope == 0)
                        if (LocalFov != 90) WriteMemory<int>(Local + (m_iFOVStart - 4), 90);
                }
                Thread.Sleep(5); // to avoid huge usage CPU
            }

        }
    }
}
