using Microsoft.EntityFrameworkCore;

namespace Mbiza.Address.Book.Api.Models
{
    public partial class AddressBookDBContext : DbContext
    {
        public AddressBookDBContext()
        {

        }

        public AddressBookDBContext(DbContextOptions<AddressBookDBContext> options) : base(options)
        {

        }

        public virtual DbSet<AddressBook> AddressBooks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AddressBook>().ToTable("AddressBook");
        }
    }
}
