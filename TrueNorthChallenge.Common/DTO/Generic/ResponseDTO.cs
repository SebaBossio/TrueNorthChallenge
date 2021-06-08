using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrueNorthChallenge.Contracts;

namespace TrueNorthChallenge.Common.DTO.Generic
{
    public class ResponseDTO<T> : IResponseDTO
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        object IResponseDTO.Data { get; set; }
        public T Data { get; set; }
    }
}
