
using LabWork_pt2.Entity;

namespace LabWork_pt2.Repository
{
    public class UserRepository
    {
        private DataBaseContext _context = new DataBaseContext();

        public User SaveUser(User user)
        {
            _context.Users.Add(user);
            return user;
        }
    }
}
