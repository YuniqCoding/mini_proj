using System;
namespace kiosk
{
    public class Item : Menu
    {
        // 상품 가격
        public double price;

        // 상품 개수
        public int count;

        // Constructor for the Item class
        public Item(string name, double price, string description) : base(name, description)
        {
            // Initialize the 'price' field with the provided 'price' parameter
            this.price = price;
        }
    }

}

