// Copyright © [insert list or range of years of product releases for this product] VMware, Inc. All rights reserved.
// This product is protected by copyright and intellectual property laws in the United States and other countries as well as by international treaties.
// VMware products are covered by one or more patents listed at http://www.vmware.com/go/patents

using System.Threading.Tasks;
using Application.Features.PrintWorks.Commands.CreatePrintWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class PrintWorkController : BaseApiController
    {
        // POST api/<controller>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post(int printerId, IFormFile file)
        {
            var createPrintWorkCommand = new CreatePrintWorkCommand();
            createPrintWorkCommand.File = file;
            createPrintWorkCommand.FileName = file.FileName;
            createPrintWorkCommand.PrinterId = printerId;
            return Ok(await Mediator.Send(createPrintWorkCommand));
        }
    }
}