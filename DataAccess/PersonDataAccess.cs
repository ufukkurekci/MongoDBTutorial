using Entity;
using MongoDB.Driver;

namespace DataAccess
{
    public class PersonDataAccess
    {
        private const string connectionString = "mongodb://127.0.0.1:27017";
        private const string databaseName = "test";
        private const string collectionName = "person";

        private IMongoCollection<TDocument> ConnectToMongo<TDocument>(in string collection)
        {
            var client = new MongoClient(connectionString);
            var db = client.GetDatabase(databaseName);
            return db.GetCollection<TDocument>(collection);
        } 

        public async Task<List<Person>> GetAllPersons()
        {
            var personCollection = ConnectToMongo<Person>("person");
            var person = await personCollection.FindAsync(_ => true);
            return person.ToList();
        } 

        public async Task CreatePerson(Person person)
        {
            var personCollection = ConnectToMongo<Person>("person");
            await personCollection.InsertOneAsync(person);
        }

        public async Task UpdatePerson(Person person)
        {
            var personCollection = ConnectToMongo<Person>("person");
            var filter = Builders<Person>.Filter.Eq("Id", person.Id);
            await personCollection.ReplaceOneAsync(filter, person, new ReplaceOptions { IsUpsert = true });
        }

        public async Task DeletePerson(Person person)
        {
            var personCollection = ConnectToMongo<Person>("person");
            await personCollection.DeleteOneAsync(p => p.Id == person.Id);
        }
    }
}