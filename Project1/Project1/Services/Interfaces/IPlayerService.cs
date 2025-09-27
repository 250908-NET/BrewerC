
using ArmaReforger.DTOs;
using ArmaReforger.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ArmaReforger.Services;

public interface IPlayerService
{
    Task<IEnumerable<ServerPlayerConnectionDto>> GetPlayerConnections(PlayerRecordDto playerRecord);

    Task<ServerPlayerConnectionDto> RegisterPlayerConnection(
        PlayerRecordDto playerRecordDto, // biIdentity
        PlayerNameDto playerNameDto, // biIdentity, name
        PlayerIpAddressDto playerIpAddressDto, // biIdentity, ipAddress
        ServerPlayerConnectionDto serverPlayerConnectionDto, // connectTime, action
        ServerRecordDto serverRecordDto // ipAddress, port, name (ignored)
        );

    Task<IEnumerable<PlayerNameDto>> GetPlayerNames(PlayerRecordDto playerRecordDto);

    Task<IEnumerable<PlayerIpAddressDto>> GetPlayerIpAddresses(PlayerRecordDto playerRecordDto);

    Task<IEnumerable<PlayerNameDto>> SearchPlayerNames(string name);
}