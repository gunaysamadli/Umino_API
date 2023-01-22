using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UminoWeb.BLL.Dto;
using UminoWeb.DAL.Entities;
using UminoWeb.DAL.Repositories.Contracts;

namespace UminoWeb.BLL.Services.contracts
{
    public interface ICategoryService : IRepository<Category>
    {
        Task UpdateById(int? id, CategoryUpdateDto categoryUpdateDto);
    }
}
