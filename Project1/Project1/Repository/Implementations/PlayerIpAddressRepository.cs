using ArmaReforger.Data;
using ArmaReforger.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArmaReforger.Repository;

public class PlayerIpAddressRepository : IPlayerIpAddressRepository
{
    private readonly ArmaReforgerDbContext _context;

    public PlayerIpAddressRepository(ArmaReforgerDbContext context)
    {
        _context = context;
    }

    public async Task<PlayerIpAddressModel?> GetByIdAsync(PlayerIpAddressModel playerIpAddress)
    {
        return await _context.playerIpAddresses
            .FirstOrDefaultAsync(pia => pia.id == playerIpAddress.id);
    }

    public async Task<IEnumerable<PlayerIpAddressModel>> GetByRecordAsync(PlayerRecordModel playerRecord)
    {
        return await _context.playerIpAddresses
            .Where(pia => pia.playerRecord.biIdentity == playerRecord.biIdentity)
            .ToListAsync();
    }

    public async Task<IEnumerable<PlayerIpAddressModel>> GetByIpAddress(PlayerIpAddressModel playerIpAddress)
    {
        return await _context.playerIpAddresses
            .Where(pia => pia.ipAddress == playerIpAddress.ipAddress)
            .ToListAsync();
    }

    public async Task<IEnumerable<PlayerIpAddressModel>> GetAllAsync()
    {
        return await _context.playerIpAddresses.ToListAsync();
    }

    public async Task<PlayerIpAddressModel> AddAsync(PlayerIpAddressModel playerIpAddress)
    {
        var existing = await _context.playerIpAddresses
            .Include(pia => pia.playerRecord)
            .FirstOrDefaultAsync(pia =>
                pia.playerRecord.biIdentity == playerIpAddress.playerRecord.biIdentity &&
                pia.ipAddress == playerIpAddress.ipAddress);

        if (existing != null)
        {
            // Conflict: return the existing record
            return existing;
        }

        _context.playerIpAddresses.Add(playerIpAddress);
        await _context.SaveChangesAsync();
        return playerIpAddress;
    }

    public async Task<PlayerIpAddressModel> DeleteAsync(PlayerIpAddressModel playerIpAddress)
    {
        var existing = await _context.playerIpAddresses
            .FirstOrDefaultAsync(pia => pia.id == playerIpAddress.id);

        if (existing != null)
        {
            _context.playerIpAddresses.Remove(existing);
            await _context.SaveChangesAsync();
        }

        return existing;
    }
}