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
        public DbSet<User> Users { get; set; }
        public DbSet<Relationship> Relationships { get; set; }
        public DbSet<TimelineEvent> TimelineEvents { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<FamilyMember> FamilyMembers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<User>().
                HasOptional(u => u.DefaultFamily).WithMany().HasForeignKey(u => u.DefaultFamilyId);

            modelBuilder.Entity<Family>().
                HasRequired(f => f.Owner).WithMany().HasForeignKey(f => f.OwnerId);
        }

    }
}