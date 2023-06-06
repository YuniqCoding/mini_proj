using System;

namespace test
{
    class CStudent    {        private int id;        private int kor;        private int math;        private int eng;        private int total;        public int ID { get { return id; } }        public int KOR { get { return kor; } }        public int MATH { get { return math; } }        public int ENG { get { return eng; } }        public int TOTAL { get { return total; } }        public CStudent(int id, int kor, int math, int eng)        {            this.id = id;            this.kor = kor;            this.math = math;            this.eng = eng;            this.total = kor + math + eng;        }    }
    class Program
    {
        static void Main(string[] args)
        {
            List<CStudent> listData = new List<CStudent>();
            bool IsLoop = true;

            InitData(listData);

            PrintList(listData);

            do
            {
                Console.WriteLine("메뉴를 골라주세요.");
                Console.Write("(1)id 정렬 (2)성적순 정렬 (3)국어점수 정렬 (4)특정점수 이상 (5)특정점수 이하 (0)나가기");

                string inputNum = Console.ReadLine();
                switch (inputNum)                 {                    case "0":                        Console.WriteLine("프로그램 종료");                        IsLoop = false;                        break;                    case "1":                        SortID(listData);                         break;                    case "2":                        SortTotal(listData);                         break;                    case "3":                        SortKor(listData);                        break;                    case "4":                        FindDataUp(listData,true);                        break;                    case "5":                        FindDataUp(listData, false);                        break;                      default:                        Console.Clear();                        Console.WriteLine("다시 입력하세요");                        break;                                }

            } while (IsLoop);
        }
        /// <summary>
        /// 학생 데이터를 생성
        /// 0 0 0 0
        /// 1, ?, ?, ?
        /// </summary>
        /// <param name="_listData"></param>
        static void InitData(List<CStudent> _listData)
        {
            Random rd = new Random();
            for (int i = 0; i < 10; i++)            {                CStudent student = new CStudent(i, rd.Next(0, 100), rd.Next(0, 100), rd.Next(0, 100));                _listData.Add(student);            }
        }

        static void PrintList(List<CStudent> _listData)
        {
            Console.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}", "ID", "KOR", "MATH", "ENG", "TOTAL");
            Console.WriteLine("======================================================================");

            foreach (var student in _listData)
            {                Console.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}", student.ID, student.KOR, student.MATH, student.ENG, student.TOTAL);
            }
        }

        static void SortID(List<CStudent> _listData)
        {
            _listData.Sort(delegate(CStudent a, CStudent b)
            {
                if (a.ID > b.ID)
                    return 1;
                else if (a.ID < b.ID)
                    return -1;
                else
                    return 0;
	        });

            Console.WriteLine("아이디 정렬");
            PrintList(_listData);
        }

        static void SortTotal(List<CStudent> _listData)
        {
            var items = from item in _listData
                        orderby item.TOTAL descending
                        select item;

            List<CStudent> sortData = items.ToList<CStudent>();

            Console.WriteLine("총점 정렬");
            PrintList(sortData);
        }

        static void SortKor(List<CStudent> _listData)
        {
            _listData.Sort((CStudent a, CStudent b) => { return b.KOR - a.KOR; });

            Console.WriteLine("국어 점수 정렬");
            PrintList(_listData);
        }

        static void FindDataUp(List<CStudent> _listData, bool isUp)
        {
            Console.WriteLine("기준 점수를 입력하세요");
            string inputData = Console.ReadLine();
            int num = 0;
            try            {                num = int.Parse(inputData);            }            catch (FormatException e)            {                Console.Clear();                Console.WriteLine("입력값 {0} 잘못된 입력입니다. 숫자만 입력하세요!",num);            }            finally            {                if (num < 0)                {                    Console.Clear();                    Console.WriteLine("입력값 {0} 잘못된 입력입니다. 작은 수 입니다!", num);                }                else if (num > 300)                {                    Console.Clear();                    Console.WriteLine("입력값 {0} 잘못된 입력입니다. 큰 수 입니다!", num);                }            }
            if (0 <= num && num <= 300)            {                if (isUp)                {                    var findData =                        from data in _listData                        where data.TOTAL >= num                        select data;                    List<CStudent> toData = findData.ToList<CStudent>();                    PrintList(toData);                    SortID(toData);                    SortTotal(toData);                }                else                {                    var findData =                        from data in _listData                        where data.TOTAL <= num                        select data;                    List<CStudent> toData = findData.ToList<CStudent>();                    PrintList(toData);                    SortID(toData);                    SortTotal(toData);                }            }
        }
    }
    
}
