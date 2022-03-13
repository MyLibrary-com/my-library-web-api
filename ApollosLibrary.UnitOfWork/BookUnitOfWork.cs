﻿using Microsoft.EntityFrameworkCore.Storage;
using ApollosLibrary.DataLayer;
using ApollosLibrary.DataLayer.Contracts;
using ApollosLibrary.UnitOfWork.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ApollosLibrary.Domain;

namespace ApollosLibrary.UnitOfWork
{
    public class BookUnitOfWork : IBookUnitOfWork
    {
        private IBookDataLayer _bookDataLayer;
        private readonly ApollosLibraryContext _context;
        private bool disposed = false;

        public BookUnitOfWork(ApollosLibraryContext context)
        {
            _context = context;
        }

        public IBookDataLayer BookDataLayer
        {
            get
            {
                if (_bookDataLayer == null)
                {
                    _bookDataLayer = new BookDataLayer(_context);
                }
                return _bookDataLayer;
            }
        }

        public async Task Begin()
        {
            await _context.Database.BeginTransactionAsync();
        }

        public async Task Commit()
        {
            await _context.Database.CommitTransactionAsync();
        }

        public async Task Rollback()
        {
            if (_context.Database.CurrentTransaction != null)
            {
                await _context.Database.RollbackTransactionAsync();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (_context.Database.CurrentTransaction != null)
            {
                _context.Database.RollbackTransaction();
            }

            if (disposing)
            {
                _context.Dispose();
            }

            disposed = true;
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        ~BookUnitOfWork()
        {
            Dispose(false);
        }
    }
}
