namespace makerspace.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class App_Model : DbContext
    {
        public App_Model()
            : base("name=App_Model")
        {
        }

        public virtual DbSet<App_Areas> App_Areas { get; set; }
        public virtual DbSet<App_Membership_Types> App_Membership_Types { get; set; }
        public virtual DbSet<App_User_Area_Memberships> App_User_Area_Memberships { get; set; }
        public virtual DbSet<App_User_Profiles> App_User_Profiles { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<App_Areas>()
                .Property(e => e.title)
                .IsUnicode(false);

            modelBuilder.Entity<App_Areas>()
                .HasMany(e => e.App_User_Area_Memberships)
                .WithRequired(e => e.App_Areas)
                .HasForeignKey(e => e.area_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<App_Membership_Types>()
                .Property(e => e.title)
                .IsUnicode(false);

            modelBuilder.Entity<App_Membership_Types>()
                .HasMany(e => e.App_User_Area_Memberships)
                .WithRequired(e => e.App_Membership_Types)
                .HasForeignKey(e => e.membership_type_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<App_User_Profiles>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<App_User_Profiles>()
                .HasMany(e => e.App_User_Area_Memberships)
                .WithRequired(e => e.App_User_Profiles)
                .HasForeignKey(e => e.user_id)
                .WillCascadeOnDelete(false);
        }
    }
}
