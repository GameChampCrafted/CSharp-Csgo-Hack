using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace CSimple
{
    class Config
    {
        static string vbCrLf = Environment.NewLine;
        static string dC = "" +
                "version: 0.0.1" + vbCrLf +
                "bot-enabled: false" + vbCrLf +
                "bot-trigger-enable: false" + vbCrLf +
                "bot-trigger-autofire: false" + vbCrLf +
                "bot-trigger-onkey: 0" + vbCrLf +
                "bot-trigger-delay: 0.0" + vbCrLf +
                "visuals-enabled: false" + vbCrLf +
                "visuals-glow-esp: false" + vbCrLf +
                "visuals-box: false" + vbCrLf +
                "visuals-filter-everyone: false" + vbCrLf +
                "visuals-filter-visible: false" + vbCrLf +
                "visuals-filter-visible-enemy: false" + vbCrLf +
                "visuals-filter-visible-teammates: false" + vbCrLf +
                "visuals-filter-enemy: false" + vbCrLf +
                "visuals-filter-teammates: false" + vbCrLf +
                "visuals-other-noflash: false" + vbCrLf +
                "visuals-other-radar: false" + vbCrLf +
                "colors-enabled: false" + vbCrLf +
                "colors-enemy-r: 1.0" + vbCrLf +
                "colors-enemy-g: 0.0" + vbCrLf +
                "colors-enemy-b: 0.0" + vbCrLf +
                "colors-teammates-r: 0.0" + vbCrLf +
                "colors-teammates-g: 1.0" + vbCrLf +
                "colors-teammates-b: 0.0" + vbCrLf +
                "settings-enabled: false" + vbCrLf +
                "settings-togglekeys-glowesp: 0" + vbCrLf +
                "settings-togglekeys-trigger: 0" + vbCrLf +
                "settings-togglekeys-radar: 0" + vbCrLf +
                "settings-togglekeys-noflash: 0";


        public static string defaultConfig()
        {
            return dC;
        }

        public static bool Get(string loct, bool a = true)
        {
            return Boolean.Parse(values(loct) + "");
        }

        public static int Get(string loct, int a = 0)
        {
            return Int32.Parse(values(loct) + ""); ;
        }

        public static string Get(string loct, string a = "")
        {
            return values(loct) + "";
        }

       static string values(string loct)
        {
            string[] settings = System.IO.File.ReadAllLines(Application.StartupPath + @"\config.ini");
            foreach (string line in settings)
            {
                if (Microsoft.VisualBasic.Strings.Split(line, ": ")[0].Equals(loct))
                    return Microsoft.VisualBasic.Strings.Split(line, ": ")[1];
            }
            return "";
        }

        public static void Set(string loct, string value)
        {
            string[] settings = System.IO.File.ReadAllLines(Application.StartupPath + @"\config.ini");
            var settingsa = System.IO.File.ReadAllText(Application.StartupPath + @"\config.ini");
            try
            {
                var i = 0;
                if (settingsa.Contains(loct + ": "))
                {
                    values(loct);
                    foreach (string line in settings)
                    {
                        if (Microsoft.VisualBasic.Strings.Split(line, ": ")[0].Equals(loct))
                        {
                            if (value != "")
                                settings[i] = loct + ": " + value;
                            else
                            {
                                var a = settings.ToList();
                                a.RemoveAt(i);
                                settings = a.ToArray();
                            }
                        }
                        i += 1;
                    }
                }
                else if (settings.Count() == 0)
                    settings = new string[] { loct + ": " + value };
                else
                {
                    string[] a = settings;
                    List<string> b = a.ToList();
                    b.Add(loct + ": " + value);
                    settings = b.ToArray();
                }
                System.IO.File.WriteAllLines(Application.StartupPath + @"\config.ini", settings);
            }
            catch (ArgumentNullException ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
    }
}
