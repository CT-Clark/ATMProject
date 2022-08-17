using System;
using ATMProjectLibrary;

namespace ATMProject
{
    class ATM
    {
        private static List<BankAccount> BankAccounts = new List<BankAccount>();
        private static int BankAccountSeed = 11111111;

        public static void Main(string[] args)
        {
            Console.Write("Welcome!\nPlease select from the options below:\n" +
                "1) Log in\n" +
                "2) Create an account\n" +
                "Your response: ");
            string response = "";
            response = Console.ReadLine();

            if (response.Equals("1"))
            {
                LogIn();
            }
            else if (response.Equals("2"))
            {
                CreateAccount();
            }
            else
            {
                Console.WriteLine("That was not a valid response. Exiting.");
            }

            Console.WriteLine("\nThank you for using our service, have a nice day!");
        }

        // Log in verification
        public static void LogIn()
        {
            BankAccount currentAccount;
            int tries = 0;
            bool foundAccount = false;

            while (tries < 3 && foundAccount == false)
            {
                Console.Write("\nPlease enter your card number: ");
                int cardNumber = Convert.ToInt32(Console.ReadLine());
                Console.Write("Please enter your pin: ");
                int pin = Convert.ToInt16(Console.ReadLine());

                foreach (BankAccount account in BankAccounts)
                {
                    if (cardNumber == account.CardNumber && account.VerifyPIN(pin))
                    {
                        foundAccount = true;
                        currentAccount = account;
                        account.LoggedIn = true;
                        Console.WriteLine($"Hello {account.Name}, you are now logged in!");
                        AccountActions(account);
                        account.LoggedIn = false;
                    }
                }

                if (!foundAccount)
                {
                    Console.WriteLine("The card number you entered does not exist or the PIN you entered was incorrect, please try again.");
                    tries++;
                }
            }

            if (!foundAccount)
            {
                Console.WriteLine("You have exceeded your number of tries, please try again later.");
            }
        }

        // Register a new account
        public static void CreateAccount()
        {
            Console.WriteLine("\nPlease enter your information to create a new account.");
            Console.Write("Name: ");
            string name = Console.ReadLine();
            Console.Write("PIN: ");
            int pin = Convert.ToInt16(Console.ReadLine());
            //int.TryParse(Console.ReadLine(), out int pin);

            BankAccounts.Add(new BankAccount(name, 1234, BankAccountSeed, 10000));
            Console.WriteLine($"Your card number is {BankAccountSeed}");
            BankAccountSeed++;
            Console.WriteLine($"Thank you {name} for creating an account!");

            string response = "";
            while(response != "3")
            {
                Console.Write("\nPlease select one of the following options:\n" +
                "1) Log in to an existing account\n" +
                "2) Create another account\n" +
                "3) Quit\n" +
                "Your response: ");

                response = Console.ReadLine();

                switch (response)
                {
                    case "1":
                        LogIn();
                        break;
                    case "2":
                        CreateAccount();
                        break;
                    default:
                        Console.WriteLine("That was an invalid response, please choose again.");
                        break;
                }
            }
            
        }

        // After the user logs in these are the actions they can take
        public static void AccountActions(BankAccount account)
        {
            string response = "";
            while (account.LoggedIn == true && response != "5")
            {
                Console.Write("\nPlease choose from the following options:\n" +
                    "1) Display current account balance\n" +
                    "2) Display the previous 5 transactions\n" +
                    "3) Make a deposit\n" +
                    "4) Make a withdrawal\n" +
                    "5) Quit\n" +
                    "Your response: ");
                response = Console.ReadLine();

                switch(response)
                {
                    // 1) Display current account balance
                    case "1":
                        Console.WriteLine($"The current account balance is: ${account.Balance}");
                        break;

                    // 2) Display the previous 5 transactions
                    case "2":
                        Console.WriteLine("The previous five transactions were:");
                        for (int i = account.TransactionList.Count - 1; (i > account.TransactionList.Count - 6) && i >= 0; i--)
                        {
                            Console.WriteLine(account.TransactionList[i]);
                        }
                        break;

                    // 3) Make a deposit
                    case "3":
                        Console.Write("Please enter the amount you would like to deposit: ");
                        decimal DepositAmount = Convert.ToDecimal(Console.ReadLine());
                        account.MakeADeposit(DepositAmount);
                        Console.WriteLine("Thank you for your deposit.\n" +
                           $"Your new balance is: ${account.Balance}");
                        break;

                    // 4) Make a withdrawal
                    case "4":
                        Console.Write("Please enter the amount you would like to withdraw: ");
                        decimal WithdrawAmount = Convert.ToDecimal(Console.ReadLine());
                        account.MakeAWithdrawal(WithdrawAmount);
                        Console.WriteLine("Thank you for your withdrawal.\n" +
                           $"Your new balance is: ${account.Balance}");
                        break;

                    // 5) Quit
                    case "5":
                        break;

                    default:
                        Console.WriteLine("That was not a valid response, please choose again.");
                        break;
                }
            }
        }
    }

}