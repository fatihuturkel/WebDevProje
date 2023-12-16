using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace WebDevProje.Models
{
    public class HastaneContext : DbContext
    {
        public DbSet<AnabilimDali> AnabilimDallari { get; set; }
        public DbSet<Poliklinik> Poliklinikler { get; set; }
        public DbSet<Kisi> Kisiler { get; set; }
        public DbSet<Doktor> Doktorlar { get; set; }

        private const string ConnectionString = "" +
            "Server=FutV4\\SQLEXPRESS;" +
            "Database=HastaneDB;" +
            "User Id=sa;Password=fatihasude0;" +
            "TrustServerCertificate=True;";

        public bool IsConnectionOpen()
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionString);
        }
    }
}
