
using ArmaReforger.Models;

namespace ArmaReforger.DTOs;

public class PlayerNameDto
{
    public PlayerRecordDto playerRecord { get; set; }
    public string name { get; set; }

    public PlayerNameDto() {}


    public PlayerNameDto(PlayerRecordDto playerRecord, string name)
    {
        this.playerRecord = playerRecord;
        this.name = name;
    }

    public PlayerNameDto(PlayerNameModel playerName)
    {
        this.playerRecord = new PlayerRecordDto(playerName.biIdentity);
        this.name = playerName.name;
    }

    public PlayerNameModel getPartialModel()
    {
        PlayerNameModel playerName = new PlayerNameModel();
        playerName.biIdentity = this.playerRecord.biIdentity;
        playerName.playerRecord = this.playerRecord.getPartialModel();
        playerName.name = this.name;
        return playerName;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(this, obj))
            return true;
        if (obj is null || obj.GetType() != this.GetType())
            return false;

        var other = (PlayerNameDto)obj;
        return string.Equals(this.name, other.name, StringComparison.Ordinal) &&
               this.playerRecord.Equals(other.playerRecord);
    }
}
