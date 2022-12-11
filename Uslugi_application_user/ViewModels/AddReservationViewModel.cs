using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Uslugi_application_user.Models;
using Uslugi_application_user.Repositories;

namespace Uslugi_application_user.ViewModels
{
    public class AddReservationViewModel : ViewModelBase
    {
        private readonly DateTime _minimalDate = DateTime.Now;
        private string _minimalHourStart = DateTime.Now.Hour.ToString() + ":00:00";
        private string _minimalHourEnd = Convert.ToString(DateTime.Now.Hour + 1) + ":00:00";
        private List<int> _reservPark;
        private List<int> _boxList;
        private DataTable choisedIndex;


        private string dateStart;
        private string dateEnd;

        private bool _showBox = false;





        public string DateStart
        {
            get
            {
                return dateStart;
            }
            set
            {
                dateStart = value;
                OnPropertyChanged(nameof(DateStart));
            }
        }
        public string DateEnd
        {
            get
            {
                return dateEnd;
            }
            set
            {
                dateEnd = value;
                OnPropertyChanged(nameof(DateEnd));
            }
        }
        public DateTime MinimalDateStart
        {
            get
            {
                return DateTime.Now;
            }
        }
        public DateTime MinimalDateEnd
        {
            get
            {
                /* if (DateStart == null)
                     return DateTime.Now.AddHours(1);
                 else if (DateStart != null && Convert.ToDateTime(DateStart) == DateTime.Now.Date)
                     return DateTime.Now.AddHours(1);
                 else
                     return Convert.ToDateTime(Convert.ToDateTime(DateStart).ToString("dd:MM:yyy hh:00:00"));*/
                return Convert.ToDateTime(DateStart).AddHours(1);
            }
        }
        public string StartDate
        {
            get {
                return DateTime.Now.ToString("dd:MM:yyyy hh:00:00");
            }
        }
        public List<int> parkingNumber 
        {
            get
            {
                return _boxList;
            }
        }
        public bool ShowBox
        {
            get
            {
                return _showBox;
            }
        }
        public ICommand ChoiseEndTime;


        private IParkingRepository parkingRepository;
        public AddReservationViewModel()
        {
            parkingRepository = new ParkingRepository();
            _reservPark = new List<int>();
            choisedIndex = new DataTable();
            choisedIndex = parkingRepository.addListPark();

        }
        public void addToList()
        {
            DateTime dsu = Convert.ToDateTime(DateStart);
            DateTime deu = Convert.ToDateTime(DateEnd);
            foreach (DataRow row in choisedIndex.Rows)
            {
                DateTime ds = Convert.ToDateTime(row[1].ToString());
                DateTime de = Convert.ToDateTime(row[2].ToString());

                if (dsu > de)
                {
                    continue;
                }
                else if (deu < de && ds <= deu)
                {
                    _reservPark.Add(Convert.ToInt32(row[0].ToString()));
                }
                else
                {
                    _reservPark.Add(Convert.ToInt32(row[0].ToString()));

                }
            }
            int a = 1;
            int c = _reservPark.Count();
            if (c != 0)
            {
                _reservPark.Sort();
                for (int n = 0; a < 171 && n != c; a++)
                {

                    if (a == _reservPark[n])
                    {
                        n++;
                    }
                    else
                    {
                        _boxList.Add(a);
                    }
                }
            }
            while (a < 171)
            {
                _boxList.Add(a);
                a++;
            }
        }
    }
}
