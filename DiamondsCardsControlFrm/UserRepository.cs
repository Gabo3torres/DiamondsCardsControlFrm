using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiamondsCardsControlFrm
{
    public class UserRepository
    {

        public User ConsultarUsuario(string userName, string userPassword)
        {
            DiamondsEntities _db = new DiamondsEntities();

            try
            {
                var user = _db.Users.FirstOrDefault(x => x.UserName == userName && x.Password == userPassword);
                if (user != null)
                {
                    return user;
                }
                else { return null; }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            finally
            {
                //_db.Database.Connection.Close();
            }
        }

        public List<User> GetUsers()
        {
            DiamondsEntities _db = new DiamondsEntities();

            return _db.Users.ToList();
        }


    }
}
