using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data;
using MySql.Web;
using MySql.Data.EntityFramework;
using MySql.Data.MySqlClient;

namespace Worter2
{
    class Database
    {
        private MySqlConnection connection;

        // Constructeur
        public Database()
        {
            this.InitConnexion();
        }

        // Méthode pour initialiser la connexion
        private void InitConnexion()
        {
                        
            string connectionString = "Server=192.168.1.34;Database=Langues;Uid=root;Pwd=test44;";
            this.connection = new MySqlConnection(connectionString);
        }
        int countcon = 0;
        int countend = 0;
        // Méthode pour ajouter un contact
        public void AddWords(Words word)
        {
            
            try
            {
                // Ouverture de la connexion SQL
                if (countcon == 0)
                {
                    Console.WriteLine("\n---Connection à la base de données---");
                    countcon += 1;
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
                if(countend == 0)
                {
                    Console.WriteLine("\n---Mots Ajoutées à la base---");
                    countend += 1;
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
        public void AddType(Type type,int count)
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
            catch
            {

            }
        }
    }
}
