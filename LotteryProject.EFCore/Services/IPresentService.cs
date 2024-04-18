using LotteryProject.Models.DTOs;
using LotteryProject.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotteryProject.EFCore.Services
{
    public interface IPresentService
    {
        Task<Present> AddPresent(AddPresentDTO present, CancellationToken cancellationToken = default);
        Task<bool> DeletePresentById(Guid presentID, CancellationToken cancellationToken = default);
        Task<IEnumerable<Present>> GetAllPresents(CancellationToken cancellationToken = default);
        Task<Present> GetPresent(Guid presentId, CancellationToken cancellationToken = default);
        Task<Present> UpdatePresent(Present present, CancellationToken cancellationToken = default);
    }
}
