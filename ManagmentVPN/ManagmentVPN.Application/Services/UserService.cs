﻿
using ManagmentVPN.Application.Services;
using ManagmentVPN.Domain;
using ManagmentVPN.Domain.Entities;

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
        public async Task AllowAsync(Guid id)
        {
            User? user = await _unitOfWork.Users.GetByIdAsync(id);
            if (user is null)
                throw new Exception("User not found");
            user.VpnAccessMode = VpnAccessMode.ALLOWED;
            await _unitOfWork.Users.UpdateAsync(user);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task ForbidAsync(Guid id)
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
    }
}
