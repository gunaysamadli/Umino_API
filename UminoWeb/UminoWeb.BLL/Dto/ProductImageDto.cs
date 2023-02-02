using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UminoWeb.BLL.Dto
{
    public class ProductImageDto
    {
        public int Id { get; set; }
        public string ImageName { get; set; }
        public bool IsDeleted { get; set; }
    }
}
