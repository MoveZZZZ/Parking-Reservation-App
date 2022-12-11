using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uslugi_application_user.Models;

namespace Uslugi_application_user.Repositories
{
    public class UserParkingRepository : RepositoryBase, IUserParkingRepository
    {
        public List<UserParkinModel> getAllData(int IDuser)
        {
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            List<UserParkinModel> retList = new List<UserParkinModel>();
            using (var connection = GetConnection())
            using (var command = new MySqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT * FROM `userpark` WHERE `iduser` = @id";
                command.Parameters.Add("@id", MySqlDbType.Int32).Value = IDuser;
                adapter.SelectCommand = command;
                adapter.Fill(table);
            }
            foreach (DataRow row in table.Rows)
            {
                retList.Add(new UserParkinModel { IdUser = Convert.ToInt32(row[0].ToString()), 
                IdPark = Convert.ToInt32(row[1].ToString()), StartReservation=Convert.ToDateTime(row[2].ToString()), EndReservation=Convert.ToDateTime(row[3].ToString()), NumberCar=row[4].ToString()});
            }
            return retList;
        }

        public void RemoveParkingPosition(int IDpark, string DataStart)
        {
            throw new NotImplementedException();
        }
    }
}
