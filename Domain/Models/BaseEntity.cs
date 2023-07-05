using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models;

public abstract class BaseEntity
{
    
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; init; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public DateTime? DeletedAt { get; set; }
}
