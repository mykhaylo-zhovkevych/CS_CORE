using ConsoleApp5._4Remastered;
using ConsoleApp5._4Remastered;
using ConsoleApp5._4Remastered.Data;
using ConsoleApp5._4Remastered.Data;
using ConsoleApp5._4Remastered.Enum;
using ConsoleApp5._4Remastered.HelperClasses;
using LibraryAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq;

namespace LibraryAPI.Service
{
    public class LibraryService
    {
        private readonly Library _library;

        public LibraryService(Library library)
        {
            _library = library;
        }

        public bool UserExists(string name, UserType userType)
        {
            return _library.Users.Any(u => u.Name == name && u.UserType == userType);
        }

        public User AddUser(CreateUserDto dto)
        {
            var user = new User(dto.Name, dto.UserType);
            _library.Users.Add(user);
            return user;
        }

        public bool ItemExists(string name, ItemType itemType)
        {
            return _library.Shelves.Any(s => s.Items.Any(i => i.Name == name && i.ItemType == itemType));
        }

        public Item AddItem(CreateItemDto dto)
        {
            var shelf = _library.Shelves.FirstOrDefault(s => s.ShelfId == 1000);
            if (shelf == null)
            {
                shelf = new Shelf(1000);
                _library.Shelves.Add(shelf);
            }

            var item = new Item(dto.Name, dto.ItemType);
            shelf.AddItemToShelf(item);
            return item;
        }

        public bool BorrowingIsPossible(Guid userId, Guid itemId)
        {
            var user = _library.Users.FirstOrDefault(u => u.Id == userId);
            var item = _library.Shelves.SelectMany(s => s.Items).FirstOrDefault(i => i.Id == itemId);

            if (user == null || item == null || item.IsBorrowed)
            {
                return false;
            }

            return true;
        }

        public (bool Success, string Message) BorrowItem(BorrowItemDto dto)
        {
            var user = _library.Users.FirstOrDefault(u => u.Id == dto.UserId);
            var item = _library.Shelves.SelectMany(s => s.Items).FirstOrDefault(i => i.Id == dto.ItemId);

            if (user == null)
            {
                return (false, "User not found");
            }
            else if (item == null)
            {
                return (false, "Item not found");
            }

            else if (item.IsBorrowed)
            {
                return (false, "Item is already borrowed");
            }

            var result = _library.BorrowItem(user, item);
            return result;
        }

        public List<Borrowing> GetActiveBorrowingsForUser(Guid userId)
        {
            return _library.Borrowings
                .Where(b => b.User.Id == userId && !b.IsReturned)
                .ToList();
        }
    }
}
