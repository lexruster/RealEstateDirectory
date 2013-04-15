using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace RealEstateDirectory.Utils
{
    /// <summary>
    /// класс хранения настроек
    /// </summary>
    public static class Config
    {
	    public static string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "RealEstateDirectory\\Config.cfg");
        public static Dictionary<string, string> Dic;

        /// <summary>
        /// выгрузить настройки
        /// </summary>
        /// <returns>Строка значений</returns>
        public static string Dump()
        {
            var st = new StringBuilder();
            foreach (var t in Dic.Keys)
            {
                st.AppendLine(t + ":" + Dic[t]);
            }
            return st.ToString();
        }

        /// <summary>
        /// получить значение свойства
        /// </summary>
        /// <param name="key"></param>
        /// <returns>Значение</returns>
        public static string GetProperty(string key)
        {
            if (Dic.ContainsKey(key))
            {
                return Dic[key];
            }

            return "";
        }

        /// <summary>
        /// сохранить в файл
        /// </summary>
        public static void Save()
        {
			using (var fs = new StreamWriter(path, false, Encoding.Default))
            {
                foreach (var t in Dic.Keys)
                {
                    fs.WriteLine(t + ":" + Dic[t]);
                }
                fs.Close();
            }
        }

        /// <summary>
        /// Заргузить из файла
        /// </summary>
        public static void Load()
        {
            try
            {
				Directory.CreateDirectory(Path.GetDirectoryName(path));
				if (!File.Exists(path))
                {
					using (var fl = File.Create(path))
                    {
                        
                    }
                }

                Dic = new Dictionary<string, string>();
				using (var sr = new StreamReader(path, Encoding.Default))
                {
                    string temp;
                    while (!sr.EndOfStream)
                    {
                        temp = sr.ReadLine();
                        Dic.Add(temp.Split(new[] {':'}, 2)[0], temp.Split(new[] {':'}, 2)[1]);
                    }
                    sr.Close();
                }
            }
            catch (Exception er)
            {
                throw new Exception("Не удалось загрузить настройки.");
            }
        }

        public static void AddProperty(string key, string value)
        {
            if (Dic.ContainsKey(key))
            {
                Dic[key] = value;
            }
            else
            {
                Dic.Add(key, value);
            }
        }
    }
}