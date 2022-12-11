using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Uslugi_application_user.Models;
using Uslugi_application_user.Repositories;

namespace Uslugi_application_user.ViewModels
{
    public class ShowReservationListModel : ViewModelBase
    {

        private int _iduser;

        private List<UserParkinModel> ListReservationUser;
        public class BoolStringClass
        {
            public string TheText { get; set; }
            public int TheValue { get; set; }
        }

        public List<BoolStringClass> ShowingList { get; set; }

        private IUserParkingRepository userParkingRepository;
        private IUserRepository userRepository;
        private class CompareSortListParkingData : IComparer<UserParkinModel>
        {
            public int Compare(UserParkinModel uRep1, UserParkinModel uRep2)
            {

                if (uRep1.StartReservation > uRep2.StartReservation)
                {
                    return 1;
                }
                else if (uRep1.StartReservation < uRep2.StartReservation)
                {
                    return -1;
                }
                return 0;
            }
        }
        public int IDUser
        {
            get
            {
                return _iduser;
            }
            set
            {
                _iduser = value;
            }
        }
        public ObservableCollection<BoolStringClass> TheList { get; set; }

        public ICommand restartData { get; set; }
       

        public ShowReservationListModel()
        {
            userRepository = new UserRepository();
            userParkingRepository = new UserParkingRepository();
            ShowingList = new List<BoolStringClass>();
            CompareSortListParkingData comp = new CompareSortListParkingData();
            _iduser = Convert.ToInt32(userRepository.GetByUsername(Thread.CurrentPrincipal.Identity.Name).Id);
            ListReservationUser = userParkingRepository.getAllData(_iduser);
            restartData = new ViewModelCommand(ExecuteRestartData);
            ListReservationUser.Sort(comp);
            CreateCheckBoxList();
            
        }

        private void ExecuteRestartData(object obj)
        {
            ShowingList.Clear();
            CreateCheckBoxList();
        }

        public void CreateCheckBoxList()
        {
            for (int i = 0; i < ListReservationUser.Count(); i++)
            {
                ShowingList.Add(new BoolStringClass
                {
                    TheText = string.Format("Slot:{0,5}, start: {1,22}, finish: {2,22}, numer pojazdu: {3,10}", Convert.ToString(ListReservationUser[i].IdPark), 
                    Convert.ToString(ListReservationUser[i].StartReservation), Convert.ToString(ListReservationUser[i].EndReservation), Convert.ToString(ListReservationUser[i].NumberCar)),

                    TheValue = i
                }) ;

            }

        }

    }
}


