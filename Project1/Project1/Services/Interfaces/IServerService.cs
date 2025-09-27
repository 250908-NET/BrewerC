
using ArmaReforger.DTOs;
using ArmaReforger.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ArmaReforger.Services;

public interface IServerService
{
    Task<ServerRecordDto> RegisterServer(ServerRecordDto serverRecordDto);

    Task<ServerRecordDto> UpdateServer(ServerRecordDto oldServerRecordDto, ServerRecordDto newServerRecordDto);

    Task<ServerRecordDto> DeleteServer(ServerRecordDto serverRecordDto);
}