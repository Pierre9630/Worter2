﻿using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections;

namespace Worter2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hallo Liebe Worter !\n");

            
            Web wb = new Web();
            wb.WebBrowse();
            string targetFilePath = "english.txt";
            FileStream readFileStream = new FileStream(targetFilePath, FileMode.OpenOrCreate, FileAccess.Read, FileShare.None);
            StreamReader streamReader = new StreamReader(readFileStream);                

            //System.Environment.Exit(0);
            Console.WriteLine("\n---Instanciation de la base de données---");
            Database db = new Database();
            Console.WriteLine("Mot de passe BDD ?\n");

            System.Security.SecureString bdd = Getpasswd.GetPassword() ;
            
            db.InitConnexion(bdd);
            //Console.WriteLine(bdd);

            /* Publier Message test */
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.WriteLine("Test Publi");
            Worter wrt = new Worter();
            string message = wrt.Publier("Un petit message de test, il faut qu'il fasse quelques charactères pour tester........^^");
            Console.BackgroundColor = ConsoleColor.Black;
            //db.AddMessage("zdzzddzdzcc edzzzd zd zd e ffe ez");
            Console.WriteLine(message);
            Console.WriteLine("Lancer le programme test ?");
            string rep = Console.ReadLine();
            
            if (rep.StartsWith("y",StringComparison.CurrentCultureIgnoreCase) || rep.StartsWith("o",StringComparison.CurrentCultureIgnoreCase))
            {
               
                //Test test = new Test();
                //Task thread = Task.Factory.StartNew(() => test.TestProgram());
                ////thread.Start();
                //Task.WaitAll(thread);
               
                try
                {
                    Test test = new Test();
                    Task thread = Task.Factory.StartNew(() => test.TestProgram());
                    Task.WaitAll(thread);
                }
                catch (AggregateException e)
                {
                    Console.WriteLine("\nThe following exceptions have been thrown by WaitAll(): (THIS WAS EXPECTED)");
                    for (int h = 0; h < e.InnerExceptions.Count; h++)
                    {
                        Console.WriteLine("\n-------------------------------------------------\n{0}", e.InnerExceptions[h].ToString());
                    }
                }
                //try
                //{
                //    Task.WaitAll(thread);
                //}
                //catch (AggregateException e)
                //{
                //    Console.WriteLine("\nThe following exceptions have been thrown by WaitAll(): (THIS WAS EXPECTED)");
                //    for (int h = 0; h < e.InnerExceptions.Count; h++)
                //    {
                //        Console.WriteLine("\n-------------------------------------------------\n{0}", e.InnerExceptions[h].ToString());
                //    }
                //}
            }
            
            List<Words> compare = new List<Words>();

            db.HasRows(compare);

            int count = db.CountID();

            // Nous pouvons utiliser ReadToEnd qui lira de notre position courante jusqu'à la fin du fichier puis nous
            // retourne une chaîne de caractères.
            Console.WriteLine("---Lecture avec ReadToEnd---\n");
            for (int i = 0; i < 4; i++)
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
            foreach (var line in resultLinesList)
            {
                Console.WriteLine($"ligne: {j += 1} " + line.ToString());
                String myString = "";
                byte[] bytes = Encoding.Default.GetBytes(line.ToString());
                myString = Encoding.UTF8.GetString(bytes);
                Console.WriteLine("en UTF8 : " + myString);
            }

            Console.WriteLine("\n---Ecriture dans la List words---");
            try
            {
                foreach (var line in resultLines)
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
                Console.WriteLine("\n---Comparaison entre Base de données et données rentrées---");
                //Words NewWords = new Words();
                List<Words> NewWordsList = new List<Words>();
                if ((compare != null) && (compare.Any())) {

                    var resultCompare = words.Except(compare);
                    //var resultCompareDeutsch = words.Where(p => compare.All(p3 => p3.Deutsch != p.Deutsch));
                    //var resultCompareFrancais = words.Where(p => compare.All(p4 => p4.Francais != p.Francais));

                    //NewWords.English = resultCompareEnglish.ToString();
                    //NewWords.Deutsch = resultCompareDeutsch.ToString();
                    //NewWords.Francais = resultCompareFrancais.ToString();
                    //NewWordsList.Add(NewWords);


                }

                foreach (var word in NewWordsList)
                {
                    Console.WriteLine(word.English.ToString());
                }


                //if (compare.OrderBy(m => m).SequenceEqual(words.OrderBy(m => m)))
                //{
                //    Console.WriteLine("Mots déjà rentrées dans la base !");
                //    Console.ReadKey();
                //    System.Environment.Exit(-1);

                //}
                //else if (!compare.Any())
                //{
                //    Console.WriteLine("Not Equal list");
                //    System.Collections.IEnumerator ListAEnum = words.GetEnumerator();
                //    System.Collections.IEnumerator ListBEnum = compare.GetEnumerator();

                //    Compare cmp = new Compare();
                //    List<Words> words1 = new List<Words>();
                //    //words1 = cmp.CompareWords(ListAEnum, ListBEnum);
                //}



                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\n---Lecture Mots par Mots(ou phrase) à partir de words---");
                Console.WriteLine("\n{0,-10} | {1,-10} | {2,-10}", "English", "Deutsch", "Français");
                Console.WriteLine("------------------------------------------------------------------");
                foreach (var word in words)
                {

                    Console.WriteLine("\n{0,-10} | {1,-10} | {2,-10}", word.English, word.Deutsch, word.Francais);
                }
                foreach (var type in types)
                {
                    Console.WriteLine("\n " + type.type);
                }
                Console.WriteLine("\n------------------------------------------------------------------\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Supprimez les retours à la ligne ! \n" + ex.StackTrace.ToString());

            }
            //Lecture avant ajout
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("---Lecture de la Base de Données---\n");

            Console.WriteLine("---Nombre entrées dans la base---\n" + count.ToString());
            //db.HasRows();

            Console.WriteLine("\n---Ajout dans la base de données---");

            foreach (var word in words)
            {
                db.AddWords(word);
            }

            foreach (var type in types)
            {
                //Incrémenter le nombre d'entrées                
                count++;
                //Console.WriteLine(count.ToString());
                db.AddType(type, count);
            }

            //for (int i = 0; i < resultLines.Length; i++)
            //{

            //    //Console.WriteLine($"ligne: {i} " + result[i]);
            //}


            Console.WriteLine("Voulez-vous rajouter des phrases dans la base ?");
            if (Console.ReadLine().Equals("O", StringComparison.InvariantCultureIgnoreCase))
            {
                if (File.Exists(targetFilePath)){
                    FileStream readFileStream2 = new FileStream(targetFilePath, FileMode.OpenOrCreate, FileAccess.Read, FileShare.None);
                    StreamReader streamReader2 = new StreamReader(readFileStream);

                    Console.WriteLine("Copie du contenu de " + targetFilePath + ".txt dans la base phrases");
                }
                else
                {
                    Console.WriteLine("Indiquez le nom du fichier sans l'extension (à mettre dans le dossier courant du exe)");
                    string file = Console.ReadLine();
                    string targetFilePath2 = file + ".txt";
                    FileStream readFileStream2 = new FileStream(targetFilePath2, FileMode.OpenOrCreate, FileAccess.Read, FileShare.None);
                    StreamReader streamReader2 = new StreamReader(readFileStream);

                    Console.WriteLine("Copie du contenu de " + file + ".txt dans la base phrases");
                }       

            }

            Console.WriteLine("Afficher des beaux tableaux LINQ ?");
            if (Console.ReadLine().Equals("O", StringComparison.InvariantCultureIgnoreCase)){
                Prog prg = new Prog();
                prg.Main(words);
            }
            

            //Console.WriteLine("Combien de lignes voulez-vous rajouter ?");
            //int nblines = short.Parse(Console.ReadLine());
            //int nb = 0;
            //string[] outputtab = new string[nblines];
            //while(nb < nblines)
            //{
            //    string output = Console.ReadLine();
            //    outputtab.Append(output);
            //}
            //Composants.FileOperation f = new Composants.FileOperation();
            //f.TestOutput(outputtab);


            // Fermeture du StreamReader et, comme avec le StreamWriter, du FileStream associé.
            streamReader.Close();
            Console.WriteLine("Appuyez sur une touche pour fermer Worter");
            Console.ReadKey();
        }

        //On Close()
        //static void CurrentDomain_ProcessExit(object sender, EventArgs e)
        //{
        //    Console.WriteLine("exit");
        //}

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
