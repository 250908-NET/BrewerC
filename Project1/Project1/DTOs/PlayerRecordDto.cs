
using ArmaReforger.Models;

namespace ArmaReforger.DTOs;

public class PlayerRecordDto
{
    public string biIdentity { get; set; }

    public PlayerRecordDto() {}

    public PlayerRecordDto(string biIdentity)
    {
        this.biIdentity = biIdentity;
    }

    public PlayerRecordDto(PlayerRecordModel playerRecord)
    {
        this.biIdentity = playerRecord.biIdentity;
    }
    public PlayerRecordModel getPartialModel()
    {
        PlayerRecordModel playerRecord = new PlayerRecordModel();
        playerRecord.biIdentity = this.biIdentity;
        return playerRecord;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(this, obj))
            return true;
        if (obj is null || obj.GetType() != this.GetType())
            return false;

        var other = (PlayerRecordDto)obj;
        return string.Equals(this.biIdentity, other.biIdentity, StringComparison.Ordinal);
    }
}
