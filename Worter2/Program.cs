using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Worter2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            string targetFilePath = "english.txt";
            FileStream readFileStream = new FileStream(targetFilePath, FileMode.OpenOrCreate, FileAccess.Read, FileShare.None);
            StreamReader streamReader = new StreamReader(readFileStream);

            // Nous pouvons utiliser ReadToEnd qui lira de notre position courante jusqu'à la fin du fichier puis nous
            // retourne une chaîne de caractères.
            Console.WriteLine("---Lecture avec ReadToEnd---\n");
            for(int i = 0;i < 4; i++)
            {
                streamReader.ReadLine();
            }
            
            string fileContent = streamReader.ReadToEnd();
            //List<Words> lines = new List<Words>();
            List<Words> words = new List<Words>();

            //Console.Write(fileContent);
            
            string[] resultLines = SplitLines(fileContent);

            Console.WriteLine("\n---Lecture ligne par ligne---");
            List<string> resultLinesList = resultLines.OfType<string>().ToList();
            int j = 0;
            Parallel.ForEach(resultLinesList, line =>
            {
                Console.WriteLine($"ligne: {j += 1} " + line.ToString());
            });

            Console.WriteLine("\n---Ecriture dans la List words---");
            try
            {
                foreach(var line in resultLines)
                 {
                     string[] entries = line.Split(',');
                     Words newWords = new Words();
                    //Console.WriteLine(entries[0] + entries[1] + entries[2]);
                    newWords.English = entries[0];
                     newWords.Deutsch = entries[1];
                     newWords.Francais = entries[2];
                     words.Add(newWords);
                 }
                Console.WriteLine("\n---Lecture Mots par Mots(ou phrase) à partir de words---");
                Parallel.ForEach(words, word =>
                {
                    Console.WriteLine(" " + word.English + " " + word.Deutsch + " " + word.Francais);
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine("Supprimez les retours à la ligne ! /n" + ex.StackTrace.ToString());
                
            }
            Console.WriteLine("\n---Instanciation de la base de données---");
            Database db = new Database();
                   
           
            Console.WriteLine("\n---Ajout dans la base de données---");

            foreach (var word in words)
            {
                db.AddWords(word);
            }

            //for (int i = 0; i < resultLines.Length; i++)
            //{

            //    //Console.WriteLine($"ligne: {i} " + result[i]);
            //}

            // Fermeture du StreamReader et, comme avec le StreamWriter, du FileStream associé.
            streamReader.Close();
            Console.ReadKey();
        }

        public static string[] SplitLines(string file)
        {
           
            //List<string> dict = new List<string>();
            string[] dict = file.Split('\n');
            
            return dict;
        }

        public static string[] SplitWords(string file)
        {
            
            //List<string> dict = new List<string>();
            
            String [] word = file.Split(",");
            

            return word;
        }
        

    }
}
