using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inlamning_2_ra_kod
{
    /* CLASS: Person
    * PURPOSE: a person entry in the contact list
    */

    class Person
    {
        public string name, address, phone, email;

        public Person()
        {
            /* METHOD: Person
            * PURPOSE: adds a new Person to contact list
            * PARAMETERS: none
            * RETURN VALUE: none
            */


            Console.Write("  1. ange namn:    ");
            name = Console.ReadLine();
            Console.Write("  2. ange adress:  ");
            address = Console.ReadLine();
            Console.Write("  3. ange telefon: ");
            phone = Console.ReadLine();
            Console.Write("  4. ange email:   ");
            email = Console.ReadLine();
        }

        public Person(string N, string A, string P, string E)
        {
            name = N; address = A; phone = P; email = E;
        }

        public void Print()
        {
            /* METHOD: Print
            * PURPOSE: Prints out variables in Person
            * PARAMETERS: none
            * RETURN VALUE: none
            */

            Console.WriteLine("{0}, {1}, {2}, {3}", name, address, phone, email);
        }

        public void ChangeContactInfo(string toChange, string newChange)
        {
            /* METHOD: ChangeContactInfo
            * PURPOSE: changes one variable in Person
            * PARAMETERS: toChange - what variable in Person to change, newChange - new value to replace old variable
            * RETURN VALUE: none
            */

            switch (toChange) //switch to change specific viariable in Person
            {
                case "namn": name = newChange; break;
                case "adress": address = newChange; break;
                case "telefon": phone = newChange; break;
                case "email": email = newChange; break;
                default: break;
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            //DECLARATIONS:
            List<Person> contactList = new List<Person>();
            string command;

            LoadFileToList(contactList);

            Console.WriteLine("Hej och välkommen till adresslistan");
            Console.WriteLine("Skriv 'sluta' för att sluta!");

            //Command prompt
            do
            {
                Console.Write("> ");
                command = Console.ReadLine();
                if (command == "sluta")
                {
                    Console.WriteLine("Hej då!");
                }
                else if (command == "ny")
                {
                    Console.WriteLine("Lägger till ny person");
                    contactList.Add(new Person());
                }
                else if (command == "ta bort")
                {
                    DeleteContact(contactList);
                }
                else if (command == "visa")
                {
                    for (int i = 0; i < contactList.Count(); i++) //writes list of contacts
                    {
                        contactList[i].Print();
                    }
                }
                else if (command == "ändra")
                {
                    ChangeContactInfo(contactList);
                }
                else
                {
                    Console.WriteLine("Okänt kommando: {0}", command);
                }
            } while (command != "sluta");
        }

        #region Methods
        static void ChangeContactInfo(List<Person> contactList)
        {
            /* METHOD: ChangeContactInfo(static)
            * PURPOSE: changes one info in contact
            * PARAMETERS: contactlist - to change variable in Person
            * RETURN VALUE: none
            */

            Console.Write("Vem vill du ändra (ange namn): ");
            string nameOfContact = Console.ReadLine();
            int contactIndex = -1;

            for (int i = 0; i < contactList.Count(); i++)
            {
                if (contactList[i].name == nameOfContact) contactIndex = i;
            }
            if (contactIndex == -1)//if contact does not exist
            {
                Console.WriteLine("Tyvärr: {0} fanns inte i telefonlistan", nameOfContact);
            }
            else
            {
                Console.Write("Vad vill du ändra (namn, adress, telefon eller email): ");
                string infoToChange = Console.ReadLine();
                Console.Write("Vad vill du ändra {0} på {1} till: ", infoToChange, nameOfContact);
                string newInfo = Console.ReadLine();

                contactList[contactIndex].ChangeContactInfo(infoToChange, newInfo);
            }
        }

        static void DeleteContact(List<Person> contactList)
        {
            /* METHOD: DeleteContact(static)
            * PURPOSE: Deletes contact from list
            * PARAMETERS: contactlist - to delete from
            * RETURN VALUE: none
            */

            Console.Write("Vem vill du ta bort (ange namn): ");
            string deleteName = Console.ReadLine();
            int contactIndex = -1;
            for (int i = 0; i < contactList.Count(); i++)
            {
                if (contactList[i].name == deleteName) contactIndex = i;
            }
            if (contactIndex == -1)//if contact does not exist
            {
                Console.WriteLine("Tyvärr: {0} fanns inte i telefonlistan", deleteName);
            }
            else
            {
                contactList.RemoveAt(contactIndex);
            }
        }

        static void LoadFileToList(List<Person> contactList)
        {
            /* METHOD: LoadFileToList(static)
            * PURPOSE: loads file information into contact list
            * PARAMETERS: contactlList - to add contacts to
            * RETURN VALUE: none
            */

            Console.Write("Laddar adresslistan ... ");
            using (StreamReader fileStream = new StreamReader(@"C:\Users\joaki\Desktop\ok.txt"))
            {
                while (fileStream.Peek() >= 0) //checks if next line is empty
                {
                    string line = fileStream.ReadLine();

                    string[] word = line.Split('#');

                    Person P = new Person(word[0], word[1], word[2], word[3]);
                    contactList.Add(P);
                }
            }
            Console.WriteLine("klart!");
        }
        #endregion
    }
}
