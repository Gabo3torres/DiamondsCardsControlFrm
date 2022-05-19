using System;
using System.Collections.Generic;
using System.Linq;

namespace DiamondsCardsControlFrm
{


    public class AirLineRepository
    {
        private DiamondsEntities _db = new DiamondsEntities();

        public IEnumerable<AereoLinea> GetAereoLineas()
        {
            return _db.AereoLineas.ToList();
        }
    }

    public class BankRepository
    {


        public List<Banco> GetBancos()
        {
            DiamondsEntities _db = new DiamondsEntities();
            var x = _db.Bancoes.ToList();
            return x;
        }
    }

    public class DescriptionCardRepository
    {
        public List<DescriptionCard> GetDescriptionCards()
        {
            DiamondsEntities _db = new DiamondsEntities();
            var x = _db.DescriptionCards.ToList();
            return x;
        }
    }

    public class CreditCardRepository
    {
        public List<CreditCard> GetCreditCards()
        {
            try
            {
                DiamondsEntities _db = new DiamondsEntities();
                return _db.CreditCards.ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
