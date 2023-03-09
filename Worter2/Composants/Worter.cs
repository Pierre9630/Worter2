using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace Worter2
{
    class Worter
    {
        private string Message;

        public string Publier(string Message)
        {
            
            //System.Security.SecureString bdd = Getpasswd.GetPassword();
            //Database db2 = new Database();
            //db2.CloseConnexion();
            //System.Threading.Thread.Sleep(2000);
            //db2.InitConnexion(bdd);
            BinaryFormatter binf = new BinaryFormatter();
            MemoryStream s = new MemoryStream();
            binf.Serialize(s,Message);
            StreamReader sr = new StreamReader(s);
            //Console.WriteLine(s.BeginRead());
            string test = sr.ReadToEnd();
            Console.WriteLine(test);
            return test;
            //return sr.ReadToEnd();
            //return BitConverter.ToString(sr.);
            //db2.AddMessage(s.ToString());
        }
    }
}
