using ConsoleApp5._4Remastered.Enum;
using ConsoleApp5._4Remastered.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp5._4Remastered.Interfaces
{
    public interface IItemRepository
    {

        Item? GetExistingItem(string name, ItemType itemType);

        Shelf? GetShelfById(int id);

    }
}
