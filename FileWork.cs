using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Lab1_V2
{
    class FileWork
    {
        public static List<string> Read(string path)
        {
            List<string> result = new List<string>();
            FileInfo fileInf = new FileInfo(path);
            if (fileInf.Exists)
            {
                StreamReader fin = new StreamReader(path);
                while (!fin.EndOfStream)
                {
                    result.Add(fin.ReadLine());
                }
                fin.Close();
            }
            return result;
        }
    }
}
