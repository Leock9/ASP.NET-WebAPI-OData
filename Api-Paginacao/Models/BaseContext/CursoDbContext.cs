using System.Data.Entity;

namespace Api_Paginacao.Models.BaseContext
{
    public class CursoDbContext : DbContext
    {
        public CursoDbContext() : base("ApiConnectionString")
        {
            Database.Log = d => System.Diagnostics.Debug.WriteLine(d);
        }

        public DbSet<Curso> Cursos { get; set; }

    }
}