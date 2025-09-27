
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArmaReforger.Models;

public class PlayerNameModel
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

    [Required, MinLength(3), MaxLength(32)]
    [Column("name")]
    public string name { get; set; }
}
