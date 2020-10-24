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
using static CSimple.Hack_Stuff.hsValues;

namespace CSimple.Hack_Stuff
{
    class hsGlow
    {
        public static float terrorR = 0f;
        public static float terrorG = 1f;
        public static float terrorB = 0f;
        public static float CterrorR = 1f;
        public static float CterrorG = 0f;
        public static float CterrorB = 0f;
        public static bool IsVisible(int local, int entity)
        {
            int mask = ReadMemory<int>(entity + m_bSpottedByMask);
            int PBASE = ReadMemory<int>(local + 0x64) - 1;
            return (mask & (1 << PBASE)) > 0;
        }

        public static void glow()
        {
            while (true)
            {

                int GlowBase = ReadMemory<int>((int)g_pClient + dwGlowObjectManager);
                int Local = ReadMemory<int>((int)g_pClient + dwLocalPlayer);
                int LocalTeam = ReadMemory<int>(Local + m_iTeamNum);
                for (var i = 0; i <= 64; i++)
                {
                    int EntBase = ReadMemory<int>((int)g_pClient + dwEntityList + i * 0x10);
                    if (EntBase == 0) continue;
                    int Dormant = ReadMemory<int> (EntBase + m_bDormant);
                    if (Dormant == 1) continue;
                    int Team = ReadMemory<int>(EntBase + m_iTeamNum);
                    int GlowIndex = ReadMemory<int>(EntBase + m_iGlowIndex);
                    int Spotted = ReadMemory<int>(EntBase + m_bSpotted);
                    bool Visible = IsVisible(Local, EntBase);
                    var M8 = (Team == LocalTeam);
                    GlowObject GlowObj = new GlowObject();
                    GlowObj = ReadMemory<GlowObject>(GlowBase + GlowIndex * 0x38);
                    if (Globals.VisualsEnable && Globals.bGlow)
                    {
                        if (M8 && !chkVis)
                        {
                            GlowObj.r = terrorR;
                            GlowObj.g = terrorG;
                            GlowObj.b = terrorB;
                            GlowObj.a = 0.7f;
                            GlowObj.m_bRenderWhenOccluded = true;
                            GlowObj.m_bRenderWhenUnoccluded = false;
                            GlowObj.m_bFullBloom = false;
                            WriteMemory<GlowObject>(GlowBase + GlowIndex * 0x38, GlowObj);
                        }
                        if (!M8 && !chkVis)
                        {
                            GlowObj.r = CterrorR;
                            GlowObj.g = CterrorG;
                            GlowObj.b = CterrorB;
                            GlowObj.a = 0.7f;
                            GlowObj.m_bRenderWhenOccluded = true;
                            GlowObj.m_bRenderWhenUnoccluded = false;
                            GlowObj.m_bFullBloom = false;
                            WriteMemory<GlowObject>(GlowBase + GlowIndex * 0x38, GlowObj);
                        }
                        if (M8 && chkVis && chkTeamVis && Visible)
                        {

                            GlowObj.r = terrorR;
                            GlowObj.g = terrorG;
                            GlowObj.b = terrorB;
                            GlowObj.a = 0.7f;
                            GlowObj.m_bRenderWhenOccluded = true;
                            GlowObj.m_bRenderWhenUnoccluded = false;
                            GlowObj.m_bFullBloom = false;
                            WriteMemory<GlowObject>(GlowBase + GlowIndex * 0x38, GlowObj);
                        }
                        if (!M8 && chkVis && chkEnemyVis && Visible)
                        {
                            GlowObj.r = CterrorR;
                            GlowObj.g = CterrorG;
                            GlowObj.b = CterrorB;
                            GlowObj.a = 0.7f;
                            GlowObj.m_bRenderWhenOccluded = true;
                            GlowObj.m_bRenderWhenUnoccluded = false;
                            GlowObj.m_bFullBloom = false;
                            WriteMemory<GlowObject>(GlowBase + GlowIndex * 0x38, GlowObj);
                        }
                        if (M8 && chkVis && !chkTeamVis)
                        {
                            GlowObj.r = terrorR;
                            GlowObj.g = terrorG;
                            GlowObj.b = terrorB;
                            GlowObj.a = 0.7f;
                            GlowObj.m_bRenderWhenOccluded = true;
                            GlowObj.m_bRenderWhenUnoccluded = false;
                            GlowObj.m_bFullBloom = false;
                            WriteMemory<GlowObject>(GlowBase + GlowIndex * 0x38, GlowObj);
                        }
                        if (!M8 && chkVis && !chkEnemyVis)
                        {
                            GlowObj.r = CterrorR;
                            GlowObj.g = CterrorG;
                            GlowObj.b = CterrorB;
                            GlowObj.a = 0.7f;
                            GlowObj.m_bRenderWhenOccluded = true;
                            GlowObj.m_bRenderWhenUnoccluded = false;
                            GlowObj.m_bFullBloom = false;
                            WriteMemory<GlowObject>(GlowBase + GlowIndex * 0x38, GlowObj);
                        }
                    }
                }
            }
        }
    }
}
