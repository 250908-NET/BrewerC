
using ArmaReforger.DTOs;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArmaReforger.Models;

public class ServerPlayerConnectionDto
{
    public ServerRecordDto serverRecord { get; set; }
    public PlayerRecordDto playerRecord { get; set; }
    public DateTime connectionTime { get; set; }
    public string action { get; set; }

    public ServerPlayerConnectionDto() {}

    public ServerPlayerConnectionDto(ServerRecordDto serverRecord, PlayerRecordDto playerRecord, DateTime connectionTime, string action)
    {
        this.serverRecord = serverRecord;
        this.playerRecord = playerRecord;
        this.connectionTime = connectionTime;
        this.action = action;
    }

    public ServerPlayerConnectionDto(ServerPlayerConnectionModel serverPlayerConnection)
    {
        this.serverRecord = new ServerRecordDto(serverPlayerConnection.serverRecord);
        this.playerRecord = new PlayerRecordDto(serverPlayerConnection.playerRecord);
        this.connectionTime = serverPlayerConnection.connectionTime;
        this.action = serverPlayerConnection.action;
    }

    public ServerPlayerConnectionModel getPartialModel()
    {
        ServerPlayerConnectionModel serverPlayerConnection = new ServerPlayerConnectionModel();
        serverPlayerConnection.serverRecord = this.serverRecord.getPartialModel();
        serverPlayerConnection.playerRecord = this.playerRecord.getPartialModel();
        serverPlayerConnection.connectionTime = this.connectionTime;
        serverPlayerConnection.action = this.action;
        return serverPlayerConnection;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(this, obj))
            return true;
        if (obj is null || obj.GetType() != this.GetType())
            return false;

        var other = (ServerPlayerConnectionDto)obj;
        return this.serverRecord.Equals(other.serverRecord) &&
               this.playerRecord.Equals(other.playerRecord) &&
               this.connectionTime == other.connectionTime &&
               string.Equals(this.action, other.action, StringComparison.Ordinal);
    }
}
