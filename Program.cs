using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            Console.Write(HolderName+"  Your current account balance is: ");
            Console.Write(Balance);
        }
        public void Withdraw()
        {
            Console.WriteLine("Enter Amount to withdraw: ");
            int Amount=Convert.ToInt32(Console.ReadLine());
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

        static void Main(string[] args)
        {

            Bank b1 = new Bank("name",23323423,"savings");
            char agn;

            do{

                Console.WriteLine("Please Select Any Function!");
                Console.WriteLine("\nPress 1 for Deposit an Amount. \nPress 2 for Withdraw an Amount. " +
                    "\nPress 3 for Display account information.") ;
                int num = Convert.ToInt32(Console.ReadLine());
                switch (num){
                case 1:
                  b1.Deposite();
                  break;

                 case 2:
                        b1.Withdraw();
                        break;
                 case 3:
                        b1.Display();
                        break;
                default:
                 Console.WriteLine("Invalid Selection!!!");
                 break;

                }

                Console.WriteLine("\nDo you want to continue this program ? (y / n)");
                agn = Convert.ToChar(Console.ReadLine());

            } while (agn == 'y');

            Console.ReadLine();

        }
    }

}
