using AutoMapper;
using BB.Finances.Core.Abstractions;
using BB.Finances.Data.DTO;
using BB.Finances.WebAPI.Models.Request;
using BB.Finances.WebAPI.Models.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BB.Finances.WebAPI.Controllers
{
    [Route("api/finances/categories")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        [HttpGet("{categoryId:guid}")]
        [ProducesResponseType(typeof(CategoryResponseModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Details([FromRoute] Guid categoryId)
        {
            try
            {
                var dto = await _categoryService.GetByIdAsync(categoryId);

                var result = _mapper.Map<CategoryResponseModel>(dto);

                return Ok(result);
            }
            catch (Exception ex)
            {
                // Log here
                return NotFound();
            }
        }

        [HttpGet("user/{userId:guid}")]
        [ProducesResponseType(typeof(CategoryResponseModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get([FromRoute] Guid userId)
        {
            try
            {
                var dtos = await _categoryService.GetAllByUserIdAsync(userId);

                var result = _mapper.Map<IEnumerable<CategoryResponseModel>>(dtos);

                return Ok(result);
            }
            catch (Exception ex)
            {
                // Log here
                return BadRequest();
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CategoryRequestModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(model);
                }

                var dto = _mapper.Map<CategoryDTO>(model);

                var result = await _categoryService.CreateNewAsync(dto);

                return Created(result.ToString(), null);
            }
            catch (Exception ex)
            {
                // Log here
                return BadRequest(model);
            }
        }
    }
}
