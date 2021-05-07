using DevTeams_Challenge_Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevTeams_Challenge_Console
{
    public class ProgramUI
    {
        //This class will be how we interact with our user through the console. When we need to access our data, we will call methods from our Repo class.

        private DevTeamsRepo _repo = new DevTeamsRepo();

        public void Run()
        {
            SeedContent();
            while (Menu())
            {
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                Console.Clear();
            }
        }

        private bool Menu()
        {
            //Start with the main menu here
            Console.WriteLine("What would you like to do?\n" +
                "1. Create a new Developer\n" +
                "2. View all Developers\n" +
                "3. View Developer Teams\n" +
                "4. View Developer By ID\n" +
                "5. Update a Developer\n" +
                "6. Delete a Developer\n" +
                "0. Exit");

            string input = Console.ReadLine().ToLower();
            if (input == "1" || input == "one" || input == "banana")
            {
                CreateNewDeveloper();
            }
            else if (input == "2" || input == "two")
            {
                DisplayAllDevelopers();
            }
            else if (input == "3" || input == "three")
            {
                DisplayDevelopersByTeam();
            }
            else if (input == "4" || input == "four")
            {
                DisplayDeveloperById();
            }
            else if (input == "5" || input == "five")
            {
                UpdateExistingDeveloper();
            }
            else if (input == "6" || input == "six")
            {
                DeleteExistingDeveloper();
            }
            else if (input == "0" || input == "exit")
            {
                return false;
            }
            else
            {
                Console.WriteLine("Please select a valid option");
            }
            return true;
        }

        //Create a new developer and add it to the directory - I use a helper method to create my developer object so I don't have to re-use so much code in the Update method. I can just call the method twice.
        private void CreateNewDeveloper()
        {
            Console.Clear();
            Developer newDeveloper = GetValuesForDeveloperObject();

            _repo.AddDeveloperToDirectory(newDeveloper);
        }

        //Display all developers that are in the directory
        private void DisplayAllDevelopers()
        {
            foreach (Developer dev in _repo.GetAllDevelopers())
            {
                DisplayBasicDevInfo(dev);
            }
        }

        //The user gives you a team assignment and you display all the developers on that team.
        private void DisplayDevelopersByTeam()
        {
            Console.WriteLine("Which team would you like to see Developers for?\n" +
                "1. Front End\n" +
                "2. Back End\n" +
                "3. Testing");
            string teamString = Console.ReadLine();
            foreach (Developer dev in _repo.GetAllDevelopers())
            {
                if (dev.TeamAssignment == (TeamAssignment)int.Parse(teamString))
                {
                    DisplayBasicDevInfo(dev);
                }
            }
        }

        //The user gives you an ID for the developer they want to see and you show them all of that developer's info
        private void DisplayDeveloperById()
        {
            Console.Clear();
            DisplayAllDevelopers();
            Console.WriteLine("Enter the ID of the Developer you would like to see?");
            //take the user input and parse it into an int so you can use that to get the Developer Object from the directory using our GetDeveloperById method
            Developer devToDisplay = _repo.GetDeveloperById(int.Parse(Console.ReadLine()));
            if (devToDisplay != null)
            {
                //calling our helper method from down below
                DisplayDeveloperDetails(devToDisplay);
            }
            else
            {
                Console.WriteLine("There is no developer with that ID");
            }
        }

        private void UpdateExistingDeveloper()
        {
            Console.Clear();
            DisplayAllDevelopers(); // show the user all the devs so they know their options when selecting a dev to show info for

            Console.WriteLine("What is the Id of the developer you would like to update?");

            //Using a TryParse to attempt to parse that string, if it parses successfully(the user actually entered a number) then output an int called result after the string is parsed and set devWasFound equal to true. If the user types letters or something else that can't be parsed, devWasFound is equal to false and no int is output.
            bool devWasFound = Int32.TryParse(Console.ReadLine(), out int result);

            //if the parse is successful
            if (devWasFound)
            {
                //use the int that was output by the TryParse to look for the Developer with that ID
                Developer devToUpdate = _repo.GetDeveloperById(result);

                //If we find a Dev with that ID
                if (devToUpdate != null)
                {
                    //use the same helper method we used in CreateNewDeveloper to get a Developer object to hold the values we want to update to.
                    Developer valuesForUpdatedDeveloper = GetValuesForDeveloperObject();
                    _repo.UpdateDeveloper(result, valuesForUpdatedDeveloper);
                }
                else
                {
                    Console.WriteLine("There is no developer with that ID");
                }
            }
        }

        private void DeleteExistingDeveloper()
        {
            Console.Clear();
            DisplayAllDevelopers();
            Console.WriteLine("Enter the Id of the Developer you would like to delete.");

            if (_repo.DeleteDeveloper(int.Parse(Console.ReadLine())))
            {
                Console.WriteLine("The developer was successfully deleted");
            }
            else
            {
                Console.WriteLine("The developer could not be deleted.");
            }
        }

        //Helper method for Creating a new Developer object. I can use this method in my CreateNewDeveloper method and then save that object to the directory or I can use this method to collect new values for my update method.
        private Developer GetValuesForDeveloperObject()
        {
            Console.Clear();
            Console.WriteLine("Enter the developer's first name.");
            string firstName = Console.ReadLine();

            Console.WriteLine("Enter the developer's last name.");
            string lastName = Console.ReadLine();

            bool hasPluralSight;
            while (true)
            {
                Console.WriteLine("Does this developer have access to PluralSight?");
                string response = Console.ReadLine().ToLower();
                if (response == "no")
                {
                    hasPluralSight = false;
                    break;
                }
                else if (response == "yes")
                {
                    hasPluralSight = true;
                    break;
                }
                else
                {
                    Console.WriteLine("Please enter yes or no.");
                }
            }

            Console.WriteLine("Which team does this developer work on?\n\n" +
                "1. Front End\n" +
                "2. Back End\n" +
                "3. Testing");
            TeamAssignment teamAssignment = (TeamAssignment)int.Parse(Console.ReadLine());
            return new Developer(firstName, lastName, hasPluralSight, teamAssignment);
        }

        //Helper method for when I only need to display a couple properties for a dev
        //I make this it's own method so I don't have to write this code multiple times - I can write it once here and call the method every time I need it.
        private void DisplayBasicDevInfo(Developer dev)
        {
            Console.WriteLine($"\tID: {dev.ID}\n" +
                $"\tName: {dev.FirstName} {dev.LastName}\n");
        }

        //Helper method for when I need to display all details for a dev
        private void DisplayDeveloperDetails(Developer dev)
        {
            string pluralSightMessage = dev.HasPluralSight ? "has" : "does not have";

            Console.WriteLine($"\tID: {dev.ID}\n" +
                $"\tName: {dev.FirstName} {dev.LastName}\n" +
                $"\tThis developer {pluralSightMessage} PluralSight access\n" +
                $"\tTeam: {dev.TeamAssignment}" +
                $"\n");
        }

        //SeedContent
        //Add some developers to our directory before we even load the menu so we don't have to create new objects to test things like update and delete every time we run the app.
        private void SeedContent()
        {
            _repo.AddDeveloperToDirectory(new Developer("Michael", "Pabody", true, TeamAssignment.BackEnd));
            _repo.AddDeveloperToDirectory(new Developer("Jacob", "Brown", false, TeamAssignment.FrontEnd));
            _repo.AddDeveloperToDirectory(new Developer("Nick", "Davies", false, TeamAssignment.Testing));
            _repo.AddDeveloperToDirectory(new Developer("Adam", "Metcalf", true, TeamAssignment.Testing));
            _repo.AddDeveloperToDirectory(new Developer("Justin", "Scroggins", false, TeamAssignment.BackEnd));
            _repo.AddDeveloperToDirectory(new Developer("Andrew", "Torr", true, TeamAssignment.FrontEnd));
        }
    }
}
