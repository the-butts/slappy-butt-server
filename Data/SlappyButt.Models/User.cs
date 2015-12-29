namespace SlappyButt.Models
{
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    public class User : IdentityUser
    {
        private ICollection<Butt> butts;
        private ICollection<Score> scores;

        public User()
            : base()
        {
            this.butts = new HashSet<Butt>();
            this.scores = new HashSet<Score>();
        }

        public User(string username)
            : base(username)
        {
            this.butts = new HashSet<Butt>();
            this.scores = new HashSet<Score>();
        }

        public bool IsDeleted { get; set; }

        public virtual ICollection<Butt> Butts
        {
            get { return this.butts; }
            set { this.butts = value; }
        }

        public virtual ICollection<Score> Scores
        {
            get { return this.scores; }
            set { this.scores = value; }
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);

            // Add custom user claims here
            return userIdentity;
        }
    }
}
