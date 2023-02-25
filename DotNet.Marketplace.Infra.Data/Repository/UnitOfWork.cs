
using DotNet.Marketplace.Domain.Repository;
using DotNet.Marketplace.Infra.Data.Context;
using Microsoft.EntityFrameworkCore.Storage;
using System.Transactions;

namespace DotNet.Marketplace.Infra.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDb _db;
        private IDbContextTransaction _transaction;

        public UnitOfWork(ApplicationDb db)
        {
            _db = db;
        }

        public async Task BeginTransaction()
        {
            _transaction = await _db.Database.BeginTransactionAsync();
        }

        public async Task Commit()
        {
            await _transaction.CommitAsync();
        }

        public async Task Rollback()
        {
            await _transaction.RollbackAsync();
        }

        public void Dispose()
        {
            _transaction?.Dispose();
        }
    }
}
