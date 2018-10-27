using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kernel.Actions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Models;
using Webapi.Requests;

namespace Webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhonesController : ControllerBase
    {
        [HttpGet]
        [Route("all")]
        [ProducesResponseType(typeof(IEnumerable<Phone>), 200)]
        public async Task<ActionResult<IEnumerable<Phone>>> GetAll([FromServices] IMediator mediator)
        {
            var request = new GetAllPhonesRequest();
            var response = await mediator.Send(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("book")]
        [ProducesResponseType(typeof(IEnumerable<Phone>), 200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> Book(
            [FromBody] BookPhoneModel model,
            [FromServices] IMediator mediator)
        {
            var request = new BookPhoneRequest(model.Model, model.Email);
            try
            {
                var response = await mediator.Send(request);
                if (response) 
                {
                    return Ok(await mediator.Send(new GetAllPhonesRequest()));
                }
                return BadRequest("Phone has already booked");
            }
            catch (ArgumentOutOfRangeException)
            {
                return BadRequest("Phone doesnt exist");
            }
        }

        [HttpPost]
        [Route("release")]
        [ProducesResponseType(typeof(IEnumerable<Phone>), 200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> Release(
            [FromBody] ReleasePhoneModel model,
            [FromServices] IMediator mediator)
        {
            var request = new ReleasePhoneRequest(model.Model);
            try
            {
                var response = await mediator.Send(request);
                if (response) 
                {
                    return Ok(await mediator.Send(new GetAllPhonesRequest()));
                }
                return BadRequest("Phone has already released");
            }
            catch (ArgumentOutOfRangeException)
            {
                return BadRequest("Phone doesnt exist");
            }
        }
    }
}
