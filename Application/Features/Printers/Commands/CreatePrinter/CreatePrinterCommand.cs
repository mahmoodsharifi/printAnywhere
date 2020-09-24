// Copyright © [insert list or range of years of product releases for this product] VMware, Inc. All rights reserved.
// This product is protected by copyright and intellectual property laws in the United States and other countries as well as by international treaties.
// VMware products are covered by one or more patents listed at http://www.vmware.com/go/patents

using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces.Repositories;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Printers.Commands.CreatePrinter
{
    public partial class CreatePrinterCommand : IRequest<Response<int>>
    {
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public int PrintQuotaPerDay { get; set; }
        public int RemainingQuotaForTheDay { get; set; }
        public string AvailabilityCron { get; set; }
    }

    public class CreatePrinterCommandHandler : IRequestHandler<CreatePrinterCommand, Response<int>>
    {
        private readonly IPrinterRepositoryAsync _printerRepository;
        private readonly IMapper _mapper;

        public CreatePrinterCommandHandler(IPrinterRepositoryAsync printerRepository, IMapper mapper)
        {
            _printerRepository = printerRepository;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(CreatePrinterCommand request, CancellationToken cancellationToken)
        {
            var printer = _mapper.Map<Printer>(request);
            await _printerRepository.AddAsync(printer);
            return new Response<int>(printer.Id);
        }
    }
}