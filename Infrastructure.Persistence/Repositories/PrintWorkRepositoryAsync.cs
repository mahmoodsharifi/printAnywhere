// Copyright © [insert list or range of years of product releases for this product] VMware, Inc. All rights reserved.
// This product is protected by copyright and intellectual property laws in the United States and other countries as well as by international treaties.
// VMware products are covered by one or more patents listed at http://www.vmware.com/go/patents

using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Persistence.Contexts;
using Infrastructure.Persistence.Repository;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class PrintWorkRepositoryAsync : GenericRepositoryAsync<PrintWork>, IPrintWorkRepositoryAsync
    {
        private readonly DbSet<PrintWork> _printWorks;
        
        public PrintWorkRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _printWorks = dbContext.Set<PrintWork>();
        }
    }
}