using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using Storage.DataAccessLayer;
using Storage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public class FriendRepository : BaseRepository<FriendRequest>, IFriendRepository
    {
        public FriendRepository(ApiDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<FriendRequest>> GetSentAndReceivedFriendRequests(Guid userId)
        {
            return await _context.FriendRequests
                .Include(fr => fr.Creator)
                .Include(fr => fr.To)
                .Where(fr => fr.CreatorId == userId || fr.ToId == userId)
                .Where(fr => fr.Approved)
                .ToListAsync();
        }

        public async Task<IEnumerable<FriendRequest>> GetSentFriendRequests(Guid userId)
        {
            return await _context.FriendRequests
                .Include(fr => fr.To)
                .Where(fr => fr.CreatorId == userId)
                .Where(fr => !fr.Approved)
                .ToListAsync();
        }

        public async Task<IEnumerable<FriendRequest>> GetReceivedFriendRequests(Guid userId)
        {
            return await _context.FriendRequests
                .Include(fr => fr.Creator)
                .Where(fr => fr.ToId == userId)
                .Where(fr => !fr.Approved)
                .ToListAsync();
        }
    }
}
