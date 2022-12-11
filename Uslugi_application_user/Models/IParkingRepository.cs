﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Uslugi_application_user.Models
{
    public interface IParkingRepository
    {
        DataTable GetAllReservationUsers(NetworkCredential credentital);
        void AddReservation(ParkingModel parkModel);
        bool ChekReservParkingNumber(string dataStart, string dataEnd, string parkingNumber); //Смотрим, свободно ли месть прям в момент резервации 
        void RemoveReservation(string startTime, string endTime);
        DataTable addListPark();

    }
}
