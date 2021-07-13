using Contacts.DAL.Entities;
using Contacts.DAL.Services;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;


namespace ConsoleContacts
{
    class Program
    {

        static void Main(string[] args)
        {
            bool showMenu = true;
            while (showMenu)
            {
                showMenu = MainMenu();
            }
        }
        private static bool MainMenu()
        {
            string ConnectionString = @"Data Source=DESKTOP-L26RC4N\TB2019;Initial Catalog=Contacts;User ID=sa;Password=Test1234=;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            using (SqlConnection oConn = new SqlConnection(ConnectionString))
            {

                ContactService service = new ContactService(oConn);

               

                Console.Clear();
                Console.WriteLine("Bienvenue dans l'assistant répertoire!");
                Console.WriteLine("Ici vous pourrez : ");
                Console.WriteLine("1)-Ajouter un contact");
                Console.WriteLine("2)-Modifier un contact");
                Console.WriteLine("3)-Supprimer un contact");
                Console.WriteLine("4)-Afficher le répertoire");
                Console.Write("\r\nChoisissez une option : ");

                switch (Console.ReadLine())
                {
                    case "1":
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("AJOUT!");
                        Console.ForegroundColor = ConsoleColor.White;
                        Contact a = new Contact();
                        Console.Write("Nom : ");
                        a.Nom = Console.ReadLine();
                        Console.Write("Prénom : ");
                        a.Prenom = Console.ReadLine();
                        Console.Write("Tel : ");
                        a.Tel = Convert.ToInt32(Console.ReadLine());
                        Console.Write("E-Mail : ");
                        a.Email = Console.ReadLine();
                        service.AddContact(a);
                        return true;
                    case "2":
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("MODIFICATION!");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("Donnez nous l'ID à modifier : ");
                        int ID = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Entrez  le nom à modifier : ");
                        string Nom = Console.ReadLine();
                        Console.Write("Entrez le prénom à modifier : ");
                        string Prenom = Console.ReadLine();
                        Console.Write("Entrez le téléphone à modifier : ");
                        int Tel = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Entrez l'e-mail à modifier : ");
                        string Email = Console.ReadLine();
                        service.Update(new Contact { Id = ID, Nom = Nom, Prenom = Prenom, Tel = Tel, Email = Email });
                        return true;
                    case "3":
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("SUPPRESSION!");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("Donnez nous l'ID à supprimer : ");
                        int ID1 = Convert.ToInt32(Console.ReadLine());
                        service.Delete(ID);
                        return true;
                    case "4":
                        Console.Clear();
                        List<Contact> result = service.GetContact();
                        foreach (Contact contact in result)
                        {
                            Console.Write($"ID : {contact.Id} |");
                            Console.Write($"Nom : {contact.Nom} |");
                            Console.Write($"Prénom : {contact.Prenom} |");
                            Console.Write($"Tel : {contact.Tel} |");
                            Console.WriteLine($"E-Mail : {contact.Email} |");
                            Console.ReadLine();
                        }
                        return true;
                    default:
                        Console.Clear();
                        Console.WriteLine("À Bientôt!");
                        return false;
                }
            }
        }
    }

}
