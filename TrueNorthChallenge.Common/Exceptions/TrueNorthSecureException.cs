using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrueNorthChallenge.Common.Exceptions
{
    public class TrueNorthSecureException : Exception
    {
        public TrueNorthSecureException(string message) : base(message) { }
    }
}
