using Xunit;

using ArmaReforger.Data;
using ArmaReforger.DTOs;
using ArmaReforger.Repository;
using ArmaReforger.Services;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using ArmaReforger.Models;
using System.Threading.Tasks;

namespace ArmaReforgerTests;

public class ServiceTests : IDisposable
{
    private ArmaReforgerDbContext _armaReforgerDbContext;
    private PlayerRecordRepository _playerRecordRepository;
    private PlayerNameRepository _playerNameRepository;
    private PlayerIpAddressRepository _playerIpAddressRepository;
    private ServerRecordRepository _serverRecordRepository;
    private ServerPlayerConnectionRepository _serverPlayerConnectionRepository;
    private PlayerService _playerService;
    private ServerService _serverService;

    public ServiceTests()
    {
        var dbOptions = new DbContextOptionsBuilder<ArmaReforgerDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDB")
            .Options;
        this._armaReforgerDbContext = new ArmaReforgerDbContext(dbOptions);
        this._playerNameRepository = new PlayerNameRepository(this._armaReforgerDbContext);
        this._playerIpAddressRepository = new PlayerIpAddressRepository(this._armaReforgerDbContext);
        this._serverPlayerConnectionRepository = new ServerPlayerConnectionRepository(this._armaReforgerDbContext);
        this._playerRecordRepository = new PlayerRecordRepository(this._armaReforgerDbContext);
        this._serverRecordRepository = new ServerRecordRepository(this._armaReforgerDbContext);
        this._playerService = new PlayerService(
            this._armaReforgerDbContext, this._playerRecordRepository, this._playerNameRepository,
            this._playerIpAddressRepository, this._serverPlayerConnectionRepository, this._serverRecordRepository
            );
        this._serverService = new ServerService(this._armaReforgerDbContext, this._serverRecordRepository);
    }

    public void Dispose()
    {
        // Remove all entities from each DbSet
        _armaReforgerDbContext.playerIpAddresses.RemoveRange(_armaReforgerDbContext.playerIpAddresses);
        _armaReforgerDbContext.playerNames.RemoveRange(_armaReforgerDbContext.playerNames);
        _armaReforgerDbContext.serverPlayerConnections.RemoveRange(_armaReforgerDbContext.serverPlayerConnections);
        _armaReforgerDbContext.serverRecords.RemoveRange(_armaReforgerDbContext.serverRecords);
        _armaReforgerDbContext.playerRecords.RemoveRange(_armaReforgerDbContext.playerRecords);

        _armaReforgerDbContext.SaveChangesAsync();
        _armaReforgerDbContext.DisposeAsync();
    }

    [Fact]
    public async Task ServerService_AddServer()
    {
        ServerRecordDto newServerRecordDto = new ServerRecordDto("1.2.3.4", 1, "TestServer1");
        ServerRecordDto postedServerRecordDto = await this._serverService.RegisterServer(newServerRecordDto);
        Assert.True(newServerRecordDto.Equals(postedServerRecordDto));
        var serverRecordModel1 = await this._armaReforgerDbContext.serverRecords
            .Where(srm => srm.ipAddress == "1.2.3.4" && srm.port == 1).FirstAsync();
        Assert.NotNull(serverRecordModel1);
        ServerRecordDto serverRecordDto2 = new ServerRecordDto(serverRecordModel1);
        Assert.True(newServerRecordDto.Equals(serverRecordDto2));
    }

    [Fact]
    public async Task ServerService_UpdateServer()
    {
        await ServerService_AddServer();
        ServerRecordDto oldServerRecordDto = new ServerRecordDto("1.2.3.4", 1, "TestServer1");
        ServerRecordDto newServerRecordDto = new ServerRecordDto("1.2.3.4", 2, "TestServer2");
        ServerRecordDto updatedServerRecordDto = await this._serverService.UpdateServer(oldServerRecordDto, newServerRecordDto);
        Assert.True(newServerRecordDto.Equals(updatedServerRecordDto));
        var serverRecordModel = await this._armaReforgerDbContext.serverRecords
            .Where(srm => srm.ipAddress == "1.2.3.4" && srm.port == 2).FirstAsync();
        Assert.NotNull(serverRecordModel);
        ServerRecordDto dbServerRecordDto = new ServerRecordDto(serverRecordModel);
        Assert.True(newServerRecordDto.Equals(dbServerRecordDto));
    }

    [Fact]
    public async Task ServerService_DeleteServer()
    {
        await ServerService_AddServer();
        ServerRecordDto newServerRecordDto = new ServerRecordDto("1.2.3.4", 1, "TestServer1");
        ServerRecordDto oldServerRecordDto = await this._serverService.DeleteServer(newServerRecordDto);
        Assert.True(newServerRecordDto.Equals(oldServerRecordDto));
        Assert.True(this._armaReforgerDbContext.serverRecords.Count() == 0);
    }

    [InlineData("1", "Test Player")]
    [Theory]
    public async Task PlayerService_ReportPlayerConnection(string playerIdentity, string playerName)
    {
        await ServerService_AddServer();
        PlayerRecordDto playerRecordDto = new PlayerRecordDto(playerIdentity);
        PlayerNameDto playerNameDto = new PlayerNameDto(playerRecordDto, playerName);
        PlayerIpAddressDto playerIpAddressDto = new PlayerIpAddressDto(playerRecordDto, "4.3.2.1");
        ServerRecordDto serverRecordDto = new ServerRecordDto("1.2.3.4", 1, "");
        ServerPlayerConnectionDto serverPlayerConnectionDto = new ServerPlayerConnectionDto(serverRecordDto, playerRecordDto, DateTime.Now, "connected");
        await this._playerService.RegisterPlayerConnection(
            playerRecordDto,
            playerNameDto,
            playerIpAddressDto,
            serverPlayerConnectionDto,
            serverRecordDto
            );

        PlayerRecordModel playerRecordModel = await this._armaReforgerDbContext.playerRecords
            .Where(prm => prm.biIdentity.Equals(playerIdentity)).FirstAsync();
        Assert.NotNull(playerRecordModel);
        ServerPlayerConnectionModel serverPlayerConnectionModel = await this._armaReforgerDbContext.serverPlayerConnections
            .Where(spcm => spcm.playerRecord.biIdentity.Equals(playerIdentity)).FirstAsync();
        Assert.NotNull(serverPlayerConnectionModel);
    }

    [Fact]
    public async Task PlayerService_SearchNames()
    {
        await PlayerService_ReportPlayerConnection("1", "John");
        await PlayerService_ReportPlayerConnection("1", "Jake");
        await PlayerService_ReportPlayerConnection("1", "Kohn");

        IEnumerable<PlayerNameDto> playerNames;
        playerNames = await this._playerService.SearchPlayerNames("J");
        Assert.True(playerNames.Count() == 2);
        playerNames = await this._playerService.SearchPlayerNames("A");
        Assert.True(playerNames.Count() == 1);
        playerNames = await this._playerService.SearchPlayerNames("ohn");
        Assert.True(playerNames.Count() == 2);
    }
}