using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace KidSteps.Models
{
    public class KidStepsContext : DbContext
    {
        public DbSet<Family> Families { get; set; }
        public DbSet<User> Members { get; set; }
        public DbSet<Relationship> Relationships { get; set; }
        public DbSet<TimelineEvent> TimelineEvents { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            //modelBuilder.Entity<Family>()
            //.HasMany<User>(f => f.Members)
            //.WithMany(u => u.Families)
            //.Map(x =>
            //{
            //    x.MapLeftKey("FamilyId");
            //    x.MapRightKey("MemberId");
            //    x.ToTable("FamilyMember");
            //});
        }
    }
}