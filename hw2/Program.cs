using System;
using System.Text.RegularExpressions;

namespace Calculator2
{

    public class Parser
    {
        private const string OPERATION_REG = "[+\\-*/]";
        private const string NUMBER_REG = "^[0-9]*$";

        private static Calculator calculator = new Calculator();

        public Parser parseFirstNum(string firstInput)
        {
            if (!Regex.IsMatch(firstInput, NUMBER_REG)) {
                throw new BadInputException("정수값");
            }   
            calculator.setFirstNumber(int.Parse(firstInput));

            return this;
        }

        public Parser parseSecondNum(string secondInput)
        {
            if (!Regex.IsMatch(secondInput, NUMBER_REG)) {
                throw new BadInputException("정수값");
            }
            calculator.setSecondNumber(int.Parse(secondInput));

            return this;
        }

        public Parser parseOperator(string operationInput)
        {
            if (!Regex.IsMatch(operationInput,OPERATION_REG)) {
                throw new BadInputException("사칙 연산의 연산자");
            }

            switch (operationInput) {
                case "+":
                    calculator.setOperation(new AddOperation());
                    break;
                case "-":
                    calculator.setOperation(new SubstractOperation());
                    break;
                case "*":
                    calculator.setOperation(new MultiplyOperation());
                    break;
                case "/":
                    calculator.setOperation(new DivideOperation());
                    break;
            }

            return this;
        }

        public double executeCalculator()
        {
            return calculator.calculate();
        }
    }
    public class BadInputException : Exception
    {
        public BadInputException(string message) : base(message)
        {
        }
    }
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

        public void setOperation(AbstractOperation operation)
        {
            this.operation = operation;
        }

        public void setFirstNumber(int firstNumber)
        {
            this.firstNumber = firstNumber;
        }

        public void setSecondNumber(int secondNumber)
        {
            this.secondNumber = secondNumber;
        }

        public double calculate()
        {
            double answer = 0;
            answer = operation.operate(this.firstNumber, this.secondNumber);
            return answer;
        }

    }
    public abstract class AbstractOperation
    {
        public abstract double operate(int firstNumber, int secondNumber);
    }
    public class CalculatorApp
    {

        public static bool start()
        {
            Parser parser = new Parser();

            Console.WriteLine("첫번째 숫자를 입력해주세요!");
            string firstInput = Console.ReadLine();
            parser.parseFirstNum(firstInput);
        

            Console.WriteLine("연산자를 입력해주세요!");
            string oper = Console.ReadLine();
            parser.parseOperator(oper);

            Console.WriteLine("두번째 숫자를 입력해주세요!");
            string secondInput = Console.ReadLine();
            parser.parseSecondNum(secondInput);

            Console.WriteLine("연산 결과 : " + parser.executeCalculator());
            return true;
        }


    }
    public class AddOperation : AbstractOperation
    {
        public override double operate(int firstNumber, int secondNumber)
        {
            return firstNumber + secondNumber;
        }
    }

    public class SubstractOperation : AbstractOperation
    {
        public override double operate(int firstNumber, int secondNumber)
        {
            return firstNumber - secondNumber;
        }
    }
    public class MultiplyOperation : AbstractOperation
    {
        public override double operate(int firstNumber, int secondNumber)
        {
            return firstNumber * secondNumber;
        }
    }
    public class DivideOperation : AbstractOperation
    {
        public override double operate(int firstNumber, int secondNumber)
        {
            return firstNumber / secondNumber;
        }
    }
    public class Program
    {
        static void Main(string[] args)
        {
            bool calculateEnded = false;

            while (!calculateEnded)
            {
                try
                {
                    calculateEnded = CalculatorApp.start();
                }
                catch (Exception e)
                {
                    Console.WriteLine("잘못된 입력입니다! " + e.Message + "을 입력해주세요!");
                }
            }
        }
    }
}
