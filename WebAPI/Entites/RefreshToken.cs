namespace WebAPI.Entites;

[Owned]
public class RefreshToken
{
    public string Token { get; set; } = string.Empty;
    public DateTime ExpireDate { get; set; }
    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    public DateTime? RevokedOn { get; set; } = DateTime.UtcNow;
    public bool IsExpired => DateTime.UtcNow >= ExpireDate;
    public bool IsActive => RevokedOn is null && !IsExpired;
}