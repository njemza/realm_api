using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mbiza.Address.Book.Api.Models
{
    public partial class AddressBook
    {
        public AddressBook()
        {
            AddressBooks = new HashSet<AddressBook>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AddressBookId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MobileNumber { get; set; }

        public string HomeNumber { get; set; }

        public string OfficeNumber { get; set; }

        public string EmailAddress { get; set; }

        public ICollection<AddressBook> AddressBooks { get; set; }
    }
}
