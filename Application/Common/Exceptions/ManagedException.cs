using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common.Exceptions
{
    public class ManagedException : Exception
    {

        public ManagedException(string message)
            : base("Error occured : " + message)
        {
        }

    }
}
