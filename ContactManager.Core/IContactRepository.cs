
using ContactManager.Core;

   public interface IContactRepository
{
    List<Contact> GetAll();
    Contact GetById(int Id);
    void Add(Contact? contact);
    void Update(Contact? contact);
    void Delete(int id);
}

