using System;

namespace kiosk
{
    public class MenuContext
    {
        private Dictionary<string, List<Menu>> menus;     // 메뉴
        private Dictionary<string, List<Item>> menuItems; // 상품메뉴
        private List<Item> cart;                            // 장바구니
        private double totalPrice;                         // 전체 가격
        private int orderNumber;                            // 주문 번호

        public MenuContext()
        {
            menus = new Dictionary<string, List<Menu>>();
            menuItems = new Dictionary<string, List<Item>>();
            cart = new List<Item>();
            totalPrice = 0.0;
            orderNumber = 0;

            InitializeMenuItems();   // 메뉴 및 상품메뉴 초기화
        }
        private void InitializeMenuItems()
        {
            List<Menu> mainMenus = new List<Menu>
            {
                new Menu("Burgers", "앵거스 비프 통살을 다져만든 버거"),
                new Menu("Frozen Custard", "매장에서 신선하게 만드는 아이스크림"),
                new Menu("Drinks", "매장에서 직접 만드는 음료"),
                new Menu("Beer", "뉴욕 브루클린 브루어리에서 양조한 맥주")
            };

            List<Menu> orderMenus = new List<Menu>
            {
                new Menu("Order", "장바구니를 확인 후 주문합니다."),
                new Menu("Cancel", "진행중인 주문을 취소합니다."),
                new Menu("Order List", "대기/완료된 주문목록을 조회합니다.")
            };

            menus.Add("Main", mainMenus);
            menus.Add("Order", orderMenus);

            List<Item> burgersMenus = new List<Item>
            {
                new Item("Hamburger", 5.4,"비프패티를 기반으로 야채가 들어간 기본버거"),
                new Item("ShackBurger", 6.9, "토마토, 양상추, 쉑소스가 토핑된 치즈버거"),
                new Item("SmokeShack", 8.9, "베이컨, 체리 페퍼에 쉑소스가 토핑된 치즈버거"),
                new Item("Shroom Burger", 9.4, "몬스터 치즈와 체다 치즈로 속을 채운 베지테리안 버거"),
                new Item("Cheeseburger", 6.9, "포테이토 번과 비프패티, 치즈가 토핑된 치즈버거"),
                new Item("Hamburger", 5.4, "비프패티를 기반으로 야채가 들어간 기본버거")
            };

            List<Item> frozenCustardMenu = new List<Item>
            {
                new Item("Frozen Custard Menu Item 1", 1.4, "Frozen Custard Menu Item 1 설명"),
                new Item("Frozen Custard Menu Item 2", 1.0, "Frozen Custard Menu Item 2 설명"),
                new Item("Frozen Custard Menu Item 3", 1.6, "Frozen Custard Menu Item 3 설명"),
                new Item("Frozen Custard Menu Item 4", 2.1, "Frozen Custard Menu Item 4 설명")
            };

            List<Item> drinksMenu = new List<Item>
            {
                new Item("Drinks Menu Item 1", 1.0, "Drinks Menu Item 1 설명"),
                new Item("Drinks Menu Item 2", 1.0, "Drinks Menu Item 2 설명")
            };

            List<Item> beerMenu = new List<Item>
            {
                new Item("Beer Menu Item 1", 3.0, "Beer Menu Item 1 설명"),
                new Item("Beer Menu Item 2", 4.0, "Beer Menu Item 2 설명")
            };

            menuItems.Add("Burgers", burgersMenus);
            menuItems.Add("Frozen Custard", frozenCustardMenu);
            menuItems.Add("Drinks", drinksMenu);
            menuItems.Add("Beer", beerMenu);
        }

        /**
         * 메뉴 조회
         * @param key 조회할 메뉴 키값
         * @return 조회된 메뉴 목록
         */
        public List<Menu> GetMenus(string key)
        {
            // Assuming 'menus' is a Dictionary<string, List<Menu>> in your class
            if (menus.ContainsKey(key))
            {
                return menus[key];
            }
            else
            {
                // Handle the case where the key is not found (return an empty list or null, depending on your requirements)
                return new List<Menu>(); // Or return null;
            }
        }

        /**
         * 상품메뉴 조회
         * @param key 조회할 상품메뉴 키값
         * @return 조회된 상품메뉴 목록
         */
        public List<Item> GetMenuItems(string key)
        {
            // Assuming 'menuItems' is a Dictionary<string, List<Item>> in your class
            if (menuItems.ContainsKey(key))
            {
                return menuItems[key];
            }
            else
            {
                // Handle the case where the key is not found (return an empty list or null, depending on your requirements)
                return new List<Item>(); // Or return null;
            }
        }

        public Dictionary<string, List<Item>> GetMenuItemMap()
        {
            return menuItems;
        }


        public List<Item> GetCart()
        {
            return cart;
        }

        public void AddMenu(string key, string description)
        {
            menus["Main"].Add(new Menu(key, description));
            menuItems[key] = new List<Item>();
        }

        public void AddMenuItem(string key, Item newItem)
        {
            menuItems[key].Add(newItem);
        }

        public string GetMainMenuName(int id)
        {
            List<Menu> mainMenus = menus["Main"];
            foreach (Menu mainMenu in mainMenus)
            {
                if (mainMenu.id == id)
                {
                    return mainMenu.name;
                }
            }
            return "";
        }


        /**
        * 장바구니에 상품메뉴 추가
        * @param menuItem 장바구니에 추가할 상품메뉴
        */
        public void AddToCart(Item menuItem)
        {
            if (cart.FirstOrDefault(item => item.name == menuItem.name) != null)
            {
                menuItem.count++;
            }
            else
            {
                menuItem.count = 1;
                cart.Add(menuItem);
            }

            totalPrice += menuItem.price;
        }


        public void DisplayAllItem()
        {
            Console.WriteLine("[ 전체 상품 목록 ]");
            foreach (var item in menuItems)
            {
                Console.WriteLine(" [ " + item.Key + " Menu ]");
                foreach (var v in item.Value)
                {
                    Console.WriteLine($"{v.id}. {v.name}   | {v.price} | {v.description}");
                }
            }
        }

        /**
         * 장바구니 출력
         */
        public void DisplayCart()
        {
            foreach (Item item in cart)
            {
                Console.WriteLine($"{item.name}   | W {item.price} | {item.count}개 | {item.description}");
            }
        }

        /**
	     * 장바구니 전체가격 조회
	     * @return 장바구니 전체가격
	     */
        public double GetTotalPrice()
        {
            return totalPrice;
        }

        /**
	     * 신규 주문번호 조회
	     * @return 신규 주문번호
	     */
        public int GenerateOrderNumber()
        {
            orderNumber++;
            return orderNumber;
        }

        /**
	     * 장바구니 초기화
	     */
        public void ResetCart()
        {
            cart.Clear();
            totalPrice = 0.0;
        }
    }

}

