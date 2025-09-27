
using Microsoft.EntityFrameworkCore;

using ArmaReforger.Models;

namespace ArmaReforger.Data;

public class ArmaReforgerDbContext : DbContext
{
    public DbSet<PlayerRecordModel> playerRecords { get; set; }
    public DbSet<ServerRecordModel> serverRecords { get; set; }
    public DbSet<ServerPlayerConnectionModel> serverPlayerConnections { get; set; }
    public DbSet<PlayerNameModel> playerNames { get; set; }
    public DbSet<PlayerIpAddressModel> playerIpAddresses { get; set; }

    public ArmaReforgerDbContext(DbContextOptions<ArmaReforgerDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ServerRecordModel>()
            .HasIndex(sr => new { sr.ipAddress, sr.port })
            .IsUnique();

        modelBuilder.Entity<ServerPlayerConnectionModel>()
            .HasOne(spc => spc.serverRecord)
            .WithMany()
            .HasForeignKey(spc => spc.server_id)
            .IsRequired();

        modelBuilder.Entity<ServerPlayerConnectionModel>()
            .HasOne(spc => spc.playerRecord)
            .WithMany()
            .HasForeignKey(spc => spc.biIdentity)
            .IsRequired();

        modelBuilder.Entity<PlayerNameModel>()
            .HasIndex(pn => new { pn.biIdentity, pn.name })
            .IsUnique();
        modelBuilder.Entity<PlayerNameModel>()
            .HasOne(pn => pn.playerRecord)
            .WithMany()
            .HasForeignKey(pn => pn.biIdentity)
            .IsRequired();

        modelBuilder.Entity<PlayerIpAddressModel>()
            .HasIndex(pia => new { pia.biIdentity, pia.ipAddress })
            .IsUnique();
        modelBuilder.Entity<PlayerIpAddressModel>()
            .HasOne(pn => pn.playerRecord)
            .WithMany()
            .HasForeignKey(pn => pn.biIdentity)
            .IsRequired();

        base.OnModelCreating(modelBuilder);
    }
}
