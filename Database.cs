using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;

namespace PerspectifyTut
{
    class Database
    {
        public SQLiteConnection myConnection;

        public Database()
        {
            myConnection = new SQLiteConnection("Data Source=Bankdb.sqlite3");
            if (!File.Exists("./Bankdb.sqlite3"))
            {
                SQLiteConnection.CreateFile("Bankdb.sqlite3");
                Console.WriteLine("file has been created!");
            }
            else
            {
                Console.WriteLine("file is already there!");
            }

        }
        public void OpenConnection()
        {
            if (myConnection.State != System.Data.ConnectionState.Open)
            {
                myConnection.Open();
            }
        }
        public void CloseConnetion()
        {
            if (myConnection.State != System.Data.ConnectionState.Closed)
            {
                myConnection.Close();
            }
        }

    }
}
