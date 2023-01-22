using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UminoWeb.BLL.Data;
using UminoWeb.BLL.Dto;
using UminoWeb.BLL.Services.contracts;
using UminoWeb.DAL.Entities;
using UminoWeb.DAL.Repositories.Contracts;

namespace UminoWeb.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandsController : ControllerBase
    {

        private readonly IMapper _mapper;
        private readonly IRepository<Brand> _brandRepository;
        private readonly IBrandService _brandService;
        private readonly IWebHostEnvironment _webHostEnvironment;


        public BrandsController(IMapper mapper, IRepository<Brand> brandRepository, IBrandService brandService, IWebHostEnvironment webHostEnvironment)
        {
            _mapper = mapper;
            _brandRepository = brandRepository;
            _brandService = brandService;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var brands = await _brandRepository.GetAllAsync();

            if (brands.Count == 0)
                return NotFound("Hele hec bir brand yaradilmayib");

            var brandsDtos = _mapper.Map<List<BrandDto>>(brands);

            brandsDtos.ForEach(x => x.ImageName = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}" + "/images/brand/" + x.ImageName);

            return Ok(brandsDtos);
        }

        [HttpGet("isNotDeleted")]
        public async Task<IActionResult> GetIsNotDeleted()
        {
            var brands = await _brandRepository.GetAllIsNotDeletedAsync();

            if (brands.Count == 0)
                return NotFound("Hele hec bir delete olmayan brand yaradilmayib");

            var brandsDtos = _mapper.Map<List<BrandDto>>(brands);

            brandsDtos.ForEach(x => x.ImageName = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}" + "/images/brand/" + x.ImageName);

            return Ok(brandsDtos);
        }

        [HttpGet("{id?}")]
        public async Task<IActionResult> Get([FromRoute] int? id)
        {
            var brands = await _brandRepository.GetAllAsync();

            if (brands.Count == 0)
                return NotFound("Hele hec bir brand yaradilmayib");

            if (id is null)
                return NotFound();

            var brand = await _brandRepository.GetAsync(id);

            if (brand is null)
                return NotFound("Bele brand movcud deyil");

            var brandDto = _mapper.Map<BrandDto>(brand);

            brandDto.ImageName = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}" + "/images/brand/" + brandDto.ImageName;

            return Ok(brandDto);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromForm] BrandCreateDto brandCreateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var forderPath = Path.Combine(_webHostEnvironment.ContentRootPath, "Images", "Brand");

            brandCreateDto.ImageName = await brandCreateDto.Image.GenerateFile(forderPath);

            var brandCategory = _mapper.Map<Brand>(brandCreateDto);

            await _brandRepository.AddAsync(brandCategory);

            return Created(HttpContext.Request.Path, brandCategory.Id);
        }

        [HttpPut("{id?}")]
        public async Task<IActionResult> Put([FromRoute] int? id, [FromForm] BrandUpdateDto brandUpdateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var brands = await _brandRepository.GetAllAsync();

            if (brands.Count == 0)
                return NotFound("Hele hec bir brand yaradilmayib");

            await _brandService.UpdateById(id, brandUpdateDto);

            return Ok();
        }

        [HttpDelete("completelyDelete/{id?}")]
        public async Task<IActionResult> CompletelyDelete([FromRoute] int? id)
        {
            await _brandService.CompletelyDeleteAsync(id);

            return Ok();
        }
    }
}
