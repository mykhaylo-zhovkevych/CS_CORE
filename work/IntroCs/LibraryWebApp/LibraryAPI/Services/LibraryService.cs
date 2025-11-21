using ConsoleApp5._4Remastered;
using ConsoleApp5._4Remastered.Storage;
using ConsoleApp5._4Remastered.Enum;
using ConsoleApp5._4Remastered.HelperClasses;
using LibraryAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq;
using ConsoleApp5._4Remastered.Interfaces;

namespace LibraryAPI.Service
{
    public class LibraryService
    {
        private readonly InMemoryRepository _inMemoryRepository;

        public LibraryService(InMemoryRepository inMemoryRepository)
        {
            _inMemoryRepository = inMemoryRepository;
        }

        public bool TryAddUser(CreateUserDto dto, out User? user)
        {
            // If user exists allready, return false and that user that exist
            var existing = _inMemoryRepository.GetExistingUser(dto.Name, dto.UserType);

            if (existing != default)
            {
                user = null;
                return false;
            }

            user = new User(dto.Name, dto.UserType);
            _inMemoryRepository.AddUserToInMemory(user);
            return true;
        }


        public bool TryAddItem(CreateItemDto dto, out Item? item)
        {
            var existing = _inMemoryRepository.GetExistingItem(dto.Name, dto.ItemType);

            if (existing != default)
            {
                item = null;
                return false;
            }

            var shelf = _inMemoryRepository.GetShelfById(1000);
            item = new Item(dto.Name, dto.ItemType);
            shelf.AddItemToShelf(item);
            return true;
        }

        public (bool Success, string Message) TryBorrowItem(BorrowItemDto dto)
        {
            var userData = _inMemoryRepository.GetPossibleBorrowing(dto.UserId, dto.ItemId);

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

            var result = _inMemoryRepository.AddBorrowing(userData.Item2, userData.Item1);
            return result;
        }

        public bool TryGetValueOfBorrowings(Guid userId, out List<Borrowing> borrowings)
        {
            var existing = _inMemoryRepository.GetActiveBorrowings(userId);

            if (existing.Count == 0)
            {
                borrowings = null;
                return false;
            }

            borrowings = existing.ToList();
            return true;
        }

        public bool TryUpdateUserProfile(Guid id, ChangeProfileDto dto, out User? updatedUser)
        {
            var existing = _inMemoryRepository.GetExistingUserById(id);

            if (existing == default)
            {
                updatedUser = null;
                return false;
            }

            _inMemoryRepository.UpdateUserProfile(existing.Id, dto.UserType);
            updatedUser = existing;

            return true;
        }

        public bool TryGetValueOfUser(Guid id, out User? user)
        {
            var existing = _inMemoryRepository.GetExistingUserById(id);

            if (existing == default)
            {
                user = null;
                return false;
            }

            user = existing;
            return true;
        }
    }
}
