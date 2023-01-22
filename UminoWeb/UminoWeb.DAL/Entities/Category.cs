using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UminoWeb.DAL.Base;

namespace UminoWeb.DAL.Entities
{
    public class Category : TimeStample, IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageName { get; set; }
        public bool IsDeleted { get; set; }
    }
}
