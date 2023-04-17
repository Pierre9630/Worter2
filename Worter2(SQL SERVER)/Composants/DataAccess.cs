using System.Collections.Generic;
using System.Linq;
using Worter2_SQL_SERVER_;
using DotNetCore.CAP.Internal;
using Dapper;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Text;
using System;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Drawing;

namespace Worter2_SQL_SERVER_
{
    public class DataAccess
    {
        //private SqlConnection con;
        readonly string conString = "Data Source=DESKTOP-CVBU275\\SQLEXPRESS;Initial Catalog=Worter2;Integrated Security=True;Pooling=False";

        public void DataAcess()
        {
            this.Connexion();
        }
        public SqlConnection Connexion()
        {
            try
            {
                SqlConnection con = new SqlConnection(conString);
                con.Open();
                Console.WriteLine("Init");
                return con;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return null;
        }
        public void Close(SqlConnection con)
        {
            con.Close();
        }
        public void CloseDataReader(SqlDataReader reader)
        {
            reader.Close();
        }
        public int CountID()
        {
            int prevcount = 0;
            //SqlConnection con = new SqlConnection(conString);
            //con.Open();
            DataAccess da = new DataAccess();
            SqlConnection con = da.Connexion();
            if (con.State == System.Data.ConnectionState.Open)
            {
                string q = "SELECT ID FROM [dbo].[Vocabulaire];";
                SqlCommand cmd = new SqlCommand(q, con);
                cmd.ExecuteNonQuery();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ++prevcount;
                    }
                }
                else
                {
                    Console.WriteLine("Pas de données dans la base !");
                }
                da.CloseDataReader(reader);
                da.Close(con);
            }
            return prevcount;


        }
        public void AddWords(Words word)
        {
            string cmdString = "INSERT INTO [dbo].[Vocabulaire] (English, Deutsch, Francais ) VALUES (@en, @de, @fr)";
            //string connString = "your connection string";
            //using (SqlConnection conn = new SqlConnection(connString))
            //{
            using (SqlCommand comm = new SqlCommand())
            {
                //comm.Connection = conn;
                comm.CommandText = cmdString;
                comm.Parameters.AddWithValue("@en", word.English);
                comm.Parameters.AddWithValue("@de", word.Deutsch);
                comm.Parameters.AddWithValue("@fr", word.Francais);
                try
                {
                    //conn.Open();
                    comm.ExecuteNonQuery();
                }
                catch (SqlException e)
                {
                    // do something with the exception
                    // don't hide it
                }
            }
            // }
            //string cmdString = "INSERT INTO Vocabulaire (English, Deutsch, Francais ) VALUES (@en, @de, @fr)";
            //using (SqlCommand comm = new SqlCommand())
            //{
            //    comm.CommandString = cmdString;
            //    comm.Parameters.AddWithValue("@en", word.English);
            //    comm.Parameters.AddWithValue("@fr", word.Francais);
            //    comm.Parameters.AddWithValue("@de", word.Deutsch);
            //    // other codes.
            //}
        }
        public void AddType(Type type, int count)
        {
            //string cmdString = "INSERT INTO [dbo].[Table] (English, Deutsch, Francais ) VALUES (@en, @de, @fr)";
            string cmdString = "UPDATE [dbo].[Vocabulaire] SET Type = @type WHERE ID IN(@count);";
            //string connString = "your connection string";
            //using (SqlConnection conn = new SqlConnection(connString))
            //{
            using (SqlCommand comm = new SqlCommand())
            {
                //comm.Connection = conn;
                comm.CommandText = cmdString;
                comm.Parameters.AddWithValue("@type", type.type);
                comm.Parameters.AddWithValue("@value", ^count);
                //comm.Parameters.AddWithValue("@fr", word.Francais);
                try
                {
                    //conn.Open();
                    comm.ExecuteNonQuery();
                }
                catch (SqlException e)
                {
                    // do something with the exception
                    // don't hide it
                }
            }
        }
        public void DeleteRows(String word, Type
            type = null)
        {
            Console.WriteLine("test delete");
            //string cmdString = "INSERT INTO [dbo].[Table] (English, Deutsch, Francais ) VALUES (@en, @de, @fr)";
            string cmdString = "DELETE FROM [dbo].[Vocabulaire] WHERE Type = @type OR English = @word OR Francais = @word OR Deutsch = @word ;";
            //string connString = "your connection string";
            //using (SqlConnection conn = new SqlConnection(connString))
            //{
            using (SqlCommand comm = new SqlCommand())
            {
                //comm.Connection = conn;
                comm.CommandText = cmdString;
                comm.Parameters.AddWithValue("@type", type.type);
                comm.Parameters.AddWithValue("@word", word);
                //comm.Parameters.AddWithValue("@fr", word.Francais);
                try
                {
                    //conn.Open();
                    comm.ExecuteNonQuery();
                }
                catch (SqlException e)
                {
                    // do something with the exception
                    // don't hide it
                }
            }
        }
        //public List<Words> GetPeople(string English,string Deutsch,string Francais)
        //{
        //    using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("SampleDB")))
        //    {
        //        //var output = connection.Query<Person>($"select * from People where LastName = '{ lastName }'").ToList();
        //        var output = connection.Query<Words>("dbo.Worter2 @English @Deutsch @Francais", new { English = English, Deutsch = Deutsch, Francais = Francais }).ToList();
        //        return output;
        //    }
        //}

        //public void InsertPerson(string firstName, string lastName, string emailAddress, string phoneNumber)
        //{
        //    using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("SampleDB")))
        //    {
        //        //Person newPerson = new Person { FirstName = firstName, LastName = lastName, EmailAddress = emailAddress, PhoneNumber = phoneNumber };
        //        List<Person> people = new List<Person>();

        //        people.Add(new Person { FirstName = firstName, LastName = lastName, EmailAddress = emailAddress, PhoneNumber = phoneNumber });

        //        connection.Execute("dbo.People_Insert @FirstName, @LastName, @EmailAddress, @PhoneNumber", people);

        //    }
        //}

    }
}
//using System;
//using System.Collections.Generic;
//using System.Text;
//using System.Security;
//using 
//using System.Runtime.InteropServices;

//namespace Worter2_SQL_SERVER_

//{
//    class Database
//    {
//        private MySqlConnection connection;
//        List<Words> reads = new List<Words>();
//        List<Type> readstype = new List<Type>();
//        //List<ID> id = new List<ID>();
//        // Constructeur
//        public Database()
//        {
//            this.InitConnexion();
//        }

//        // Méthode pour initialiser la connexion
//        public void InitConnexion([Optional] SecureString mpsroot)
//        {
//            try
//            {
//                if(mpsroot.GetString().Length > 0)
//                {


//                    Console.WriteLine("TEST " + mpsroot.GetString());
//                    string connectionString = "Server=192.168.1.42;Database=Langues;Uid=root;Pwd=" + mpsroot.GetString() + ";";
//                    this.connection = new MySqlConnection(connectionString);
//                }
//                Console.WriteLine("Init");

//            }
//            catch(Exception ex)
//            {
//                Console.WriteLine(ex.ToString());
//            }
//        }

//        //Nombre Affichages de Connexions limités à 1
//        bool countcon = false;
//        bool countend = false;

//        //public bool ReadWords(List<Words>query,List<Words> reads, List<Type> readstype, List<ID> id)
//        //{
//        //    try
//        //    {
//        //        this.connection.Open();


//        //        // Création d'une commande SQL en fonction de l'objet connection
//        //        MySql.Data.MySqlClient.MySqlCommand cmd = this.connection.CreateCommand();

//        //        // Requête SQL
//        //        cmd.CommandText = "SELECT * FROM Vocabulaire";
//        //        MySql.Data.MySqlClient.MySqlDataReader dataReader = cmd.ExecuteReader();
//        //        // Exécution de la commande SQL
//        //        //cmd.ExecuteNonQuery();
//        //        //Read the data and store them in the list
//        //        while (dataReader.Read())
//        //        {
//        //            reads.Add(new Words
//        //            {
//        //                English = dataReader["English"].ToString(),
//        //                Deutsch = dataReader["Deutsch"].ToString(),
//        //                Francais = dataReader["Francais"].ToString()
//        //            });
//        //            Console.WriteLine(dataReader["English"].ToString());
//        //        }

//        //        while (dataReader.Read())
//        //        {
//        //            readstype.Add(new Type
//        //            {
//        //                type = dataReader["Type"].ToString()
//        //            });
//        //        }
//        //        while (dataReader.Read())
//        //        {
//        //            id.Add(new ID
//        //            {
//        //                id = Convert.ToInt32(dataReader["ID"].ToString())
//        //            });
//        //        }

//        //        //close Data Reader
//        //        dataReader.Close();


//        //        // Fermeture de la connexion
//        //        this.connection.Close();
//        //        Console.WriteLine("---Mots déjà présents !---\n");
//        //        return false;
//        //    }
//        //    catch(Exception ex)
//        //    {
//        //        Console.WriteLine("---Problème lors de la requete SELECT !---\n" + ex);
//        //        return true;
//        //    }

//        //}      

//        // Méthode pour ajouter un contact
//        public int CountID()
//        {
//            int prevcount = 0;
//            try
//            {
//                using (connection)
//                {
//                    MySqlCommand command = new MySqlCommand(
//                      "SELECT ID FROM Vocabulaire;",
//                      connection);
//                    connection.Open();

//                    MySqlDataReader reader = command.ExecuteReader();

//                    if (reader.HasRows)
//                    {
//                        while (reader.Read())
//                        {
//                            ++prevcount;
//                            //Console.WriteLine("{0}\t{1}\t{2}`\t{3}\t{4}", reader.GetInt32(0),
//                            //    reader.GetString(1),reader.GetString(2), reader.GetString(3), reader.GetString(4));
//                        }
//                    }
//                    else
//                    {
//                        Console.WriteLine("\n---No rows found.---");
//                    }

//                    reader.Close();

//                    connection.Close();
//                    return prevcount;
//                }
//            }catch(Exception ex)
//            {
//                Console.WriteLine("\n Connexion echouée" + ex);
//            }
//            return 0;
//        }
//        public bool HasRows(List<Words> words)
//        {

//            using (connection)
//            {
//                MySqlCommand command = new MySqlCommand(
//                  "SELECT * FROM Vocabulaire;",
//                  connection);
//                connection.Open();

//                MySqlDataReader reader = command.ExecuteReader();

//                if (reader.HasRows)
//                {
//                    while (reader.Read())
//                    {

//                        Console.WriteLine("{0}\t{1}\t{2}`\t{3}\t{4}", reader.GetInt32(0),
//                            reader.GetString(1),reader.GetString(2), reader.GetString(3), reader.GetString(4));



//                        reads.Add(new Words
//                        {
//                            English = reader.GetString(2),
//                            Deutsch = reader.GetString(3),
//                            Francais = reader.GetString(4)
//                        });

//                    }
//                }
//                else
//                {
//                    Console.WriteLine("\n---No rows found.---");
//                }

//                reader.Close();

//                return false;
//            }
//        }
//        public void AddWords(Words word)
//        {

//            try
//            {
//                // Ouverture de la connexion SQL
//                if (countcon == false)
//                {
//                    Console.WriteLine("\n---Connection à la base de données---");
//                    countcon = true;
//                }

//                this.connection.Open();

//                // Création d'une commande SQL en fonction de l'objet connection
//                MySql.Data.MySqlClient.MySqlCommand cmd = this.connection.CreateCommand();

//                // Requête SQL
//                cmd.CommandText = "INSERT INTO Vocabulaire (English, Deutsch, Francais ) VALUES (@en, @de, @fr)";

//                // utilisation de l'objet contact passé en paramètre
//                //cmd.Parameters.AddWithValue("@type", type.type);
//                cmd.Parameters.AddWithValue("@en", word.English);
//                cmd.Parameters.AddWithValue("@de", word.Deutsch);
//                cmd.Parameters.AddWithValue("@fr", word.Francais);

//                // Exécution de la commande SQL
//                cmd.ExecuteNonQuery();

//                // Fermeture de la connexion
//                if(countend == false)
//                {
//                    Console.WriteLine("\n---Mots Ajoutées à la base---");
//                    countend = true;
//                }
//                this.connection.Close();
//            }            
//            catch(Exception ex)
//            {
//                // Gestion des erreurs :
//                // Possibilité de créer un Logger pour les exceptions SQL reçus
//                // Possibilité de créer une méthode avec un booléan en retour pour savoir si le contact à été ajouté correctement.
//                Console.WriteLine("\n---Base non connecté !---" + ex.Message);


//            }          
//        }
//        public void AddType(Type type,int count)
//        {
//            try
//            {
//                this.connection.Open();

//                // Création d'une commande SQL en fonction de l'objet connection
//                MySql.Data.MySqlClient.MySqlCommand cmd = this.connection.CreateCommand();


//                // Requête SQL
//                cmd.CommandText = "UPDATE Vocabulaire SET Type=@type WHERE ID IN(@count);";

//                // utilisation de l'objet contact passé en paramètre
//                cmd.Parameters.AddWithValue("@type", type.type);
//                cmd.Parameters.AddWithValue("@count", count);

//                // Exécution de la commande SQL
//                cmd.ExecuteNonQuery();

//                // Fermeture de la connexion
//                this.connection.Close();
//            }
//            catch(Exception ex)
//            {
//                Console.WriteLine("\n---Base non connecté !---" + ex.Message);
//            }
//        }
//        public void AddPhrase(Phrases Phrase, [Optional] Words Word, [Optional] Words Word2, [Optional] Words Word3)
//        {
//            try
//            {
//                // Ouverture de la connexion SQL
//                if (countcon == false)
//                {
//                    Console.WriteLine("\n---Connection à la base de données---");
//                    countcon = true;
//                }

//                this.connection.Open();

//                // Création d'une commande SQL en fonction de l'objet connection
//                MySql.Data.MySqlClient.MySqlCommand cmd = this.connection.CreateCommand();

//                // Requête SQL
//                cmd.CommandText = "INSERT INTO Phrases (Phrase,Word1,Word2,Word3 ) VALUES (@Phrase,@Word,@Word2,@Word3)";

//                // utilisation de l'objet contact passé en paramètre
//                //cmd.Parameters.AddWithValue("@type", type.type);
//                cmd.Parameters.AddWithValue("@Phrase", Phrase);

//                //Si word est défini dans les parametres
//                if (!Word.Equals(null))
//                {
//                    cmd.Parameters.AddWithValue("@Word", Word);
//                }
//                else
//                {
//                    Console.WriteLine("Word is null");
//                }

//                // Exécution de la commande SQL
//                cmd.ExecuteNonQuery();

//                // Fermeture de la connexion
//                if (countend == false)
//                {
//                    Console.WriteLine("\n---Mots Ajoutées à la base---");
//                    countend = true;
//                }
//                this.connection.Close();
//            }
//            catch (Exception ex)
//            {
//                // Gestion des erreurs :
//                // Possibilité de créer un Logger pour les exceptions SQL reçus
//                // Possibilité de créer une méthode avec un booléan en retour pour savoir si le contact à été ajouté correctement.
//                Console.WriteLine("\n---Base non connecté !---" + ex.Message);


//            }
//        }
//        public void DeleteRows()
//        {
//            try
//            {

//                // Ouverture de la connexion SQL

//                if (countcon == false)
//                {
//                    Console.WriteLine("\n---Connection à la base de données---");
//                    countcon = true;
//                }
//                this.connection.Open();

//                // Création d'une commande SQL en fonction de l'objet connection
//                MySql.Data.MySqlClient.MySqlCommand cmd = this.connection.CreateCommand();

//                // Requête SQL
//                cmd.CommandText = "DELETE ALL ROWS FROM Vocabulaire;";



//                // Exécution de la commande SQL
//                cmd.ExecuteNonQuery();

//                // Fermeture de la connexion
//                if (countend == false)
//                {
//                    Console.WriteLine("\n---Suppression---");
//                    countend = true;
//                }
//                this.connection.Close();
//            }
//            catch (Exception ex)
//            {
//                // Gestion des erreurs :
//                // Possibilité de créer un Logger pour les exceptions SQL reçus
//                // Possibilité de créer une méthode avec un booléan en retour pour savoir si le contact à été ajouté correctement.
//                Console.WriteLine("\n---Base non connecté !---" + ex.Message);


//            }
//        }
//    }

//}
