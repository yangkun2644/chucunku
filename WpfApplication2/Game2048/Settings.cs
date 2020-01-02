using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.IO;

namespace UI.Game2048
{
    public class Settings
    {
        public static string[] textArr = { "2", "4", "8", "16", "32", "64", "128", "256", "512", "1024", "2048", "4096" };
        public static Brush[] textArrBrush1 = { Brushes.Olive, Brushes.Green, Brushes.Yellow, Brushes.Blue, Brushes.Brown, Brushes.Coral, 
                                              Brushes.Gray,Brushes.OrangeRed,Brushes.Pink,Brushes.Purple,Brushes.Orange,Brushes.Red,};
        public static Brush[] textArrBrush = { Brushes.Olive, Brushes.Green };

        public static int rowCount = 4;
        public static int columnCount = 4;

        public static void load()
        {
            String str = File.ReadAllText("settings.txt");
            string[] settingsArr = str.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            string str1 = settingsArr[0];
            string str2 = settingsArr[1];
            string[] textArr1 = str1.Split(',');
            textArr = textArr1;
            string[] textArr2 = str2.Split(',');
            rowCount = Int32.Parse(textArr2[0]);
            columnCount = Int32.Parse(textArr2[1]);
        }

        public static void change(string string1,string row,string col)
        {
            String str = string1;
            string rowC = row;
            string colC = col;
            //File.WriteAllText("settings.txt",str);
            List<string> list = new List<string>();
            list.Add(str);

            try
            {
                if (Int32.Parse(rowC) > 0 && Int32.Parse(colC) > 0)
                {

                }
                else
                {
                    rowC = rowCount.ToString();
                    col = columnCount.ToString();
                }
            }
            catch (Exception)
            {
                rowC = rowCount.ToString();
                col = columnCount.ToString();
            }
            finally
            {
                list.Add(row + "," + col);
            }
            File.WriteAllText("settings.txt","");
            File.AppendAllLines("settings.txt", list);
        }
    }
}
