using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shapes;

namespace ToDoProject
{
    public class FileManager
    {
        public List<string> list = new List<string>();
        public List<string[]> sList= new List<string[]>();

        public FileManager()
        {
            ReadAndSortFile();
        }

        public void ReadAndSortFile()
        {
            ReadFile(); 
            sList.Clear();
            for (int x = 0; x < list.Count; x++)
            {
                string[] thing = list[x].Split('|');
                sList.Add(thing);
            }
        }

        public void ReadFile()
        {
            list.Clear();
            using (StreamReader sr = new StreamReader(@"C:\Users\Konmiho\Desktop\OOP Finals\Bananana.txt"))
            {
                string line;                
                while ((line = sr.ReadLine()) != null)
                {
                    list.Add(line);
                }
            }
        }

        public void WriteFile(List<string> banana)
        {
            using (StreamWriter sw = new StreamWriter(@"C:\Users\Konmiho\Desktop\OOP Finals\Bananana.txt"))
            {
                foreach (string line in banana)
                {
                    sw.WriteLine(line);
                }
            }

        }

    }
}
