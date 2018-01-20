
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net.Mail;

namespace SkickaMail
{
    class TaskMaker
    {
        private MailSend Sender = new MailSend();
        private string messagePath = string.Empty;
        private string MailsPath = string.Empty;
        private bool Connectect = false;
        private enum ProgramStatus { Open, Close };
        private ProgramStatus status = ProgramStatus.Open;
        

        public TaskMaker()
        {
            GetUserMailInfo();
        }
        private void Logged()
        {
            while (Connectect) {
                try
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Welcome to Outlook Spam Email Sender");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("1.Send More Mails From Txt File");
                    Console.WriteLine("2. Logout");
                    Console.WriteLine("3.Close Program");
                    int userInput = int.Parse(Console.ReadLine());




                    switch (userInput)
                    {
                        case 1:
                            GetFiles();
                            GetAttachments();
                            SedMailTask();
                            break;
                        case 2:
                            Connectect = false;
                            break;
                        default:
                            status = ProgramStatus.Close;
                            break;

                    }
                }
                catch
                {
                    Console.WriteLine("Wrong input ! please Select only Numbers for the menu!");
                }
            }
        }
        private void CredentialsFromTextFile()
        {
            bool check = true;
            while (check)
            {
                Console.WriteLine("Inside The file should look somethin like this exampel(Username.outlook.com,Password)");
                Console.WriteLine("Dont forget that your file has to be file.txt");
                Console.WriteLine("Please insert your file path with you username and password");
             


                string path = Console.ReadLine();
            List<string> ReadLines = File.ReadAllLines(path).ToList();
            if (!CheckFile(path))
            {

                    foreach (string lines in ReadLines)
                    {
                        try
                        {
                            string[] entries = lines.Split(',');
                            Sender.EmailAcount = entries[0];
                            Sender.Password = entries[1];
                            if (CheckUserCredentials())
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("Loggin Succes!");
                                Connectect = true;
                                Console.WriteLine("Type Any Key..");
                                Console.ReadLine();
                                check = false;
                            }
                            else
                            {
                                Console.WriteLine("The username or password was incorrect, check your file!");
                            }
                        }
                        catch
                        {
                            Console.WriteLine("some is wrong with your file ");
                            Console.WriteLine("Try again! ");
                        }
                    }
                }


            
            }
        }
        private void GetUserMailInfo()
        {
            while ( status == ProgramStatus.Open)
            {
                if (Connectect == true)
                {
                    Logged();
                }
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Welcome to Outlook Spam Email Sender");
                Console.ForegroundColor = ConsoleColor.White; 
                Console.WriteLine("1. To Give Your Login credentials Manually");
                Console.WriteLine("2. To Get Your Login credentials From TextFile");
                try
                {
                    int userInput = int.Parse(Console.ReadLine());




                    switch (userInput)
                    {
                        case 1:
                            GetuserCredentials();
                            GetFiles();
                            GetAttachments();
                            SedMailTask();
                            break;
                        case 2:
                            CredentialsFromTextFile();
                            GetFiles();
                            GetAttachments();
                            SedMailTask();
                            break;
                        default:
                            status = ProgramStatus.Close;
                            break;

                    }
                }
                catch
                {
                    Console.WriteLine("Wrong input ! please Select only Numbers for the menu!");
                }
            }

            Console.WriteLine("Press any key.....");
            Console.ReadKey();
        }
        private void GetuserCredentials()
        {
            bool cheker = true;
            do
            {
               
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Type your OutLook Username");
                Sender.EmailAcount = Console.ReadLine();
                Console.WriteLine("Type your OutLook Password");
                Sender.Password = ReadPassword();
                Console.WriteLine("Reading..");
                if (CheckUserCredentials() == false)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Password or username was incorrect!");
                }
                else
                {
                    cheker = false;
                }

            }
            while (cheker);
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Loggin Succes!");
                Connectect = true;
                Console.WriteLine("Type Any Key..");
                Console.ReadLine();
               
            }
        }
        private void GetFiles()
        {
            while (CheckFile(messagePath) || CheckFile(messagePath))
            {
                Console.WriteLine("Type the Subject of your message");
                Console.ForegroundColor = ConsoleColor.Red;
                Sender.Subject = Console.ReadLine();
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Type path route of your Email list Exampel (C:/Documents/Email-list.txt)");
                Console.ForegroundColor = ConsoleColor.Yellow;
                MailsPath = Console.ReadLine();
                if (CheckFile(MailsPath))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(" the route of your Email list dosent exist! Exampel (C:/Documents/Email-list.txt)");
                }
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Type path route of your Mesage file Exampel (C:/Documents/Message.txt) ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                messagePath = Console.ReadLine();
                if (CheckFile(MailsPath))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(" The route of your Mesage file  dosent exist! Exampel (C:/Documents/Message.txt)");
                }

            }
        }
        private void GetAttachments()
        {
            Console.ForegroundColor = ConsoleColor.White;
            bool t = true;
            while (t)
            {
                try
                {
                    Console.WriteLine("1.Add Attachment ");
                    Console.WriteLine("2.Quit");
                    int i = int.Parse(Console.ReadLine());
                    switch (i)
                    {
                        case 1:
                            string file = string.Empty;
                            while (CheckFile(file))
                            {
                                Console.WriteLine(" The route of your  file : Exampel (C:/Documents/file.txt)");
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                file = Console.ReadLine();
                                if (CheckFile(MailsPath))
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine(" The route of your  file  dosent exist! Exampel (C:/Documents/file.txt)");
                                }
                            }
                            Attachment S = new Attachment(file);
                            Sender.AddAttachment.Add(S);
                            break;
                        case 2:
                            t = false;

                            break;

                    }

                }
                catch
                {
                    Console.WriteLine("Your input was incorrect");
                }

            }

        }
        private static bool CheckFile(string path)
        {
            if (File.Exists(path))
            {

                return false;
            }
            else
            {


                return true;
            }

        }
        private bool CheckUserCredentials()
        {
           return Sender.TestConnection();
           
           

        }
        private void SedMailTask()
        {
            string userinput = string.Empty;
            Console.WriteLine("1.Start Sending ");
            Console.WriteLine("2.Cancel And Close");
            userinput = Console.ReadLine();
            if (userinput == "1")
            {
                try
                {
                    Console.WriteLine("Sending..");
                    Sender.SendMailFromFile(messagePath, MailsPath);
                    Console.WriteLine("Send Succes!");
                    
                }
                catch 
                {
                   
                }

            }
            else
            {
                Console.WriteLine("Press any key...");
            }
            Console.WriteLine("Press any key...");
            Console.ReadLine();
            Console.Clear();
            messagePath = string.Empty;
            MailsPath = string.Empty;
        }
        private static string ReadPassword()
        {
            ConsoleKeyInfo key; string pass = "";
            do
            {
                key = Console.ReadKey(true);
                if (key.Key != ConsoleKey.Backspace) // Backspace Should Not Work
                {
                    pass += key.KeyChar;
                    Console.Write("*");
                }
            } while (key.Key != ConsoleKey.Enter); // Stops Receving Keys Once Enter is Pressed
            {
                Console.WriteLine("");

            }
            return pass;
        }

    }
    
}
