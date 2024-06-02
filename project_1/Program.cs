using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Hotel;

class Program
{
    private static HotelContext hotelContext;
    private static CustomerContext customerContext;
    private static readonly string DATE_TIME_FORMATTER = "yyyy-MM-dd";


    public static void Main(string[] args)
    {
        hotelContext = new HotelContext();
        hotelContext.InitializeRooms(); // Room 정보 초기화
        DisplayMainMenu();
    }

    private static void DisplayMainMenu()
    {
        Console.WriteLine("[ 나의 호텔에 오신 것을 환영합니다 :) ] ");
        Console.WriteLine("1. 객실 조회");
        Console.WriteLine("2. 객실 예약");
        Console.WriteLine("3. 회원 등록");

        if (customerContext != null)
        {
            Console.WriteLine(customerContext.Name + "님의 소지금: " + customerContext.Money + " 원");
        }

        try
        {
            HandleMainMenuInput();
        }
        catch (FormatException e)
        {
            Console.WriteLine("잘못된 입력입니다.\n다시 입력해주세요.");
            //DisplayManagerMenu();
        }
    }

    private static void HandleMainMenuInput()
    {
        int input = Convert.ToInt32(Console.ReadLine());

        switch (input)
        {
            case 0:
                DisplayManagerMenu(); // 매니져 모드
                break;
            case 1:
                hotelContext.DisplayRooms(); // 객실 조회
                break;
            case 2:
                RequestReservationMenu(); // 객실 예약
                break;
            case 3:
                RequestAddCustomer(); // 고객 정보 등록
                break;
            default:
                Console.WriteLine("잘못된 입력입니다.\n다시 입력해주세요.");
                break;
        }

        Sleep();
        DisplayMainMenu();
    }

    private static void Sleep()
    {
        Console.WriteLine("(3초후 돌아갑니다.)");
        Thread.Sleep(3000); // 3초 대기
    }

    private static void DisplayManagerMenu()
    {
        Console.WriteLine("[ 운영자 메뉴 ] ");
        Console.WriteLine("1. 예약 목록 조회");
        Console.WriteLine("2. 돌아가기");
        int input = int.Parse(Console.ReadLine());
        switch (input)
        {
            case 1:
                // 예약 목록 조회 기능 구현
                PrintAllReservation();
                Console.WriteLine("호텔의 총 수익: " + hotelContext.Revenue + " 원");
                break;
            case 2:
                // 돌아가기
                break;
            default:
                Console.WriteLine("잘못된 입력입니다. 다시 입력하세요");
                DisplayManagerMenu();
                break;
        }
    }

    private static void PrintAllReservation()
    {
        // 모든 예약된 정보 출력하기!
        if (hotelContext.ReservationList.Count == 0)
        {
            Console.WriteLine("예약된 정보가 없습니다");
        }
        else
        {
            foreach (var reservation in hotelContext.ReservationList)
            {
                Console.WriteLine($"방 번호 : {reservation.RoomIndex + 1} | 예약 날짜 : {reservation.ReservationDate} | 예약 번호 : {reservation.ReservationNumber} ");
            }
        }
    }

    private static void DisplayReservationMenu()
    {
        Console.WriteLine("[ 객실 예약 메뉴 ] ");
        Console.WriteLine("1. 객실 예약");
        Console.WriteLine("2. 예약 목록");
        Console.WriteLine("3. 예약 확인");
        Console.WriteLine("4. 예약 취소");
        Console.WriteLine("5. 돌아가기");

        ReservationMenuInputHandle();
    }

    private static void RequestAddCustomer()
    {
        Console.Write("이름을 입력하세요: ");
        string name = Console.ReadLine();

        Console.Write("전화번호를 입력하세요: ");
        string phoneNumber = Console.ReadLine();

        Console.Write("소지금을 입력하세요: (원) ");
        double money = Convert.ToDouble(Console.ReadLine());

        customerContext = new CustomerContext(name, phoneNumber, money); 
        Console.WriteLine("고객 정보가 입력되었습니다.");
    }

    private static void RequestReservationMenu()
    {
        if (customerContext == null)
        {
            Console.WriteLine("고객 정보가 없습니다. 등록 후 이용부탁드립니다.\n");
        }
        else
        {
            DisplayReservationMenu();
        }
    }

    private static void CheckReservation()
    {
        // 해당 손님의 정보 1건만 출력하기
        Console.WriteLine("[ 예약 확인 ] ");
        Console.Write("예약번호를 입력해 주세요\n예약번호: ");
        string reservationInput = Console.ReadLine();
        bool checkedReservation = false;

        foreach (var printReservation in hotelContext.ReservationList)
        {
            if (printReservation.ReservationNumber == reservationInput)
            {
                Console.WriteLine($"방 번호 : {printReservation.RoomIndex + 1} | 예약 날짜 : {printReservation.ReservationDate} | 예약 번호 : {printReservation.ReservationNumber} ");
                checkedReservation = true;
                break;
            }
        }

        if (!checkedReservation)
        {
            Console.WriteLine("일치하는 예약 번호가 없습니다.");
        }
    }


    private static void PrintReservationList()
    {
        Console.WriteLine("[ 예약 목록 ] ");
        foreach (var printReservation in hotelContext.ReservationList)
        {
            if (printReservation.CustomerName == customerContext.Name)
            {
                Console.WriteLine($"방 번호 : {printReservation.RoomIndex + 1} | 예약 날짜 : {printReservation.ReservationDate} | 예약 번호 : {printReservation.ReservationNumber} ");
            }
        }
    }


    private static void ReservationMenuInputHandle()
    {
        Console.Write("번호를 선택해주세요 : ");
        int input = Convert.ToInt32(Console.ReadLine());
        switch (input)
        {
            case 1:
                RoomReservation();  // 객실 예약 기능
                break;
            case 2:
                PrintReservationList(); // 예약 목록 조회 기능
                break;
            case 3:
                CheckReservation(); // 예약 확인 기능
                break;
            case 4:
                CancelReservation();// 예약 취소 기능
                break;
            case 5:
                break; // 상위 메뉴로 돌아가기
            default:
                Console.WriteLine("잘못된 입력입니다. 다시 입력해주세요");
                ReservationMenuInputHandle();
                break;
        }
    }

    private static void CancelReservation()
    {
        Console.WriteLine("[ 예약 취소 ] ");
        Console.Write("예약코드를 입력해주세요 : ");
        string reservationCode = Console.ReadLine();
        hotelContext.CancelRes(reservationCode, customerContext);
    }

    private static void RoomReservation()
    {
        Console.WriteLine("[ 객실 예약 ] ");
        hotelContext.DisplayRooms();
        Console.Write("객실의 번호를 입력해주세요: ");
        int input = Convert.ToInt32(Console.ReadLine());
        Console.Write("예약할 날짜를 입력해주세요 (ex. 2023-11-01) : ");
        string dateInput = Console.ReadLine();
        DateTime date = DateTime.ParseExact(dateInput, DATE_TIME_FORMATTER, CultureInfo.InvariantCulture);

        // 호텔의 객실 목록을 가져옴
        List<Room> roomList = hotelContext.RoomList;

        // 입력된 객실 타입에 해당하는 객실을 찾음
        int roomIndex = input - 1;
        Room selectedRoom = null;
        try
        {
            selectedRoom = roomList[roomIndex];
        }
        catch (ArgumentOutOfRangeException)
        {
            Console.WriteLine("잘못된 입력입니다. 다시 입력해주세요");
            RoomReservation();
            return;
        }

        // 해당 객실이 예약 가능한지 확인하고 예약 처리
        if (selectedRoom != null && !selectedRoom.IsOccupied(date))
        {
            Console.WriteLine(input + "번 방을 예약합니다.");
            DateTime reservationDate = DateTime.Now;

            // 예약한 객실의 가격과 고객의 소지금 비교
            double roomFee = selectedRoom.RoomFee;
            double customerMoney = customerContext.Money;

            if (customerMoney < roomFee)
            {
                Console.WriteLine("고객의 소지금이 부족하여 예약할 수 없습니다.");
                Console.WriteLine("고객의 소지금: " + customerMoney + " 원");
                Console.WriteLine("객실의 가격: " + roomFee + " 원");
            }
            else
            {
                // 예약번호 생성
                string reservationCode = hotelContext.GenerateReservationCode();
                // 예약 객체 생성
                Reservation reservation = new Reservation(customerContext.Name, customerContext.PhoneNumber, roomIndex, reservationDate,date, reservationCode);

                // 예약 정보를 예약 목록에 추가
                hotelContext.ReservationList.Add(reservation);

                // 매출 업데이트
                hotelContext.AddRevenue(roomFee);

                // 예약 상태 변경
                selectedRoom.Occupy(date);

                // 고객의 남은 소지금 계산
                double remainingMoney = customerMoney - roomFee;
                customerContext.Money = remainingMoney;

                Console.WriteLine("객실이 예약되었습니다.");
                Console.WriteLine("예약 일자 :" + reservationDate.ToString(DATE_TIME_FORMATTER) + " 입니다.");
                Console.WriteLine("예약 코드는 : " + reservationCode + " 입니다.");
                Console.WriteLine("고객의 남은 소지금: " + remainingMoney + " 원");
            }
        }
        else
        {
            Console.WriteLine("해당 객실은 예약할 수 없습니다.");
        }
    }


}
