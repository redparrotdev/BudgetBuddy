using AutoMapper;
using BB.Finances.Core.Abstractions;
using BB.Finances.Data.DTO;
using BB.Finances.WebAPI.Models.Request;
using BB.Finances.WebAPI.Models.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BB.Finances.WebAPI.Controllers
{
    [Route("api/finances/accounts")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;

        public AccountsController(IAccountService accountService, IMapper mapper)
        {
            _accountService = accountService;
            _mapper = mapper;
        }

        [HttpGet("{accountId:guid}")]
        [ProducesResponseType(typeof(AccountResponseModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Details([FromRoute] Guid accountId)
        {
            try
            {
                var dto = await _accountService.GetByIdAsync(accountId);

                var response = _mapper.Map<AccountResponseModel>(dto);

                return Ok(response);
            }
            catch(Exception ex)
            {
                // Log here
                return NotFound();
            }
        }

        [HttpGet("user/{userId:guid}")]
        [ProducesResponseType(typeof(IEnumerable<AccountResponseModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get([FromRoute] Guid userId)
        {
            try
            {
                var dtos = await _accountService.GetAllByUserIdAsync(userId);

                var response = _mapper.Map<IEnumerable<AccountResponseModel>>(dtos);

                return Ok(response);
            }
            catch(Exception ex)
            {
                // Log here
                return BadRequest();
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] AccountRequestModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(model);
                }

                var dto = _mapper.Map<AccountDTO>(model);

                var result = await _accountService.CreateNewAsync(dto);

                return Created(result.ToString(), null);
            }
            catch(Exception ex)
            {
                // Log here
                Console.WriteLine(ex);
                return BadRequest();
            }
        }
    }
}
