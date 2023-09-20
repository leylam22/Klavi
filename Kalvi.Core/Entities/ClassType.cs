using Kalvi.Core.Interface;
using System.ComponentModel.DataAnnotations;

namespace Kalvi.Core.Entities;

public class ClassType : IEntity
{
    public int Id { get ; set ; }
    [Required, MaxLength(50)]
    public string Type { get; set; } = null!;
}
