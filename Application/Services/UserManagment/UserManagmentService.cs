using Infrastructure.Entities.User;
using Infrastructure.Entities.User.Dtos;
using Infrastructure.Paging;
using Infrastructure.RepositoryRelated;
using Infrastructure.UnitOfWorkRelated;
using Microsoft.EntityFrameworkCore;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.UserManagment
{
    public class UserManagmentService : IUserManagmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<UserEntity> _userRepository;

        public UserManagmentService(IUnitOfWork unitOfWork, IGenericRepository<UserEntity> userRepository)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
        }

        public async Task<UserDto> CreateUser(CreateDto model)
        {
            var checkUser = await _userRepository.FindOneByConditionAsync(x => x.UserName == model.UserName);
            if (checkUser is not null)
            {
                throw new CustomException("this username already exists", 404);
            }
            var newUser = new UserEntity
            {
                UserName = model.UserName,
                CreatedAt = DateTime.Now,
                IsDisabled=false,
            };
            await _userRepository.AddAsync(newUser);
            await _unitOfWork.CompleteAsync();
            return new UserDto(newUser.Id,newUser.UserName,(bool)newUser.IsDisabled);
        }

        public async Task<bool> DeleteUser(string UserName)
        {
            var checkUser = await _userRepository.FindOneByConditionAsync(x => x.UserName == UserName);
            if (checkUser is  null)
            {
                throw new CustomException("this user does not exist", 404);
            }
            _userRepository.Remove(checkUser);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<UserDto> DisableUser(int userId)
        {
            var checkUser = await _userRepository.FindOneByConditionAsync(x => x.Id == userId);
            if (checkUser is null)
            {
                throw new CustomException("this user does not exist", 404);
            }
            checkUser.IsDisabled = true;
            checkUser.UpdatedAt = DateTime.Now;
            await _unitOfWork.CompleteAsync();
            return new UserDto(userId,checkUser.UserName,(bool)checkUser.IsDisabled);
        }

        public async Task<PageReturnDto<UserDto>> ListUsers(UserParameters model)
        {
            var Users = await _userRepository.Query()
                .Where(x => x.UserName.Contains( model.SearchTerm))
                .Skip((model.Page - 1) * model.ItemsPerPage)
                .Take(model.ItemsPerPage)
                .ToListAsync();
            var count =await _userRepository.Query()
                .Where(x=>x.UserName.Contains(model.SearchTerm))
                .CountAsync();
            var UserDtos = Users.Select(x =>
            {
                return new UserDto(x.Id, x.UserName, (bool)x.IsDisabled);
            });
            return new PageReturnDto<UserDto>(UserDtos, count, model.Page, model.ItemsPerPage);
        }

        public async Task<UserDto> LoginWithUser(string UserName)
        {
            var user = await _userRepository.FindOneByConditionAsync(x => x.UserName == UserName);
            if (user is null)
            {
                throw new CustomException("this username does not exist", 404);
            }
            if (user.IsDisabled  is true)
            {
                throw new CustomException("this username is  disabled", 403);
            }
            return new UserDto(user.Id,user.UserName,(bool)user.IsDisabled);
        }

        public async Task<UserDto> UpdateUser(UpdateDto model)
        {
            var checkUser = await _userRepository.FindOneByConditionAsync(x => x.UserName== model.UserName);
            if (checkUser is null)
            {
                throw new CustomException("this user does not exist", 404);
            }
            checkUser.IsDisabled = model.IsDisabledStatus;
            await _unitOfWork.CompleteAsync();
            return new UserDto(checkUser.Id, checkUser.UserName, (bool)checkUser.IsDisabled);
        }
    }
}
