﻿using Microsoft.EntityFrameworkCore;
using OnionAPI.Application.Interfaces.Repositories;
using OnionAPI.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionAPI.Persistence.Repositories
{
    public class WriteRepository<T> : IWriteRepository<T> where T : class, IBaseEntity, new()
    {
        private readonly DbContext dbContext;
        public WriteRepository(DbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        private DbSet<T> Table { get=>dbContext.Set<T>(); }
        public async Task AddAsync(T entity)
        {
            await Table.AddAsync(entity);
        }
        public async Task AddRange(IList<T> entities)
        {
            await Table.AddRangeAsync(entities);
        }
        public async Task HardDeleteAsync(T entity)
        {
                await Task.Run(() =>
                {
                    Table.Remove(entity);
                });
        }
        public async Task<T> UpdateAsync(T entity)
        {
                await Task.Run(() =>
                {
                    Table.Update(entity);
                });
                return entity;
        }
    }
}
