using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UminoWeb.BLL.Dto;
using UminoWeb.BLL.Services.contracts;
using UminoWeb.DAL.DataContext;
using UminoWeb.DAL.Entities;
using UminoWeb.DAL.Repositories;

namespace UminoWeb.BLL.Services
{
    public class ProductColorManager : EfCoreRepository<ProductColor>, IProductColorService
    {
        private readonly AppDbContext _dbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IMapper _mapper;
        public ProductColorManager(AppDbContext dbContext, IWebHostEnvironment webHostEnvironment, IMapper mapper) : base(dbContext)
        {
            _dbContext = dbContext;
            _webHostEnvironment = webHostEnvironment;
            _mapper = mapper;
        }


        public override async Task AddAsync(ProductColor entity)
        {
            Color color = await _dbContext.Colors.FindAsync(entity.ColorId);

            if (color is null) throw new Exception();

            Product product = await _dbContext.Products.FindAsync(entity.ProductId);

            if (product is null) throw new Exception();

            if (entity.IsDeleted == false)
            {
                if (product.IsDeleted == true || color.IsDeleted == true)
                    throw new Exception();
            }

            var sameColorProduct = await _dbContext.ProductColors
                                         .Where(x => x.ColorId == entity.ColorId && entity.ProductId == x.ProductId)
                                         .FirstOrDefaultAsync();

            if (sameColorProduct is not null) throw new Exception();

            await base.AddAsync(entity);
        }

        public override async Task CompletelyDeleteAsync(int? id)
        {
            if (id is null) throw new Exception();

            var deletedEntity = await _dbContext.ProductColors.FindAsync(id);

            if (deletedEntity is null) throw new Exception();

            var images = await _dbContext.ProductImages.Where(x => x.ProductColorId == id).ToListAsync();

            foreach (var image in images)
            {
                var path = Path.Combine(_webHostEnvironment.ContentRootPath, "Images", "Product", image.ImageName);

                if (File.Exists(path))
                    File.Delete(path);
            }

            _dbContext.Remove(deletedEntity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateById(int? id, ProductColorUpdateDto productColorUpdateDto)
        {
            if (id is null) throw new Exception();

            var existPCUpdateDto = await _dbContext.ProductColors.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            if (existPCUpdateDto is null) throw new Exception();

            if (productColorUpdateDto.Id != id) throw new Exception();

            if (productColorUpdateDto.Quantity is null) productColorUpdateDto.Quantity = existPCUpdateDto.Quantity;

            if (productColorUpdateDto.IsDeleted is null)
            {
                productColorUpdateDto.IsDeleted = existPCUpdateDto.IsDeleted;
            }

            if (productColorUpdateDto.ColorId is null)
            {
                productColorUpdateDto.ColorId = existPCUpdateDto.ColorId;
            }

            Color color = await _dbContext.Colors.FindAsync(productColorUpdateDto.ColorId);
            if (color is null) throw new Exception();

            if (productColorUpdateDto.ProductId is null)
            {
                productColorUpdateDto.ProductId = existPCUpdateDto.ProductId;
            }

            Product product = await _dbContext.Products.FindAsync(productColorUpdateDto.ProductId);
            if (product is null) throw new Exception();

            if (productColorUpdateDto.IsDeleted is null)
            {
                productColorUpdateDto.IsDeleted = existPCUpdateDto.IsDeleted;
            }

            if (productColorUpdateDto.IsDeleted == false && color.IsDeleted == true) throw new Exception();
            if (productColorUpdateDto.IsDeleted == false && product.IsDeleted == true) throw new Exception();

            var productColor = _mapper.Map<ProductColor>(productColorUpdateDto);
            productColor.SKU = existPCUpdateDto.SKU;
            _dbContext.ProductColors.Update(productColor);
            await _dbContext.SaveChangesAsync();
        }
    }
}
