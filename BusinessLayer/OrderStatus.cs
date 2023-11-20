using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public enum OrderStatus
    {
        New = 0,
        InProgress = 1,
        Sent = 2,
        Customs = 3,
        Completed = 4
    }
}
