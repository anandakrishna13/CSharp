using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;

namespace FolderSize
{
    class Program
    {
        static void Main(string[] args)
        {
            DateTime startTime = DateTime.Now;
            DateTime endTime;

            string folderPath = args.Length > 0 ? args[0] : "c:/";
            List<Folder> lst = ListSize(folderPath);
            //var output = from l in lst orderby l.size descending select l;

            var output = lst.OrderBy(p=>p.size).ToList() ;

            foreach (var a in output)
            {
                Console.WriteLine("{0}--{1}", a.name, a.size);
            }
            endTime = DateTime.Now;
            Console.WriteLine("Elapsed time {0}", endTime - startTime);
            Console.ReadLine();
        }

        private static List<Folder> ListSize(string folderPath)
        {
            long size1 = 0;
            Folder folder;
            List<Folder> lst = new List<Folder>();
                        
            DirectoryInfo d = new DirectoryInfo(folderPath);
            if (d.Exists)
            {
                DirectoryInfo[] subDir = d.GetDirectories("*.*", SearchOption.TopDirectoryOnly);
                foreach (DirectoryInfo di in subDir)
                {
                    size1 = 0;
                    folder = new Folder();

                    try
                    {

                        FileInfo[] allFiles = di.GetFiles("*.*", SearchOption.AllDirectories);
                   
                    foreach (FileInfo f in allFiles)
                    {
                        size1 += f.Length;
                        //Console.WriteLine(f.Length);
                    }
                    }
                    catch
                    {
                    }
                    folder.name = di.Name;
                    folder.size = size1;
                    lst.Add(folder);
                }
            }
            else
            {
                throw new Exception("file not found");
            }
            return lst;
        }
    }

    public class Folder
    {
      public  string name;
      public  long size;
    }
}
