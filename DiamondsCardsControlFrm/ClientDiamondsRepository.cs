using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiamondsCardsControlFrm
{
    public class ClientDiamondsRepository
    {
        public IEnumerable<DiamondsClientFormat> GetDiamondsClients()
        {
            DiamondsEntities _db = new DiamondsEntities();

            try
            {
                var diamondsClient = _db.DiamondsClients.Where(x=> x.IsDeleted != true).Select(Transform);
                if (diamondsClient != null)
                {
                    return diamondsClient;
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

        public IEnumerable<DiamondsClientFormat> GetDiamondsClientsByRoom(int sala)
        {
            DiamondsEntities _db = new DiamondsEntities();

            try
            {
                RoomVipRepository z = new RoomVipRepository();
                SalaVIP a = z.ConsultarSalaVip(sala);
                var diamondsClients = _db.DiamondsClients.Where(x => x.NumeroSala.Contains(a.Nombre) && x.IsDeleted!= true).Select(Transform);
                if (diamondsClients != null)
                    return diamondsClients;
                return null;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public DiamondsClient Insert(DiamondsClient client)
        {
            if (client == null)
                return null;

            DiamondsEntities _db = new DiamondsEntities();
            _db.DiamondsClients.Add(client);
            _db.SaveChanges();
            return client;
        }

        public DiamondsClient GetClient(long id)
        {
            DiamondsEntities _db = new DiamondsEntities();
            //if (string.IsNullOrEmpty(id))
            //    return null;

            return _db.DiamondsClients.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<DiamondsClientFormat> GetDiamondsClientByCreditCard(string cardNumber)
        {
            if (string.IsNullOrEmpty(cardNumber))
                return null;
            DiamondsEntities _db = new DiamondsEntities();
            return _db.DiamondsClients.Where(x => x.NoDelDocTarjetaCredito == cardNumber).Select(Transform);
        }

        public bool UpdateClientExitHour(long id)
        {
            DiamondsEntities _db = new DiamondsEntities();
            var currentClient = GetClient(id);
            var originalEntity = _db.DiamondsClients.FirstOrDefault(x => x.Id == id);
            if (originalEntity == null)
                return false;
            var updateClient = currentClient;
            updateClient.ExitDate = DateTime.Now;
            _db.Entry(originalEntity).CurrentValues.SetValues(updateClient);
            _db.SaveChanges();
            return true;

        }

        public bool UpdateClientRevisor(string  Administrator, long id, DiamondsClient cliente)
        {
            DiamondsEntities _db = new DiamondsEntities();
            var currentClient = GetClient(id);
            var originalEntity = _db.DiamondsClients.FirstOrDefault(x => x.Id == id);
            if (originalEntity == null)
                return false;
            var updateClient = currentClient;
            cliente.Coordinador = Administrator;
            cliente.ETD = originalEntity.ETD;
            _db.Entry(originalEntity).CurrentValues.SetValues(cliente);
            _db.SaveChanges();
            return true;

        }
        
        public bool DeleteClient(string Administrator, long id, DiamondsClient cliente) {
            DiamondsEntities _db = new DiamondsEntities();
            var currentClient = GetClient(id);
            var originalEntity = _db.DiamondsClients.FirstOrDefault(x => x.Id == id);
            if (originalEntity == null)
                return false;
            var updateClient = currentClient;
            cliente.Coordinador = Administrator;
            cliente.ETD = originalEntity.ETD;
            cliente.IsDeleted = true;
            _db.Entry(originalEntity).CurrentValues.SetValues(cliente);
            _db.SaveChanges();
            return true;
        }

        public bool UpdateClinetInfo(DiamondsClient client)
        {
            DiamondsEntities _db = new DiamondsEntities();
            try
            {
                var currentClient = GetClient(client.Id);
                var originalEntity = _db.DiamondsClients.FirstOrDefault(x => x.Id == client.Id);
                if (originalEntity == null)
                    return false;
                var updateClient = currentClient;

                _db.Entry(originalEntity).CurrentValues.SetValues(client);
                _db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }


        private DiamondsClientFormat Transform(DiamondsClient client)
        {
            if (client == null)
                return null;

            return new DiamondsClientFormat
            {
                Aerolinea = client.Aerolinea,
                Aprobacion = client.Aprobacion,
                Entidad = client.Banco,
                Ciudad = client.Ciudad,
                Coordinador = client.Coordinador,
                DescripcionTC = client.DescripcionTC,
                DocIdentidad = client.DocIdentidad,
                DocumentoHabilitante = client.DocumentoHabilitante,
                Edad = client.Edad,
                ETD = client.ETD,
                FechaIngreso = client.FechaIngreso,
                Id = client.Id,
                NoDelDocHabilVoucher = client.NoDelDocHabilVoucher,
                NoDelDocTarjetaCredito = client.NoDelDocTarjetaCredito,
                Nombre = client.Nombre,
                Sala = client.NumeroSala,
                Observaciones = client.Observaciones,
                Supervisor = client.Supervisor,
                Vuelo = client.Vuelo,
                NoInvitacionLineaAerea = client.NoInvitacionLineaAerea,
                
            };
        }
    }
}
