using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientRegistrations.Domain.Helpers
{
    public class DomainException : Exception
    {
        public DomainException(string error) : base(error) { }

        public static void When(bool valid, string error)
        {
            if (valid)
                throw new DomainException(error);
        }

    }
}
