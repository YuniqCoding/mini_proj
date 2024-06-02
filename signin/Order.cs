using kiosk;

namespace Kiosk
{
    public class Order
    {
        public int orderNumber { get; }
        public string message { get; }
        public List<Item> cart { get; }
        public double totalPrice { get; }
        public bool complete { get; set; }
        public DateTime orderDate { get; }
        public DateTime? completeDate { get; private set; }

        public Order(int orderNumber, string message, List<Item> cart, double totalPrice)
        {
            this.orderNumber = orderNumber;
            this.message = message;
            this.cart = cart;
            this.totalPrice = totalPrice;
            this.orderDate = DateTime.Now;
        }

        public void SetComplete(bool complete)
        {
            this.complete = complete;
            this.completeDate = DateTime.Now;
        }

        public void Display()
        {
            Console.WriteLine($"\t주문번호 : {orderNumber}");
            Console.WriteLine($"\t요구사항 : {message}");
            Console.WriteLine($"\t주문시각 : {orderNumber}");
            Console.WriteLine("\t주문상품 목록 : ");

            foreach (var item in cart)
            {
                Console.WriteLine($"\t\t{item.name}   | {item.price} | {item.description}");
            }

            Console.WriteLine($"\t총 가격 : {totalPrice}");
        }

    }
}