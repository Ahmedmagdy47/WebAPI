namespace WebAPI.Persistence.EntitesConfigrations
{
    public class UserRoleConfigration : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            //Default Data

            builder.HasData([ new IdentityUserRole<string>
                {
                    RoleId = DefaultRoles.AdminRoleId,
                    UserId = DefaultUsers.AdminId
                }
            ]);
        }
    }
}
