using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UminoWeb.DAL.Base;

namespace UminoWeb.DAL.Entities
{
    public class Product : TimeStample, IEntity
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public bool Availability { get; set; }
        public string? Description { get; set; }

        [Required]
        public double Price { get; set; }
        public double? DisCount { get; set; }
        public double? Rate { get; set; }

        [Required]
        public bool IsDeleted { get; set; }

        [Required]
        public int BrandId { get; set; }
        public virtual Brand Brand { get; set; }

        [Required]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

        public virtual List<ProductColor> ProductColors { get; set; } = new List<ProductColor>();
    }
}
