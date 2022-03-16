using MVCWebApp.Data;
using MVCWebApp.Models.Person.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCWebApp.Models.Person
{
    public class PersonRepository : IPersonRepository
    {
        private readonly ApplicationDbContext _context;

        public PersonRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Person> GetAllPersons()
        {
            return _context.People.ToList();
        }

        public Person GetPerson(int id)
        {
            return _context.People.Find(id);
        }

        public List<Person> Search(string searchTerm, bool caseSensitive)
        {
            List<Person> searchList = new List<Person>();

            if (searchTerm != null)
            {
                if(caseSensitive)
                {
                    IEnumerable<Person> searchList2 = from Person in _context.People
                                                where Person.City.Contains(searchTerm)
                                                select Person;
                    searchList = searchList2.ToList();
                }
                else
                {
                    searchList = _context.People.Where(p => p.City.Contains(searchTerm) ||
                                                    p.Name.Contains(searchTerm)).ToList();
                }
            }

            return searchList;
        }

        public List<Person> Sort(SortOptionsViewModel sortOptions, string sortType)
        {
            //default by ID
            List<Person> sortedList = _context.People.ToList();

            if (sortType == "city")
            {
                sortedList = _context.People.OrderBy(p => p.City).ToList();
            }
            else if(sortType == "name")
            {
                sortedList = _context.People.OrderBy(p => p.Name).ToList();
            }

            if (sortOptions.ReverseAplhabeticalOrder == true)
            {
                sortedList.Reverse();
            }

            return sortedList;
        }

        public Person Add(CreatePersonViewModel createPersonViewModel)
        {
            Person person = new Person();
            person.Name = createPersonViewModel.Name;
            person.City = createPersonViewModel.City;
            person.PhoneNumber = createPersonViewModel.PhoneNumber;

            _context.People.Add(person);
            _context.SaveChanges();

            return person;
        }

        public bool Delete(int id)
        {
            if (id > 0)
            {
                var personToDelete = _context.People.Find(id);

                if (personToDelete != null)
                {
                    _context.People.Remove(personToDelete);
                    _context.SaveChanges();
                    return true;
                }
            }
            return false;
        }
    }
}