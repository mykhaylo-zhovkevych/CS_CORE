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
        private readonly UserManagmentDepartment _department;

        public LibraryService(Library library)
        {
            _library = library;
            _department = new UserManagmentDepartment(_library);
        }

        public bool TryAddUser(CreateUserDto dto, out User? user)
        {
            // If user exists allready, return false and that user that exist
            var existing = _library.GetExistingUser(dto.Name, dto.UserType);

            if (existing != default)
            {
                user = existing;
                return false;
            }

            user = new User(dto.Name, dto.UserType);
            _library.Users.Add(user);
            return true;
        }


        public bool TryAddItem(CreateItemDto dto, out Item? item)
        {
            var existing = _library.GetExistingItem(dto.Name, dto.ItemType);

            if (existing != default)
            {
                item = existing;
                return false;
            }

            var shelf = _library.Shelves.FirstOrDefault(s => s.ShelfId == 1000);
            if (shelf == default)
            {
                shelf = new Shelf(1000);
                _library.Shelves.Add(shelf);
            }

            item = new Item(dto.Name, dto.ItemType);
            shelf.AddItemToShelf(item);
            return true;
        }

        public (bool Success, string Message) TryBorrowItem(BorrowItemDto dto)
        {
            var userData = _library.GetPossibleBorrowing(dto.UserId, dto.ItemId);

            if (userData.Item1 == default)
            {
                return (false, "Item not found");
            }
            else if (userData.Item2 == default)
            {
                return (false, "User not found");
            }

            else if (userData == default)
            {
                return (false, "Item is already borrowed");
            }

            var result = _library.BorrowItem(userData.Item2, userData.Item1);
            return result;
        }

        public bool TryGetValueOfBorrowings(Guid userId, out List<Borrowing> borrowings)
        {
            var existing = _library.GetActiveBorrowings(userId);

            if (existing.Count == 0)
            {
                borrowings = existing;
                return false;
            }

            borrowings = [.. existing];
            return true;
        }

        public bool TryUpdateUserProfile(Guid id, ChangeProfileDto dto, out User? updatedUser)
        {
            var existing = _library.Users.FirstOrDefault(u => u.Id == id);

            if (existing.Id == Guid.Empty)
            {
                updatedUser = existing;
                return false;
            }

            updatedUser = _department.UpdateUserProfile(existing, dto.UserType);
            return true;
        }
    }
}
