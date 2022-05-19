using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiamondsCardsControlFrm
{
    public class DiamondsClientFormat
    {
        public long Id { get; set; }//0
        public string Supervisor { get; set; }//1
        public Nullable<System.DateTime> FechaIngreso { get; set; }//2
        //public Nullable<System.DateTime> ExitDate { get; set; }
        public string Aerolinea { get; set; }//3
        public string Vuelo { get; set; }//4
        public Nullable<System.DateTime> ETD { get; set; }//5
        public string Nombre { get; set; }//6
        
        public string Edad { get; set; }//7
        public string Entidad { get; set; }//8
        public string NoDelDocTarjetaCredito { get; set; }//9
        public string NoDelDocHabilVoucher { get; set; }//10
        public string Aprobacion { get; set; }//11
        public string NoInvitacionLineaAerea { get; set; }//12
        public string DocIdentidad { get; set; }//13
        public string Ciudad { get; set; }//14
        public string DocumentoHabilitante { get; set; }//15
        public string DescripcionTC { get; set; }//16
        public string Observaciones { get; set; }//17
        
        public string Coordinador { get; set; }//18
        //public string Descripcion { get; set; }
        
        public string Sala { get; set; }//19
    }
}
