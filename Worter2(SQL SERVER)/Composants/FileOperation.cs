using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Worter2_SQL_SERVER_

{
    class FileOperation
    {
        
        public void TestOutput(List <string> args)
        {
            FileOperation f = new FileOperation();
            if(args.Count < 1)
            {
                Console.WriteLine("Saisie Vide!");

            }
            else
            {
                f.WriteOutput(args);
            }
        }
        private void WriteOutput(List<string> args)
        {

            
            Console.WriteLine("Ecriture de la sortie...");
            using (StreamWriter outputFile = new StreamWriter("Output.txt"))
            {
               foreach(string line in args)
                {
                    outputFile.Write(line);
                    
                }
            }
            //var lines = System.IO
            //File.ReadAllLines("Output.txt");
            //System.IO.File.WriteAllLines("Output.txt", lines.Take(lines.Length - 1).ToArray());
            // Set a variable to the Documents path.
            //string docPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            // Append text to an existing file named "WriteLines.txt".
            //using (StreamWriter outputFile = new StreamWriter(Path.Combine(docPath, "Output.txt"), true))
            //{
            //    outputFile.WriteLine(args);
            //}

        }
        public List<Words> CastToWords(List<string> args)
        {
            Console.WriteLine("Rajout dans la base de données");            
            List<Words> words = new List<Words>();
            foreach (var line in args)
            {
                string[] entries = line.Split(',');
                //Convert To UTF-8
                String entry0 = "";
                byte[] bytes = Encoding.Default.GetBytes(entries[0]);
                entry0 = Encoding.UTF8.GetString(bytes);
                String entry1 = "";
                byte[] bytes1 = Encoding.Default.GetBytes(entries[1]);
                entry1 = Encoding.UTF8.GetString(bytes1);
                String entry2 = "";
                byte[] bytes2 = Encoding.Default.GetBytes(entries[2]);
                entry2 = Encoding.UTF8.GetString(bytes2);
                Words newWords = new Words();
                Type type = new Type();
                if (entries[3].StartsWith("[word"))
                {
                    type.type = "word";
                }
                else
                {
                    type.type = "sentence";
                }
                //Console.WriteLine(entries[0] + entries[1] + entries[2]);
                newWords.English = entry0;
                newWords.Deutsch = entry1;
                newWords.Francais = entry2;
                words.Add(newWords);                                
            }
            return words;
        }
        public void WriteToDatabase(List<Words> words)
        {
            DataAccess db = new DataAccess();
            foreach (var word in words)
            {
                db.AddWords(word);
            }
        }
    }
}
