
using ArmaReforger.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ArmaReforger.Repository;

public interface IServerPlayerConnectionRepository
{
    Task<IEnumerable<ServerPlayerConnectionModel>> GetByServerRecord(ServerPlayerConnectionModel serverPlayerConnection);
    Task<IEnumerable<ServerPlayerConnectionModel>> GetByPlayerRecord(PlayerRecordModel playerRecord);
    Task<IEnumerable<ServerPlayerConnectionModel>> GetAll();
    Task<ServerPlayerConnectionModel?> AddAsync(ServerPlayerConnectionModel serverPlayerConnection);
    Task<ServerPlayerConnectionModel?> DeleteAsync(ServerPlayerConnectionModel serverPlayerConnection);
}
