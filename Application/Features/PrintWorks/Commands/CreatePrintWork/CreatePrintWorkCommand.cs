// Copyright © [insert list or range of years of product releases for this product] VMware, Inc. All rights reserved.
// This product is protected by copyright and intellectual property laws in the United States and other countries as well as by international treaties.
// VMware products are covered by one or more patents listed at http://www.vmware.com/go/patents

using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces.Repositories;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.PrintWorks.Commands.CreatePrintWork
{
    public partial class CreatePrintWorkCommand : IRequest<Response<int>>
    {
        public string FileName { get; set; }
        public Printer Printer { get; set; }
        public DateTime PaidWhen { get; set; }
        public DateTime PrintStartWhen { get; set; }
        public DateTime WorkFinishWhen { get; set; }
    }

    public class CreatePrinterCommandHandler : IRequestHandler<CreatePrintWorkCommand, Response<int>>
    {
        private readonly IPrintWorkRepositoryAsync _printWorkRepository;
        private readonly IMapper _mapper;
        
        public CreatePrinterCommandHandler(IPrintWorkRepositoryAsync printWorkRepository, IMapper mapper)
        {
            _printWorkRepository = printWorkRepository;
            _mapper = mapper;
        }
        
        public async Task<Response<int>> Handle(CreatePrintWorkCommand request, CancellationToken cancellationToken)
        {
            var printWork = _mapper.Map<PrintWork>(request);
            await _printWorkRepository.AddAsync(printWork);
            return new Response<int>(printWork.Id);
        }
    }
}