
using ArmaReforger.DTOs;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArmaReforger.Models;

public class ServerRecordDto
{
    public string ipAddress { get; set; }
    public int port { get; set; }
    public string name { get; set; }

    public ServerRecordDto() {}

    public ServerRecordDto(string ipAddress, int port, string name)
    {
        this.ipAddress = ipAddress;
        this.port = port;
        this.name = name;
    }

    public ServerRecordDto(ServerRecordModel serverRecord)
    {
        this.ipAddress = serverRecord.ipAddress;
        this.port = serverRecord.port;
        this.name = serverRecord.name;
    }

    public ServerRecordModel getPartialModel()
    {
        ServerRecordModel serverRecord = new ServerRecordModel();
        serverRecord.ipAddress = this.ipAddress;
        serverRecord.port = this.port;
        serverRecord.name = this.name;
        return serverRecord;
    }
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(this, obj))
            return true;
        if (obj is null || obj.GetType() != this.GetType())
            return false;

        var other = (ServerRecordDto)obj;
        return string.Equals(this.ipAddress, other.ipAddress, StringComparison.Ordinal) &&
               this.port == other.port &&
               string.Equals(this.name, other.name, StringComparison.Ordinal);
    }
}
