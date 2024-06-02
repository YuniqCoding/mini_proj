using System;
namespace Hotel
{
    using System.Collections.Generic;
    using System.Xml.Linq;

    public class HotelContext
    {
        public List<Room> roomList; // 객실 목록
        public double revenue; // 매출
        public List<Reservation> reservationList; // 예약 목록
        private int reservationNumber; // 예약 번호

        // 생성자
        public HotelContext()
        {
            roomList = new List<Room>();
            revenue = 0.0;
            reservationList = new List<Reservation>();
        }

        // Getter,Setter 메서드

        public List<Reservation> ReservationList => reservationList;

        public List<Room> RoomList => roomList;

        public double Revenue => revenue;

        // 서비스 메서드
        public void CancelRes(string resNum, CustomerContext customerContext)
        {
            bool canceled = false;
            foreach (var res in reservationList.ToList())
            {
                if (res.ReservationNumber == resNum)
                {
                    // 취소할 방 조회
                    Room room = roomList[res.RoomIndex];

                    // 호텔 매출에서 차감
                    revenue -= room.RoomFee;

                    Console.WriteLine($"{room.RoomType}의 가격 : {room.RoomFee}원이 환불 되었습니다.");

                    // 취소한 방 예약날짜 목록에서 제거
                    room.CancelOccupancy(res.VisitDate);

                    // 고객 소지금으로 환불
                    customerContext.RefundMoney(room.RoomFee);

                    canceled = true;

                    // 호텔 예약 목록에서 제거
                    reservationList.Remove(res);
                    Console.WriteLine("취소 완료 되었습니다.");
                    break;
                }
            }
            if (!canceled)
            {
                Console.WriteLine("일치하는 예약 번호가 없습니다.");
            }
        }


        public void InitializeRooms()
        {
            Room singleRoom = new Room("싱글룸", 100.0, 16);
            Room doubleRoom = new Room("더블룸", 150.0, 24);
            Room twinRoom = new Room("트윈룸", 200.0, 16);
            Room suiteRoom = new Room("스위트룸", 250.0, 34);

            roomList.Add(singleRoom);
            roomList.Add(doubleRoom);
            roomList.Add(twinRoom);
            roomList.Add(suiteRoom);
        }

        public void DisplayRooms()
        {
            Console.WriteLine("[ 객실 목록 ]");
            Console.WriteLine();
            for (int i = 0; i < RoomList.Count; i++)
            {
                Room room = RoomList[i];
                Console.WriteLine("[ 객실 번호: " + (i + 1) + " ]");
                room.DisplayRoomInfo(); // 객실 정보 출력
            }
        }

        public void AddRevenue(double income)
        {
            this.revenue += income;
        }

        /// <summary>
        /// 신규 예약코드 조회
        /// </summary>
        /// <returns>신규 예약코드</returns>
        public string GenerateReservationCode()
        {
            reservationNumber++;
            return "MHR" + reservationNumber;
        }

    }

}

