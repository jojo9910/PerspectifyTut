using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace PerspectifyTut
{
    class Bank
    {
        public string HolderName;
        public long AccountNo;
        public string type;
        private double Balance;

        public Bank(string AHolderName, long AAccountNo, string AType)
        {
            HolderName = AHolderName;
            AccountNo = AAccountNo;
            type = AType;

        }

        public double CurrBalance
        {
            get { return Balance; }
            set
            {
                if (value >= 0)
                {
                    Balance = value;
                }
                else
                {
                    Console.WriteLine("Can not set the Balance to this amount!");
                }
            }
        }

        public void Display()
        {
            Console.Write(HolderName + "  Your current account balance is: ");
            Console.Write(Balance);
        }
        public void Withdraw()
        {
            Console.WriteLine("Enter Amount to withdraw: ");
            int Amount = Convert.ToInt32(Console.ReadLine());
            if (this.CurrBalance - Amount >= 0)
            {
                this.CurrBalance = this.CurrBalance - Amount;
                this.Display();
            }
            else
            {
                Console.WriteLine("Enter a Valid amount to withdraw!");
            }

        }
        public void Deposite()
        {
            Console.WriteLine("Enter Amount to Deposite: ");
            int Amount = Convert.ToInt32(Console.ReadLine());
            if (Amount > 0)
            {
                this.CurrBalance = this.CurrBalance + Amount;
                this.Display();
            }
            else
            {
                Console.WriteLine("Enter a valid amount to Deposite!!");
            }

        }

    }
    class Program
    {

        public void Deposite(Database Dataobject)
        {
            try
            {
                Console.WriteLine("Enter Account HolderName: ");
                string Name = Console.ReadLine();
                Console.WriteLine("Enter Account number: ");
                long AccountNo = Convert.ToInt64(Console.ReadLine());

                Console.WriteLine("Enter Amount to Deposite: ");
                int Amount = Convert.ToInt32(Console.ReadLine());
                if (Amount > 0)
                {
                    string query = "UPDATE Users SET Balance =Balance+@amount WHERE AccountNo = @AccountNo";
                    SQLiteCommand myCommand = new SQLiteCommand(query, Dataobject.myConnection);
                    myCommand.Parameters.AddWithValue("@AccountNo", AccountNo);
                    myCommand.Parameters.AddWithValue("@amount", Amount);
                    if (myCommand.ExecuteNonQuery() == 1)
                    {
                        string q1 = "SELECT * FROM Users WHERE AccountNo=@AccountNo";
                        SQLiteCommand find = new SQLiteCommand(q1, Dataobject.myConnection);
                        find.Parameters.AddWithValue("@AccountNo", AccountNo);
                        SQLiteDataReader result = find.ExecuteReader();
                        while (result.Read())
                        {
                            Console.WriteLine("User: {0} - AccountNo.:{1} - CurrentBalanceis:{2}", result["HolderName"], result["AccountNO"], result["Balance"]);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Some error!!");
                    }
                }
                else
                {
                    Console.WriteLine("Enter A Valid Amount!!");
                }
            }
            catch(Exception err)
            {
                Console.WriteLine(err.Message);
            }

        }

        public void Display(Database Dataobject)
        {
            try
            {
                Console.WriteLine("Enter Account HolderName: ");
                string Name = Console.ReadLine();
                Console.WriteLine("Enter Account number: ");
                long AccountNo = Convert.ToInt64(Console.ReadLine());
                string query = "SELECT * FROM Users WHERE AccountNo=@AccountNo";
                SQLiteCommand myCommand = new SQLiteCommand(query, Dataobject.myConnection);
                myCommand.Parameters.AddWithValue("@AccountNo", AccountNo);
                SQLiteDataReader result = myCommand.ExecuteReader();
                if (result.HasRows)
                {
                    while (result.Read())
                        Console.WriteLine("User: {0} - AccountNo.:{1} - CurrentBalanceis:{2}", result["HolderName"], result["AccountNo"], result["Balance"]);
                }
                else
                {
                    Console.WriteLine("Credentials wrong or user does not exits!");
                    return;
                }
            }
            catch(Exception err)
            {
                Console.WriteLine("Error!!");
                Console.WriteLine(err.Message);
            }
            
        }

        public void CreateNew(Database Dataobject) 
        {
            try
            {
                Console.WriteLine("Enter Account HolderName: ");
                string HolderName = Console.ReadLine();
                Console.WriteLine("Enter Account number: ");
                long AccountNo = Convert.ToInt64(Console.ReadLine());
                Console.WriteLine("Enter Account Type: ");
                string Type = Console.ReadLine();
                Console.WriteLine("Enter initial Amount: ");
                int Amount = Convert.ToInt32(Console.ReadLine());
                if (Amount < 0) Amount = 0;
                string Query = "INSERT INTO Users(`HolderName`,`AccountNo`,`Type`,`Balance`) VALUES(@HolderName,@AccountNo,@type,@Balance)";
                SQLiteCommand InsertCommand = new SQLiteCommand(Query, Dataobject.myConnection);
                InsertCommand.Parameters.AddWithValue("@HolderName", HolderName);
                InsertCommand.Parameters.AddWithValue("@AccountNo", AccountNo);
                InsertCommand.Parameters.AddWithValue("@type", Type);
                InsertCommand.Parameters.AddWithValue("@Balance", Amount);
                int res = InsertCommand.ExecuteNonQuery();
                Console.WriteLine(res);
            }
            catch(Exception err)
            {
                Console.WriteLine(err.Message);
            }
        }

        public void Withdraw(Database Dataobject)
        {
            try
            {
                int CurrBalance = 0;
                Console.WriteLine("Enter Account number: ");
                long AccountNo = Convert.ToInt64(Console.ReadLine());
                string query1 = "SELECT * FROM Users WHERE AccountNo=@AccountNo";
                SQLiteCommand myCommand1 = new SQLiteCommand(query1, Dataobject.myConnection);
                myCommand1.Parameters.AddWithValue("@AccountNo", AccountNo);
                SQLiteDataReader result1 = myCommand1.ExecuteReader();
                if (result1.HasRows)
                {
                    while (result1.Read())
                       CurrBalance= Convert.ToInt32(result1["Balance"]);
                }
                else
                {
                    Console.WriteLine("Credentials wrong or user does not exits!");
                    return;
                }

                Console.WriteLine("Enter Amount to Withdraw!: ");
                int Amount = Convert.ToInt32(Console.ReadLine());
                if (CurrBalance >= Amount)
                {
                    string query = "UPDATE Users SET Balance = Balance-@Amount WHERE AccountNo = @AccountNo";
                    SQLiteCommand myCommand = new SQLiteCommand(query, Dataobject.myConnection);
                    myCommand.Parameters.AddWithValue("@AccountNo", AccountNo);
                    myCommand.Parameters.AddWithValue("@amount", Amount);
                    if (myCommand.ExecuteNonQuery() == 1)
                        Console.WriteLine("Done!");
                    else
                        Console.WriteLine("Query not executed!!");
                }
                else
                {
                    Console.WriteLine("Not a valid amount!!");
                    return;
                }
                
            }
            catch(Exception err)
            {
                Console.WriteLine(err.Message);
            }

        }
        public void AllRowsInTable(Database Dataobject)
        {
            try { 
            string query = "SELECT * FROM Users";
            SQLiteCommand myCommand1 = new SQLiteCommand(query, Dataobject.myConnection);
            SQLiteDataReader result = myCommand1.ExecuteReader();
            if (result.HasRows)
            {
                while (result.Read())
                {
                    Console.WriteLine("User: {0} - AccountNo.:{1} - CurrentBalanceis:{2}", result["HolderName"], result["AccountNO"], result["Balance"]);
                }
            }
        }
            catch(Exception err)
            {
                Console.WriteLine(err.Message);
            }

}
        static void Main(string[] args)
        {
            Program p1 = new Program();
            Database Dataobject = new Database();
            Dataobject.OpenConnection();
            p1.AllRowsInTable(Dataobject);

            char agn;
            do
            {
               Console.WriteLine("Please Select Any Function!");
                Console.WriteLine("\nPress 1 for Deposit an Amount. \nPress 2 for Withdraw an Amount. " +
                    "\nPress 3 for Display account information.\nPress 4 for Creating newAccount.");
                int num = Convert.ToInt32(Console.ReadLine());
                
                switch (num)
                {
                    case 1:
                        p1.Deposite(Dataobject);
                        break;

                    case 2:
                        p1.Withdraw(Dataobject);
                        break;
                    case 3:
                        p1.Display(Dataobject);
                        break;
                    case 4:
                        p1.CreateNew(Dataobject);
                        break;
                    default:
                        Console.WriteLine("Invalid Selection!!!");
                        break;

                }

                Console.WriteLine("\nDo you want to continue this program ? (y / n)");
                agn = Convert.ToChar(Console.ReadLine());

            } while (agn == 'y');

            p1.AllRowsInTable(Dataobject);
            Dataobject.CloseConnetion();
            Console.ReadLine();

        }
    }



}
