using Application.Features.Products.Commands.CreateProduct;
using Application.Features.Products.Queries.GetAllProducts;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Application.Features.Printers.Commands.CreatePrinter;
using Application.Features.Printers.Queries.GetAllPrinters;
using Application.Features.PrintWorks.Commands.CreatePrintWork;

namespace Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            // Product mapping
            CreateMap<Product, GetAllProductsViewModel>().ReverseMap();
            CreateMap<CreateProductCommand, Product>();
            CreateMap<GetAllProductsQuery, GetAllProductsParameter>();
            
            // Printer mapping
            CreateMap<Printer, GetAllPrintersViewModel>().ReverseMap();
            CreateMap<CreatePrinterCommand, Printer>();
            CreateMap<GetAllPrintersQuery, GetAllPrintersParameter>();
            
            // PrintWork mapping
            CreateMap<CreatePrintWorkCommand, PrintWork>();
        }
    }
}
