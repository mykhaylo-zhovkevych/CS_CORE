using ConsoleApp5._4Remastered.HelperClasses;
using ConsoleApp5._4Remastered.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp5._4Remastered.Interfaces
{
    public interface IBorrowingsRepository
    {

        (Item?, User?) GetPossibleBorrowing(Guid userId, Guid itemId);

        List<Borrowing> GetActiveBorrowings(Guid userId);

        (bool Success, string Message) AddBorrowing(User user, Item item);

    }
}
