using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Uslugi_application_user.Models;

namespace Uslugi_application_user.Repositories
{
    public class ParkingRepository : RepositoryBase, IParkingRepository
    {
        public void AddReservation(ParkingModel parkModel)
        {
            using (var connection = GetConnection())
            using (var command = new MySqlCommand())
            {

                connection.Open();
                command.Connection = connection;
                command.CommandText = "INSERT INTO userpark(`iduser`, `idpark`, `startres`, `endres`, `tablenumber`) VALUES(@us, @park, @sres, @eres, @tabnum)";
                command.Parameters.Add("@us", MySqlDbType.VarChar).Value = Convert.ToInt32(parkModel.IdUser);
                command.Parameters.Add("@park", MySqlDbType.VarChar).Value = Convert.ToInt32(parkModel.IdPark);
                command.Parameters.Add("@sres", MySqlDbType.VarChar).Value = Convert.ToDateTime(parkModel.StartReservation);
                command.Parameters.Add("@eres", MySqlDbType.VarChar).Value = Convert.ToDateTime(parkModel.EndReservation);
                command.Parameters.Add("@tabnum", MySqlDbType.VarChar).Value = parkModel.TableNumber;
                command.ExecuteNonQuery();
            }
        }

        public DataTable GetAllReservationUsers(NetworkCredential credentital)
        {
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataTable table = new DataTable();
            using (var connection = GetConnection())
            using (var command = new MySqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT * FROM `userpark` WHERE `iduser` = @idus";
                command.Parameters.Add("@username", MySqlDbType.VarChar).Value = credentital.UserName;
                adapter.SelectCommand = command;
                adapter.Fill(table);
            }
            return table;
        }

        public bool ChekReservParkingNumber(string dataStart, string dataEnd, string parkingNumber)
        {
            throw new NotImplementedException();
        }

        public void RemoveReservation(string startTime, string endTime)
        {
            throw new NotImplementedException();
        }

        DataTable IParkingRepository.addListPark()
        {
            List<int> numberP= new List<int>();
            List<int> indexReser = new List<int>();
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            using (var connection = GetConnection())
            using (var command = new MySqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT `idpark`, `startres`, `endres` FROM userpark";
                adapter.SelectCommand = command;
                adapter.Fill(table);
            }           
            return table;
        }
    }
}
