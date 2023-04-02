using ContactPro.Models;

namespace ContactPro.Services.Interfaces
{
    public interface IAddressBookService
    {
        //add contact to a category async
        Task AddContactToCategoryAsync(int categoryId, int contactId);

        //is the contact in a category
        Task<bool> IsContactInCategory(int categoryId, int contactId);

       //get the user categories async
       //try to list the categories
        Task<IEnumerable<Category>> GetUserCategoriesAsync(string userId);

        //get the contact category id
        Task<ICollection<int>> GetContactCategoryIdsAsync(int contactId);

        //give the categories for the id
        Task<ICollection<Category>> GetContactCategoriesAsync(int contactId);

        //remove a contact from a category
        Task RemoveContactFromCategoryAsync(int categoryId, int contactId);

        //search for contacts
        IEnumerable<Contact> SearchForContacts(string searchString, string userId);
    }
}
