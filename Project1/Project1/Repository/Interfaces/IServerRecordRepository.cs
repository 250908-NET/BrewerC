
using ArmaReforger.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ArmaReforger.Repository;

public interface IServerRecordRepository
{
    Task<ServerRecordModel?> GetByIdAsync(ServerRecordModel serverRecord);
    Task<ServerRecordModel?> GetByIpPortAsync(ServerRecordModel serverRecord);
    Task<IEnumerable<ServerRecordModel>> GetAll();
    Task<ServerRecordModel?> CreateAsync(ServerRecordModel serverRecord);
    Task<ServerRecordModel?> UpdateAsync(ServerRecordModel serverRecord);
    Task<ServerRecordModel?> DeleteAsync(ServerRecordModel serverRecord);
    Task<IEnumerable<ServerRecordModel>> SearchByName(string name);
}