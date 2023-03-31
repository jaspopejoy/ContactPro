﻿using ContactPro.Data;
using ContactPro.Models;
using ContactPro.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ContactPro.Services
{
    public class AddressBookService : IAddressBookService
    {

        private readonly ApplicationDbContext _context;

        public AddressBookService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddContactToCategoryAsync(int categoryId, int contactId)
        {
           try
            {
                //check to see if the category is in the contact 
                if (!await IsContactInCategory(categoryId, contactId))
                {
                    Contact? contact = await _context.Contacts.FindAsync(contactId);
                    Categories? category = await _context.Categories.FindAsync(categoryId);

                    if(category != null && contact != null)
                    {
                        category.Contacts.Add(contact);
                        await _context.SaveChangesAsync();
                    }

                }
            }
            catch (Exception) 
            {
                throw;
            }
        }

        public Task<ICollection<Categories>> GetContactCateforiesAsync(int contactId)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<int>> GetContactCategoryIdsAsync(int contactId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Categories>> GetUserCategoriesAsync(string userId)
        {
           List<Categories> categories = new List<Categories>();

            try
            {
                categories = await _context.Categories.Where(c => c.AppUserId == userId)
                                                          .OrderBy(c => c.Name)
                                                          .ToListAsync();
            }
            catch
            {
                throw;
            }

            return categories;
        }

        public async Task<bool> IsContactInCategory(int categoryId, int contactId)
        {
            Contact? contact = await _context.Contacts.FindAsync(contactId);

            return await _context.Categories
                                 .Include(c => c.Contacts)
                                 .Where(c => c.Id == categoryId && c.Contacts.Contains(contact))
                                 .AnyAsync();
        }

        public Task RemoveContactFromCategoryAsync(int categoryId, int contactId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Contact> SearchForContacts(string searchString, string userId)
        {
            throw new NotImplementedException();
        }
    }
}
