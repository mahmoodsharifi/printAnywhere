// Copyright © [insert list or range of years of product releases for this product] VMware, Inc. All rights reserved.
// This product is protected by copyright and intellectual property laws in the United States and other countries as well as by international treaties.
// VMware products are covered by one or more patents listed at http://www.vmware.com/go/patents

using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Exceptions;
using Application.Interfaces.Repositories;
using Application.Wrappers;
using Domain.Entities;
using MediatR;

namespace Application.Features.PrintWorks.Commands.UpdatePrintWork
{
    public class UpdatePrintWorkCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public Printer Printer { get; set; }
        public DateTime PaidWhen { get; set; }
        public DateTime PrintStartWhen { get; set; }
        public DateTime WorkFinishWhen { get; set; }

        public class UpdatePrintWorkCommandHandler : IRequestHandler<UpdatePrintWorkCommand, Response<int>>
        {
            private readonly IPrintWorkRepositoryAsync _printWorkRepository;
            
            public UpdatePrintWorkCommandHandler(IPrintWorkRepositoryAsync printWorkRepository)
            {
                _printWorkRepository = printWorkRepository;
            }
            
            public async Task<Response<int>> Handle(UpdatePrintWorkCommand command, CancellationToken cancellationToken)
            {
                var printWork = await _printWorkRepository.GetByIdAsync(command.Id);

                if (printWork == null)
                {
                    throw new ApiException($"PrintWork Not Found.");
                }
                else
                {
                    printWork.Printer = command.Printer;
                    printWork.FileName = command.FileName;
                    printWork.PaidWhen = command.PaidWhen;
                    printWork.PrintStartWhen = command.PrintStartWhen;
                    printWork.WorkFinishWhen = command.WorkFinishWhen;
                    
                    await _printWorkRepository.UpdateAsync(printWork);
                    return new Response<int>(printWork.Id);
                }
            }
        }
    }
}