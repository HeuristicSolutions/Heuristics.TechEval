using Heuristics.TechEval.Core.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Heuristics.TechEval.Core
{

    public class DataContext : DbContext {

		public DataContext() : base("Database") { }

		public DbSet<Member> Members { get; set; }
		public DbSet<Category> Categories { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            modelBuilder.Entity<Member>()
                .ToTable("Member")
                .HasKey(m => m.Id)
                .Property(m => m.Name)
                .IsRequired();

            modelBuilder.Entity<Member>()
                .Property(m => m.LastUpdated)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed); // Use DatabaseGeneratedOption for default value

            modelBuilder.Entity<Member>()
                .HasRequired(m => m.Category)
                .WithMany(c => c.Members)
                .HasForeignKey(m => m.CategoryId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Category>()
                .ToTable("Category")
                .HasKey(c => c.Id)
                .Property(c => c.Name)
                .IsRequired();

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
	}
}