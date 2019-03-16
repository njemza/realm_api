using Mbiza.Address.Book.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mbiza.Address.Book.Api.Repository
{
    public interface IAddressBookRepository
    {
        Task<IList<AddressBook>> GetAddressBookList();
        Task<AddressBook> GetAddressBook(int addressId);
        Task<bool> AddAddressBook(AddressBook addressBook);
        Task<bool> DeleteAddressBook(int addressId);
        Task<bool> UpdateAddressBook(AddressBook addressBook);
    }
}
