using System;
namespace kiosk
{
    public class Menu
    {
        private static int idSeq = 1;

        public int id;
        public string name;
        public string description;

        public Menu(string name, string description)
        {
            this.id = idSeq++;
            this.name = name;
            this.description = description;
        }
    }
}

