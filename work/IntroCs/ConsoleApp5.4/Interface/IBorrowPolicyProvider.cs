using ConsoleApp5._2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp5._4.Interface
{
    // As a help class that is used for logic
    public record BorrowPolicy(int LoanPeriod, decimal Fees, int Extensions);

    public interface IBorrowPolicyProvider
    {
        BorrowPolicy GetPolicy(User user, Item item);
    }
}
