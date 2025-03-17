using Talabat.Core.Entity;

namespace Talabat.DTO
{
    public class ProductReturnDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public string PictureUrl { get; set; }

        public int ProductBreandId { get; set; }

        public string ProductBreand { get; set; }

        public int ProductTypeId { get; set; }

        public string ProductTypes{ get; set; }
        }
}
