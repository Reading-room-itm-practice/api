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

        public async Task<IEnumerable<FriendRequest>> GetApprovedSentAndReceivedFriendRequests(Guid userId)
        {
            return await _context.FriendRequests
                .Include(fr => fr.Creator)
                .Include(fr => fr.To)
                .Where(fr => fr.CreatorId == userId || fr.ToId == userId)
                .Where(fr => fr.Approved)
                .ToListAsync();
        }

        public async Task<FriendRequest> GetApprovedSentAndReceivedFriendRequest(Guid userId)
        {
            return await _context.FriendRequests
                .Include(fr => fr.Creator)
                .Include(fr => fr.To)
                .Where(fr => fr.CreatorId == userId || fr.ToId == userId)
                .Where(fr => fr.Approved)
                .FirstOrDefaultAsync();
        }

    }
}
