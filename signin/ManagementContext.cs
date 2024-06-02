
using kiosk;

namespace Kiosk
{
    public class ManagementContext
    {
        private List<Order> orderList = new List<Order>();

        public void DisplayMainMenu()
        {
            Console.WriteLine("0. 메인 메뉴");
            Console.WriteLine("1. 대기주문 목록");
            Console.WriteLine("2. 완료주문 목록");
            Console.WriteLine("3. 상품 생성");
            Console.WriteLine("4. 상품 삭제");
        }

        public void AddCartToOrder(int orderNumber, string message, List<Item> cart, double totalPrice)
        {
            List<Item> orderItemList = new List<Item>(cart);
            orderList.Add(new Order(orderNumber, message, orderItemList, totalPrice));
        }


        public void DisplayWaitingOrdersAndProcess()
        {
            DisplayWaitingOrders();

            Console.WriteLine();
            Console.WriteLine("대기중인 주문을 처리하시겠습니까?");
            Console.WriteLine("1. 확인        2. 취소");
            int input = int.Parse(Console.ReadLine());

            if (input == 1)
            {
                ProcessWaitingOrder();
            }
        }

        public void DisplayWaitingOrders()
        {
            Console.WriteLine("대기 주문 목록:");

            foreach (var order in orderList)
            {
                if (!order.complete)
                {
                    order.Display();
                }
            }
        }

        public void ProcessWaitingOrder()
        {
            if (orderList.Count == 0)
            {
                Console.WriteLine("대기 주문이 없습니다.");
                return;
            }

            Console.Write("완료할 주문 번호: ");
            int orderNumber = int.Parse(Console.ReadLine());

            Order orderToComplete = null;
            foreach (var order in orderList)
            {
                if (order.orderNumber == orderNumber)
                {
                    order.SetComplete(true);
                    orderToComplete = order;
                    break;
                }
            }

            if (orderToComplete != null)
            {
                Console.WriteLine("처리가 완료되었습니다.");
            }
            else
            {
                Console.WriteLine("주문 번호를 찾을 수 없습니다.");
            }

            Console.WriteLine();
        }

        public void DisplayCompletedOrders()
        {
            Console.WriteLine("완료 주문 목록:");
            foreach (Order order in orderList)
            {
                if (order.complete)
                {
                    order.Display();
                    Console.WriteLine("\t완료시각 : " + order.completeDate);
                    Console.WriteLine();
                }
            }
            Console.WriteLine();
            Console.WriteLine("(3초후 메뉴판으로 돌아갑니다.)");
            try
            {
                Thread.Sleep(3000); // 3초 대기
            }
            catch (ThreadInterruptedException e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }

        public Item CreateMenuItem()
        {
            Console.Write("이름: ");
            string name = Console.ReadLine();

            Console.Write("설명: ");
            string description = Console.ReadLine();

            Console.Write("가격: ");
            double price = Convert.ToDouble(Console.ReadLine());

            Console.WriteLine("상품이 생성되었습니다.");
            Console.WriteLine();

            return new Item(name, price, description);
        }

        public void DeleteMenuItems(Dictionary<string, List<Item>> menuItems, int itemId)
        {
            foreach (var item in menuItems)
            {
                int removeIndex = -1;
                for (int i = 0; i < item.Value.Count; i++)
                {
                    if (item.Value[i].id == itemId)
                    {
                        removeIndex = i;
                        break;  // 해당 아이템을 찾았으면 더 이상 순회할 필요가 없으므로 루프를 종료합니다.
                    }
                }

                if (removeIndex > -1)
                {
                    item.Value.RemoveAt(removeIndex);
                }
            }
            Console.WriteLine("상품이 삭제되었습니다.");
        }



    }
}

