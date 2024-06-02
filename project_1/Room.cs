using System;
using System.Collections.Generic;

namespace Hotel
{
    public class Room
    {
        private readonly string roomType;
        private readonly double roomFee;
        private readonly int roomSize;
        private List<DateTime> occupiedDateList;

        // 생성자
        public Room(string roomType, double roomFee, int roomSize)
        {
            this.roomType = roomType;
            this.roomFee = roomFee;
            this.roomSize = roomSize;
            this.occupiedDateList = new List<DateTime>();
        }

        // Getter 메서드
        public string RoomType => roomType;
        public double RoomFee => roomFee;

        // 서비스 메서드
        public bool IsOccupied(DateTime date)
        {
            return this.occupiedDateList.Contains(date.Date);
        }

        public void CancelOccupancy(DateTime date)
        {
            this.occupiedDateList.Remove(date.Date);
        }

        public void Occupy(DateTime date)
        {
            this.occupiedDateList.Add(date.Date);
        }

        public void DisplayRoomInfo()
        {
            Console.WriteLine("예약 불가능한 날짜: " + String.Join(", ", occupiedDateList));
            Console.WriteLine("객실 타입: " + roomType);
            Console.WriteLine("객실 요금: " + roomFee + " 원");
            Console.WriteLine("객실 크기: " + roomSize + " m^2");

            Console.WriteLine();
        }
    }
}
