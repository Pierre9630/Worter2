using System;
using System.Collections.Generic;
using System.Text;

namespace Worter2
{
    class Test: Prog
    {
        public void TestProgram()
        {
            Console.WriteLine("Choix 1 : Ajout dans la base à partir de english.txt \n" +
                "Choix 2 : Ajout de lignes manuellement à la base \n" +
                 "Choix 3 : Parcourir la base actuelle"
                 ,"Choix 4 : Supprimer la base (à confirmer !)" +
                 "Choix 5 : Bonus !");
            int choice = 0;
            choice = int.Parse(Console.ReadLine());
            Test:
            switch (choice)
            {
                case 1:
                    Prog prg = new Prog();
                    prg.addWords();
                    /* Remplir avec le programme original
                     * 
                     * */
                    break;
                    //break;
                case 2:
                    Console.WriteLine("Combien de lignes voulez-vous rajouter ?");
                    int nblines = short.Parse(Console.ReadLine());
                    int nb = 0;
                    var outputtab = new List<string>();
                    for (int i = 0; i < 4; i++)
                    {
                        outputtab.Add("generated\n");
                    }
                    while (nb < nblines)
                    {


                        //Si c'est la derniere ligne.
                        if (nb == nblines - 1)
                        {
                            string output = Console.ReadLine();
                            outputtab.Add(output);
                        }
                        else
                        {
                            string output = Console.ReadLine() + '\n';
                            outputtab.Add(output);
                        }
                        nb++;
                        //Console.WriteLine(outputtab[nb]);
                    }
                    FileOperation f = new FileOperation();

                    f.TestOutput(outputtab);
                    f.WriteToDatabase(f.CastToWords(outputtab));
                    break;
                case 3:
                    Console.WriteLine("Base de données actuelle");

                    
                    break;
                case 4:
                    Console.WriteLine("Ceci supprimera toute la base ! Etes vous sûr de faire cela?");
                    Console.WriteLine("Mot de  passe root :");
                    System.Security.SecureString bdd = Getpasswd.GetPassword();                   
                    Database db = new Database();
                    db.closeConnexion();
                    System.Threading.Thread.Sleep(2000);
                    db.InitConnexion(bdd);
                    db.deleteRows();
                    Console.WriteLine("test");
                    System.Threading.Thread.Sleep(10000);
                    break;                
                case 5:
                    Console.WriteLine("PacMan");

                    break;
                default:
                    Console.WriteLine(" -----------------");
                    goto Test;                    
            }
            //System.Threading.Thread.Sleep()

        }

        public void TestUnitaires()
        {
            Database db = new Database();
            Words word = new Words();
            db.addWords(word);
            db.readFromDb();
            db.closeConnexion();
        }
    }
}
