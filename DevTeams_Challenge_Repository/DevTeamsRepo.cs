using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevTeams_Challenge_Repository
{
    public class DevTeamsRepo
    {
        //This is our Repository class that will hold our directory (which will act as our database) and methods that will directly talk to our directory.

        private List<Developer> _devDirectory = new List<Developer>();
        private int _idCounter = 1;

        public bool AddDeveloperToDirectory(Developer developer)
        {
            int initialCount = _devDirectory.Count;
            developer.ID = _idCounter;
            _devDirectory.Add(developer);
            _idCounter++;
            return _devDirectory.Count > initialCount ? true : false;
        }

        public List<Developer> GetAllDevelopers()
        {
            return _devDirectory;
        }

        public Developer GetDeveloperById(int id)
        {
            foreach(Developer dev in _devDirectory)
            {
                if(dev.ID == id)
                {
                    return dev;
                }
            }
            return null;
        }

        public bool UpdateDeveloper(int id, Developer newDeveloperValues)
        {
            Developer oldDeveloper = GetDeveloperById(id);

            // Update the content if it exists
            if (oldDeveloper != null)
            {
                oldDeveloper.FirstName = newDeveloperValues.FirstName;
                oldDeveloper.LastName = newDeveloperValues.LastName;
                oldDeveloper.HasPluralSight = newDeveloperValues.HasPluralSight;
                oldDeveloper.TeamAssignment = newDeveloperValues.TeamAssignment;

                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DeleteDeveloper(int developerIdToDelete)
        {
            return _devDirectory.Remove(GetDeveloperById(developerIdToDelete));
        }
    }
}
