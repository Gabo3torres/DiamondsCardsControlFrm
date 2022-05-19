using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiamondsCardsControlFrm
{
    public class RoomVipRepository
    {
        public IEnumerable<SalaVIP> ConsultarSalaVip()
        {            

            try
            {

                DiamondsEntities _db = new DiamondsEntities();
                
                var user = _db.SalaVIPs;
                if (user != null)
                {
                    return user;
                }
                else { return null; }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            finally
            {
                //_db.Database.Connection.Close();
            }
        }

        public SalaVIP ConsultarSalaVip(int numberRoom)
        {

            try
            {

                DiamondsEntities _db = new DiamondsEntities();

                var user = _db.SalaVIPs.FirstOrDefault(x=>x.Id == numberRoom);
                if (user != null)
                {
                    return user;
                }
                else { return null; }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            finally
            {
                //_db.Database.Connection.Close();
            }
        }
    }
}
