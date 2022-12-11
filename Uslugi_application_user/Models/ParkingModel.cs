using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uslugi_application_user.Models
{
    public class ParkingModel
    {
        public string IdUser { get; set; }
        public string IdPark { get; set; }
        public string StartReservation { get; set; }
        public string EndReservation { get; set; }
        public string TableNumber { get; set; }
    }
}
