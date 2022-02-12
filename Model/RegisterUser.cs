using MongoDB.Bson.Serialization.Attributes;

namespace OnlineRentalSystemAPI
{
    public class RegisterUser
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string id { get; set; }

        public string Username { get; set; } = string.Empty;

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string password { get; set; } = string.Empty;

        public string cpassword { get; set; } = string.Empty;



    }
}
