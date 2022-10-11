using DataAccess;
using Entity;

PersonDataAccess dataAccess = new PersonDataAccess();

await dataAccess.CreatePerson(new Person
{
    FirstName = "Deniz",
    LastName = "Duman"
});

var persons =  await dataAccess.GetAllPersons();

await dataAccess.UpdatePerson(new Person
{
    Id = persons.First().Id,
    FirstName = "DENİZ",
    LastName = "DUMAN 28"
});

await dataAccess.DeletePerson(persons.Last());

foreach (var item in persons)
{
    Console.WriteLine(item.FirstName + " " + item.LastName);
}