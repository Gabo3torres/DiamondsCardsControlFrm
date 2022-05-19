using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiamondsCardsControlFrm
{
    public class UserTransversal
    {
        public long Id { get; set; }
        public string TaxId { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string Password { get; set; }
        public int RolId { get; set; }
        public string UserName { get; set; }
        public SalaVIP Sala { get; set; }
        public bool? Admin { get; set; }
    }
}
