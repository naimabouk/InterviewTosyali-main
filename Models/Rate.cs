using System;
using System.Collections.Generic;

#nullable disable

namespace JobInterview.Models
{
    public partial class Rate
    {
        public int Id { get; set; }
        public double? From { get; set; }
        public double? To { get; set; }
        public string Date { get; set; }
        public string Currency { get; set; }
    }
}
