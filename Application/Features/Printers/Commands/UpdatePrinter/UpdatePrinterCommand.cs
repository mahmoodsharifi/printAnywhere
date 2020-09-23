// Copyright © [insert list or range of years of product releases for this product] VMware, Inc. All rights reserved.
// This product is protected by copyright and intellectual property laws in the United States and other countries as well as by international treaties.
// VMware products are covered by one or more patents listed at http://www.vmware.com/go/patents

using System.Threading;
using System.Threading.Tasks;
using Application.Exceptions;
using Application.Interfaces.Repositories;
using Application.Wrappers;
using Domain.Entities;
using MediatR;

namespace Application.Features.Printers.Commands.UpdatePrinter
{
    public class UpdatePrinterCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public int PrintQuotaPerDay { get; set; }
        public int RemainingQuotaForTheDay { get; set; }
        public string AvailabilityCron { get; set; }

        public class UpdatePrinterCommandHandler : IRequestHandler<UpdatePrinterCommand, Response<int>>
        {
            private readonly IPrinterRepositoryAsync _printerRepository;

            public UpdatePrinterCommandHandler(IPrinterRepositoryAsync printerRepository)
            {
                _printerRepository = printerRepository;
            }
            
            public async Task<Response<int>> Handle(UpdatePrinterCommand command, CancellationToken cancellationToken)
            {
                var printer = await _printerRepository.GetByIdAsync(command.Id);

                if (printer == null)
                {
                    throw new ApiException($"Printer Not Found.");
                }
                else
                {
                    printer.Latitude = command.Latitude;
                    printer.Longitude = command.Longitude;
                    printer.AvailabilityCron = command.AvailabilityCron;
                    printer.PrintQuotaPerDay = command.PrintQuotaPerDay;
                    printer.RemainingQuotaForTheDay = command.RemainingQuotaForTheDay;
                    await _printerRepository.UpdateAsync(printer);
                    return new Response<int>(printer.Id);
                }
            }
        }
    }
}