using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs.Users;
using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;



namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task RegisterAsync(UserRegisterDto dto)
        {
            var user = new User
            {
                FullName = dto.FullName,
                Email = dto.Email,
                PasswordHash = dto.Password,
                Role = Enum.Parse<UserRole>(dto.Role, true)
            };

            await _unitOfWork.Users.AddAsync(user);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<UserDto> LoginAsync(UserLoginDto dto)
        {
            var user = await _unitOfWork.Users.GetByEmailAsync(dto.Email);
            if (user == null || !CheckPassword(dto.Password, user.PasswordHash))
                throw new Exception("Invalid Email Or Password");
            return MapToDto(user);
        }

        public async Task<IEnumerable<UserDto>> GetAllAsync()
        {
            var users = await _unitOfWork.Users.GetAllAsync();
            return users.Select(MapToDto);
        }

        public async Task<UserDto> GetByIdAsync(Guid id)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(id);
            if (user == null) throw new Exception("User Not Found");
            return MapToDto(user);
        }

        public async Task DeleteAsync(Guid id)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(id);
            if (user == null) throw new Exception("User Not Found");
            _unitOfWork.Users.Delete(user);
            await _unitOfWork.CompleteAsync();
        }

        private string HashPassword(string password)
        => Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(password));

        private bool CheckPassword(string plain, string hashed)
        => HashPassword(plain) == hashed;

        private UserDto MapToDto(User user) => new UserDto
        {
            Id = user.Id,
            FullName = user.FullName,
            Email = user.Email,
            Role = user.Role.ToString()
        };
    }
}
