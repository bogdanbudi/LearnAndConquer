namespace Shopping.Aggregator.Models
{
    public class ShoppingBasketItemExtendedModel
    {
        public string IdCourse { get; set; }
        public string NameCourse { get; set; }
        public decimal Price { get; set; }

        //Course (Tutorials) Additional Fields
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
