using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UminoWeb.BLL.Dto
{
    public class ColorCreateDto
    {
        [Required]
        public string ColorName { get; set; }

        [Required]
        public string ColorCode { get; set; }

        public bool IsDeleted = false;
    }
}
