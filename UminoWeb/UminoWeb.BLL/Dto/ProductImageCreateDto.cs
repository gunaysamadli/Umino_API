using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UminoWeb.BLL.Dto
{
    public class ProductImageCreateDto
    {
        [NotMapped]
        public IFormFile Image { get; set; }

        public string ImageName = string.Empty;

        public bool IsDeleted = false;

        [Required]
        public int ProductColorId { get; set; }
    }
}
