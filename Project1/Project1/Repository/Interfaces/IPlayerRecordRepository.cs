
using ArmaReforger.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ArmaReforger.Repository;

public interface IPlayerRecordRepository
{
    Task<PlayerRecordModel?> GetByRecordAsync(PlayerRecordModel playerRecord);
    Task<IEnumerable<PlayerRecordModel>> GetAllAsync();
    Task<PlayerRecordModel> AddAsync(PlayerRecordModel playerRecord);
    Task<PlayerRecordModel> DeleteAsync(PlayerRecordModel playerRecord);
}