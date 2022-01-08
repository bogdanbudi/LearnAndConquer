using MongoDB.Bson.Serialization.Attributes;

namespace Tutorial.API.Entities
{
    public class Course
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Name")]  //the name of the column
        public string Name { get; set; }

        [BsonElement("Price")]
        public decimal Price { get; set; }

        public string PrimaryTehnology { get; set; }

        public string Company { get; set; }
        public string InstructorName { get; set; }

        public string Summary { get; set; }

        public string Description { get; set; }

        public string Category { get; set; }

        public string ImageFile { get; set; }

        public string VideoFile { get; set; }
    }
}
