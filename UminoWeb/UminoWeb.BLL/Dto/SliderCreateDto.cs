using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UminoWeb.BLL.Dto
{
    public class SliderCreateDto
    {
        public string Title { get; set; }
        public string Subtitle { get; set; }

        [NotMapped]
        public IFormFile Image { get; set; }

        public string ImageName = string.Empty;
        public string ButtonName { get; set; }
        public string ButtonLink { get; set; }

        public bool IsDeleted = false;

    }
}
