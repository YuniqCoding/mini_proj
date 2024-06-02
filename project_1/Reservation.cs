using System;

namespace Hotel
{
    public class Reservation
    {
        private readonly int roomIndex;
        private readonly DateTime reservationDate;     // 예약 날짜
        private readonly DateTime visitDate;
        private readonly string reservationNumber;
        private readonly string customerName;
        private readonly string customerPhoneNumber;

        // 생성자
        public Reservation(string name, string phoneNumber, int roomIndex, DateTime reservationDate, DateTime visitDate, string reservationNumber)
        {
            this.customerName = name;
            this.customerPhoneNumber = phoneNumber;
            this.roomIndex = roomIndex;
            this.reservationDate = reservationDate;
            this.visitDate = visitDate;
            this.reservationNumber = reservationNumber;
        }

        // Getter 메서드
        public int RoomIndex => roomIndex;
        public DateTime ReservationDate => reservationDate;
        public DateTime VisitDate => visitDate;
        public string ReservationNumber => reservationNumber;
        public string CustomerName => customerName;

        //// 서비스 메서드
        //public override string ToString()
        //{
        //    return " 예약번호: " + reservationNumber + "\n" +
        //            " 이름: " + customerName + "\n" +
        //            " 핸드폰번호: " + customerPhoneNumber + "\n" +
        //            " 방 번호: " + (roomIndex + 1) + "\n" +
        //            " 예약날짜: " + reservationDate.ToString("yyyy년 M월 d일") + "\n";
        //}
    }
}
