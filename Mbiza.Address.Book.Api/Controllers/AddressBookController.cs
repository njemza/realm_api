using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Mbiza.Address.Book.Api.Cache;
using Mbiza.Address.Book.Api.Models;
using Mbiza.Address.Book.Api.Repository;
using Microsoft.Extensions.Caching.Memory;

namespace Mbiza.Address.Book.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressBookController : ControllerBase
    {
        IAddressBookRepository _addressBookRepository;
        IMemoryCache _memoryCache;
        const string cacheKey = "Records:GetAllRecords";

        /// <summary>
        /// Initializes a new instance of the <see cref="AddressBookController"/> class.
        /// </summary>
        /// <param name="addressBookRepository">The address book repository.</param>
        /// <param name="memoryCache">The memory cache.</param>
        /// <param name="logger">The logger.</param>
        public AddressBookController(IAddressBookRepository addressBookRepository, IMemoryCache memoryCache, ILogger<AddressBookController> logger)
        {
            _addressBookRepository = addressBookRepository;
            _memoryCache = memoryCache;
        }

        /// <summary>
        /// Searches the address book.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("SearchAddressBook")]
        public async Task<ActionResult<IEnumerable<AddressBook>>> SearchAddressBook(string search)
        {
            try
            {
                var result = CacheManager<List<AddressBook>>.Get(_memoryCache, cacheKey);
                if (result == null || result.Count == 0)
                {
                    var addressBooks = await _addressBookRepository.GetAddressBookList();
                    if (addressBooks == null)
                    {
                        return NotFound();
                    }
                    var searchResults = addressBooks.Where(s => s.FirstName.Contains(search) || s.LastName.Contains(search) || s.MobileNumber.Contains(search) || s.HomeNumber.Contains(search) || s.OfficeNumber.Contains(search) || s.EmailAddress.Contains(search)).ToList();
                    return Ok(searchResults);
                }
                var searchResult = result.Where(s => s.FirstName.Contains(search) || s.LastName.Contains(search) || s.MobileNumber.Contains(search) || s.HomeNumber.Contains(search) || s.OfficeNumber.Contains(search) || s.EmailAddress.Contains(search)).ToList();
                return Ok(searchResult);
            }
            catch (Exception ex)
            {
                //Logging TO DO
                return BadRequest();
            }
        }

        /// <summary>
        /// Gets the address books.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAddressBooks")]
        public async Task<ActionResult<IEnumerable<AddressBook>>> GetAddressBooks()
        {
            try
            {
                var result = CacheManager<List<AddressBook>>.Get(_memoryCache, cacheKey);
                if (result == null || result.Count == 0)
                {
                    var addressBooks = await _addressBookRepository.GetAddressBookList();
                    if (addressBooks == null)
                    {
                        return NotFound();
                    }
                    CacheManager<List<AddressBook>>.Set(_memoryCache, cacheKey, addressBooks.ToList());
                    return Ok(addressBooks);
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                //Logging TO DO
                return BadRequest();
            }
        }

        /// <summary>
        /// Gets the address book.
        /// </summary>
        /// <param name="addressId">The address identifier.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAddressBook")]
        public async Task<ActionResult<AddressBook>> GetAddressBook(int addressId)
        {
            if (addressId == 0)
                return BadRequest();

            try
            {
                var result = CacheManager<List<AddressBook>>.Get(_memoryCache, cacheKey);
                if (result == null || result.Count == 0)
                {
                    var address = await _addressBookRepository.GetAddressBook(addressId);
                    if (address == null)
                    {
                        return NotFound();
                    }
                    return Ok(address);
                }
                var item = result.FirstOrDefault(s => s.AddressBookId == addressId);
                return Ok(item);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Adds the address book.
        /// </summary>
        /// <param name="addressBook">The address book.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddAddressBook")]
        public async Task<IActionResult> AddAddressBook([FromBody] AddressBook addressBook)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    bool result = await _addressBookRepository.AddAddressBook(addressBook).ConfigureAwait(false);
                    if (result)
                    {
                        CacheManager<List<AddressBook>>.Remove(_memoryCache, cacheKey);
                        return Ok(result);
                    }                        
                    else
                        return NotFound();
                }
                catch (Exception)
                {
                    return BadRequest();
                }
            }
            return BadRequest();
        }

        /// <summary>
        /// Updates the address book.
        /// </summary>
        /// <param name="addressBook">The address book.</param>
        /// <returns></returns>
        [HttpPut]
        [Route("UpdateAddressBook")]
        public async Task<IActionResult> UpdateAddressBook([FromBody] AddressBook addressBook)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    bool result = await _addressBookRepository.UpdateAddressBook(addressBook).ConfigureAwait(false);
                    if (result)
                    {
                        CacheManager<List<AddressBook>>.Remove(_memoryCache, cacheKey);
                        return Ok(result);
                    }
                    else
                        return NotFound();
                }
                catch (Exception)
                {
                    return BadRequest();
                }
            }
            return BadRequest();
        }

        /// <summary>
        /// Deletes the address book.
        /// </summary>
        /// <param name="addressId">The address identifier.</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("UpdateAddressBook")]
        public async Task<IActionResult> DeleteAddressBook(int addressId)
        {
            if (addressId == 0)
                return BadRequest();

            try
            {
                var success = await _addressBookRepository.DeleteAddressBook(addressId);
                if (success)
                {
                    CacheManager<List<AddressBook>>.Remove(_memoryCache, cacheKey);
                    return Ok(success);
                }
                else
                    return NotFound();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
