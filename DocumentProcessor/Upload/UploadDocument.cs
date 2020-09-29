using System;
using System.Collections.Generic;
using DocumentProcessor.Interfaces;
using Microsoft.AspNetCore.Http;

namespace DocumentProcessor
{
    public class UploadDocument : IUploadDocument
    {
        public bool Upload(List<IFormFile> files)
        {
            
        }
    }
}