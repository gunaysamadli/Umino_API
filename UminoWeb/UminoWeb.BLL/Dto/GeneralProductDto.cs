using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UminoWeb.BLL.Dto
{
    public class GeneralProductDto
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
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
        public int SubCategoryId { get; set; }
        public string SubCategoryName { get; set; }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public List<GeneralProductColorDto>? GeneralProductColors { get; set; } = new List<GeneralProductColorDto>();

    }
}
