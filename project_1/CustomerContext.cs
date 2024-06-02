namespace Hotel
{
    public class CustomerContext
    {
        private readonly string name;
        private readonly string phoneNumber;
        private double money; // 소지금

        // 생성자
        public CustomerContext(string name, string phoneNumber, double money)
        {
            this.name = name;
            this.phoneNumber = phoneNumber;
            this.money = money;
        }

        // Getter, Setter 메서드
        public double Money
        {
            get { return money; }
            set { money = value; }
        }

        public string Name => name;

        public string PhoneNumber => phoneNumber;

        // 서비스 메서드
        public void RefundMoney(double amount)
        {
            money += amount;
        }
    }
}
