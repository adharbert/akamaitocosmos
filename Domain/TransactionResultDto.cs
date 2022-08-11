﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class TransactionResultDto
    {
        public int SourceRecordCount { get; set; }
        public int DestinationCount { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}
