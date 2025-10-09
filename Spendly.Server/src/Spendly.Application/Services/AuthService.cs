using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Spendly.Application.Common.Enums;
using Spendly.Application.Common.Mappings;
using Spendly.Application.Common.Result;
using Spendly.Application.Dtos.Auth;
using Spendly.Application.Dtos.User;
using Spendly.Application.Interfaces.IRepositories;
using Spendly.Application.Interfaces.IServices;
using Spendly.Domain.Entities;

namespace Spendly.Application.Services
{
    public class AuthService(IUnitOfWork unitOfWork, IPasswordHasher<User> hasher, IJwtService jwtService, IConfiguration configuration) : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IPasswordHasher<User> _hasher = hasher;
        private readonly IJwtService _jwtService = jwtService;
        private readonly IConfiguration _configuration = configuration;

        public async Task<Result<AuthResponseDto>> LoginAsync(AuthRequestDto dto)
        {
            var user = await _unitOfWork.Users.GetByEmailAsync(dto.Email);
            if (user == null)
                return Result<AuthResponseDto>.Failure(ErrorType.Unauthorized, "Invalid Email or Password");

            var passwordValid = _hasher.VerifyHashedPassword(user, user.Password, dto.Password);
            if(passwordValid != PasswordVerificationResult.Success)
                return Result<AuthResponseDto>.Failure(ErrorType.Unauthorized, "Invalid Email or Password");

            var token = _jwtService.GenerateToken(user);
            var exp = DateTime.UtcNow.AddMinutes(double.Parse(_configuration["Jwt:DurationInMinutes"]!));

            var response = user.MapToAuthResponse(token, exp);

            return Result<AuthResponseDto>.Success(response);
        }
    }
}
