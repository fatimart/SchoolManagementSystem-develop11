using SchoolManagementSystem.Models;
using SchoolManagementSystem.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Windows;

namespace SchoolManagementSystem.ViewModels
{
    class RoomViewModel : ViewModelBase
    {
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

        private string _responseMessage = "";
        public string ResponseMessage
        {
            get { return _responseMessage; }
            set
            {
                _responseMessage = value;
                OnPropertyChanged("ResponseMessage");
            }
        }

        public RoomViewModel()
        {
            //GetAll();
        }

        #region CRUD

        /// <summary>
        /// Fetches Room details
        /// </summary>
        public void GetRoomDetails ()

        {
            var roomDetails = WebAPI.GetCall(API_URIs.rooms);
            if (roomDetails.Result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                AllRooms = roomDetails.Result.Content.ReadAsAsync<ObservableCollection<Room>>().Result;
            }
        }

        /// <summary>
        /// Adds new Room
        /// </summary>
        public void CreateNewRoom ( string RoomNum )
        {
            Room newRoom = new Room()
            {
                RoomNum = RoomNum
            };

            var roomDetails = WebAPI.PostCall(API_URIs.rooms, newRoom);
            if (roomDetails.Result.StatusCode == System.Net.HttpStatusCode.Created)
            {
                ResponseMessage = newRoom.RoomNum + "'s details has successfully been added!";
                MessageBox.Show(ResponseMessage);
            }
            else
            {
                ResponseMessage = "Failed to update" + newRoom.RoomNum + "'s details.";
            }
        }


        /// <summary>
        /// Updates Room's record
        /// </summary>
        /// <param name="years"></param>
        public void UpdateRoomDetails ( int RoomID, string RoomNum )
        {
            Room updateRoom = new Room()
            {
                RoomID = RoomID,
                RoomNum = RoomNum,

            };


            var roomDetails = WebAPI.PutCall(API_URIs.rooms + "?id=" + updateRoom.RoomID, updateRoom);
            if (roomDetails.Result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                ResponseMessage = updateRoom.RoomID + "'s details has successfully been updated!";
                MessageBox.Show(ResponseMessage);
            }
            else
            {
                ResponseMessage = "Failed to update" + updateRoom.RoomID + "'s details.";
                MessageBox.Show(ResponseMessage);
            }
        }

        /// <summary>
        /// Deletes Year's record
        /// </summary>
        public void DeleteRoomeDetails ( int RoomID )
        {

            Room deleteRoom = new Room()
            {
                RoomID = RoomID
            };

            var roomDetails = WebAPI.DeleteCall(API_URIs.rooms + "?id=" + deleteRoom.RoomID);
            if (roomDetails.Result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                ResponseMessage = deleteRoom.RoomID + "'s details has successfully been deleted!";
                MessageBox.Show(ResponseMessage);
            }
            else
            {
                ResponseMessage = "Failed to delete" + deleteRoom.RoomID + "'s details.";
                MessageBox.Show(ResponseMessage);
            }
        }
        #endregion



    }
}
