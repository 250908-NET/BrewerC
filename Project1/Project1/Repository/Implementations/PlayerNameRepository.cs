using ArmaReforger.Data;
using ArmaReforger.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArmaReforger.Repository;

public class PlayerNameRepository : IPlayerNameRepository
{
    private readonly ArmaReforgerDbContext _context;

    public PlayerNameRepository(ArmaReforgerDbContext context)
    {
        _context = context;
    }

    public async Task<PlayerNameModel?> GetByIdAsync(PlayerNameModel playerName)
    {
        return await _context.playerNames
            .FirstOrDefaultAsync(pn => pn.id == playerName.id);
    }

    public async Task<IEnumerable<PlayerNameModel>> GetByRecordAsync(PlayerRecordModel playerRecord)
    {
        return await _context.playerNames
            .Where(pn => pn.playerRecord.biIdentity == playerRecord.biIdentity)
            .ToListAsync();
    }

    public async Task<IEnumerable<PlayerNameModel>> GetAllAsync()
    {
        return await _context.playerNames.ToListAsync();
    }

    public async Task<PlayerNameModel> AddAsync(PlayerNameModel playerNameModel)
    {
        var existingNames = await this.GetByRecordAsync(playerNameModel.playerRecord);
        var matchingNames = existingNames.Where(pnm => pnm.name.Equals(playerNameModel.name));
        if (matchingNames.Count() > 0)
        {
            return matchingNames.First();
        }

        await _context.playerNames.AddAsync(playerNameModel);
        await _context.SaveChangesAsync();
        return playerNameModel;
    }

    public async Task<PlayerNameModel> DeleteAsync(PlayerNameModel playerName)
    {
        var existing = await _context.playerNames
            .FirstOrDefaultAsync(pn => pn.id == playerName.id);

        if (existing != null)
        {
            _context.playerNames.Remove(existing);
            await _context.SaveChangesAsync();
        }

        return existing;
    }

    public async Task<IEnumerable<PlayerNameModel>> SearchByName(string name)
    {
        return await _context.playerNames
            .Where(pn => pn.name.ToLower().Contains(name.ToLower()))
            .ToListAsync();
    }
}