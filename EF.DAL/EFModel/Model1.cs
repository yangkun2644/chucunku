namespace EF.DAL.EFModel
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

        public virtual DbSet<Dispose> Dispose { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Dispose>()
                .Property(e => e.DisposeName)
                .IsUnicode(false);

            modelBuilder.Entity<Dispose>()
                .Property(e => e.DisposeText1)
                .IsUnicode(false);

            modelBuilder.Entity<Dispose>()
                .Property(e => e.DisposeText2)
                .IsUnicode(false);

            modelBuilder.Entity<Dispose>()
                .Property(e => e.DisposeText3)
                .IsUnicode(false);
        }
    }
}
