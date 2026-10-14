namespace AlertSync.Web.Models;

public class UserDocument
{
    public int Id { get; set; }

    public string UserId { get; set; } = string.Empty;

    public ApplicationUser Owner { get; set; } = null!;

    /// <summary>Null means the document belongs to the account holder, not a family member.</summary>
    public int? FamilyMemberId { get; set; }

    public FamilyMember? FamilyMember { get; set; }

    public string DocumentName { get; set; } = string.Empty;

    public DocumentType DocumentType { get; set; }

    public string StoredFileName { get; set; } = string.Empty;

    public string OriginalFileName { get; set; } = string.Empty;

    /// <summary>Private storage path. Files must not be placed under wwwroot.</summary>
    public string StoragePath { get; set; } = string.Empty;

    public string ContentType { get; set; } = string.Empty;

    public long FileSize { get; set; }

    public string? Description { get; set; }

    public DateTime UploadedAt { get; set; }
}
