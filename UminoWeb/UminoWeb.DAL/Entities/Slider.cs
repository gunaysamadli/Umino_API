using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UminoWeb.DAL.Base;

namespace UminoWeb.DAL.Entities
{
    public class Slider : TimeStample, IEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string ImageName { get; set; }
        public string ButtonName { get; set; }
        public string ButtonLink { get; set; }
        public bool IsDeleted { get; set; }
    }
}
