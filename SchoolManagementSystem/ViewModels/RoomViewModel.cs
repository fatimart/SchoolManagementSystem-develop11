using SchoolManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace SchoolManagementSystem.ViewModels
{
    class RoomViewModel : ViewModelBase
    {
        public SchoolMSEntities1 ty = new SchoolMSEntities1();
        public Room Room;

        private ObservableCollection<Room> _RoomRecord;
        public ObservableCollection<Room> AllRooms
        {
            get
            {
                return _RoomRecord;
            }
            set
            {
                _RoomRecord = value;
                OnPropertyChanged("AllRooms");
            }
        }

        private int _RoomID;
        public int RoomID
        {
            get { return _RoomID; }
            set
            {
                _RoomID = value;
                OnPropertyChanged("RoomID");

            }
        }

        private string _RoomNum;
        public string RoomNum
        {
            get { return _RoomNum; }
            set
            {
                _RoomNum = value;
                OnPropertyChanged("RoomNum");

            }
        }

        public RoomViewModel()
        {
            GetAll();
        }

        public List<Room> GetAll1 ()
        {
            return ty.Rooms.ToList();
        }

        public void GetAll ()
        {
            AllRooms = new ObservableCollection<Room>();
            GetAll1().ForEach(data => AllRooms.Add(new Room()
            {

                RoomID = data.RoomID,
                RoomNum = data.RoomNum

            }));

        }

        //MARK: DataAccess function
        public void AddRoom(string RoomNum)
        {

            Room room = new Room();
            room.RoomNum = RoomNum;

            ty.Rooms.Add(room);
            ty.SaveChanges();

        }


        public void UpdateRoom(int RoomID, string RoomNum)
        {

            Room updateRoom = (from m in ty.Rooms where m.RoomID == RoomID select m).Single();
            updateRoom.RoomID = RoomID;
            updateRoom.RoomNum = RoomNum;
            ty.SaveChanges();

        }

        public void DeleteRoom(int RoomID)
        {

            var deleteRoom = ty.Rooms.Where(m => m.RoomID == RoomID).Single();
            ty.Rooms.Remove(deleteRoom);
            ty.SaveChanges();

        }
        public bool CheckRoomID(int roomID)
        {
            try
            {
                var RoomID = ty.Rooms.Where(m => m.RoomID == roomID).Single();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return false;

        }



    }
}
