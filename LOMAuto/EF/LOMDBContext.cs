namespace LOMAuto.EF
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class LOMDBContext : DbContext
    {
        public LOMDBContext()
            : base("name=LOMDBContext")
        {
        }

        public virtual DbSet<T_LOM> T_LOM { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
