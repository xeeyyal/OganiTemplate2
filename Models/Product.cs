using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;

namespace FrontToBack_2.Models
{
    public class Product
    {
        public int Id { get; set; }
        [MaxLength(25, ErrorMessage = "Uzunlugu 25 xarakterden cox olmamalidir.")]
        public string Name { get; set; }
        //public IFormFile Photo { get; set; }
        public string ImagegUrl { get; set; }
        public decimal Price { get; set; }
        public DateTime CreateTime { get; set; }
        public Departament? Department { get; set; } //icindeki departmente kecmek ucun
        public int DepartmentId { get; set; } //one-to-many relations
    }
}
