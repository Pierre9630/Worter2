using System;
using System.Collections.Generic;
using System.Text;
using System.Security;
using MySql.Data;
using MySql.Web;
using MySql.Data.EntityFramework;
using MySql.Data.MySqlClient;
using System.Runtime.InteropServices;

namespace Worter2
{
    class Database
    {
        private MySqlConnection connection;
        List<Words> reads = new List<Words>();
        List<Type> readstype = new List<Type>();
        //List<ID> id = new List<ID>();
        // Constructeur
        public Database()
        {
            this.InitConnexion();
        }

        // Méthode pour initialiser la connexion
        public void InitConnexion([Optional] SecureString mpsroot)
        {
            try
            {
                if(mpsroot.GetString().Length > 0)
                {
                    
                    
                    //Console.WriteLine("TEST " + mpsroot.GetString());
                    string connectionString = "Server=192.168.1.42;Database=Langues;Uid=root;Pwd=" + mpsroot.GetString() + ";";
                    this.connection = new MySqlConnection(connectionString);
                }
                Console.WriteLine("Init");
                
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public void closeConnexion()
        {
            this.connection.Close();
        }

        //Nombre Affichages de Connexions limités à 1
        bool countcon = false;
        bool countend = false;

        public bool readwords(List<Words> query, List<Words> reads, List<Type> readstype, List<ID> id)
        {
            try
            {
                this.connection.Open();


                // Création d'une commande SQL en fonction de l'objet connection
                MySql.Data.MySqlClient.MySqlCommand cmd = this.connection.CreateCommand();

                // Requête SQL
                cmd.CommandText = "SELECT * FROM Vocabulaire WHERE ID = @id OR English  = @en OR Deutsch = @de OR Francais = @fr";
                MySql.Data.MySqlClient.MySqlDataReader dataReader = cmd.ExecuteReader();

                //cmd.Parameters.AddWithValue("@id", word.English);
                //cmd.Parameters.AddWithValue("@en", word.English);
                //cmd.Parameters.AddWithValue("@de", word.Deutsch);
                //cmd.Parameters.AddWithValue("@fr", word.Francais);

                // Exécution de la commande SQL
                //cmd.ExecuteNonQuery();
                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    reads.Add(new Words
                    {
                        English = dataReader["English"].ToString(),
                        Deutsch = dataReader["Deutsch"].ToString(),
                        Francais = dataReader["Francais"].ToString()
                    });
                    Console.WriteLine(dataReader["English"].ToString());
                }

                while (dataReader.Read())
                {
                    readstype.Add(new Type
                    {
                        type = dataReader["Type"].ToString()
                    });
                }
                while (dataReader.Read())
                {
                    id.Add(new ID
                    {
                        id = Convert.ToInt32(dataReader["ID"].ToString())
                    });
                }

                //close Data Reader
                dataReader.Close();


                // Fermeture de la connexion
                this.connection.Close();
                Console.WriteLine("---Mots déjà présents !---\n");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine("---Problème lors de la requete SELECT !---\n" + ex);
                return true;
            }

        }

        // Méthode pour ajouter un contact
        public int countID()
        {
            int prevcount = 0;
            try
            {
                using (connection)
                {
                    MySqlCommand command = new MySqlCommand(
                      "SELECT ID FROM Vocabulaire;",
                      connection);
                    connection.Open();

                    MySqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            ++prevcount;
                            //Console.WriteLine("{0}\t{1}\t{2}`\t{3}\t{4}", reader.GetInt32(0),
                            //    reader.GetString(1),reader.GetString(2), reader.GetString(3), reader.GetString(4));
                        }
                    }
                    else
                    {
                        Console.WriteLine("\n---No rows found.---");
                    }

                    reader.Close();

                    connection.Close();
                    return prevcount;
                }
            }catch(Exception ex)
            {
                Console.WriteLine("\n Connexion echouée" + ex);
            }
            return 0;
        }
        public bool hasRows(List<Words> words)
        {
            
            using (connection)
            {
                MySqlCommand command = new MySqlCommand(
                  "SELECT * FROM Vocabulaire;",
                  connection);
                connection.Open();

                MySqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        
                        Console.WriteLine("{0}\t{1}\t{2}`\t{3}\t{4}", reader.GetInt32(0),
                            reader.GetString(1),reader.GetString(2), reader.GetString(3), reader.GetString(4));



                        reads.Add(new Words
                        {
                            English = reader.GetString(2),
                            Deutsch = reader.GetString(3),
                            Francais = reader.GetString(4)
                        });
                        
                    }
                }
                else
                {
                    Console.WriteLine("\n---No rows found.---");
                }

                reader.Close();

                return false;
            }
        }
        public void addWords(Words word)
        {
            
            try
            {
                // Ouverture de la connexion SQL
                if (countcon == false)
                {
                    Console.WriteLine("\n---Connection à la base de données---");
                    countcon = true;
                }
                
                this.connection.Open();

                // Création d'une commande SQL en fonction de l'objet connection
                MySql.Data.MySqlClient.MySqlCommand cmd = this.connection.CreateCommand();

                // Requête SQL
                cmd.CommandText = "INSERT INTO Vocabulaire (English, Deutsch, Francais ) VALUES (@en, @de, @fr)";

                // utilisation de l'objet contact passé en paramètre
                //cmd.Parameters.AddWithValue("@type", type.type);
                cmd.Parameters.AddWithValue("@en", word.English);
                cmd.Parameters.AddWithValue("@de", word.Deutsch);
                cmd.Parameters.AddWithValue("@fr", word.Francais);
                
                // Exécution de la commande SQL
                cmd.ExecuteNonQuery();
                
                // Fermeture de la connexion
                if(countend == false)
                {
                    Console.WriteLine("\n---Mots Ajoutées à la base---");
                    countend = true;
                }
                this.connection.Close();
            }            
            catch(Exception ex)
            {
                // Gestion des erreurs :
                // Possibilité de créer un Logger pour les exceptions SQL reçus
                // Possibilité de créer une méthode avec un booléan en retour pour savoir si le contact à été ajouté correctement.
                Console.WriteLine("\n---Base non connecté !---" + ex.Message);
                
                
            }          
        }
        public void addType(Type type,int count)
        {
            try
            {
                this.connection.Open();

                // Création d'une commande SQL en fonction de l'objet connection
                MySql.Data.MySqlClient.MySqlCommand cmd = this.connection.CreateCommand();

                                
                // Requête SQL
                cmd.CommandText = "UPDATE Vocabulaire SET Type=@type WHERE ID IN(@count);";

                // utilisation de l'objet contact passé en paramètre
                cmd.Parameters.AddWithValue("@type", type.type);
                cmd.Parameters.AddWithValue("@count", count);

                // Exécution de la commande SQL
                cmd.ExecuteNonQuery();

                // Fermeture de la connexion
                this.connection.Close();
            }
            catch(Exception ex)
            {
                Console.WriteLine("\n---Base non connecté !---" + ex.Message);
            }
        }
        public void addPhrase(Phrases Phrase, [Optional] Words Word, [Optional] Words Word2, [Optional] Words Word3)
        {
            try
            {
                // Ouverture de la connexion SQL
                if (countcon == false)
                {
                    Console.WriteLine("\n---Connection à la base de données---");
                    countcon = true;
                }

                this.connection.Open();

                // Création d'une commande SQL en fonction de l'objet connection
                MySql.Data.MySqlClient.MySqlCommand cmd = this.connection.CreateCommand();

                // Requête SQL
                cmd.CommandText = "INSERT INTO Phrases (Phrase,Word1,Word2,Word3 ) VALUES (@Phrase,@Word,@Word2,@Word3)";

                // utilisation de l'objet contact passé en paramètre
                //cmd.Parameters.AddWithValue("@type", type.type);
                cmd.Parameters.AddWithValue("@Phrase", Phrase);
                
                //Si word est défini dans les parametres
                if (!Word.Equals(null))
                {
                    cmd.Parameters.AddWithValue("@Word", Word);
                }
                else
                {
                    Console.WriteLine("Word is null");
                }

                // Exécution de la commande SQL
                cmd.ExecuteNonQuery();

                // Fermeture de la connexion
                if (countend == false)
                {
                    Console.WriteLine("\n---Mots Ajoutées à la base---");
                    countend = true;
                }
                this.connection.Close();
            }
            catch (Exception ex)
            {
                // Gestion des erreurs :
                // Possibilité de créer un Logger pour les exceptions SQL reçus
                // Possibilité de créer une méthode avec un booléan en retour pour savoir si le contact à été ajouté correctement.
                Console.WriteLine("\n---Base non connecté !---" + ex.Message);


            }
        }
        public void deleteRows()
        {
            try
            {

                // Ouverture de la connexion SQL

                if (countcon == false)
                {
                    Console.WriteLine("\n---Connection à la base de données---");
                    countcon = true;
                }
                this.connection.Open();

                // Création d'une commande SQL en fonction de l'objet connection
                MySql.Data.MySqlClient.MySqlCommand cmd = this.connection.CreateCommand();

                // Requête SQL
                cmd.CommandText = "DELETE ALL ROWS FROM Vocabulaire;SET  @num := 0;UPDATE Vocabulaire SET id = @num := (@num+1);ALTER TABLE Vocabulaire AUTO_INCREMENT =1;";
                     

                // Exécution de la commande SQL
                cmd.ExecuteNonQuery();

                // Fermeture de la connexion
                if (countend == false)
                {
                    Console.WriteLine("\n---Suppression---");
                    countend = true;
                }
                this.connection.Close();
            }
            catch (Exception ex)
            {
                // Gestion des erreurs :
                // Possibilité de créer un Logger pour les exceptions SQL reçus
                // Possibilité de créer une méthode avec un booléan en retour pour savoir si le contact à été ajouté correctement.
                Console.WriteLine("\n---Base non connecté !---" + ex.Message);


            }
        }
        public List<Words> readFromDb()
        {
            this.connection.Open();


            // Création d'une commande SQL en fonction de l'objet connection
            MySql.Data.MySqlClient.MySqlCommand cmd = this.connection.CreateCommand();

            // Requête SQL
            cmd.CommandText = "SELECT English,Deutsch,Francais FROM Langues.Vocabulaire;";
            MySql.Data.MySqlClient.MySqlDataReader dataReader = cmd.ExecuteReader();

            //cmd.Parameters.AddWithValue("@id", word.English);
            //cmd.Parameters.AddWithValue("@en", word.English);
            //cmd.Parameters.AddWithValue("@de", word.Deutsch);
            //cmd.Parameters.AddWithValue("@fr", word.Francais);

            // Exécution de la commande SQL
            //cmd.ExecuteNonQuery();
            //Read the data and store them in the list
            //Words words = new Words();
            
            while (dataReader.Read())
            {
                reads.Add(new Words
                {
                    English = dataReader["English"].ToString(),
                    Deutsch = dataReader["Deutsch"].ToString(),
                    Francais = dataReader["Francais"].ToString()
                });
                //Console.WriteLine(dataReader["English"].ToString());
            }
            return reads;
        }
        public void AddMessage(string Message)
        {
            try
            {
                // Ouverture de la connexion SQL
                if (countcon == false && countend == true)
                {
                    Console.WriteLine("\n---Connection à la base de données---");
                    countcon = true;
                }

                this.connection.Open();

                // Création d'une commande SQL en fonction de l'objet connection
                MySql.Data.MySqlClient.MySqlCommand cmd = this.connection.CreateCommand();

                // Requête SQL
                cmd.CommandText = "INSERT INTO Messages (Message) VALUES (@Message)";

                // utilisation de l'objet contact passé en paramètre
                //cmd.Parameters.AddWithValue("@type", type.type);
                //cmd.Parameters.AddWithValue("@Message", Message);

                //Si word est défini dans les parametres
                if (!Message.Equals(null))
                {
                    cmd.Parameters.AddWithValue("@Message", Message);
                }
                else
                {
                    Console.WriteLine("Message est vide ! ");
                }

                // Exécution de la commande SQL
                cmd.ExecuteNonQuery();

                // Fermeture de la connexion
                if (countend == false)
                {
                    Console.WriteLine("\n---Message Ajoutée à la base---");
                    countcon = false;
                    countend = true;
                }
                this.connection.Close();
            }
            catch (Exception ex)
            {
                // Gestion des erreurs :
                // Possibilité de créer un Logger pour les exceptions SQL reçus
                // Possibilité de créer une méthode avec un booléan en retour pour savoir si le contact à été ajouté correctement.
                Console.WriteLine("\n---Base non connecté !---" + ex.Message);


            }
        }
    }
    
    
}
