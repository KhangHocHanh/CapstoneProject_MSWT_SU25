using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;
using MSWT_BussinessObject.Model;
using MSWT_Repositories.IRepository;
using MSWT_Repositories.Repository;

namespace MSWT_Services
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly SmartTrashBinandCleaningStaffManagementContext _context;
        private IUserRepository _userRepository;
        private IRoleRepository _roleRepository;
        private IWorkerGroupRepository _workerGroupRepository;
        private IRequestRepository _requestRepository;
        private IWorkGroupMemberRepository _workGroupMemberRepository;

        private IDbContextTransaction _transaction;

        public UnitOfWork(SmartTrashBinandCleaningStaffManagementContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IUserRepository UserRepository
        {
            get
            {
                _userRepository ??= new UserRepository(_context);
                return _userRepository;
            }
        }
        public IRoleRepository RoleRepository
        {
            get
            {
                _roleRepository ??= new RoleRepository(_context);
                return _roleRepository;
            }
        }
        public IRequestRepository RequestRepository
        {
            get
            {
                _requestRepository ??= new RequestRepository(_context);
                return _requestRepository;
            }
        }
        public IWorkerGroupRepository WorkerGroupRepository
        {
            get
            {
                _workerGroupRepository ??= new WorkerGroupRepository(_context);
                return _workerGroupRepository;
            }
        }
        public IWorkGroupMemberRepository WorkGroupMemberRepository
        {
            get
            {
                _workGroupMemberRepository ??= new WorkGroupMemberRepository(_context);
                return _workGroupMemberRepository;
            }
        }
        public async Task<int> SaveChangesAsync()
        {
            try
            {
                return await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error saving changes to database", ex);
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
            if (_context != null)
            {
                _context.Dispose();
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context?.Dispose();
            }
        }
        // Transaction methods
        public async Task BeginTransactionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitAsync()
        {
            try
            {
                await _context.SaveChangesAsync();

                if (_transaction != null)
                {
                    await _transaction.CommitAsync();
                    await _transaction.DisposeAsync();
                    _transaction = null;
                }
            }
            catch
            {
                await RollbackAsync();
                throw;
            }
        }


        public async Task RollbackAsync()
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }
    }
}
