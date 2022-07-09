namespace Cart.API.Entities
{
    public class ShoppingBasketItem
    {

        public string IdCourse { get; set; }
        public string NameCourse { get; set; }
        public decimal Price { get; set; }

        public string ImageFile { get; set; }

        public string InstructorName { get; set; }

        public string Category { get; set; }

    }
}
