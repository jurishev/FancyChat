using System.Collections.Generic;

namespace Chat.Data
{
    public interface IUserService
    {
        bool Exists(string login);

        IEnumerable<User> Get(int offset, int limit);

        void Create(User user);

        User Read(string login);

        void Update(User user);

        void Delete(string login);
    }
}
