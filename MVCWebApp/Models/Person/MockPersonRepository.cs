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
        private static int idCounter = 0;
        private static List<Person> PersonList = new List<Person>();
        private readonly ApplicationDbContext _context;

        public PersonRepository(ApplicationDbContext context)
        {
            _context = context;

            #region old
            //Person p1 = new Person { Name = "John Doe", City = "New York", PhoneNumber = "324432234", ID = GetNewID() };
            //Person p2 = new Person { Name = "Jane Doe", City = "Las Vegas", PhoneNumber = "3453455433", ID= GetNewID() };

            //PersonList.Add(p1);
            //PersonList.Add(p2);
            #endregion
        }

        public List<Person> GetAllPersons()
        {
            //old
            //return PersonList;

            return _context.People.ToList();
        }

        public Person GetPerson(int id)
        {
            //old
            //return PersonList.SingleOrDefault(c => c.ID == id);

            return _context.People.Find(id);
        }

        public List<Person> Search(string searchTerm, bool caseSensitive)
        {
            List<Person> searchList = new List<Person>();

            #region old
            //if (searchTerm != null)
            //{
            //    foreach (Person item in PersonList)
            //    {
            //        if(caseSensitive)
            //        {
            //            if (item.Name.Contains(searchTerm) || item.City.Contains(searchTerm))
            //            {
            //                searchList.Add(item);
            //            }
            //        }
            //        else
            //        {
            //            if (item.Name.ToLowerInvariant().Contains(searchTerm.ToLowerInvariant()) ||
            //                item.City.ToLowerInvariant().Contains(searchTerm.ToLowerInvariant()))
            //            {
            //                searchList.Add(item);
            //            }
            //        }

            //    }
            //}
            #endregion

            //new
            if (searchTerm != null)
            {
                if(caseSensitive)
                {
                    //searchList = _context.People.Where(p => p.City.Contains(searchTerm) || 
                    //                                p.Name.Contains(searchTerm)).ToList();

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
            //List<Person> sortedList = PersonList.OrderBy(p => p.ID).ToList();
            List<Person> sortedList = _context.People.ToList();

            if (sortType == "city")
            {
                //old
                //sortedList = PersonList.OrderBy(p => p.City).ToList();

                sortedList = _context.People.OrderBy(p => p.City).ToList();
            }
            else if(sortType == "name")
            {
                //old
                //sortedList = PersonList.OrderBy(p => p.Name).ToList();

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
            //person.ID = GetNewID();

            //PersonList.Add(person);

            _context.People.Add(person);
            _context.SaveChanges();

            return person;
        }

        public bool Delete(int id)
        {
            #region old
            //if (id > 0)
            //{
            //    Person personToRemove = null;

            //    foreach (Person item in PersonList)
            //    {
            //        if (item.ID == id)
            //        {
            //            personToRemove = item;
            //        }
            //    }

            //    if (personToRemove != null)
            //    {
            //        PersonList.Remove(personToRemove);
            //        return true;
            //    }
            //}
            #endregion

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

        private int GetNewID()
        {
            idCounter++;
            return idCounter;
        }
    }
}