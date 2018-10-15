namespace Lab4University
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class UniversityDB : DbContext
    {
        public UniversityDB()
            : base("name=University")
        {
        }

        public virtual DbSet<Chair> Chairs { get; set; }
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<Institute> Institutes { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Subject> Subjects { get; set; }
        public virtual DbSet<Teacher> Teachers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Chair>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Chair>()
                .HasMany(e => e.Teachers)
                .WithRequired(e => e.Chair)
                .HasForeignKey(e => e.Chair_ID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Group>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Group>()
                .HasMany(e => e.Students)
                .WithRequired(e => e.Group)
                .HasForeignKey(e => e.Group_ID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Group>()
                .HasMany(e => e.Subjects)
                .WithMany(e => e.Groups)
                .Map(m => m.ToTable("GroupSubject"));

            modelBuilder.Entity<Institute>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Institute>()
                .HasMany(e => e.Chairs)
                .WithRequired(e => e.Institute)
                .HasForeignKey(e => e.Institute_ID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Student>()
                .Property(e => e.PIB)
                .IsUnicode(false);

            modelBuilder.Entity<Subject>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Subject>()
                .HasMany(e => e.Teachers)
                .WithMany(e => e.Subjects)
                .Map(m => m.ToTable("TeacherSubject"));

            modelBuilder.Entity<Teacher>()
                .Property(e => e.PIB)
                .IsUnicode(false);
        }
    }
}
