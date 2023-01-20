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
    public class SlidersController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Slider> _sliderRepository;
        private readonly ISliderService _sliderService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public SlidersController(IMapper mapper, IRepository<Slider> sliderRepository, IWebHostEnvironment webHostEnvironment, ISliderService sliderService)
        {
            _mapper = mapper;
            _sliderRepository = sliderRepository;
            _webHostEnvironment = webHostEnvironment;
            _sliderService = sliderService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var sliders = await _sliderRepository.GetAllAsync();

            if (sliders.Count == 0)
                return NotFound("Hele hec bir slider yaradilmayib");

            var slidersDtos = _mapper.Map<List<SliderDto>>(sliders);

            slidersDtos.ForEach(x => x.ImageName = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}" + "/images/slider/" + x.ImageName);

            return Ok(slidersDtos);
        }

        [HttpGet("isNotDeleted")]
        public async Task<IActionResult> GetIsNotDeleted()
        {
            var sliders = await _sliderRepository.GetAllIsNotDeletedAsync();

            if (sliders.Count == 0)
                return NotFound("Hele hec bir delete olmayan slider yaradilmayib");

            var slidersDtos = _mapper.Map<List<SliderDto>>(sliders);

            slidersDtos.ForEach(x => x.ImageName = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}" + "/images/slider/" + x.ImageName);

            return Ok(slidersDtos);
        }

        [HttpGet("{id?}")]
        public async Task<IActionResult> Get([FromRoute] int? id)
        {
            var sliders = await _sliderRepository.GetAllAsync();

            if (sliders.Count == 0)
                return NotFound("Hele hec bir slider yaradilmayib");

            if (id is null)
                return NotFound();

            var slider = await _sliderRepository.GetAsync(id);

            if (slider is null)
                return NotFound("Bele slider movcud deyil");

            var sliderDto = _mapper.Map<SliderDto>(slider);

            sliderDto.ImageName = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}" + "/images/slider/" + sliderDto.ImageName;

            return Ok(sliderDto);
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromForm] SliderCreateDto sliderCreateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var forderPath = Path.Combine(_webHostEnvironment.ContentRootPath, "Images", "Slider");

            sliderCreateDto.ImageName = await sliderCreateDto.Image.GenerateFile(forderPath);

            var slideredCategory = _mapper.Map<Slider>(sliderCreateDto);

            await _sliderRepository.AddAsync(slideredCategory);

            return Created(HttpContext.Request.Path, slideredCategory.Id);
        }

        [HttpPut("{id?}")]
        public async Task<IActionResult> Put([FromRoute] int? id, [FromForm] SliderUpdateDto sliderUpdateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var sliders = await _sliderRepository.GetAllAsync();

            if (sliders.Count == 0)
                return NotFound("Hele hec bir slider yaradilmayib");

            await _sliderService.UpdateById(id, sliderUpdateDto);

            return Ok();
        }

        [HttpDelete("completelyDelete/{id?}")]
        public async Task<IActionResult> CompletelyDelete([FromRoute] int? id)
        {
            await _sliderService.CompletelyDeleteAsync(id);

            return Ok();
        }

    }
}
