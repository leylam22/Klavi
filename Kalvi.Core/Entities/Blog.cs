using Kalvi.Core.Interface;

namespace Kalvi.Core.Entities;

public class Blog : IEntity
{
    public int Id { get ; set ; }
    public string ImagePath { get; set; } = null!;
    public string Blogger { get; set; } = null!;
    public DateTime Date { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
}
