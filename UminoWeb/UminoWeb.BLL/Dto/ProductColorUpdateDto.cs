using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UminoWeb.BLL.Dto
{
    public class ProductColorUpdateDto
    {
        [Required]
        public int Id { get; set; }
        public int? Quantity { get; set; }
        public int? ProductId { get; set; }
        public int? ColorId { get; set; }
        public bool? IsDeleted { get; set; }

    }
}
