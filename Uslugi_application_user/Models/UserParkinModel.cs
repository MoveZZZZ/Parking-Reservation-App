using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uslugi_application_user.Models
{
    public class UserParkinModel
    {
        public int IdUser { get; set; }
        public int IdPark { get; set; }
        public DateTime StartReservation { get; set; }
        public DateTime EndReservation { get; set; }
        public string NumberCar { get; set; }


    }
}
