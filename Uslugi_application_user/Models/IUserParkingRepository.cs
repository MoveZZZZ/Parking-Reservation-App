using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uslugi_application_user.Models
{
    interface IUserParkingRepository
    {
        List<UserParkinModel> getAllData(int IDuser);
        void RemoveParkingPosition(int IDpark, string DataStart);

    }
}
