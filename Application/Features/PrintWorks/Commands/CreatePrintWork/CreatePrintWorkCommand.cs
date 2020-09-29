// Copyright © [insert list or range of years of product releases for this product] VMware, Inc. All rights reserved.
// This product is protected by copyright and intellectual property laws in the United States and other countries as well as by international treaties.
// VMware products are covered by one or more patents listed at http://www.vmware.com/go/patents

using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces.Repositories;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.PrintWorks.Commands.CreatePrintWork
{
    public partial class CreatePrintWorkCommand : IRequest<Response<HttpResponseMessage>>
    {
        public string FileName { get; set; }
        public IFormFile File { get; set; }
        public int PrinterId { get; set; }
        public DateTime PaidWhen { get; set; }
        public DateTime PrintStartWhen { get; set; }
        public DateTime WorkFinishWhen { get; set; }
    }

    public class CreatePrinterCommandHandler : IRequestHandler<CreatePrintWorkCommand, Response<HttpResponseMessage>>
    {
        private readonly IPrintWorkRepositoryAsync _printWorkRepository;
        private readonly IMapper _mapper;
        private readonly IRequestHandler<UploadPrintWorkCommand, Response<byte[]>> _uploadPrintWorkCommand;
        
        public CreatePrinterCommandHandler(
            IPrintWorkRepositoryAsync printWorkRepository,
            IMapper mapper,
            IRequestHandler<UploadPrintWorkCommand, Response<byte[]>> uploadPrintWorkCommand)
        {
            _printWorkRepository = printWorkRepository;
            _mapper = mapper;
            _uploadPrintWorkCommand = uploadPrintWorkCommand;
        }
        
        public async Task<Response<HttpResponseMessage>> Handle(CreatePrintWorkCommand request, CancellationToken cancellationToken)
        {
            var uploadPrintWorkCommand = new UploadPrintWorkCommand();
            uploadPrintWorkCommand.File = request.File;
            var fileUploadResult = await _uploadPrintWorkCommand.Handle(uploadPrintWorkCommand, cancellationToken);

            if (fileUploadResult.Succeeded)
            {
                var printWork = _mapper.Map<PrintWork>(request);
                await _printWorkRepository.AddAsync(printWork);
                return new Response<HttpResponseMessage>(new HttpResponseMessage(HttpStatusCode.Created), "PrintWorkId: "+printWork.Id);
            }
            else
            {
                return new Response<HttpResponseMessage>(new HttpResponseMessage(HttpStatusCode.BadRequest), string.Join(',', fileUploadResult.Errors));
            }

        }
    }
}