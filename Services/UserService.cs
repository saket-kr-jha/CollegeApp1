﻿using AutoMapper;
using DotNetCore_New.Data.Repository;
using DotNetCore_New.Data;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using DotNetCore_New.Model;

namespace DotNetCore_New.Services
{
    public class UserService: IUserService
    {
        private readonly IMapper _mapper;
        private readonly ICollegeRepository<User> _userRepository;
        public UserService(ICollegeRepository<User> userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<bool> CreateUserAsync(UserDTO userDTO)
        {
            ArgumentNullException.ThrowIfNull(userDTO, nameof(userDTO));
            var existingUser = await _userRepository.GetAsync(x => x.Username == userDTO.Username);
            if (existingUser != null)
            {
                throw new Exception("User already exists");
            }
            User user = _mapper.Map<User>(userDTO);
            user.CreatedDate = DateTime.Now;
            user.ModifiedDate = DateTime.Now;
            user.IsDeleted = false;
            if(!string.IsNullOrEmpty(userDTO.Password))
            {
                var passwordHashWithSalt = CreatePasswordHashWithSalt(userDTO.Password);
                user.Password = passwordHashWithSalt.PasswordHash;
                user.PasswordSalt = passwordHashWithSalt.Salt;
            }
            await _userRepository.CreateAsync(user);
            return true;
        }

        public async Task<List<UserReadOnlyDTO>> GetUsersAsync()
        {
            var users = await _userRepository.GetAllAsync();
            return _mapper.Map<List<UserReadOnlyDTO>>(users);
        }

        public async Task<UserReadOnlyDTO> GetUserByIdAsync(int id)
        {
            var user = await _userRepository.GetAsync(u => u.Id == id);
            return _mapper.Map<UserReadOnlyDTO>(user);
        }

        public async Task<UserReadOnlyDTO> GetUserByNameAsync(string name)
        {
            var user = await _userRepository.GetAsync(u => u.Username.Equals(name));
            return _mapper.Map<UserReadOnlyDTO>(user);
        }
        public async Task<bool> UpdateUserAsync(UserDTO userDTO)
        {
            ArgumentNullException.ThrowIfNull(userDTO, nameof(userDTO));
            var existingUser = await _userRepository.GetAsync(x => x.Id == userDTO.Id, true);
            if (existingUser == null)
            {
                throw new Exception("User does not exist");
            }
            User userToUpdate = _mapper.Map<User>(userDTO);
            userToUpdate.ModifiedDate = DateTime.Now;
            await _userRepository.UpdateAsync(userToUpdate);
            return true;
        }

        public async Task<bool> UpdateUserPasswordAsync(int id, string password)
        {
            var user = await _userRepository.GetAsync(x => x.Id == id);
            if (user == null)
            {
                throw new Exception("User does not exist");
            }
            if(string.IsNullOrEmpty(password))
            {
                throw new Exception("Password cannot be empty");
            }
            var passwordHashWithSalt = CreatePasswordHashWithSalt(password);
            user.Password = passwordHashWithSalt.PasswordHash;
            user.PasswordSalt = passwordHashWithSalt.Salt;
            await _userRepository.UpdateAsync(user);
            return true;
        }
        public async Task<bool> DeleteUserAsync(int id)
        {
            if (id <= 0)
            {
                throw new Exception("Invalid user id");
            }
            var user = await _userRepository.GetAsync(x => x.Id == id);
            if (user == null)
            {
                throw new Exception("User does not exist");
            }
            user.IsDeleted = true;
            await _userRepository.UpdateAsync(user);
            return true;
        }
        public (string PasswordHash, string Salt) CreatePasswordHashWithSalt(string password)
        {
            var salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            var hash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));
            return (hash, Convert.ToBase64String(salt));
        }
    }
}
