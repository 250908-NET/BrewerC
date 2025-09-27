
using ArmaReforger.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

using ArmaReforger.Repository;
using ArmaReforger.DTOs;
using ArmaReforger.Data;

namespace ArmaReforger.Services;

public class PlayerService: IPlayerService
{
    private readonly ArmaReforgerDbContext _armaReforgerDbContext;
    private readonly IPlayerRecordRepository _playerRecordRepository;
    private readonly IPlayerNameRepository _playerNameRepository;
    private readonly IPlayerIpAddressRepository _playerIpAddressRepository;
    private readonly IServerPlayerConnectionRepository _serverPlayerConnectionRepository;
    private readonly IServerRecordRepository _serverRecordRepository;

    public PlayerService(
            ArmaReforgerDbContext armaReforgerDbContext,
            IPlayerRecordRepository playerRecordRepository,
            IPlayerNameRepository playerNameRepository,
            IPlayerIpAddressRepository playerIpAddressRepository,
            IServerPlayerConnectionRepository serverPlayerConnectionRepository,
            IServerRecordRepository serverRecordRepository
        )
    {
        _armaReforgerDbContext = armaReforgerDbContext;
        _playerRecordRepository = playerRecordRepository;
        _playerNameRepository = playerNameRepository;
        _playerIpAddressRepository = playerIpAddressRepository;
        _serverPlayerConnectionRepository = serverPlayerConnectionRepository;
        _serverRecordRepository = serverRecordRepository;
    }

    public async Task<IEnumerable<ServerPlayerConnectionDto>> GetPlayerConnections(PlayerRecordDto playerRecord)
    {
        var foundPlayerRecord = await _playerRecordRepository.GetByRecordAsync(playerRecord.getPartialModel());
        if (foundPlayerRecord == null) return new List<ServerPlayerConnectionDto>();
        var connections = await _serverPlayerConnectionRepository.GetByPlayerRecord(foundPlayerRecord);
        return connections.Select(spc => new ServerPlayerConnectionDto(spc));
    }

    public async Task<ServerPlayerConnectionDto> RegisterPlayerConnection(
        PlayerRecordDto playerRecordDto, // biIdentity
        PlayerNameDto playerNameDto, // biIdentity, name
        PlayerIpAddressDto playerIpAddressDto, // biIdentity, ipAddress
        ServerPlayerConnectionDto serverPlayerConnectionDto, // connectTime, action
        ServerRecordDto serverRecordDto // ipAddress, port, name (ignored)
        )
    {
        // TODO: Verify name, ip address, server info, connect
        var playerRecordModel = await _playerRecordRepository.AddAsync(playerRecordDto.getPartialModel()); // write/save 1
        PlayerNameModel playerNameModel = playerNameDto.getPartialModel();
        playerNameModel.playerRecord = playerRecordModel;
        await _playerNameRepository.AddAsync(playerNameModel); // write/save 2
        PlayerIpAddressModel playerIpAddressModel = playerIpAddressDto.getPartialModel();
        playerIpAddressModel.playerRecord = playerRecordModel;
        await _playerIpAddressRepository.AddAsync(playerIpAddressModel); // write/save 3
        var serverRecordModel = await _serverRecordRepository.GetByIpPortAsync(serverRecordDto.getPartialModel());
        if (serverRecordModel == null)
        {
            // TODO: What do I do if an invalid server is given?
            throw new InvalidOperationException("Server record not found.");
        }
        ServerPlayerConnectionModel serverPlayerConnectionModel = serverPlayerConnectionDto.getPartialModel();
        serverPlayerConnectionModel.serverRecord = serverRecordModel;
        serverPlayerConnectionModel.playerRecord = playerRecordModel;
        serverPlayerConnectionModel = await _serverPlayerConnectionRepository.AddAsync(serverPlayerConnectionModel);
        // TODO: Check for unlikely null connection var?
        return new ServerPlayerConnectionDto(serverPlayerConnectionModel);
    }

    public async Task<IEnumerable<PlayerNameDto>> GetPlayerNames(PlayerRecordDto playerRecordDto)
    {
        var playerRecordModel = await _playerRecordRepository.GetByRecordAsync(playerRecordDto.getPartialModel());
        if (playerRecordModel == null) return new List<PlayerNameDto>();
        IEnumerable<PlayerNameModel> playerNameModels = await _playerNameRepository.GetByRecordAsync(playerRecordModel);
        return playerNameModels.Select(pnm => new PlayerNameDto(pnm));
    }

    public async Task<IEnumerable<PlayerIpAddressDto>> GetPlayerIpAddresses(PlayerRecordDto playerRecordDto)
    {
        var playerRecordModel = await _playerRecordRepository.GetByRecordAsync(playerRecordDto.getPartialModel());
        if (playerRecordModel == null) return new List<PlayerIpAddressDto>();
        IEnumerable<PlayerIpAddressModel> playerIpAddressModels = await _playerIpAddressRepository.GetByRecordAsync(playerRecordModel);
        return playerIpAddressModels.Select(piam => new PlayerIpAddressDto(piam));
    }

    public async Task<IEnumerable<PlayerNameDto>> SearchPlayerNames(string name)
    {
        IEnumerable<PlayerNameModel> playerNameModels = await _playerNameRepository.SearchByName(name);
        return playerNameModels.Select(pnm => new PlayerNameDto(pnm));
    }
}