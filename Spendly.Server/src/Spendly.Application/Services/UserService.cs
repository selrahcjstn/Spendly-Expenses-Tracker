using Microsoft.AspNetCore.Identity;
using Spendly.Application.Common.Enums;
using Spendly.Application.Common.Mappings;
using Spendly.Application.Common.Result;
using Spendly.Application.Dtos.User;
using Spendly.Application.Interfaces.IRepositories;
using Spendly.Application.Interfaces.IServices;
using Spendly.Domain.Entities;

namespace Spendly.Application.Services
{
    public class UserService(IUnitOfWork unitOfWork, IPasswordHasher<User> hasher) : IUserService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IPasswordHasher<User> _hasher = hasher;

        public async Task<Result<CreateUserResponseDto>> AddAsync(CreateUserRequestDto dto)
        {
            var existingUser = await _unitOfWork.Users.GetByEmailOrUserAsync(dto.Username, dto.Email);

            if (existingUser?.Username == dto.Username)
                return Result<CreateUserResponseDto>.Failure(ErrorType.Conflict, "Username already exists!");

            if (existingUser?.Email == dto.Email)
                return Result<CreateUserResponseDto>.Failure(ErrorType.Conflict, "Email already exists!");

            var user = new User(dto.Username, dto.Email, "");
            user.Password = _hasher.HashPassword(user, dto.Password);

            var profile = new Profile(dto.Firstname, dto.LastName, dto.MiddleName, dto.Sex, dto.BirthDate);

            user.AddProfile(profile);

            await _unitOfWork.Users.Add(user);
            await _unitOfWork.SaveChangesAsync();

            return Result<CreateUserResponseDto>.Success(user.UserMapToDto());
        }

        public async Task<Result<UserResponseDto>> GetByIdAsync(Guid id)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(id);

            if (user == null)
                return Result<UserResponseDto>.Failure(ErrorType.NotFound, "User not found");

            return Result<UserResponseDto>.Success(user.MapToUserDto());
        }

        public async Task<Result<UserResponseDto>> UpdateEmailAsync(UpdateEmailRequestDto dto)
        {
            var emailExists = await _unitOfWork.Users.GetByEmailAsync(dto.Email);
            if (emailExists != null)
                return Result<UserResponseDto>.Failure(ErrorType.Conflict, "Email address already exists");

            var user = await _unitOfWork.Users.GetByIdAsync(dto.Id);
            if (user == null)
                return Result<UserResponseDto>.Failure(ErrorType.NotFound, "User not found");

            user.UpdateEmail(dto.Email);

            await _unitOfWork.Users.Update(user);
            await _unitOfWork.SaveChangesAsync();

            return Result<UserResponseDto>.Success(user.MapToUserDto());
        }
    }
}

