
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArmaReforger.Models;

public class ServerRecordModel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public int id { get; set; }

    [Required]
    [Column("ip_address")]
    public string ipAddress { get; set; }
    
    [Required]
    [Column("port")]
    public int port { get; set; }

    [Required]
    [Column("name")]
    public string name { get; set; }
}
