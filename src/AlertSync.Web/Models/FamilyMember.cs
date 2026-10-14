namespace AlertSync.Web.Models;

public class FamilyMember
{
    public int Id { get; set; }

    public string UserId { get; set; } = string.Empty;

    public ApplicationUser Owner { get; set; } = null!;

    public string Name { get; set; } = string.Empty;

    public RelationshipType Relationship { get; set; }

    public DateOnly? DateOfBirth { get; set; }

    public string? Notes { get; set; }

    public DateTime CreatedAt { get; set; }

    public ICollection<UserDocument> Documents { get; set; } = new List<UserDocument>();
}
