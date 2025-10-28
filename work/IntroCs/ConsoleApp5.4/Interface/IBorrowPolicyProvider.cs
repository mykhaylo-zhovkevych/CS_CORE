using ConsoleApp5._4    ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp5._4.Interface
{
    // init acccessor 
    public record BorrowPolicy(int LoanPeriod) { }

    public interface IBorrowPolicyProvider
    {
        BorrowPolicy GetPolicy(User user, Item item);
    }
}
