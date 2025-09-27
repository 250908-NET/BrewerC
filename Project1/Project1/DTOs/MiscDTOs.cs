
using ArmaReforger.Models;

namespace ArmaReforger.DTOs;

class NameDto
{
    public string name { get; set; }
}

class UpdateServerDto
{
    public ServerRecordDto oldServer { get; set; }
    public ServerRecordDto newServer { get; set; }
}

/*
public async Task<ServerPlayerConnectionDto> RegisterPlayerConnection(
        PlayerRecordDto playerRecordDto, // biIdentity
        PlayerNameDto playerNameDto, // name
        PlayerIpAddressDto playerIpAddressDto, // ipAddress
        ServerPlayerConnectionDto serverPlayerConnectionDto, // connectionTime, action
        ServerRecordDto serverRecordDto // ipAddress, port
        )
*/
class PlayerConnectionDto
{
    public string biIdentity { get; set; }
    public string playerName { get; set; }
    public string playerIpAddress { get; set; }
    public DateTime connectionTime { get; set; }
    public string action { get; set; }
    public string serverIpAddress { get; set; }
    public int serverPort { get; set; }
}