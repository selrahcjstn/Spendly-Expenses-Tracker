using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Spendly.Application.Common.Enums;
using Spendly.Application.Common.Mappings;
using Spendly.Application.Common.Result;
using Spendly.Application.Dtos.Auth;
using Spendly.Application.Interfaces.IRepositories;
using Spendly.Application.Interfaces.IServices;
using Spendly.Domain.Entities;

namespace Spendly.Application.Services
{
    public class AuthService(
        IUnitOfWork unitOfWork,
        IPasswordHasher<User> hasher,
        IJwtService jwtService,
        IConfiguration configuration
    ) : IAuthService
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
            if (passwordValid != PasswordVerificationResult.Success)
                return Result<AuthResponseDto>.Failure(ErrorType.Unauthorized, "Invalid Email or Password");

            var accessTokenValue = _jwtService.GenerateToken(user);
            var refreshTokenValue = _jwtService.GenerateRefreshToken();

            var refreshToken = new RefreshToken
            {
                User = user,
                Token = refreshTokenValue,
                ExpiresAt = DateTime.UtcNow.AddDays(7)
            };

            await _unitOfWork.RefreshTokens.AddAsync(refreshToken);

            var accessTokenExpiresAt = DateTime.UtcNow.AddMinutes(
                double.Parse(_configuration["Jwt:DurationInMinutes"]!)
            );

            var response = user.MapToAuthResponse(
                accessTokenValue,
                accessTokenExpiresAt,
                refreshTokenValue,
                refreshToken.ExpiresAt
            );

            return Result<AuthResponseDto>.Success(response);
        }

        public async Task<Result<AuthResponseDto>> RefreshTokenAsync(RefreshTokenRequestDto dto)
        {
            var principal = _jwtService.GetPrincipalFromExpiredToken(dto.AccessToken);
            if (principal == null)
                return Result<AuthResponseDto>.Failure(ErrorType.Unauthorized, "Invalid access token");

            var userIdClaim = principal.Claims.FirstOrDefault(c => c.Type == "sub");
            if (userIdClaim == null)
                return Result<AuthResponseDto>.Failure(ErrorType.Unauthorized, "Invalid token payload");

            var userId = Guid.Parse(userIdClaim.Value);
            var user = await _unitOfWork.Users.GetByIdAsync(userId);

            if (user == null)
                return Result<AuthResponseDto>.Failure(ErrorType.NotFound, "User not found");

            var storedToken = await _unitOfWork.RefreshTokens.GetByTokenAsync(dto.RefreshToken);

            if (storedToken == null || storedToken.UserId != user.Id)
                return Result<AuthResponseDto>.Failure(ErrorType.Unauthorized, "Invalid refresh token");

            if (storedToken.ExpiresAt < DateTime.UtcNow)
                return Result<AuthResponseDto>.Failure(ErrorType.Unauthorized, "Refresh token expired");

            var newAccessToken = _jwtService.GenerateToken(user);
            var newRefreshTokenValue = _jwtService.GenerateRefreshToken();

            storedToken.Revoke();
            var newRefreshToken = new RefreshToken(user, newRefreshTokenValue);


            await _unitOfWork.RefreshTokens.AddAsync(newRefreshToken);
            await _unitOfWork.SaveChangesAsync();

            var newAccessTokenExpiresAt = DateTime.UtcNow.AddMinutes(
                double.Parse(_configuration["Jwt:DurationInMinutes"]!)
            );

            var response = user.MapToAuthResponse(
                newAccessToken,
                newAccessTokenExpiresAt,
                newRefreshTokenValue,
                newRefreshToken.ExpiresAt
            );

            return Result<AuthResponseDto>.Success(response);
        }

    }
}
