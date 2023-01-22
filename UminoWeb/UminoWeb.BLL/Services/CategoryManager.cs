using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UminoWeb.BLL.Data;
using UminoWeb.BLL.Dto;
using UminoWeb.BLL.Services.contracts;
using UminoWeb.DAL.DataContext;
using UminoWeb.DAL.Entities;
using UminoWeb.DAL.Repositories;

namespace UminoWeb.BLL.Services
{
    public class CategoryManager : EfCoreRepository<Category>, ICategoryService
    {
        private readonly AppDbContext _dbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IMapper _mapper;
        public CategoryManager(AppDbContext dbContext, IWebHostEnvironment webHostEnvironment, IMapper mapper) : base(dbContext)
        {
            _dbContext = dbContext;
            _webHostEnvironment = webHostEnvironment;
            _mapper = mapper;
        }

        public override async Task CompletelyDeleteAsync(int? id)
        {
            if (id is null) throw new Exception();

            var deletedEntity = await _dbContext.Categories.FindAsync(id);

            if (deletedEntity is null) throw new Exception();

            var path = Path.Combine(_webHostEnvironment.ContentRootPath, "Images", "Category", deletedEntity.ImageName);

            if (File.Exists(path))
                File.Delete(path);

            _dbContext.Remove(deletedEntity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateById(int? id, CategoryUpdateDto categoryUpdateDto)
        {
            if (id is null) throw new Exception();

            var existCategory = await _dbContext.Categories.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            if (existCategory is null) throw new Exception();

            if (categoryUpdateDto.Id != id) throw new Exception();

            if (categoryUpdateDto.Image is not null)
            {
                var forderPath = Path.Combine(_webHostEnvironment.ContentRootPath, "Images", "Category");
                var existImageName = Path.Combine(forderPath, existCategory.ImageName);

                if (File.Exists(existImageName))
                    File.Delete(existImageName);

                categoryUpdateDto.ImageName = await categoryUpdateDto.Image.GenerateFile(forderPath);

            }
            else categoryUpdateDto.ImageName = existCategory.ImageName;

            if (categoryUpdateDto.Name is null) categoryUpdateDto.Name = existCategory.Name;

            if (categoryUpdateDto.IsDeleted is null)
            {
                categoryUpdateDto.IsDeleted = existCategory.IsDeleted;
            }

            if (categoryUpdateDto.IsDeleted == true) throw new Exception();

            var category = _mapper.Map<Category>(categoryUpdateDto);

            _dbContext.Categories.Update(category);
            await _dbContext.SaveChangesAsync();
        }
    }
}
