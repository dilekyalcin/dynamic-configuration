using System.ComponentModel.DataAnnotations;

namespace A_ServiceAPI.Entity
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
