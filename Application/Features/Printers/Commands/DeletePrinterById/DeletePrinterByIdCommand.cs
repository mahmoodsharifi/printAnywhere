// Copyright © [insert list or range of years of product releases for this product] VMware, Inc. All rights reserved.
// This product is protected by copyright and intellectual property laws in the United States and other countries as well as by international treaties.
// VMware products are covered by one or more patents listed at http://www.vmware.com/go/patents

using System.Threading;
using System.Threading.Tasks;
using Application.Exceptions;
using Application.Interfaces.Repositories;
using Application.Wrappers;
using MediatR;

namespace Application.Features.Printers.Commands.DeletePrinterById
{
    public class DeletePrinterByIdCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }

        public class DeletePrinterByIdCommandHandler : IRequestHandler<DeletePrinterByIdCommand, Response<int>>
        {
            private readonly IPrinterRepositoryAsync _printerRepository;

            public DeletePrinterByIdCommandHandler(IPrinterRepositoryAsync printerRepository)
            {
                _printerRepository = printerRepository;
            }
            public async Task<Response<int>> Handle(DeletePrinterByIdCommand command, CancellationToken cancellationToken)
            {
                var printer = await _printerRepository.GetByIdAsync(command.Id);
                if (printer == null) throw new ApiException($"Printer Not Found.");
                await _printerRepository.DeleteAsync(printer);
                return new Response<int>(printer.Id);
            }
        }

    }
}