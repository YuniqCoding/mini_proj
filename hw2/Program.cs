using System;

namespace homework1
{
    delegate void Delegate(List<string> a);
    class Program
    {

        //1번

        struct Pokemon
        {
            public int number;
            public string name;
            public string type;
            public double height;
            public double weight;
            public double attack;
            public double defense;
            public double speed;

            public Pokemon(int number, string name, string type, double height, double weight,
                double attack, double defense, double speed)
            {
                this.number = number;
                this.name = name;
                this.type = type;
                this.height = height;
                this.weight = weight;
                this.attack = attack;
                this.defense = defense;
                this.speed = speed;
            }
        }
        static void Main(string[] args)
        {
            Pokemon[] arrPokemon =
             {
                new Pokemon(10, "caterpie", "bug", 0.3, 2.9, 30, 35, 45),
                new Pokemon(25, "pikachu", "electric", 0.4, 6, 55, 40, 90),
                new Pokemon(26, "raichu", "electric", 0.8, 30, 90, 55, 110),
                new Pokemon(133, "eevee", "normal", 0.3, 6.5, 55, 50, 55),
                new Pokemon(152, "chikoirita", "grass", 0.9, 6.4, 49, 65, 45)
            };

            var aData1 =
                from data in arrPokemon
                where data.name == "eevee"
                select data.type;
            var aData2 =
                from data in arrPokemon
                where data.name == "caterpie"
                select (data.attack, data.defense);
            var aData3 =
                from data in arrPokemon
                where data.weight >6
                select data.name;
            var aData4 =
                from data in arrPokemon
                where data.weight >= 6 && data.height >0.5
                select data.name;
            var aData5=
                from data in arrPokemon
                where Math.Abs(data.attack - data.defense)>= 10
                select (data.name,data.attack,data.defense);

            foreach (var data in aData1)
            {
                Console.WriteLine("answer1: {0}", data);
            }
            foreach (var data in aData2)
            {
                Console.WriteLine("answer2: {0}", data);
            }
            foreach (var data in aData3)
            {
                Console.WriteLine("answer3: {0}", data);
            }
            foreach (var data in aData4)
            {
                Console.WriteLine("answer4: {0}", data);
            }
            foreach (var data in aData5)
            {
                Console.WriteLine("answer5: {0}", data);
            }
        }


    }
}
