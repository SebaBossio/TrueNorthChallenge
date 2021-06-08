﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrueNorthChallenge.Contracts
{
    public interface IResponseDTO
    {
        bool Success { get; set; }
        string Message { get; set; }
        object Data { get; set; }
    }
}
