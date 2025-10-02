using Spendly.Application.Dtos;
using Spendly.Application.Interfaces.IRepositories;
using Spendly.Application.Interfaces.IServices;
using Spendly.Application.Mappings;
using Spendly.Domain.Entities;

namespace Spendly.Application.Services
{
    public class UserService(IUnitOfWork unitOfWork) : IUserService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<UserResponseDto> AddAsync(UserCreateRequestDto dto)
        {
            if (await _unitOfWork.Users.GetByUserAsync(dto.Username) is not null)
                throw new ArgumentException("Username already exists!");

            if (await _unitOfWork.Users.GetByEmailAsync(dto.Email) is not null)
                throw new ArgumentException("Email already exists!");

            var newUser = new User(dto.Username, dto.Email, dto.Password);

            await _unitOfWork.Users.AddAsync(newUser);
            await _unitOfWork.SaveChangesAsync();

            var response = new UserResponseDto();

            return response;
        }

        public async Task<UserResponseDto> GetByIdAsync(Guid id)
        {
            var existingUser = await _unitOfWork.Users.GetByIdAsync(id)
                ?? throw new KeyNotFoundException($"User with id of '{id}' was not found");

            var response = existingUser.UserMapToDto();

            return response;
        }

        public async Task<UserResponseDto> UpdateEmailAsync(UpdateEmailRequestDto dto)
        {
            if(await _unitOfWork.Users.GetByEmailAsync(dto.Email) is not null)
                throw new ArgumentException("Existing email");

            var user = await _unitOfWork.Users.GetByIdAsync(dto.Id) 
                ?? throw new KeyNotFoundException("User not found");

            user.UpdateEmail(dto.Email);

            await _unitOfWork.SaveChangesAsync();

            var response = user.UserMapToDto();

            return response;
        }
    }
}
