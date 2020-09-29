// Copyright © [insert list or range of years of product releases for this product] VMware, Inc. All rights reserved.
// This product is protected by copyright and intellectual property laws in the United States and other countries as well as by international treaties.
// VMware products are covered by one or more patents listed at http://www.vmware.com/go/patents

using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Application.Wrappers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Application.Features.PrintWorks.Commands.CreatePrintWork
{
    public partial class UploadPrintWorkCommand : IRequest<Response<byte[]>>
    {
        public IFormFile File { get; set; }
    }

    public class UploadPrintWorkCommandHandler : IRequestHandler<UploadPrintWorkCommand, Response<byte[]>>
    {
        private readonly IConfiguration _configuration;
        public UploadPrintWorkCommandHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<Response<byte[]>> Handle(UploadPrintWorkCommand request, CancellationToken cancellationToken)
        {
            var file = request.File;
            if (file.Length > _configuration.GetValue<long>("MaxFileSize"))
            {
                return new Response<byte[]>("File size too big.");
            }

            if (file.Length == 0)
            {
                return new Response<byte[]>("File size is zero");
            }

            byte[] fileBytes;
            using (var ms = new MemoryStream())
            {
                file.CopyTo(ms);
                fileBytes = ms.ToArray();
            }
            return new Response<byte[]>(fileBytes);
        }
    }
}