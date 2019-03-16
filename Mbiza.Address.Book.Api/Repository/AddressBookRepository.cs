using System.Threading.Tasks;
using System.Collections.Generic;
using Mbiza.Address.Book.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Mbiza.Address.Book.Api.Repository
{
    public class AddressBookRepository : IAddressBookRepository
    {
        AddressBookDBContext _db;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddressBookRepository"/> class.
        /// </summary>
        /// <param name="db">The database.</param>
        public AddressBookRepository(AddressBookDBContext db)
        {
            _db = db;
        }

        /// <summary>
        /// Adds the address book.
        /// </summary>
        /// <param name="addressBook">The address book.</param>
        /// <returns></returns>
        public async Task<bool> AddAddressBook(AddressBook addressBook)
        {
            bool success = false;
            if(_db != null)
            {
                await _db.AddressBooks.AddAsync(addressBook);
                await _db.SaveChangesAsync();
                success = true;
            }
            return success;
        }

        /// <summary>
        /// Deletes the address book.
        /// </summary>
        /// <param name="addressId">The address identifier.</param>
        /// <returns></returns>
        public async Task<bool> DeleteAddressBook(int addressId)
        {
            bool success = false;
            if (_db != null)
            {
                var address = await _db.AddressBooks.FirstOrDefaultAsync(a => a.AddressBookId == addressId);
                if (address != null)
                {
                    _db.AddressBooks.Remove(address);

                    success = await _db.SaveChangesAsync() == 1 ? true : false;
                }
            }
            return success;
        }

        /// <summary>
        /// Gets the address book.
        /// </summary>
        /// <param name="addressId">The address identifier.</param>
        /// <returns></returns>
        public async Task<AddressBook> GetAddressBook(int addressId)
        {
            if (_db != null)
            {
                return await _db.AddressBooks.FirstOrDefaultAsync(a => a.AddressBookId == addressId);
            }
            return null;
        }

        /// <summary>
        /// Gets the address book list.
        /// </summary>
        /// <returns></returns>
        public async Task<IList<AddressBook>> GetAddressBookList()
        {
            try
            {
                if (_db != null)
                {
                    var results = await _db.AddressBooks.ToListAsync();
                    return results.ToArray();
                }
            }
            catch (System.Exception ex)
            {
                throw;
            }
            
            return null;
        }

        /// <summary>
        /// Updates the address book.
        /// </summary>
        /// <param name="addressBook">The address book.</param>
        /// <returns></returns>
        public async Task<bool> UpdateAddressBook(AddressBook addressBook)
        {
            bool success = false;
            if (_db != null)
            {
                _db.AddressBooks.Update(addressBook);
                await _db.SaveChangesAsync();
                success = true;
            }
            return success;
        }
    }
}
