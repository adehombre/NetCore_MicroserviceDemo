using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CustomerAPI.Data;
using CustomerAPI.Data.Models;
using CustomerAPI.Service.Command;
using CustomerAPI.Service.Query;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CustomerAPI.Controllers
{
    [Route("api/customer")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ILogger<CustomerController> _logger;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public CustomerController(ILogger<CustomerController> logger, IMapper mapper, IMediator mediator)
        {
            _logger = logger;
            _mapper = mapper;
            _mediator = mediator;
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [HttpPost("create")]
        public async Task<ActionResult> Create(CustomerModel model)
        {
            var response = await _mediator.Send(new CreateCustomerCommand {
                Customer = _mapper.Map<Customer>(model)
            });
            return Created($"/api/get/{response.Id}", response);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("all/{currentPage}/{itemByPage}")]
        public async Task<ActionResult> All(int currentPage, int itemByPage)
        {
            try
            {
                var response = await _mediator.Send(new GetAllCustomer() { 
                    CurrentPage = currentPage,
                    ItemByPage = itemByPage
                });
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ErrorMessage = ex.Message });
            }
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("customer-by-id/{id}")]
        public async Task<ActionResult> CustomerById(Guid id)
        {
            try
            {
                var getByIdQuery = new GetCustomerByIdQuery { Id = id };
                var response = await _mediator.Send(getByIdQuery);
                return Ok(response);
            }
            catch (Exception ex)
            {
                if (ex is KeyNotFoundException)
                    return NotFound(new { ErrorMessage = ex.Message });
                else
                    return StatusCode(500, new { ErrorMessage = ex.Message });
            }
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            try
            {
                var deleteCommand = new DeleteCustomerCommand { Id = id };
                var response = await _mediator.Send(deleteCommand);
                return Ok();
            }
            catch (Exception ex)
            {
                if (ex is KeyNotFoundException)
                    return NotFound(new { ErrorMessage = ex.Message });
                else
                    return StatusCode(500, new { ErrorMessage = ex.Message });
            }
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPut("update/{id}")]
        public async Task<ActionResult> Update(Guid id, CustomerModel model)
        {
            try
            {
                var updateCommand = new UpdateCustomerCommand
                {
                    Customer = model,
                    Id = id
                };
                var response = await _mediator.Send(updateCommand);
                return NoContent();
            }
            catch (Exception ex)
            {
                if (ex is KeyNotFoundException)
                    return NotFound(new { ErrorMessage = ex.Message });
                else
                    return StatusCode(500, new { ErrorMessage = ex.Message });
            }
        }
    }
}