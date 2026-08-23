namespace WebAPI.Persistence.EntitesConfigrations
{
    public class RoleClaimConfigration : IEntityTypeConfiguration<IdentityRoleClaim<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityRoleClaim<string>> builder)
        {
            //Default Data
            var permission = Permissions.GetAllPermissions();
            var adminClaims = new List<IdentityRoleClaim<string>>();

            for (var i = 0; i < permission.Count; i++)
            {
                adminClaims.Add(new IdentityRoleClaim<string>
                {
                    Id = i + 1,
                    ClaimType = Permissions.Type,
                    ClaimValue = permission[i],
                    RoleId = DefaultRoles.AdminRoleId
                });
            }

            builder.HasData(adminClaims);
        }
    }
}
