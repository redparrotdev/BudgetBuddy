using AutoMapper;
using BB.Finances.Core.CQRS;
using BB.Finances.Data.Entities;
using BB.Finances.Data.Exceptions;
using BB.Finances.WebAPI.Models.Request;
using BB.Finances.WebAPI.Models.Response;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BB.Finances.WebAPI.Controllers
{
    [Route("api/finances/accounts")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public AccountsController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet("user/{userId:guid}")]
        public async Task<IActionResult> GetAll([FromRoute] Guid userId)
        {
            if (userId.Equals(Guid.Empty))
            {
                throw new ArgumentException($"{nameof(userId)} is empty!");
            }

            var entities = await _mediator.Send(new GetAccountsByUserId()
            {
                UserId = userId
            });

            var result = _mapper.Map<IEnumerable<AccountResponseModel>>(entities);

            return Ok(result);
        }

        [HttpGet("{accountId:guid}")]
        public async Task<IActionResult> Details([FromRoute] Guid accountId)
        {
            if (accountId.Equals(Guid.Empty))
            {
                throw new ArgumentException($"{nameof(accountId)} is empty");
            }

            var entity = await _mediator.Send(new GetAccountById()
            {
                EntityId = accountId,
            });

            var result = _mapper.Map<AccountResponseModel>(entity);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AccountRequestModel model)
        {
            var entity = _mapper.Map<Account>(model);

            entity.Id = Guid.NewGuid();
            entity.CreationDate = DateTime.UtcNow;

            var result = await _mediator.Send(new CreateAccount()
            {
                Entity = entity
            });

            if (result != 1)
            {
                throw new GeneralException("Creating a new Account goes wrong");
            }

            var response = _mapper.Map<AccountResponseModel>(entity);

            return Created(entity.Id.ToString(), response);
        }
    }
}
