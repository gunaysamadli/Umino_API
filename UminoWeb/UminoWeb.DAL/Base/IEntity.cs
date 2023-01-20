using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UminoWeb.DAL.Base
{
    public interface IEntity
    {
        int Id { get; set; }
        public bool IsDeleted { get; set; }
    }
}
