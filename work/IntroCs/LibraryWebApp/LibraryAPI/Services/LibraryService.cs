using ConsoleApp5._4Remastered;
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

        // Try Methode, attemp not to pass null value but make it with `out` somehow 

        //public (bool, User?) TryAddUser(CreateUserDto dto)
        //{

        //    if (_library.UserExists(dto.Name, dto.UserType))
        //    {
        //        return (false, null);
        //    }

        //    var user = new User(dto.Name, dto.UserType);
        //    _library.Users.Add(user);
        //    return (true, user);
        //}


        public bool TryAddUser(CreateUserDto dto, out User? user)
        {
            // If user exists allready, return false and that user that exist
            var existing = _library.UserExists(dto.Name, dto.UserType);

            if (existing != null)
            {
                user = existing;
                return false;
            }

            user = new User(dto.Name, dto.UserType);
            _library.Users.Add(user);
            return true;
        }


        public (bool, Item?) AddItem(CreateItemDto dto)
        {

            if (_library.ItemExists(dto.Name, dto.ItemType))
            {
                return (false, null);
            }

            var shelf = _library.Shelves.FirstOrDefault(s => s.ShelfId == 1000);
            if (shelf == null)
            {
                shelf = new Shelf(1000);
                _library.Shelves.Add(shelf);
            }

            var item = new Item(dto.Name, dto.ItemType);
            shelf.AddItemToShelf(item);
            return (true, item);
        }

        //public bool BorrowingIsPossible(Guid userId, Guid itemId)
        //{
        //    var user = _library.Users.FirstOrDefault(u => u.Id == userId);
        //    var item = _library.Shelves.SelectMany(s => s.Items).FirstOrDefault(i => i.Id == itemId);

        //    if (user == null || item == null || item.IsBorrowed)
        //    {
        //        return false;
        //    }

        //    return true;
        //}



        //public (bool Success, string Message) BorrowItem(BorrowItemDto dto)
        //{
        //    var user = _library.Users.FirstOrDefault(u => u.Id == dto.UserId);
        //    var item = _library.Shelves.SelectMany(s => s.Items).FirstOrDefault(i => i.Id == dto.ItemId);

        //    if (user == null)
        //    {
        //        return (false, "User not found");
        //    }
        //    else if (item == null)
        //    {
        //        return (false, "Item not found");
        //    }

        //    else if (item.IsBorrowed)
        //    {
        //        return (false, "Item is already borrowed");
        //    }

        //    var result = _library.BorrowItem(user, item);
        //    return result;
        //}

        // temp disabled

        //public (bool Success, string Message) BorrowItem(BorrowItemDto dto)
        //{
        //    var userData = BorrowingIsPossible(dto.UserId, dto.ItemId);

        //    if (userData.Item1 == null)
        //    {
        //        return (false, "User not found");
        //    }
        //    else if (userData.Item2 == null)
        //    {
        //        return (false, "Item not found");
        //    }

        //    else if (userData == default)
        //    {
        //        return (false, "Item is already borrowed");
        //    }

        //    var result = _library.BorrowItem(userData.Item2, userData.Item1);
        //    return result;
        //}


    }
}
