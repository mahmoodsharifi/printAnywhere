// Copyright © [insert list or range of years of product releases for this product] VMware, Inc. All rights reserved.
// This product is protected by copyright and intellectual property laws in the United States and other countries as well as by international treaties.
// VMware products are covered by one or more patents listed at http://www.vmware.com/go/patents

using System.Threading.Tasks;
using Application.Features.Printers.Commands.CreatePrinter;
using Application.Features.Printers.Commands.DeletePrinterById;
using Application.Features.Printers.Commands.UpdatePrinter;
using Application.Features.Printers.Queries.GetAllPrinters;
using Application.Features.Printers.Queries.GetPrinterById;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class PrinterController : BaseApiController
    {
        // GET: api/<controller>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAllPrintersParameter filter)
        {
            return Ok(await Mediator.Send(new GetAllPrintersParameter() { PageSize = filter.PageSize, PageNumber = filter.PageNumber  }));
        }
        
        // GET api/<controller>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await Mediator.Send(new GetPrinterByIdQuery { Id = id }));
        }
        
        // POST api/<controller>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post(CreatePrinterCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
        
        // PUT api/<controller>/5
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Put(int id, UpdatePrinterCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            return Ok(await Mediator.Send(command));
        }
        
        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await Mediator.Send(new DeletePrinterByIdCommand { Id = id }));
        }
    }
}