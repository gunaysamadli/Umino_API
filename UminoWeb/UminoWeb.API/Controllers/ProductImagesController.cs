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
    public class ProductImagesController : ControllerBase
    {

        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IRepository<ProductColor> _productColorRepository;
        private readonly IRepository<ProductImage> _productImageRepository;
        private readonly IProductImageService _productImageService;


        public ProductImagesController(IMapper mapper, IWebHostEnvironment webHostEnvironment, IRepository<ProductColor> productColorRepository, IRepository<ProductImage> productImageRepository, IProductImageService productImageService)
        {
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
            _productColorRepository = productColorRepository;
            _productImageRepository = productImageRepository;
            _productImageService = productImageService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var productImages = await _productImageRepository.GetAllAsync();

            if (productImages.Count == 0)
                return NotFound("Hele hec bir product image yaradilmayib");

            var productColorImagesDtos = _mapper.Map<List<GeneralProductImageDto>>(productImages);

            productColorImagesDtos.ForEach(x => x.ImageName = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}" + "/images/product/" + x.ImageName);

            return Ok(productColorImagesDtos);
        }

        [HttpGet("{id?}")]
        public async Task<IActionResult> Get([FromRoute] int? id)
        {
            var images = await _productImageRepository.GetAllAsync();

            if (images.Count == 0)
                return NotFound("Hele hec bir product image yaradilmayib");

            if (id is null)
                return NotFound();

            var image = await _productImageRepository.GetAsync(id);

            if (image is null)
                return NotFound("Bele product image movcud deyil");

            var imageDto = _mapper.Map<GeneralProductImageDto>(image);

            imageDto.ImageName = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}" + "/images/product/" + imageDto.ImageName;

            return Ok(imageDto);
        }

        [HttpPost]
        public async Task<IActionResult> Post(int? productColorId, [FromForm] List<IFormFile> files)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (productColorId is null)
                return BadRequest(ModelState);

            var productColor = await _productColorRepository.GetAsync(productColorId);

            if (productColor is null)
                return BadRequest(ModelState);

            var forderPath = Path.Combine(_webHostEnvironment.ContentRootPath, "Images", "Product");
            string newImage;
            List<ProductImageCreateDto> images = new List<ProductImageCreateDto>();

            foreach (var image in files)
            {
                newImage = await image.GenerateFile(forderPath);
                images.Add(new ProductImageCreateDto { ImageName = newImage.ToString(), ProductColorId = (int)productColorId });
            }

            List<ProductImage> createdProductColorImages = _mapper.Map<List<ProductImageCreateDto>, List<ProductImage>>(images);

            await _productImageRepository.AddAsync(createdProductColorImages);

            return Created(HttpContext.Request.Path, "ok");
        }

        [HttpDelete("completelyDelete/{id?}")]
        public async Task<IActionResult> CompletelyDelete([FromRoute] int? id)
        {
            await _productImageService.CompletelyDeleteAsync(id);

            return Ok();
        }

    }
}
