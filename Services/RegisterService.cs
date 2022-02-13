using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace OnlineRentalSystemAPI.Data
{
    public class RegisterService
    {

        private readonly IMongoCollection<RegisterUser> _users;

        public RegisterService(IOptions<DatabaseSettings> options)
        {
            var mongoClient = new MongoClient(options.Value.ConnectionString);

            _users = mongoClient.GetDatabase(options.Value.DatabaseName)
                .GetCollection<RegisterUser>("Accounts");
        }

        public async Task<List<RegisterUser>> Get() =>
            await _users.Find(_ => true).ToListAsync();

        public async Task Create(RegisterUser newUser) =>
            await _users.InsertOneAsync(newUser);

        public async Task<string> Login(RegisterUser user)  {
           RegisterUser dbuser = await _users.Find(m => m.Username == user.Username).FirstOrDefaultAsync();

            if (dbuser == null) {
                return "User Doesn't Exist";
            }

            if(dbuser.password == user.password)
            {
                return "Successfully Authenticated";

            } else
            {
                return "Credentials mismatch";
            }

           return "ERROR";
        }

    }
}
