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
        public DbSet<FeedItem> FeedItems { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<ImageTag> ImageTags { get; set; }
        //public DbSet<FamilyMember> FamilyMembers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            modelBuilder.Entity<Family>().HasOptional(f => f.Admin).WithOptionalDependent();//.WithOptionalPrincipal();

            modelBuilder.Entity<User>().HasOptional(u => u.Family).WithMany(f => f.Members);//.WithOptionalDependent();//.WithOptionalPrincipal();

            modelBuilder.Entity<Relationship>().HasRequired(r => r.SourceUser).WithMany(u => u.Relationships).WillCascadeOnDelete(false);
            modelBuilder.Entity<Relationship>().HasRequired(r => r.RelatedUser).WithMany().WillCascadeOnDelete(true);//.WithOptionalDependent();

            //modelBuilder.Entity<Relationship>().HasRequired(r => r.RelatedUser).WithRequiredPrincipal();
            //modelBuilder.Entity<Relationship>().HasRequired(r => r.User).WithMany(u => u.Relationships);

            //modelBuilder.Entity<User>().
            //    HasOptional(u => u.Family).WithMany().HasForeignKey(u => u.FamilyId);

            //modelBuilder.Entity<User>().HasMany(u => u.Relationships).WithRequired(r => r.User).HasForeignKey(
            //    r => r.UserId).WillCascadeOnDelete(false);

            //modelBuilder.Entity<Relationship>().HasRequired(r => r.User).WithMany(u => u.Relationships).
            //    HasForeignKey(r => r.UserId).WillCascadeOnDelete(false);

            // ---------------- this is the one that was needed to get it to run
            //modelBuilder.Entity<Relationship>().HasRequired(r => r.RelatedUser).WithMany().
            //    HasForeignKey(r => r.RelatedUserId).WillCascadeOnDelete(false);

            //modelBuilder.Entity<Family>().
            //    HasRequired(f => f.Owner).WithMany().HasForeignKey(f => f.OwnerId);


        }

    }
}