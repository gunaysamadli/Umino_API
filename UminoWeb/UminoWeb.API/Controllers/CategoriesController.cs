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
    public class CategoriesController : ControllerBase
    {

        private readonly IMapper _mapper;
        private readonly IRepository<Category> _categoryRepository;
        private readonly ICategoryService _categoryService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public CategoriesController(IMapper mapper, IRepository<Category> categoryRepository, ICategoryService categoryService, IWebHostEnvironment webHostEnvironment)
        {
            _mapper = mapper;
            _categoryRepository = categoryRepository;
            _categoryService = categoryService;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var categories = await _categoryRepository.GetAllAsync();

            if (categories.Count == 0)
                return NotFound("Hele hec bir category yaradilmayib");

            var categoriesDtos = _mapper.Map<List<CategoryDto>>(categories);

            categoriesDtos.ForEach(x => x.ImageName = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}" + "/images/category/" + x.ImageName);

            return Ok(categoriesDtos);
        }

        [HttpGet("isNotDeleted")]
        public async Task<IActionResult> GetIsNotDeleted()
        {
            var categories = await _categoryRepository.GetAllIsNotDeletedAsync();

            if (categories.Count == 0)
                return NotFound("Hele hec bir delete olmayan category yaradilmayib");

            var categoriesDtos = _mapper.Map<List<CategoryDto>>(categories);

            categoriesDtos.ForEach(x => x.ImageName = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}" + "/images/category/" + x.ImageName);

            return Ok(categoriesDtos);
        }

        [HttpGet("{id?}")]
        public async Task<IActionResult> Get([FromRoute] int? id)
        {
            var categories = await _categoryRepository.GetAllAsync();

            if (categories.Count == 0)
                return NotFound("Hele hec bir category yaradilmayib");

            if (id is null)
                return NotFound();

            var category = await _categoryRepository.GetAsync(id);

            if (category is null)
                return NotFound("Bele category movcud deyil");

            var categoryDto = _mapper.Map<CategoryDto>(category);

            categoryDto.ImageName = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}" + "/images/category/" + categoryDto.ImageName;

            return Ok(categoryDto);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromForm] CategoryCreateDto categoryCreateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var forderPath = Path.Combine(_webHostEnvironment.ContentRootPath, "Images", "Category");

            categoryCreateDto.ImageName = await categoryCreateDto.Image.GenerateFile(forderPath);

            var categoriedCategory = _mapper.Map<Category>(categoryCreateDto);

            await _categoryRepository.AddAsync(categoriedCategory);

            return Created(HttpContext.Request.Path, categoriedCategory.Id);
        }

        [HttpPut("{id?}")]
        public async Task<IActionResult> Put([FromRoute] int? id, [FromForm] CategoryUpdateDto categoryUpdateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var categories = await _categoryRepository.GetAllAsync();

            if (categories.Count == 0)
                return NotFound("Hele hec bir category yaradilmayib");

            await _categoryService.UpdateById(id, categoryUpdateDto);

            return Ok();
        }


        [HttpDelete("completelyDelete/{id?}")]
        public async Task<IActionResult> CompletelyDelete([FromRoute] int? id)
        {
            await _categoryService.CompletelyDeleteAsync(id);

            return Ok();
        }

    }
}
