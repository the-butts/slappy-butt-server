namespace SlappyButt.Data
{
    using System.Data.Entity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using SlappyButt.Models;

    public class SlappyButtDbContext : IdentityDbContext<User>
    {
        public SlappyButtDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public virtual IDbSet<Avatar> Images { get; set; }

        public virtual IDbSet<Butt> Butts { get; set; }

        public virtual IDbSet<Score> Scores { get; set; }

        public virtual IDbSet<ImageInfo> ImageInfos { get; set; }
        
        public static SlappyButtDbContext Create()
        {
            return new SlappyButtDbContext();
        }
    }
}
