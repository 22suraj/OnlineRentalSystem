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

        public async Task<string> Create(RegisterUser newUser) {

            if(newUser == null)
            {
                return "Add Vaild Credentails";
            }

            if(newUser.FirstName == null || newUser.FirstName.Length == 0)
            {
                return "Add Vaild FirstName";
            }

            if (newUser.LastName == null || newUser.LastName.Length == 0)
            {
                return "Add Vaild LastName";
            }

            if (newUser.Username == null || newUser.Username.Length == 0)
            {
                return "Add Vaild UserName";
            }

            if (newUser.Type == null || newUser.Type.Length == 0)
            {
                return "Select Vaild Type";
            }

            if (newUser.email == null || newUser.email.Length == 0)
            {
                return "Add Vaild Email";
            }

            if (newUser.password == null || newUser.password.Length == 0)
            {
                return "Add Vaild Password";
            }
            await _users.InsertOneAsync(newUser);
            return "User Successfully Registered";
        }
            

        public async Task<string> Login(RegisterUser user)  {
           RegisterUser dbuser = await _users.Find(m => m.email == user.email).FirstOrDefaultAsync();

            if (dbuser == null) {
                return "User Doesn't Exist";
            }

           
            if (dbuser.password == user.password && dbuser.email == user.email)
            {
                return "Successfully Authenticated-" + dbuser.Type;

            } else
            {
                return "Credentials mismatch";
            }

           return "ERROR";
        }

    }
}
