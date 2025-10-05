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
    public class AuthService(IUnitOfWork unitOfWork, IPasswordHasher<User> hasher) : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IPasswordHasher<User> _hasher = hasher;

        public async Task<Result<UserResponseDto>> LoginAsync(AuthRequestDto dto)
        {
            var user = await _unitOfWork.Users.GetByEmailAsync(dto.Email);
            if (user == null)
                return Result<UserResponseDto>.Failure(ErrorType.Unauthorized, "Invalid Email or Password");

            var passwordValid = _hasher.VerifyHashedPassword(user, user.Password, dto.Password);
            if(passwordValid != PasswordVerificationResult.Success)
                return Result<UserResponseDto>.Failure(ErrorType.Unauthorized, "Invalid Email or Password");

            var response = user.UserMapToDto();

            return Result<UserResponseDto>.Success(response);
        }
    }
}
