using MongoDB.Bson.Serialization.Attributes;

namespace OnlineRentalSystemAPI.Model
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string id { get; set; }

        public string title { get; set; } = string.Empty;

        public string subtitle { get; set; } = string.Empty;


        public string description { get; set; } = string.Empty;

        public string owner { get; set; } = string.Empty;

    }
        
    
}
