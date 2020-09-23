// Copyright © [insert list or range of years of product releases for this product] VMware, Inc. All rights reserved.
// This product is protected by copyright and intellectual property laws in the United States and other countries as well as by international treaties.
// VMware products are covered by one or more patents listed at http://www.vmware.com/go/patents

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces.Repositories;
using Application.Wrappers;
using AutoMapper;
using MediatR;

namespace Application.Features.Printers.Queries.GetAllPrinters
{
    public class GetAllPrintersQuery : IRequest<PagedResponse<IEnumerable<GetAllPrintersViewModel>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }   
    }

    public class GetAllPrintersQueryHandler : IRequestHandler<GetAllPrintersQuery, PagedResponse<IEnumerable<GetAllPrintersViewModel>>>
    {
        private readonly IPrinterRepositoryAsync _printerRepository;
        private readonly IMapper _mapper;
        public GetAllPrintersQueryHandler(IPrinterRepositoryAsync printerRepository, IMapper mapper)
        {
            _printerRepository = printerRepository;
            _mapper = mapper;
        }

        public async Task<PagedResponse<IEnumerable<GetAllPrintersViewModel>>> Handle(GetAllPrintersQuery request, CancellationToken cancellationToken)
        {
            var validFilter = _mapper.Map<GetAllPrintersParameter>(request);
            var printer = await _printerRepository.GetPagedReponseAsync(validFilter.PageNumber,validFilter.PageSize);
            var printerViewModel = _mapper.Map<IEnumerable<GetAllPrintersViewModel>>(printer);
            return new PagedResponse<IEnumerable<GetAllPrintersViewModel>>(printerViewModel, validFilter.PageNumber, validFilter.PageSize);
        }
    }
}