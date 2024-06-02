using System;
using System.Text.RegularExpressions;

namespace Week04.Homework
{
    public class Calculator
    {
        private int firstNumber;
        private int secondNumber;
        private AbstractOperation operation;

        public Calculator(AbstractOperation operation)
        {
            this.operation = operation;
        }

        public Calculator()
        {
        }

        public void SetOperation(AbstractOperation operation)
        {
            this.operation = operation;
        }

        public void SetFirstNumber(int firstNumber)
        {
            this.firstNumber = firstNumber;
        }

        public void SetSecondNumber(int secondNumber)
        {
            this.secondNumber = secondNumber;
        }

        public double Calculate()
        {
            double answer = 0;
            answer = operation.Operate(this.firstNumber, this.secondNumber);
            return answer;
        }
    }

    public class BadInputException : Exception
    {
        public BadInputException(string type) : base("잘못된 입력입니다! " + type + "을 입력해주세요!")
        {
        }
    }

    public class CalculatorApp
    {
        public static bool Start()
        {
            Parser parser = new Parser();
            Console.WriteLine("첫번째 숫자를 입력해주세요!");
            string firstInput = Console.ReadLine();

            try
            {
                parser.ParseFirstNum(firstInput);
            }
            catch (BadInputException e)
            {
                Console.WriteLine(e.Message);
                return false;
            }

            Console.WriteLine("연산자를 입력해주세요!");
            string operatorInput = Console.ReadLine();

            try
            {
                parser.ParseOperator(operatorInput);
            }
            catch (BadInputException e)
            {
                Console.WriteLine(e.Message);
                return false;
            }

            Console.WriteLine("두번째 숫자를 입력해주세요!");
            string secondInput = Console.ReadLine();

            try
            {
                parser.ParseSecondNum(secondInput);
            }
            catch (BadInputException e)
            {
                Console.WriteLine(e.Message);
                return false;
            }

            Console.WriteLine("연산 결과 : " + parser.ExecuteCalculator());
            return true;
        }
    }

    public class DivideOperation : AbstractOperation
    {
        public override double Operate(int a, int b)
        {
            return (double)a / b;
        }
    }

    public class MultiplyOperation : AbstractOperation
    {
        public override double Operate(int a, int b)
        {
            return a * b;
        }
    }

    public class AbstractOperation
    {
        public virtual double Operate(int a, int b)
        {
            throw new NotImplementedException();
        }
    }

    public class Parser
    {
        private static readonly string OPERATION_REG = "[+\\-*/]";
        private static readonly string NUMBER_REG = "^[0-9]*$";

        private readonly Calculator calculator = new Calculator();

        public Parser ParseFirstNum(string firstInput)
        {
            if (Regex.IsMatch(firstInput, NUMBER_REG))
            {
                calculator.SetFirstNumber(int.Parse(firstInput));
            }
            else
            {
                throw new BadInputException("int");
            }
            return this;
        }

        public Parser ParseSecondNum(string secondInput)
        {
            if (Regex.IsMatch(secondInput, NUMBER_REG))
            {
                calculator.SetSecondNumber(int.Parse(secondInput));
            }
            else
            {
                throw new BadInputException("int");
            }
            return this;
        }

        public Parser ParseOperator(string operationInput)
        {
            if (Regex.IsMatch(operationInput, OPERATION_REG))
            {
                switch (operationInput)
                {
                    case "+":
                        calculator.SetOperation(new AddOperation()); break;
                    case "-":
                        calculator.SetOperation(new SubstractOperation()); break;
                    case "*":
                        calculator.SetOperation(new MultiplyOperation()); break;
                    case "/":
                        calculator.SetOperation(new DivideOperation()); break;
                }
            }
            else
            {
                throw new BadInputException("String");
            }
            return this;
        }

        public double ExecuteCalculator()
        {
            return calculator.Calculate();
        }
    }

    public class SubstractOperation : AbstractOperation
    {
        public override double Operate(int a, int b)
        {
            return a - b;
        }
    }

    public class AddOperation : AbstractOperation
    {
        public override double Operate(int a, int b)
        {
            return a + b;
        }
    }

    class MainClass
    {
        public static void Main(string[] args)
        {
            bool calculateEnded = false;
            while (!calculateEnded)
            {
                try
                {
                    calculateEnded = CalculatorApp.Start();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}
