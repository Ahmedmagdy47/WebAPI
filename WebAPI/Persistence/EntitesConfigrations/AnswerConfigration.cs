namespace WebAPI.Persistence.EntitesConfigrations
{
    public class AnswerConfigration : IEntityTypeConfiguration<Answer>
    {
        public void Configure(EntityTypeBuilder<Answer> builder)
        {
            builder.HasIndex(x => new { x.Content, x.QuestionId }).IsUnique();
            builder.Property(x => x.Content).HasMaxLength(1000);
        }
    }
}
