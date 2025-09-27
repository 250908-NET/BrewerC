
using ArmaReforger.Data;
using ArmaReforger.Models;
using ArmaReforger.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArmaReforger.Repository;

public class ServerPlayerConnectionRepository : IServerPlayerConnectionRepository
{
    private readonly ArmaReforgerDbContext _context;

    public ServerPlayerConnectionRepository(ArmaReforgerDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ServerPlayerConnectionModel>> GetByServerRecord(ServerPlayerConnectionModel serverPlayerConnection)
    {
        return await _context.serverPlayerConnections
            .Where(spc => spc.serverRecord.id == serverPlayerConnection.serverRecord.id)
            .ToListAsync();
    }

    public async Task<IEnumerable<ServerPlayerConnectionModel>> GetByPlayerRecord(PlayerRecordModel playerRecord)
    {
        return await _context.serverPlayerConnections
            .Where(spc => spc.playerRecord.biIdentity == playerRecord.biIdentity)
            .ToListAsync();
    }

    public async Task<IEnumerable<ServerPlayerConnectionModel>> GetAll()
    {
        //return await _context.serverPlayerConnections
        //    .Include(spc => spc.serverRecord)
        //    .Include(spc => spc.playerRecord)
        //    .ToListAsync();
        return await _context.serverPlayerConnections.ToListAsync();
    }

    public async Task<ServerPlayerConnectionModel?> AddAsync(ServerPlayerConnectionModel serverPlayerConnection)
    {
        _context.serverPlayerConnections.Add(serverPlayerConnection);
        await _context.SaveChangesAsync();
        return serverPlayerConnection;
    }

    public async Task<ServerPlayerConnectionModel?> DeleteAsync(ServerPlayerConnectionModel serverPlayerConnection)
    {
        var existing = await _context.serverPlayerConnections
            .FirstOrDefaultAsync(spc => spc.id == serverPlayerConnection.id);

        if (existing == null)
            return null;

        _context.serverPlayerConnections.Remove(existing);
        await _context.SaveChangesAsync();
        return existing;
    }
}