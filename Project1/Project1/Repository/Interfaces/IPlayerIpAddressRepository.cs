
using ArmaReforger.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ArmaReforger.Repository;

public interface IPlayerIpAddressRepository
{
    Task<PlayerIpAddressModel?> GetByIdAsync(PlayerIpAddressModel playerIpAddress);
    Task<IEnumerable<PlayerIpAddressModel>> GetByRecordAsync(PlayerRecordModel playerRecord);
    Task<IEnumerable<PlayerIpAddressModel>> GetByIpAddress(PlayerIpAddressModel playerIpAddress);
    Task<IEnumerable<PlayerIpAddressModel>> GetAllAsync();
    Task<PlayerIpAddressModel> AddAsync(PlayerIpAddressModel playerIpAddress);
    Task<PlayerIpAddressModel> DeleteAsync(PlayerIpAddressModel playerIpAddress);
}