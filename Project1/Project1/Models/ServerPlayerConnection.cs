
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArmaReforger.Models;

public class ServerPlayerConnectionModel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public int id { get; set; }

    [Required]
    [Column("server_id")]
    public int server_id { get; set; } // Foreign key
    [ForeignKey(nameof(server_id))]
    public ServerRecordModel serverRecord { get; set; }

    [Required]
    [Column("bi_identity")]
    public string biIdentity { get; set; } // Foreign key
    [ForeignKey(nameof(biIdentity))]
    public PlayerRecordModel playerRecord { get; set; }

    [Required]
    [Column("connection_time")]
    public DateTime connectionTime { get; set; }

    [Required, MinLength(5)]
    [Column("action")]
    public string action { get; set; }
}
