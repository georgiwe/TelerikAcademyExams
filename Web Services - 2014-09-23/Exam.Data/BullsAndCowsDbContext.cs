namespace Exam.Data
{
    using System.Data.Entity;

    using Microsoft.AspNet.Identity.EntityFramework;

    using Exam.Models;
    using Exam.Data.Migrations;

    public class BullsAndCowsDbContext : IdentityDbContext<User>
    {
        public BullsAndCowsDbContext()
            : base("BullsAndCowsDb", throwIfV1Schema: false)
        {
            Database.SetInitializer(
                new MigrateDatabaseToLatestVersion<BullsAndCowsDbContext, Configuration>());
        }

        public static BullsAndCowsDbContext Create()
        {
            return new BullsAndCowsDbContext();
        }

        public virtual IDbSet<Game> Games { get; set; }

        public virtual IDbSet<Guess> Guesses { get; set; }

        public virtual IDbSet<Notification> Alerts { get; set; }
    }
}
