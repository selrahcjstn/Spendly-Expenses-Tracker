using FluentValidation;
using FluentValidation.Results;
using Spendly.Application.Common.Enums;
using Spendly.Application.Common.Mappings;
using Spendly.Application.Common.Result;
using Spendly.Application.Dtos;
using Spendly.Application.Interfaces.IRepositories;
using Spendly.Application.Interfaces.IServices;
using Spendly.Domain.Entities;

namespace Spendly.Application.Services
{
    public class UserService(IUnitOfWork unitOfWork, IValidator<CreateUserRequestDto> validator) : IUserService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IValidator<CreateUserRequestDto> _validator = validator;

        public async Task<Result<UserResponseDto>> AddAsync(CreateUserRequestDto dto)
        {
            // DTO Validation
            var result = await _validator.ValidateAsync(dto);
            if (!result.IsValid)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.ErrorMessage));
                return Result<UserResponseDto>.Failure(ErrorType.BadRequest, errors);
            }

            // Repository Validation
            if (await _unitOfWork.Users.GetByUserAsync(dto.Username) is not null)
                return Result<UserResponseDto>.Failure(ErrorType.Conflict, "Username already exist!");

            if (await _unitOfWork.Users.GetByEmailAsync(dto.Email) is not null)
                return Result<UserResponseDto>.Failure(ErrorType.Conflict, "Email already exist!");

            // Dto to Entity Mapping with domain method
            var newUser = new User(dto.Username, dto.Email, dto.Password);

            // Saving to database using repository and UnitOfWork
            await _unitOfWork.Users.AddAsync(newUser);
            await _unitOfWork.SaveChangesAsync();

            // Mapping back to DTO for the response
            var response = newUser.UserMapToDto();

            // Return response to the controller
            return Result<UserResponseDto>.Success(response);
        }

        public async Task<Result<UserResponseDto>> GetByIdAsync(Guid id)
        {
           var existingUser = await _unitOfWork.Users.GetByIdAsync(id);
           if(existingUser == null)
                return Result<UserResponseDto>.Failure(ErrorType.NotFound, "User Not Found");

            var response = existingUser.UserMapToDto();

            return Result<UserResponseDto>.Success(response);
        }

        public async Task<Result<UserResponseDto>> UpdateEmailAsync(UpdateEmailRequestDto dto)
        {
            var result = await _unitOfWork.Users.GetByEmailAsync(dto.Email);
            if (result != null)
                return Result<UserResponseDto>.Failure(ErrorType.Conflict, "Email address already exist");

            var user = await _unitOfWork.Users.GetByIdAsync(dto.Id);
            if (user == null)
                return Result<UserResponseDto>.Failure(ErrorType.NotFound, "User not found!");

            user.UpdateEmail(dto.Email);

            await _unitOfWork.SaveChangesAsync();

            var response = user.UserMapToDto();

            return Result<UserResponseDto>.Success(response);
        }
    }
}
