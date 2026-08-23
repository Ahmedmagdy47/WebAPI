namespace WebAPI.Persistence.EntitesConfigrations
{
    public class QuestionConfigration : IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> builder)
        {
            builder.HasIndex(x => new { x.Content, x.PollId }).IsUnique();
            builder.Property(x => x.Content).HasMaxLength(1000);
        }
    }
}
