using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEO_DROID.Resources.Lib.Extra
{
    public class FileIni
    {
        protected Dictionary<string, string> table;
        string formatdatetime = "dd/MM/yyyy hh:mm:ss";

        public FileIni(string filename)
        {
            table = new Dictionary<string, string>();

            if (!File.Exists(filename)) return;

            string[] lines = WriteSafeReadAllLines(filename);
            string section = string.Empty;
            foreach (string l in lines)
            {
                string line = l.Trim();
                if (line.StartsWith("#")) continue;

                // parse Section
                if (line.StartsWith("["))
                {
                    int pos1 = line.IndexOf("[");
                    int pos2 = line.IndexOf("]");

                    if (pos1 != -1 && pos2 != -1)
                        section = line.Substring(pos1 + 1, pos2 - (pos1 + 1)).Trim();
                    else section = string.Empty;
                }

                // parse Key = Value
                int position = line.IndexOf('=');

                if (position > 0)
                {
                    string key = line.Substring(0, position).Trim();
                    string value = line.Substring(position + 1).Trim();

                    // quitamos comentario del value
                    if (value.Contains("#"))
                    {
                        int pos = value.IndexOf('#');
                        value = value.Substring(0, pos).Trim();
                    }

                    // Si hay Section creamos la key = Section # key, value
                    if (!string.IsNullOrEmpty(section))
                        key = section + "#" + key;

                    // key siempre toUpper
                    table.Add(key.ToUpper(), value);
                }
            }

            // ---
        }

        private string[] WriteSafeReadAllLines(String path)
        {
            using (var csv = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (var sr = new StreamReader(csv))
            {
                List<string> file = new List<string>();
                while (!sr.EndOfStream)
                {
                    file.Add(sr.ReadLine());
                }

                return file.ToArray();
            }
        }

        /*
        public List<string> getSections()
        {
            List <string> sections = new List<string>();

            foreach(string key in table.Keys)
            {
                if(key.Contains("#"))
                {
                    string k = key.Substring(0, key.IndexOf('#'));
                    if(!sections.Contains(k))
                        sections.Add(k);
                }
            }

            return sections;
        }
        */





        #region Set Active Section 

        string activeSection = string.Empty;

        public void setSection(string value)
        {
            if (string.IsNullOrEmpty(value))
                activeSection = "";
            else activeSection = value.ToUpper().Trim() + "#";
        }

        #endregion

        #region Get String

        private string getValueFromHash(string section, string key)
        {
            // Añadimos la Section activa
            key = section + key.ToUpper();

            if (table.ContainsKey(key))
            {
                string value = table[key] as string;

                // Hacemos el replace de los defines
                int lastIndex = 0;
                while (value.Contains("$"))
                {
                    int p1 = value.IndexOf('$', lastIndex);
                    int p2 = value.IndexOf('$', p1 + 1);
                    if (p1 == -1 || p2 == -1) break;

                    string keyword = value.Substring(p1 + 1, p2 - p1 - 1);
                    string valueword = get(keyword);
                    if (!string.IsNullOrEmpty(valueword))
                    {
                        value = value.Replace("$" + keyword + "$", valueword);
                    }

                    lastIndex = p2 + 1;
                }

                return value;
            }
            return null;
        }



        public string get(string key, string defecto = null)
        {
            // 1 - sacamos la key de la seccion seleccionada
            string value = getValueFromHash(activeSection, key);

            if (string.IsNullOrEmpty(value))
                return defecto;
            else return value;
        }

        public string get(string key, string keySecond, string defecto = null)
        {
            // Este metodo esta hecho para cargar FLAGS de un INI y si n oexiste consultar otro
            //
            // if(Exist(key))
            //      return value[key]
            // else if(Exist(keySecond))
            //      return value[keySecond]
            // else
            //      return defecto

            return get(key, get(keySecond, defecto));

        }


        public bool exist(string key)
        {
            string value = getValueFromHash(activeSection, key);
            return (value != null);
        }

        #endregion

        #region Get INT

        public int getInt(string key, int defecto = 0)
        {
            string value = get(key, defecto.ToString());
            return parseInt(value, defecto);
        }

        private int parseInt(string cadena, int defecto)
        {
            // Parsea un string a int
            //
            // permite valores de tiempo, pasados a milisegundos
            //   1s = 1000
            //   1m = 1 * 60 * 1000
            //   1h = 1 * 60 * 60 * 1000
            //
            // permite valores booleanos
            //   true | on | si | enable -> 1
            //   false | off | no | disable-> 1
            //

            try
            {
                int multiplicador = 1;

                if (cadena.Equals("true", StringComparison.InvariantCultureIgnoreCase)) return 1;
                if (cadena.Equals("on", StringComparison.InvariantCultureIgnoreCase)) return 1;
                if (cadena.Equals("si", StringComparison.InvariantCultureIgnoreCase)) return 1;
                if (cadena.Equals("enable", StringComparison.InvariantCultureIgnoreCase)) return 1;
                if (cadena.Equals("enabled", StringComparison.InvariantCultureIgnoreCase)) return 1;

                if (cadena.Equals("false", StringComparison.InvariantCultureIgnoreCase)) return 0;
                if (cadena.Equals("off", StringComparison.InvariantCultureIgnoreCase)) return 0;
                if (cadena.Equals("no", StringComparison.InvariantCultureIgnoreCase)) return 0;
                if (cadena.Equals("disable", StringComparison.InvariantCultureIgnoreCase)) return 0;
                if (cadena.Equals("disabled", StringComparison.InvariantCultureIgnoreCase)) return 0;

                // DAY / WEEK / MONTH
                if (cadena.Equals("day", StringComparison.InvariantCultureIgnoreCase)) return 1;
                if (cadena.Equals("week", StringComparison.InvariantCultureIgnoreCase)) return 7;
                if (cadena.Equals("month", StringComparison.InvariantCultureIgnoreCase)) return 30;


                if (cadena.EndsWith("s")) { cadena = cadena.Substring(0, cadena.Length - 1); multiplicador = 1000; }
                if (cadena.EndsWith("m")) { cadena = cadena.Substring(0, cadena.Length - 1); multiplicador = 60 * 1000; }
                if (cadena.EndsWith("h")) { cadena = cadena.Substring(0, cadena.Length - 1); multiplicador = 60 * 60 * 1000; }

                return int.Parse(cadena) * multiplicador;
            }
            catch (Exception) { }

            return defecto;
        }

        public int[] getIntArray(string key)
        {
            string value = get(key, "0");
            string[] values = value.Split(',');

            List<int> listaValues = new List<int>();

            foreach (string s in values)
            {
                listaValues.Add(parseInt(s, 0));
            }

            return listaValues.ToArray();
        }


        #endregion

        #region Get Bool

        public bool getBool(string key, bool defecto = false)
        {
            string value = get(key, defecto.ToString());
            try
            {
                return bool.Parse(value);
            }
            catch { }
            return defecto;
        }

        public bool getBool(string key, string keySecond, bool defecto = false)
        {
            // Este metodo esta hecho para cargar FLAGS de un INI y si n oexiste consultar otro
            //
            // if(Exist(key))
            //      return value[key]
            // else if(Exist(keySecond))
            //      return value[keySecond]
            // else
            //      return defecto

            return getBool(key, getBool(keySecond, defecto));

        }


        #endregion










    }
}
