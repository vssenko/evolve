using Evolve.Domain.PostAggr;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evolve.Infrastructure.DB.Configuration.Mappings
{
    class CommentMapping : EntityTypeConfiguration<Comment>
    {
        public CommentMapping()
        {
            ToTable("dbo.Comment");
            HasKey(x => x.CommentId);
            Property(x => x.CommentId).HasColumnName("CommentId");
            Property(x => x.RelatedCommentId).HasColumnName("RelatedCommentId");
            Property(x => x.CreatedDate).HasColumnName("CreatedDate");
            Property(x => x.PostId).HasColumnName("PostId");
            Property(x => x.UserId).HasColumnName("UserId");
            //Property(x => x.Body).HasColumnName("Body");

            HasRequired(x => x.Post).WithMany(y => y.Comments);
            HasRequired(x => x.User).WithMany(y => y.Comments);
            HasOptional(x => x.RelatedComment).WithMany(x => x.Answers).HasForeignKey(x => x.RelatedCommentId);
        }
    }
}
