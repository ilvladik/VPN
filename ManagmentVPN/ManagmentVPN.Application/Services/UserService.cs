using Application.Dtos;
using Domain;
using Domain.Entities;
using ManagmentVPN.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private bool disposedValue;

        public UserService(IUnitOfWork unitOfWork)
        {  
            _unitOfWork = unitOfWork; 
        }
        public async Task AllowVpnAccess(Guid id)
        {
            User? user = await _unitOfWork.Users.GetByIdAsync(id);
            if (user is null)
                throw new Exception("User not found");
            user.VpnAccessMode = VpnAccessMode.ALLOWED;
            await _unitOfWork.Users.UpdateAsync(user);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DisableVpnAccess(Guid id)
        {
            User? user = await _unitOfWork.Users.GetByIdAsync(id);
            if (user is null)
                throw new Exception("User not found");
            user.VpnAccessMode = VpnAccessMode.FORBIDDEN;
            if (user.Key is not null)
                await _unitOfWork.Keys.DeleteByIdAsync(user.Key.Id);

            await _unitOfWork.Users.UpdateAsync(user);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task CreateAsync(UserDto userDto)
        {
            await _unitOfWork.Users.CreateAsync(new User
            {
                Id = Guid.NewGuid(),
                Login = userDto.Login,
                Password = userDto.Password,
            });
            await _unitOfWork.SaveChangesAsync();
        }


        public async Task<User?> GetByIdAsync(Guid id)
        {
            return await _unitOfWork.Users.GetByIdAsync(id);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _unitOfWork.Dispose();
                }

                disposedValue = true;
            }
        }
        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        public async Task<User?> GetByLoginAsync(string login)
        {
            return await _unitOfWork.Users.GetByLoginAsync(login);
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _unitOfWork.Users.GetAllAsync();
        }

        public async Task<KeyDtoForUserResponse?> GetVpn(Guid id)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(id);
            if (user.VpnAccessMode != VpnAccessMode.FORBIDDEN)
                return null;
            if (user.Key != null)
                return new KeyDtoForUserResponse(user.Key.Server.NetworkId, user.Key.Password, user.Key.KeyPort, user.Key.Method);

            _unitOfWork.Keys.CreateAsync();


        }
    }
}
