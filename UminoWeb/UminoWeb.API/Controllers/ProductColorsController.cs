using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UminoWeb.BLL.Dto;
using UminoWeb.BLL.Services.contracts;
using UminoWeb.DAL.Entities;
using UminoWeb.DAL.Repositories.Contracts;

namespace UminoWeb.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductColorsController : ControllerBase
    {

        private readonly IMapper _mapper;
        private readonly IRepository<Category> _categoryRepository;
        private readonly IRepository<Brand> _brandRepository;
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<ProductColor> _productColorRepository;
        private readonly IRepository<Color> _ColorRepository;
        private readonly IProductColorService _productColorService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IRepository<ProductImage> _productImageRepository;

        public ProductColorsController(IMapper mapper,  IWebHostEnvironment webHostEnvironment, IRepository<Category> categoryRepository, IRepository<Brand> brandRepository, IRepository<Product> productRepository, IRepository<ProductColor> productColorRepository, IProductColorService productColorService, IRepository<Color> colorRepository, IRepository<ProductImage> productImageRepository)
        {
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
            _categoryRepository = categoryRepository;
            _brandRepository = brandRepository;
            _productRepository = productRepository;
            _productColorRepository = productColorRepository;
            _productColorService = productColorService;
            _ColorRepository = colorRepository;
            _productImageRepository = productImageRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var productColors = await _productColorRepository.GetAllAsync();

            if (productColors.Count == 0)
                return NotFound("Hele hec bir productun coloru yaradilmayib");

            var productColorsDtos = _mapper.Map<List<ProductColorDto>>(productColors);

            var products = await _productRepository.GetAllAsync();
            productColorsDtos.ForEach(x => x.ProductName = products.Where(y => y.Id == x.ProductId).FirstOrDefault().Name);

            var colors = await _ColorRepository.GetAllAsync();
            productColorsDtos.ForEach(x => x.ColorName = colors.Where(y => y.Id == x.ColorId).FirstOrDefault().ColorName);

            var productColorImages = await _productImageRepository.GetAllIsNotDeletedAsync();

            productColorsDtos.ForEach(y => y.GeneralProductImages = _mapper.Map<List<GeneralProductImageDto>>(productColorImages.Where(z => z.ProductColorId == y.Id).ToList()));

            string imagePath = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}" + "/images/product/";

            productColorsDtos.ForEach(y => y.GeneralProductImages.ForEach(z => z.ImageName = imagePath + z.ImageName));

            return Ok(productColorsDtos);
        }

        [HttpGet("isNotDeleted")]
        public async Task<IActionResult> GetIsNotDeletede()
        {
            var productColors = await _productColorRepository.GetAllIsNotDeletedAsync();

            if (productColors.Count == 0)
                return NotFound("Hele hec bir delete olmayan productColor yaradilmayib");

            var productColorsDtos = _mapper.Map<List<ProductColorDto>>(productColors);

            var products = await _productRepository.GetAllAsync();
            productColorsDtos.ForEach(x => x.ProductName = products.Where(y => y.Id == x.ProductId).FirstOrDefault().Name);

            var colors = await _ColorRepository.GetAllAsync();
            productColorsDtos.ForEach(x => x.ColorName = colors.Where(y => y.Id == x.ColorId).FirstOrDefault().ColorName);

            var productColorImages = await _productImageRepository.GetAllIsNotDeletedAsync();

            productColorsDtos.ForEach(y => y.GeneralProductImages = _mapper.Map<List<GeneralProductImageDto>>(productColorImages.Where(z => z.ProductColorId == y.Id).ToList()));

            string imagePath = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}" + "/images/product/";

            productColorsDtos.ForEach(y => y.GeneralProductImages.ForEach(z => z.ImageName = imagePath + z.ImageName));

            return Ok(productColorsDtos);
        }

        [HttpGet("{id?}")]
        public async Task<IActionResult> Get([FromRoute] int? id)
        {
            if (id is null)
                return NotFound();

            var productColors = await _productColorRepository.GetAllAsync();

            if (productColors.Count == 0)
                return NotFound("Hele hec bir productColor yaradilmayib");

            var productColor = await _productColorRepository.GetAsync(id);

            if (productColor is null)
                return NotFound("Bele productColor movcud deyil");

            var productColorDto = _mapper.Map<ProductColorDto>(productColor);

            var products = await _productRepository.GetAllAsync();
            productColorDto.ProductName = products.Where(y => y.Id == productColorDto.ProductId).FirstOrDefault().Name;

            var colors = await _ColorRepository.GetAllAsync();
            productColorDto.ColorName = colors.Where(y => y.Id == productColorDto.ColorId).FirstOrDefault().ColorName;

            var productColorImages = await _productImageRepository.GetAllIsNotDeletedAsync();

            productColorDto.GeneralProductImages = _mapper.Map<List<GeneralProductImageDto>>(productColorImages.Where(z => z.ProductColorId == productColorDto.Id).ToList());

            string imagePath = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}" + "/images/product/";

            productColorDto.GeneralProductImages.ForEach(z => z.ImageName = imagePath + z.ImageName);

            return Ok(productColorDto);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromForm] ProductColorCreateDto productColorCreateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var productColor = _mapper.Map<ProductColor>(productColorCreateDto);

            var db = await _productColorRepository.GetAllAsync();

            int lastCount;

            if (db.ToList().Count == 0)
            {
                lastCount = 1;
            }
            else
            {
                lastCount = db.ToList().Last().Id + 1;
            }

            productColor.SKU = "PRD-" + productColor.ProductId + "-CLR" + productColor.ColorId + productColor.SKU + "-PCI" + lastCount;

            await _productColorService.AddAsync(productColor);

            return Created(HttpContext.Request.Path, productColor.Id);
        }

        [HttpPut("{id?}")]
        public async Task<IActionResult> Put([FromRoute] int? id, [FromForm] ProductColorUpdateDto productColorUpdateDto)
        {
            var productColors = await _productColorRepository.GetAllAsync();

            if (productColors.Count == 0)
                return NotFound("Hele hec bir product yaradilmayib");

            await _productColorService.UpdateById(id, productColorUpdateDto);

            return Ok();
        }

        [HttpDelete("completelyDelete/{id?}")]
        public async Task<IActionResult> CompletelyDelete([FromRoute] int? id)
        {
            await _productColorService.CompletelyDeleteAsync(id);

            return Ok();
        }

    }
}
