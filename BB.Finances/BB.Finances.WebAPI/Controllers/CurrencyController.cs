using AutoMapper;
using BB.Finances.Core.Abstractions;
using BB.Finances.Data.DTO;
using BB.Finances.WebAPI.Models.Request;
using BB.Finances.WebAPI.Models.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BB.Finances.WebAPI.Controllers
{
    [Route("api/finances/currency")]
    [ApiController]
    public class CurrencyController : ControllerBase
    {
        private readonly ICurrencyService _currencyService;
        private readonly IMapper _mapper;

        public CurrencyController(ICurrencyService currencyService, IMapper mapper)
        {
            _currencyService = currencyService;
            _mapper = mapper;
        }

        [HttpGet("all")]
        [ProducesResponseType(typeof(IEnumerable<CurrencyResponseModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var dtos = await _currencyService.GetAllCurrenciesAsync();

                var result = _mapper.Map<IEnumerable<CurrencyResponseModel>>(dtos);

                return Ok(result);
            }
            catch (Exception ex)
            {
                // Log here
                return BadRequest();
            }
        }

        [HttpGet("{currencyId:guid}")]
        [ProducesResponseType(typeof(CurrencyResponseModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Details([FromRoute] Guid currencyId)
        {
            try
            {
                var dto = await _currencyService.GetByIdAsync(currencyId);

                var result = _mapper.Map<CurrencyResponseModel>(dto);

                return Ok(result);
            }
            catch (Exception ex)
            {
                // Log here
                return NotFound();
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CurrencyRequestModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(model);
                }

                var dto = _mapper.Map<CurrencyDTO>(model);

                var result = await _currencyService.CreateNewAsync(dto);

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
