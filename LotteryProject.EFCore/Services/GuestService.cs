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
    public class GuestService : IGuestService
    {
        private readonly EntityDbContext.DataContext _dbContext;

        public GuestService(EntityDbContext.DataContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<Guest>> GetAllGuests(CancellationToken cancellationToken = default)
        {
            return await _dbContext.Guests.ToListAsync(cancellationToken);
        }
        public async Task<Guest> GetGuest(Guid guestId, CancellationToken cancellationToken = default)
        {
            var guest = await _dbContext.Guests.FindAsync(guestId, cancellationToken);

            if (guest is null)
            {
                throw new GuestNotFoundException("Guest not found");
            }

            return guest;

        }
        public async Task<Guest> AddGuest(AddGuestDTO guest, CancellationToken cancellationToken = default)
        {
            var guestToAdd = new Guest(guest.GuestName, guest.GuestSurname);

            if (guestToAdd == null)
                throw new GuestNotFoundException("Guest not found");

            if (_dbContext.Guests.Any(g => g.GuestName == guestToAdd.GuestName && g.GuestSurname == guestToAdd.GuestSurname))
                throw new GuestDuplicateException("Guest is already Exists");
            _dbContext.Guests.Add(guestToAdd);
            await _dbContext.SaveChangesAsync();
            return guestToAdd;
        }
        public async Task<Guest> UpdateGuest(Guest guest, CancellationToken cancellationToken = default)
        {
            var dbGuest = await _dbContext.Guests.FindAsync(guest.Id);
            if (dbGuest == null)
                throw new GuestNotFoundException("Guest not found");
            dbGuest.GuestName = guest.GuestName;
            dbGuest.GuestSurname = guest.GuestSurname;
            await _dbContext.SaveChangesAsync();
            return dbGuest;
        }
        public async Task<bool> DeleteGuestById(Guid guestId, CancellationToken cancellationToken = default)
        {
            var dbGuest = await _dbContext.Guests.FindAsync(guestId);
            if (dbGuest == null)
                throw new GuestNotFoundException("Guest not found");

            _dbContext.Guests.Remove(dbGuest);
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}
