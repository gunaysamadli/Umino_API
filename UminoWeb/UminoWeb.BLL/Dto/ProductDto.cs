using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UminoWeb.BLL.Dto
{
    public class ProductDto
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public bool Availability { get; set; }
        public string Description { get; set; }

        [Required]
        public double Price { get; set; }
        public double DisCount { get; set; }
        public double Rate { get; set; }

        [Required]
        public bool IsDeleted { get; set; }

        [Required]
        public int BrandId { get; set; }
        public string BrandName { get; set; }

        [Required]
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

    }
}
