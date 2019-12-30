using Blog.Core.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Blog.Data.Mapping.Articles
{
    public class ArticleCommentMap : EntityTypeConfiguration<ArticleComment>
    {
        public override void Configure(EntityTypeBuilder<ArticleComment> b)
        {
            b.ToTable(nameof(ArticleComment));
            b.HasKey(x => x.Id);

            b.Property(x => x.CommentText).HasMaxLength(500).IsRequired();
            b.Property(x => x.IsApproved).HasDefaultValue(true).IsRequired();

            b.HasOne(x => x.Article)
                .WithMany(x => x.ArticleComments)
                .HasForeignKey(x => x.ArticleId);

            base.Configure(b);
        }
    }
}
