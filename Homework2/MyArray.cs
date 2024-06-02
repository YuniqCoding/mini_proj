using System;
namespace MyArray
{
    public class MyObjectArray
    {

        private int cout;
        private object[] array;
        public int ARRAY_SIZE;

        public MyObjectArray()
        {
            ARRAY_SIZE = 10;
            array = new object[ARRAY_SIZE];
        }

        public MyObjectArray(int size)
        {
            ARRAY_SIZE = size;
            array = new object[ARRAY_SIZE];
        }
    }
    public class MyArray
    {

        int[] intArr;       //int array
        int count;          //개수

        public int ARRAY_SIZE;
        public const int ERROR_NUM = -999999999;

        public MyArray()
        {
            count = 0;
            ARRAY_SIZE = 10;
            intArr = new int[ARRAY_SIZE];
        }

        public MyArray(int size)
        {
            count = 0;
            ARRAY_SIZE = size;
            intArr = new int[size];
        }

        public void addElement(int num)
        {
            if (count >= ARRAY_SIZE)
            {
                Console.WriteLine("not enough memory");
                return;
            }
            intArr[count++] = num;

        }

        public void insertElement(int position, int num)
        {
            int i;

            if (count >= ARRAY_SIZE)
            {  //꽉 찬 경우
                Console.WriteLine("not enough memory");
                return;
            }

            if (position < 0 || position > count)
            {  //index error
                Console.WriteLine("insert Error");
                return;
            }

            for (i = count - 1; i >= position; i--)
            {
                intArr[i + 1] = intArr[i];        // 하나씩 이동
            }

            intArr[position] = num;
            count++;
        }

        public int removeElement(int position)
        {
            int ret = ERROR_NUM;

            if (isEmpty())
            {
                Console.WriteLine("There is no element");
                return ret;
            }

            if (position < 0 || position >= count)
            {  //index error
                Console.WriteLine("remove Error");
                return ret;
            }

            ret = intArr[position];

            for (int i = position; i < count - 1; i++)
            {
                intArr[i] = intArr[i + 1];
            }
            count--;
            return ret;
        }

        public int getSize()
        {
            return count;
        }

        public bool isEmpty()
        {
            if (count == 0)
            {
                return true;
            }
            else return false;
        }

        public int getElement(int position)
        {
            if (position < 0 || position > count - 1)
            {
                Console.WriteLine("검색 위치 오류. 현재 리스트의 개수는 " + count + "개 입니다.");
                return ERROR_NUM;
            }
            return intArr[position];
        }

        public void printAll()
        {
            if (count == 0)
            {
                Console.WriteLine("출력할 내용이 없습니다.");
                return;
            }

            for (int i = 0; i < count; i++)
            {
                Console.WriteLine(intArr[i]);
            }

        }

        public void removeAll()
        {
            for (int i = 0; i < count; i++)
            {
                intArr[i] = 0;
            }
            count = 0;
        }
    }
}

