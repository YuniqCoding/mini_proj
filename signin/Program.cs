using System;
using System.Collections.Generic;
using kiosk;

namespace Kiosk
{
    public class Program
    {
        private static MenuContext menuContext; // 메뉴 정보를 가지고 있는 컨텍스트 (컨텍스트? 문맥 = 현재 상태를 저장하는 공간)
        private static ManagementContext managementContext;

        public static void Main(string[] args)
        {
            menuContext = new MenuContext();
            managementContext = new ManagementContext();
            DisplayMainMenu();
        }
        /**
         * 메인메뉴 출력
         */
        private static void DisplayMainMenu()
        {
            Console.WriteLine("SHAKESHACK BURGER 에 오신걸 환영합니다.");
            Console.WriteLine("아래 메뉴판을 보시고 메뉴를 골라 입력해주세요.\n");

            Console.WriteLine("[ SHAKESHACK MENU ]");
            List<Menu> mainMenus = menuContext.GetMenus("Main"); // 메뉴 컨텍스트에서 메인메뉴 조회
            int nextNum = PrintMenu(mainMenus, 1); // 메인메뉴 출력

            Console.WriteLine("[ ORDER MENU ]");
            List<Menu> orderMenus = menuContext.GetMenus("Order"); // 메뉴 컨텍스트에서 주문메뉴 조회
            PrintMenu(orderMenus, nextNum); // 주문메뉴 출력

            HandleMainMenuInput(); // 메인메뉴 입력처리
        }
        /**
         * 메뉴 목록 출력
         * @param menus : 출력할 메뉴 리스트
         * @param num : 출력중인 전체 순번
         * @return 출력 후 전체 순번
         */
        private static int PrintMenu(List<Menu> menus, int num)
        {
            for (int i = 0; i < menus.Count; i++, num++)
            {
                Console.WriteLine($"{num}. {menus[i].name}   | {menus[i].description}");
            }
            return num;
        }
        /**
         * 메인메뉴 입력처리
         */
        private static void HandleMainMenuInput()
        {
            Console.WriteLine("메뉴를 선택하세요: ");
            int input = Convert.ToInt32(Console.ReadLine());
            int mainMenuSize = menuContext.GetMenus("Main").Count;
            int orderMenuSize = menuContext.GetMenus("Order").Count;

            if (input == 0)
            {
                DisplayManagementMenu();
            }
            if (input <= mainMenuSize)
            {
                DisplayMenu(menuContext.GetMenus("Main")[input - 1]);
            }
            else if (input <= mainMenuSize + orderMenuSize)
            {
                int orderInput = input - mainMenuSize;
                switch (orderInput)
                {
                    case 1:
                        DisplayOrderMenu();
                        break;
                    case 2:
                        HandleCancelMenuInput();
                        break;
                    case 3:
                        HandleListMenuInput();
                        break;
                    default:
                        Console.WriteLine("잘못된 입력입니다. 다시 입력해주세요.");
                        HandleMainMenuInput();
                        break;
                }
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다. 다시 입력해주세요.");
                HandleMainMenuInput();
            }
        }

        /**
        * 메뉴에 있는 상품메뉴 목록 출력
        * @param menu 출력할 메뉴
        */
        private static void DisplayMenu(Menu menu)
        {
            Console.WriteLine("SHAKESHACK BURGER 에 오신걸 환영합니다.");
            Console.WriteLine("아래 상품메뉴판을 보시고 상품을 골라 입력해주세요.\n");

            Console.WriteLine($"[ {menu.name} MENU ]");
            List<Item> items = menuContext.GetMenuItems(menu.name); // 메뉴에 있는 상품메뉴 목록 조회
            PrintMenuItems(items);         // 상품메뉴 목록 출력

            HandleMenuItemInput(items);    // 상품메뉴 입력 처리
        }

        /**
         * 상품메뉴 입력 처리
         * @param items 입력처리 할 상품메뉴 목록
         */
        private static void HandleMenuItemInput(List<Item> items)
        {
            
            int input = int.Parse(Console.ReadLine());
            
            if (input == 1 && items[0].name == "Hamburger") // 햄버거 선택시 
            {
                Item selectedItem = items[input - 1]; // 선택한 상품메뉴 조회
                DisplayDetailOption(selectedItem);
            }
            else if (input >= 1 && input <= items.Count) // 입력값 유효성 검증
            {
                Item selectedItem = items[input - 1]; // 선택한 상품메뉴 조회
                DisplayConfirmation(selectedItem); // 선택한 상품메뉴 확인 문구 출력
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다. 다시 입력해주세요.");
                HandleMenuItemInput(items); // 상품메뉴 입력 처리 재수행
            }
         
        }

        // 세부 옵션 선택 문구 출력
        private static void DisplayDetailOption(Item menuItem)
        {
            Console.WriteLine($"{menuItem.name}   | {menuItem.price} | {menuItem.description}");
            Console.WriteLine("위 메뉴의 어떤 옵션으로 추가하시겠습니까?");
            Console.WriteLine("1. Single(W 5.4)        2. Double(W 9.0)");


            HandleDetailOption(menuItem);    // 옵션 입력 처리
        }

        private static void HandleDetailOption(Item menuItem)
        {
            int input = int.Parse(Console.ReadLine());

            switch (input)
            {
                case 1: // 1. Single 입력시
                    menuItem.name = "Hamburger(Single)";
                    menuItem.price = 5.4;
                    DisplayConfirmation(menuItem); 
                    break;

                case 2: // 2. Double 입력시
                    menuItem.name = "Hamburger(Double)";
                    menuItem.price = 9.0;
                    DisplayConfirmation(menuItem); 
                    break;

                default:
                    Console.WriteLine("잘못된 입력입니다. 다시 입력해주세요.");
                    HandleDetailOption(menuItem); // 잘못된 입력시 다시 확인여부 입력 처리 재수행
                    break;
            }
        }

        private static void DisplayManagementMenu()
        {
            Console.WriteLine("SHAKESHACK BURGER 관리 메뉴에 오신걸 환영합니다.");
            Console.WriteLine("아래 목록에서 원하는 명령을 골라 입력해주세요.\n");

            managementContext.DisplayMainMenu();

            HandleCommandInput();
        }

        private static void HandleCommandInput()
        {
            Console.WriteLine("메뉴를 선택하세요:");
            int input = int.Parse(Console.ReadLine());

            if (input == 0)
            {
                DisplayMainMenu();
            }
            else if (input >= 1 && input <= 4)
            {
                switch (input)
                {
                    case 1:
                        managementContext.DisplayWaitingOrdersAndProcess();
                        break;
                    case 2:
                        managementContext.DisplayCompletedOrders();
                        break;
                    case 3:
                        string menuName = GetMenuName();
                        Item newItem = managementContext.CreateMenuItem();
                        menuContext.AddMenuItem(menuName, newItem);
                        break;
                    case 4:
                        menuContext.DisplayAllItem();
                        Console.Write("삭제할 상품 ID: ");
                        int itemId = int.Parse(Console.ReadLine());
                        managementContext.DeleteMenuItems(menuContext.GetMenuItemMap(), itemId);
                        break;
                    default:
                        Console.WriteLine("잘못된 입력입니다. 다시 입력해주세요.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다. 다시 입력해주세요.");
            }

            DisplayManagementMenu();
        }

        private static string GetMenuName()
        {
            Console.WriteLine("[ 메뉴 목록 ]");
            List<Menu> mainMenus = menuContext.GetMenus("Main");
            PrintMenu(mainMenus, 1);
            Console.WriteLine(mainMenus.Count + 1 + ". 신규 메뉴");
            Console.Write("메뉴 ID: ");
            int menuId = Convert.ToInt32(Console.ReadLine());

            if (menuId <= mainMenus.Count)
            {
                return menuContext.GetMainMenuName(menuId);
            }
            else
            {
                Console.Write("신규 메뉴이름: ");
                string newMenuName = Console.ReadLine();
                Console.Write("신규 메뉴설명: ");
                string newMenuDescription = Console.ReadLine();
                menuContext.AddMenu(newMenuName, newMenuDescription);
                return newMenuName;
            }
        }

        private static void PrintMenuItems(List<Item> items)
        {
            for (int i = 0; i < items.Count; i++)
            {
                int num = i + 1;
                Console.WriteLine($"{num}. {items[i].name}   | {items[i].price} | {items[i].description}");
            }
        }

        /**
         * 선택한 상품메뉴 확인 문구 출력
         * @param menuItem 선택한 상품메뉴
         */
        private static void DisplayConfirmation(Item menuItem)
        {
            Console.WriteLine($"{menuItem.name}   | W{menuItem.price} | {menuItem.description}");
            Console.WriteLine("위 메뉴를 장바구니에 추가하시겠습니까?");
            Console.WriteLine("1. 확인        2. 취소");

            HandleConfirmationInput(menuItem);    // 확인여부 입력 처리
        }

        /**
         * 확인여부 입력 처리
         * @param menuItem 확인한 상품메뉴
         */
        private static void HandleConfirmationInput(Item menuItem)
        {
            int input = int.Parse(Console.ReadLine());
            
            switch (input)
            {
                case 1: // 1. 확인 입력시
                    menuContext.AddToCart(menuItem); // 선택한 상품을 컨텍스트의 장바구니에 추가
                    Console.WriteLine("장바구니에 추가되었습니다.");
                    DisplayMainMenu(); // 메인메뉴 출력하며 처음으로 돌아가기
                    break;

                case 2: // 2. 취소 입력시
                    DisplayMainMenu(); // 바로 메인메뉴 출력하며 처음으로 돌아가기
                    break;

                default:
                    Console.WriteLine("잘못된 입력입니다. 다시 입력해주세요.");
                    HandleConfirmationInput(menuItem); // 잘못된 입력시 다시 확인여부 입력 처리 재수행
                    break;
            }
        }

        /**
         * 주문메뉴1. 주문진행 메뉴 출력
         */
        private static void DisplayOrderMenu()
        {
            Console.WriteLine("아래와 같이 주문 하시겠습니까?\n");
            menuContext.DisplayCart(); // 컨텍스트에서 장바구니 목록 출력

            Console.WriteLine("[ Total ]");
            Console.WriteLine("W " + menuContext.GetTotalPrice() + "\n"); // 컨텍스트에서 전체 가격 조회하여 출력

            Console.WriteLine("1. 주문      2. 메뉴판");

            HandleOrderMenuInput(); // 주문진행 입력 처리
        }

        /**
	     * 주문메뉴1. 주문진행 입력 처리
	     */
        private static void HandleOrderMenuInput()
        {
            int input = int.Parse(Console.ReadLine());
            
            switch (input)
            {
                case 1:
                    DisplayOrderComplete(); // 1. 주문 입력시 주문완료 처리
                    break;

                case 2:
                    DisplayMainMenu(); // 2. 메뉴판 입력시 메인메뉴 출력하며 돌아가기
                    break;

                default:
                    Console.WriteLine("잘못된 입력입니다. 다시 입력해주세요.");
                    HandleOrderMenuInput(); // 잘못된 입력시 주문진행 입력처리 재수행
                    break;
            }
        }

        /**
	     * 주문메뉴1. 주문 입력시 주문완료 처리
	     */
        private static void DisplayOrderComplete()
        {
            int orderNumber = menuContext.GenerateOrderNumber(); // 컨텍스트에서 신규 주문번호 조회
            List<Item> cart = menuContext.GetCart();
            double totalPrice = menuContext.GetTotalPrice();

            Console.WriteLine("요구사항을 입력해주세요.");
            string message = Console.ReadLine();

            managementContext.AddCartToOrder(orderNumber, message, cart, totalPrice);

            Console.WriteLine("주문이 완료되었습니다!\n");
            Console.WriteLine($"대기번호는 [ {orderNumber} ] 번 입니다.");

            ResetCartAndDisplayMainMenu(); // 장바구니 초기화 후 메인메뉴 출력
        }

        /**
	     * 장바구니 초기화 후 메인메뉴 출력
	     */
        private static void ResetCartAndDisplayMainMenu()
        {
            menuContext.ResetCart(); // 컨텍스트에서 장바구니 초기화
            Console.WriteLine("(3초후 메뉴판으로 돌아갑니다.)");

            Thread.Sleep(3000); // 3초 대기

            DisplayMainMenu(); // 메인메뉴 출력하며 돌아가기
        }

        /**
	     * 주문메뉴2. 주문취소 메뉴 출력
	     */
        private static void HandleCancelMenuInput()
        {
            Console.WriteLine("주문을 취소하시겠습니까?");
            Console.WriteLine("1. 확인        2. 취소");

            HandleCancelConfirmationInput(); // 주문취소 확인 입력값 처리
        }

        private static void HandleListMenuInput()
        {
            managementContext.DisplayWaitingOrders();
            managementContext.DisplayCompletedOrders();

            DisplayMainMenu();
        }


        /**
	     * 주문메뉴2. 주문취소 확인 입력값 처리
	     */
        private static void HandleCancelConfirmationInput()
        {
            int input = int.Parse(Console.ReadLine());
            if (input == 1)
            {
                menuContext.ResetCart(); // 장바구니 초기화
                Console.WriteLine("주문이 취소되었습니다.");
                DisplayMainMenu(); // 메인메뉴 출력하며 돌아가기
            }
            else if (input == 2)
            {
                DisplayMainMenu(); // 메인메뉴 출력하며 돌아가기
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다. 다시 입력해주세요.");
                HandleCancelConfirmationInput(); // 주문취소 확인 입력값 처리 재수행
            }
        }
    }
}


    