using AutoMapper;
using BB.Finances.Core.Abstractions;
using BB.Finances.Data.DTO;
using BB.Finances.WebAPI.Models.Request;
using BB.Finances.WebAPI.Models.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BB.Finances.WebAPI.Controllers
{
    [Route("api/finances")]
    [ApiController]
    public class ExpenseController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IExpenseService _expenseService;

        public ExpenseController(IMapper mapper, IExpenseService expenseService)
        {
            _mapper = mapper;
            _expenseService = expenseService;
        }

        [HttpGet("{expenseId:guid}")]
        [ProducesResponseType(typeof(ExpenseResponseModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Details([FromRoute] Guid expenseId)
        {
            try
            {
                var dto = await _expenseService.GetByIdAsync(expenseId);

                var result = _mapper.Map<ExpenseResponseModel>(dto);

                return Ok(result);
            }
            catch (Exception ex)
            {
                // Log here
                return NotFound();
            }
        }

        [HttpGet("user/{userId:guid}")]
        [ProducesResponseType(typeof(IEnumerable<ExpenseResponseModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get([FromRoute] Guid userId)
        {
            try
            {
                var dtos = await _expenseService.GetAllByUserIdAsync(userId);

                var result = _mapper.Map<IEnumerable<ExpenseResponseModel>>(dtos);

                return Ok(result);
            }
            catch (Exception ex)
            {
                // Log here
                return BadRequest();
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] ExpenseRequestModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(model);
                }

                var dto = _mapper.Map<ExpenseDTO>(model);

                var result = await _expenseService.CreateNewAsync(dto);

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
