using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UminoWeb.BLL.Dto
{
    public class ProductUpdateDto
    {
        [Required]
        public int Id { get; set; }
        public string? Name { get; set; }
        public bool? Availability { get; set; }
        public string? Description { get; set; }
        public double? Price { get; set; }
        public double? DisCount { get; set; }
        public double? Rate { get; set; }
        public bool? IsDeleted { get; set; }
        public int? BrandId { get; set; }
        public int? CategoryId { get; set; }
    }
}
