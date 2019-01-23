using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Lab_PWSystm
{
    class Program
    {
        static void Main(string[] args)
        {
            bool validInput = false;
            bool notExit = true;

            User logInInfo = new User();
            List<User> userList = new List<User> { };
            User admin = new User("admin@admin.com", "Password1");
            userList.Add(admin);//added default user to always be able to log in as someone

            do
            {
                Console.WriteLine("Welcome to the ACME Login System");
                Console.WriteLine("Please press 1 for new user.");
                Console.WriteLine("Please press 2 for existing user.");
                var input = Console.ReadKey();
                Console.WriteLine("");

                do
                {
                    if (input.Key == ConsoleKey.D1)
                    {
                        validInput = true;
                        logInInfo = NewUserCreation(userList);
                    }
                    else if (input.Key == ConsoleKey.D2)
                    {
                        validInput = true;
                        logInInfo = ExistingUser(userList);
                    }
                    else
                    {
                        Console.WriteLine("That is not an option.");
                        Console.WriteLine("Please press 1 for new user or 2 for existing user.");
                        input = Console.ReadKey();
                        Console.WriteLine(" ");
                    }

                } while (!validInput);

                notExit = UserActions(logInInfo);
               
            } while (notExit);
            

            Console.WriteLine("Have a great day");
        }

        private static bool UserActions(User logInInfo)
        {
            //User has logged into the system at this point. If us user loggs out, any new user
            //data is continued to be stored within the list and can be used to log in with. If
            //a user loggs out, all data is lost.
            Console.WriteLine(" ");
            Console.WriteLine("Welcome to the ACME System, you have successfully logged in.");
            Console.WriteLine("Log out Press 1, Exit Program press 2, and to change password press 3.");

            do
            {
                var input = Console.ReadKey();
                Console.WriteLine(" ");
                if (input.Key == ConsoleKey.D1)
                {
                    Console.WriteLine("Logging Out.");
                    return true;
                }
                else if (input.Key == ConsoleKey.D2)
                {
                    Console.WriteLine("Exiting Application.");
                    return false;
                }
                else if (input.Key == ConsoleKey.D3)
                {
                    logInInfo.Password = ValidPassword();
                    Console.WriteLine("Log out Press 1, Exit Program press 2, and to change password press 3.");
                }
                else
                {
                    Console.WriteLine("That is not an option.");
                    Console.WriteLine("Log out Press 1, Exit Program press 2, and to change password press 3.");
                }
            } while (true);

        }

        private static User NewUserCreation(List<User> userList)
        {
            string userName = ValidUserName();
            string password = ValidPassword();
            User newUser = new User(userName, password);
            userList.Add(newUser);
            return newUser;
        }

        private static User ExistingUser(List<User> userList)
        {
            bool correctPassword = false;
            User temp;
            do
            {
                string userName = ValidUserName();
                Console.WriteLine("Please type your password");
                string input = Console.ReadLine();

                for (int i = 0; i < userList.Count; i++)
                {
                    if(userName == userList[i].UserName)
                    {
                        correctPassword = userList[i].PassMatch(input);
                        temp = userList[i];
                        if (correctPassword)
                        { return temp; }
                    }
                }
                Console.WriteLine("Either your user name or password did not match.");
                Console.WriteLine("Please try again.");
            } while (true);


        }

        private static string ValidUserName()
        {
            do
            {
                Console.WriteLine("Please enter a valid email address.");
                string input = Console.ReadLine();
                if (Regex.IsMatch(input, @"[A-z0-9]{3,}(@)[A-z0-9]{3,}(.+)[A-z0-9]{2,3}"))
                {
                    return input;
                }
                Console.WriteLine("Your input wasn't a valid email address.");
            } while (true);
        }

        private static string ValidPassword()
        {
            string input;

            Console.WriteLine("Please enter a password.");
            Console.WriteLine("Your password must be at least 5 characters," +
                " it must contain at least one capital leter and at least one number.");
            
            do
            {
                //create three bool conditions to test password agains and only switch to true if met when looping 
                //through the character array of the password string. If these three aren't met, then return is never
                //triggered and continues to loop, until conditions are met.

                bool grtrFive = false; 
                bool ichiban = false;
                bool hasCapLetter = false;

                input = Console.ReadLine();
                char[] charList = input.ToCharArray();
                try
                {
                    if(charList.Length >= 5) { grtrFive = true; }

                    for (int i = 0; i < charList.Length; i++)
                    {
                        if (Char.IsUpper(charList[i])) { hasCapLetter = true; }
                        if (Char.IsNumber(charList[i])) { ichiban = true; }
                    }
                }
                catch (NullReferenceException)
                {
                    Console.WriteLine("Your input was empty, please type a valid. Null");
                }
                catch (IndexOutOfRangeException)
                {
                    Console.WriteLine("Your input was empty, please type a valid. ArrayOutOfBounds");
                }
                catch (Exception)
                {
                    Console.WriteLine("You dun broke it good to get here... Here's your cookie.");
                }

                if(grtrFive && ichiban && hasCapLetter)
                {
                    Console.WriteLine("Password Accepted.");
                    return input;
                }

                if (!grtrFive) { Console.WriteLine("Your Password was not at least five characters long."); }
                if (!ichiban) { Console.WriteLine("Your Password did not have at least one number."); }
                if (!hasCapLetter) { Console.WriteLine("Your Password did not have at least one capital letter."); }
                Console.WriteLine("Please Try again.");

            } while (true);

        }
    } 
}
