using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UminoWeb.BLL.Dto
{
    public class ProductColorDto
    {
        public int Id { get; set; }
        public string SKU { get; }
        public int Quantity { get; set; }

        [Required]
        public int ProductId { get; set; }
        public string ProductName { get; set; }

        [Required]
        public int ColorId { get; set; }
        public string ColorName { get; set; }
        public bool IsDeleted { get; set; }
        public List<GeneralProductImageDto>? GeneralProductImages { get; set; } = new List<GeneralProductImageDto>();
    }
}
