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
    public class BrandManager : EfCoreRepository<Brand>, IBrandService
    {
        private readonly AppDbContext _dbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IMapper _mapper;
        public BrandManager(AppDbContext dbContext, IWebHostEnvironment webHostEnvironment, IMapper mapper) : base(dbContext)
        {
            _dbContext = dbContext;
            _webHostEnvironment = webHostEnvironment;
            _mapper = mapper;
        }

        public override async Task CompletelyDeleteAsync(int? id)
        {
            if (id is null) throw new Exception();

            var deletedEntity = await _dbContext.Brands.FindAsync(id);

            if (deletedEntity is null) throw new Exception();


            var path = Path.Combine(_webHostEnvironment.ContentRootPath, "Images", "Brand", deletedEntity.ImageName);

            if (File.Exists(path))
                File.Delete(path);

            _dbContext.Remove(deletedEntity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateById(int? id, BrandUpdateDto brandUpdateDto)
        {
            if (id is null) throw new Exception();

            var existBrand = await _dbContext.Brands.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            if (existBrand is null) throw new Exception();

            if (brandUpdateDto.Id != id) throw new Exception();

            if (brandUpdateDto.Image is not null)
            {
                var forderPath = Path.Combine(_webHostEnvironment.ContentRootPath, "Images", "Brand");
                var existImageName = Path.Combine(forderPath, existBrand.ImageName);

                if (File.Exists(existImageName))
                    File.Delete(existImageName);

                brandUpdateDto.ImageName = await brandUpdateDto.Image.GenerateFile(forderPath);

            }
            else brandUpdateDto.ImageName = existBrand.ImageName;

            if (brandUpdateDto.Name is null) brandUpdateDto.Name = existBrand.Name;

            if (brandUpdateDto.IsDeleted is null)
            {
                brandUpdateDto.IsDeleted = existBrand.IsDeleted;
            }
            if (brandUpdateDto.IsDeleted == true) throw new Exception();

            var brand = _mapper.Map<Brand>(brandUpdateDto);

            _dbContext.Brands.Update(brand);
            await _dbContext.SaveChangesAsync();
        }
    }
}
