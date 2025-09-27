
using ArmaReforger.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ArmaReforger.Repository;

public interface IPlayerNameRepository
{
    Task<PlayerNameModel?> GetByIdAsync(PlayerNameModel playerName);
    Task<IEnumerable<PlayerNameModel>> GetByRecordAsync(PlayerRecordModel playerRecord);
    Task<IEnumerable<PlayerNameModel>> GetAllAsync();
    Task<PlayerNameModel> AddAsync(PlayerNameModel playerName);
    Task<PlayerNameModel> DeleteAsync(PlayerNameModel playerName);
    Task<IEnumerable<PlayerNameModel>> SearchByName(string name);
}