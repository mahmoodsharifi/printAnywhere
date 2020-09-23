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

namespace Application.Features.Printers.Queries.GetPrinterById
{
    public class GetPrinterByIdQuery : IRequest<Response<Printer>>
    {
        public int Id { get; set; }

        public class GetPrinterByIdQueryHandler : IRequestHandler<GetPrinterByIdQuery, Response<Printer>>
        {
            private readonly IPrinterRepositoryAsync _printerRepository;
            public GetPrinterByIdQueryHandler(IPrinterRepositoryAsync printerRepository)
            {
                _printerRepository = printerRepository;
            }
            public async Task<Response<Printer>> Handle(GetPrinterByIdQuery query, CancellationToken cancellationToken)
            {
                var printer = await _printerRepository.GetByIdAsync(query.Id);
                if (printer == null) throw new ApiException($"Printer Not Found.");
                return new Response<Printer>(printer);
            }
        }
    }
}