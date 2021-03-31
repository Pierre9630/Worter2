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
            Console.WriteLine("Hallo Liebe Worter !\n");
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
            List<Type> types = new List<Type>();
            //Console.Write(fileContent);

            string[] resultLines = SplitLines(fileContent);

            Console.WriteLine("\n---Lecture ligne par ligne---");
            List<string> resultLinesList = resultLines.OfType<string>().ToList();
            int j = 0;
            Parallel.ForEach(resultLinesList, line =>
            {
                Console.WriteLine($"ligne: {j += 1} " + line.ToString());
                String myString = "";
                byte[] bytes = Encoding.Default.GetBytes(line.ToString());
                myString = Encoding.UTF8.GetString(bytes);
                Console.WriteLine("en UTF8 : " + myString);
            });

            Console.WriteLine("\n---Ecriture dans la List words---");
            try
            {
                foreach(var line in resultLines)
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
                    types.Add(type);
                 }
                Console.WriteLine("\n---Lecture Mots par Mots(ou phrase) à partir de words---");
                Parallel.ForEach(words, word =>
                {
                    Console.WriteLine(" " + word.English + " " + word.Deutsch + " " + word.Francais);                    
                });
                foreach(var type in types)
                {
                    Console.WriteLine("\n " + type.type);
                }
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
            int count = 0;
            foreach (var type in types)
            {
                //Incrémenter le nombre d'entrées                
                count++;
                //Console.WriteLine(count.ToString());
                db.AddType(type,count);
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
