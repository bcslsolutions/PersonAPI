using PersonAPI.Data.Dto;

namespace PersonAPI.Data
{
    public interface IDataManager
    {
        void AddPersonToRepository(Person person);

        Person GetPersonFromRepositoryById(int id);
    }
}
