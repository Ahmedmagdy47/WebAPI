namespace WebAPI.Persistence.EntitesConfigrations
{
    public class VoteAnswersConfigration : IEntityTypeConfiguration<VoteAnswer>
    {
        public void Configure(EntityTypeBuilder<VoteAnswer> builder)
        {
            builder.HasIndex(x => new { x.VoteId, x.AnswerId }).IsUnique();
        }
    }
}
