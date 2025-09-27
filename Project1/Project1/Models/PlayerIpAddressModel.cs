
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArmaReforger.Models;

public class PlayerIpAddressModel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public int id { get; set; }

    [Required]
    [Column("bi_identity")]
    public string biIdentity { get; set; } // Foreign key
    [ForeignKey(nameof(biIdentity))]
    public PlayerRecordModel playerRecord { get; set; }

    [Required]
    [Column("ip_address")]
    public string ipAddress { get; set; }
}
