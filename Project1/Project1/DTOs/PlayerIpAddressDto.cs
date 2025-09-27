
using ArmaReforger.Models;

namespace ArmaReforger.DTOs;

public class PlayerIpAddressDto
{
    public PlayerRecordDto playerRecord { get; set; }
    public string ipAddress { get; set; }

    public PlayerIpAddressDto() {}

    public PlayerIpAddressDto(PlayerRecordDto playerRecord, string ipAddress)
    {
        this.playerRecord = playerRecord;
        this.ipAddress = ipAddress;
    }

    public PlayerIpAddressDto(PlayerIpAddressModel playerIpAddress)
    {
        this.playerRecord = new PlayerRecordDto(playerIpAddress.playerRecord);
        this.ipAddress = playerIpAddress.ipAddress;
    }

    public PlayerIpAddressModel getPartialModel()
    {
        PlayerIpAddressModel playerIpAddress = new PlayerIpAddressModel();
        playerIpAddress.biIdentity = this.playerRecord.biIdentity;
        playerIpAddress.playerRecord = this.playerRecord.getPartialModel();
        playerIpAddress.ipAddress = this.ipAddress;
        return playerIpAddress;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(this, obj))
            return true;
        if (obj is null || obj.GetType() != this.GetType())
            return false;

        var other = (PlayerIpAddressDto)obj;
        return string.Equals(this.ipAddress, other.ipAddress, StringComparison.Ordinal) &&
               this.playerRecord.Equals(other.playerRecord);
    }
}
