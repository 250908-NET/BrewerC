
using ArmaReforger.Data;
using ArmaReforger.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ArmaReforger.Repository;

public class PlayerRecordRepository : IPlayerRecordRepository
{
    private readonly ArmaReforgerDbContext _context;

    public PlayerRecordRepository(ArmaReforgerDbContext context)
    {
        _context = context;
    }

    public async Task<PlayerRecordModel?> GetByRecordAsync(PlayerRecordModel playerRecord)
    {
        string biIdentity = playerRecord.biIdentity;
        return await _context.playerRecords
            .FirstOrDefaultAsync(pr => pr.biIdentity == biIdentity);
    }
    public async Task<IEnumerable<PlayerRecordModel>> GetAllAsync()
    {
        return await _context.playerRecords.ToListAsync();
    }

    /* 
     * Safely adds a player record, or simply returns the already existing record
     */
    public async Task<PlayerRecordModel> AddAsync(PlayerRecordModel playerRecord)
    {
        var existing = await this.GetByRecordAsync(playerRecord);

        if (existing != null)
            return existing;

        _context.playerRecords.Add(playerRecord);
        await _context.SaveChangesAsync();
        return playerRecord;
    }
    public async Task<PlayerRecordModel> DeleteAsync(PlayerRecordModel playerRecord)
    {
        var existing = await this.GetByRecordAsync(playerRecord);

        if (existing != null)
        {
            _context.playerRecords.Remove(existing);
            await _context.SaveChangesAsync();
        }

        return existing;
    }
}