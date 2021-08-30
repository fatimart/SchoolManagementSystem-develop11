using SchoolManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SchoolManagementSystem.ViewModels
{
    class RoomViewModel : ViewModelBase
    {
        public SchoolMSEntities1 ty = new SchoolMSEntities1();
        public Room Room;

        public int RoomID
        {
            get { return Room.RoomID; }
            set
            {
                if (Room.RoomID != value)
                {
                    Room.RoomID = value;
                    OnPropertyChanged("RoomID");
                }
            }
        }
        public string RoomNum
        {
            get { return Room.RoomNum; }
            set
            {
                if (Room.RoomNum != value)
                {
                    Room.RoomNum = value;
                    OnPropertyChanged("RoomNum");
                }
            }
        }


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
