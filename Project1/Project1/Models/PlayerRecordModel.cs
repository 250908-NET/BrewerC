
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArmaReforger.Models;

public class PlayerRecordModel
{
    [Key]
    [Column("bi_identity")]
    public string biIdentity { get; set; }

    public override bool Equals(object? obj)
    {
        if (obj is not PlayerRecordModel other)
            return false;
        return string.Equals(biIdentity, other.biIdentity, StringComparison.Ordinal);
    }

    public override int GetHashCode()
    {
        return biIdentity != null ? biIdentity.GetHashCode(StringComparison.Ordinal) : 0;
    }
}
