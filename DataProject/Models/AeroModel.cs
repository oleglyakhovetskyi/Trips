namespace DataProject
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class AeroModel : DbContext
    {
        public AeroModel()
            : base("name=AeroModel")
        {
        }
      
        public virtual DbSet<Company> Company { get; set; }
        public virtual DbSet<Pass_in_trip> Pass_in_trip { get; set; }
        public virtual DbSet<Passenger> Passenger { get; set; }
        public virtual DbSet<Trip> Trip { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>()
                .Property(e => e.name)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Company>()
                .HasMany(e => e.Trip)
                .WithRequired(e => e.Company)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Pass_in_trip>()
                .Property(e => e.place)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Passenger>()
                .Property(e => e.name)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Passenger>()
                .HasMany(e => e.Pass_in_trip)
                .WithRequired(e => e.Passenger)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Trip>()
                .Property(e => e.plane)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Trip>()
                .Property(e => e.town_from)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Trip>()
                .Property(e => e.town_to)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Trip>()
                .HasMany(e => e.Pass_in_trip)
                .WithRequired(e => e.Trip)
                .WillCascadeOnDelete(false);
        }
    }
}
