using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jfrs_personal_loans.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        protected readonly JFRSPersonalLoansDBContext _context;
        public UnitOfWork(JFRSPersonalLoansDBContext context)
        {
            _context = context;
        }

        public bool SaveChanges()
        {
            bool returnValue = true;
            using (var dbContextTransaction = _context.Database.BeginTransaction())
            {
                try
                {
                    _context.SaveChanges();
                    dbContextTransaction.Commit();
                }
                catch (Exception)
                {
                    returnValue = false;
                    dbContextTransaction.Rollback();

                }
                return returnValue;
            }
        }

        private bool _disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (_disposedValue) return;
            if (disposing)
            {
                //dispose managed state (managed objects).
            }
            _disposedValue = true;
        }
        public void Dispose()
        {
            Dispose(true);
        }
    }
}
