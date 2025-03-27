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
            using (StreamReader sr = new StreamReader(@"D:\Krizia\source\repos\ToDoProject\ToDoProject\Assets\ToDoList.txt"))
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
            using (StreamWriter sw = new StreamWriter(@"D:\Krizia\source\repos\ToDoProject\ToDoProject\Assets\ToDoList.txt"))
            {
                foreach (string line in banana)
                {
                    sw.WriteLine(line);
                }
            }

        }

        public void RewriteSList()
        {
            using (StreamWriter sw = new StreamWriter(@"D:\Krizia\source\repos\ToDoProject\ToDoProject\Assets\ToDoList.txt"))
            {
                foreach (string[] task in sList)
                {
                    string taskLine = $"{task[0]}|{task[1]}|{task[2]}|{task[3]}|{task[4]}|{task[5]}|{task[6]}";
                    sw.WriteLine(taskLine);
                }
            }
        }

    }
}
