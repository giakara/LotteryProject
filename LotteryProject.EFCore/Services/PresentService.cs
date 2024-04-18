using LotteryProject.Models.DTOs;
using LotteryProject.Models.Entities;
using LotteryProject.Models.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotteryProject.EFCore.Services
{
    public class PresentService : IPresentService
    {
        private readonly EntityDbContext.DataContext _dbContext;

        public PresentService(EntityDbContext.DataContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<Present>> GetAllPresents(CancellationToken cancellationToken = default)
        {
            return await _dbContext.Presents.ToListAsync(cancellationToken);
        }
        public async Task<Present> GetPresent(Guid presentId, CancellationToken cancellationToken = default)
        {
            var present = await _dbContext.Presents.FindAsync(presentId, cancellationToken);

            if (present is null)
            {
                throw new PresentNotFoundException("Present not found");
            }

            return present;

        }
        public async Task<Present> AddPresent(AddPresentDTO present, CancellationToken cancellationToken = default)
        {
            var presentToAdd = new Present
            {
                Description = present.Description,
                Category = present.Category,
            };
            if (presentToAdd == null)
            {
                throw new PresentNotFoundException("Present not found");
            }
            _dbContext.Presents.Add(presentToAdd);
            await _dbContext.SaveChangesAsync();
            return presentToAdd;
        }
        public async Task<Present> UpdatePresent(Present present, CancellationToken cancellationToken = default)
        {
            var dbPresent = await _dbContext.Presents.FindAsync(present.Id);
            if (dbPresent == null)
                throw new PresentNotFoundException("Present not found");
            dbPresent.Description = present.Description;
            dbPresent.Category = present.Category;
            await _dbContext.SaveChangesAsync();
            return dbPresent;
        }
        public async Task<bool> DeletePresentById(Guid presentId, CancellationToken cancellationToken = default)
        {
            var dbPresent = await _dbContext.Presents.FindAsync(presentId);
            if (dbPresent == null)
                throw new PresentNotFoundException("Present not found");

            _dbContext.Presents.Remove(dbPresent);
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}
