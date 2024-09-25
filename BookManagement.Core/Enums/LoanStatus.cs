using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManagement.Core.Enums;
public enum LoanStatus
{
    Loaned = 1,
    Returned = 2,
    Late = 3,
    Canceled = 4
}
