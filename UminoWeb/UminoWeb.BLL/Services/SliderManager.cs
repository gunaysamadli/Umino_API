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
    public class SliderManager : EfCoreRepository<Slider>, ISliderService
    {
        private readonly AppDbContext _dbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IMapper _mapper;

        public SliderManager(AppDbContext dbContext, IWebHostEnvironment webHostEnvironment, IMapper mapper) : base(dbContext)
        {
            _dbContext = dbContext;
            _webHostEnvironment = webHostEnvironment;
            _mapper = mapper;
        }
        public override async Task CompletelyDeleteAsync(int? id)
        {
            if (id is null) throw new Exception();

            var deletedEntity = await _dbContext.Sliders.FindAsync(id);

            if (deletedEntity is null) throw new Exception();

            var path = Path.Combine(_webHostEnvironment.ContentRootPath, "Images", "Slider", deletedEntity.ImageName);

            if (File.Exists(path))
                File.Delete(path);

            _dbContext.Remove(deletedEntity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateById(int? id, SliderUpdateDto sliderUpdateDto)
        {
            if (id is null) throw new Exception();

            var existSlider = await _dbContext.Sliders.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            if (existSlider is null) throw new Exception();

            if (sliderUpdateDto.Id != id) throw new Exception();

            if (sliderUpdateDto.Image is not null)
            {
                var forderPath = Path.Combine(_webHostEnvironment.ContentRootPath, "Images", "Slider");
                var existImageName = Path.Combine(forderPath, existSlider.ImageName);

                if (File.Exists(existImageName))
                    File.Delete(existImageName);

                sliderUpdateDto.ImageName = await sliderUpdateDto.Image.GenerateFile(forderPath);

            }
            else sliderUpdateDto.ImageName = existSlider.ImageName;

            if (sliderUpdateDto.Title is null) sliderUpdateDto.Title = existSlider.Title;

            if (sliderUpdateDto.Subtitle is null) sliderUpdateDto.Subtitle = existSlider.Subtitle;

            if (sliderUpdateDto.ButtonName is null) sliderUpdateDto.ButtonName = existSlider.ButtonName;

            if (sliderUpdateDto.ButtonLink is null) sliderUpdateDto.ButtonLink = existSlider.ButtonLink;

            if (sliderUpdateDto.IsDeleted is null) sliderUpdateDto.IsDeleted = existSlider.IsDeleted;

            var slider = _mapper.Map<Slider>(sliderUpdateDto);

            _dbContext.Sliders.Update(slider);
            await _dbContext.SaveChangesAsync();
        }
    }
}
