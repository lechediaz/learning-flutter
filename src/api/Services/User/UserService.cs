using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using api.Data.Repositories;
using api.Models.Dtos;
using api.Models.Entities;
using api.Services.Base;
using AutoMapper;

namespace api.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
        }

        private ServiceResult ValidateFields(User user)
        {
            var result = new ServiceResult();

            if (user == null || (user != null &&
                string.IsNullOrEmpty(user?.Name) || string.IsNullOrEmpty(user?.UserName))
            )
            {
                result.Message = "Information not supplied.";
                return result;
            }

            if (user.Name.Length > 60)
            {
                result.Message = "The Name can have 60 characters only.";
                return result;
            }

            if (user.UserName.Length > 20)
            {
                result.Message = "The UserName can have 20 characters only.";
                return result;
            }

            result.Ok = true;

            return result;
        }

        public async Task<ServiceResult<UserDto>> AddAsync(CreateUserDto createUserDto)
        {
            var result = new ServiceResult<UserDto>();

            if (createUserDto is null)
            {
                result.Message = "Information not supplied";
                return result;
            }

            User user = mapper.Map<User>(createUserDto);
            ServiceResult fieldsValidation = ValidateFields(user);

            if (!fieldsValidation.Ok)
            {
                result.Message = fieldsValidation.Message;
                return result;
            }

            ServiceResult userNameValidation = await ValidateUserNameAsync(user.UserName);

            if (!userNameValidation.Ok)
            {
                result.Message = userNameValidation.Message;
                return result;
            }

            try
            {
                await userRepository.AddAsync(user);

                result.Extras = mapper.Map<UserDto>(user);
                result.Ok = true;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }

            return result;
        }

        public async Task<ServiceResult> DeleteAsync(int id)
        {
            var result = new ServiceResult();

            bool exists = await userRepository.ExistsAsync(p => p.Id == id);

            if (!exists)
            {
                result.Message = "User not found.";
                return result;
            }

            try
            {
                await userRepository.DeleteAsync(id);

                result.Ok = true;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }

            return result;
        }

        public async Task<ServiceResult<List<UserDto>>> GetAllAsync()
        {
            var result = new ServiceResult<List<UserDto>>();

            try
            {
                List<UserDto> users = await userRepository.GetWithRoleAsync();

                result.Extras = users;
                result.Ok = true;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }

            return result;
        }

        public async Task<ServiceResult<UserDto>> GetByIdAsync(int id)
        {
            var result = new ServiceResult<UserDto>();

            try
            {
                UserDto user = await userRepository.GetWithRoleAsync(id);

                result.Extras = user;
                result.Ok = true;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }

            return result;
        }

        public async Task<ServiceResult> UpdateAsync(UpdateUserDto updateUserDto)
        {
            var result = new ServiceResult();

            if (updateUserDto is null)
            {
                result.Message = "Information not supplied";
                return result;
            }

            bool exists = await userRepository.ExistsAsync(p => p.Id == updateUserDto.Id);

            if (!exists)
            {
                result.Message = "User not found.";
                return result;
            }

            User user = await userRepository.GetAsync(updateUserDto.Id);

            user = mapper.Map(updateUserDto, user);

            ServiceResult fieldsValidation = ValidateFields(user);

            if (!fieldsValidation.Ok)
            {
                result.Message = fieldsValidation.Message;
                return result;
            }

            ServiceResult userNameValidation = await ValidateUserNameAsync(user.UserName, user.Id);

            if (!userNameValidation.Ok)
            {
                result.Message = userNameValidation.Message;
                return result;
            }

            try
            {
                await userRepository.UpdateAsync(user);

                result.Ok = true;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }

            return result;
        }

        public async Task<ServiceResult> ValidateUserNameAsync(string userName)
        {
            var result = new ServiceResult();

            try
            {
                userName = userName.ToLower();

                bool exists = await userRepository.ExistsAsync(p => p.UserName.ToLower() == userName);

                if (exists)
                {
                    result.Message = "The UserName was taken by other user.";
                    return result;
                }

                result.Ok = true;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }

            return result;
        }

        public async Task<ServiceResult> ValidateUserNameAsync(string userName, int ignoreId)
        {
            var result = new ServiceResult();

            try
            {
                userName = userName.ToLower();

                bool exists = await userRepository.ExistsAsync(p =>
                    p.Id != ignoreId && p.UserName.ToLower() == userName);

                if (exists)
                {
                    result.Message = "The UserName was taken by other user.";
                    return result;
                }

                result.Ok = true;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }

            return result;
        }
    }
}