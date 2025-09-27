using ArmaReforger.Data;
using ArmaReforger.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArmaReforger.Repository;

public class ServerRecordRepository : IServerRecordRepository
{
    private readonly ArmaReforgerDbContext _context;

    public ServerRecordRepository(ArmaReforgerDbContext context)
    {
        _context = context;
    }

    public async Task<ServerRecordModel?> GetByIdAsync(ServerRecordModel serverRecord)
    {
        return await _context.serverRecords
            .FirstOrDefaultAsync(sr => sr.id == serverRecord.id);
    }

    public async Task<ServerRecordModel?> GetByIpPortAsync(ServerRecordModel serverRecord)
    {
        return await _context.serverRecords
            .FirstOrDefaultAsync(sr => sr.ipAddress == serverRecord.ipAddress && sr.port == serverRecord.port);
    }

    public async Task<IEnumerable<ServerRecordModel>> GetAll()
    {
        return await _context.serverRecords.ToListAsync();
    }

    public async Task<ServerRecordModel?> CreateAsync(ServerRecordModel serverRecord)
    {
        _context.serverRecords.Add(serverRecord);
        await _context.SaveChangesAsync();
        return serverRecord;
    }

    public async Task<ServerRecordModel?> UpdateAsync(ServerRecordModel serverRecord)
    {
        var existing = await _context.serverRecords
            .FirstOrDefaultAsync(sr => sr.id == serverRecord.id);

        if (existing == null)
            return null;

        existing.ipAddress = serverRecord.ipAddress;
        existing.port = serverRecord.port;
        existing.name = serverRecord.name;

        await _context.SaveChangesAsync();
        return existing;
    }

    public async Task<ServerRecordModel?> DeleteAsync(ServerRecordModel serverRecord)
    {
        var existing = await _context.serverRecords
            .FirstOrDefaultAsync(sr => sr.id == serverRecord.id);

        if (existing == null)
            return null;

        _context.serverRecords.Remove(existing);
        await _context.SaveChangesAsync();
        return existing;
    }

    public async Task<IEnumerable<ServerRecordModel>> SearchByName(string name)
    {
        return await _context.serverRecords
            .Where(sr => sr.name != null && sr.name.Contains(name))
            .ToListAsync();
    }
}