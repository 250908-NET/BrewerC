
using ArmaReforger.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

using ArmaReforger.Repository;
using ArmaReforger.DTOs;
using ArmaReforger.Data;
using System.Runtime.InteropServices;

namespace ArmaReforger.Services;

public class ServerService : IServerService
{
    private readonly ArmaReforgerDbContext _armaReforgerDbContext;
    private readonly IServerRecordRepository _serverRecordRepository;

    public ServerService(
            ArmaReforgerDbContext armaReforgerDbContext,
            IServerRecordRepository serverRecordRepository
        )
    {
        _armaReforgerDbContext = armaReforgerDbContext;
        _serverRecordRepository = serverRecordRepository;
    }

    public async Task<ServerRecordDto> RegisterServer(ServerRecordDto serverRecordDto)
    {
        // TODO: Check that all values are added
        // TODO: Check for ip port conflict
        ServerRecordModel serverRecordModel = serverRecordDto.getPartialModel();
        serverRecordModel = await _serverRecordRepository.CreateAsync(serverRecordModel);
        // TODO: Check for null and catch for conflict
        return new ServerRecordDto(serverRecordModel);
    }

    public async Task<ServerRecordDto> UpdateServer(ServerRecordDto oldServerRecordDto, ServerRecordDto newServerRecordDto)
    {
        // TODO: Check for updated ip 
        ServerRecordModel oldServerRecordModel = oldServerRecordDto.getPartialModel();
        oldServerRecordModel = await _serverRecordRepository.GetByIpPortAsync(oldServerRecordModel);
        // TODO: Check for returned server record
        ServerRecordModel newServerRecordModel = newServerRecordDto.getPartialModel();
        newServerRecordModel.id = oldServerRecordModel.id;
        newServerRecordModel = await _serverRecordRepository.UpdateAsync(newServerRecordModel);
        // TODO: Check for successful update
        if (true /*isSuccessful*/)
            return new ServerRecordDto(newServerRecordModel);
        else
            return oldServerRecordDto;
    }

    public async Task<ServerRecordDto> DeleteServer(ServerRecordDto serverRecordDto)
    {
        ServerRecordModel serverRecordModel = serverRecordDto.getPartialModel();
        var fullServerRecordModel = await _serverRecordRepository.GetByIpPortAsync(serverRecordModel);
        fullServerRecordModel = await _serverRecordRepository.DeleteAsync(fullServerRecordModel);
        return new ServerRecordDto(fullServerRecordModel);
    }
}