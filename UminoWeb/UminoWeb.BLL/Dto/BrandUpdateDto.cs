using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UminoWeb.BLL.Dto
{
    public class BrandUpdateDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        [NotMapped]
        public IFormFile? Image { get; set; }

        public string? ImageName = string.Empty;
        public bool? IsDeleted { get; set; }
    }
}
