﻿// Copyright © [insert list or range of years of product releases for this product] VMware, Inc. All rights reserved.
// This product is protected by copyright and intellectual property laws in the United States and other countries as well as by international treaties.
// VMware products are covered by one or more patents listed at http://www.vmware.com/go/patents

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Printer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public int PrintQuotaPerDay { get; set; }
        public int RemainingQuotaForTheDay { get; set; }
        public string AvailabilityCron { get; set; }
    }
}