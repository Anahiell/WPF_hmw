using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalkulator
{
    public class CalculatorLogic
    {
        public static double Add(double operand1, double operand2)
        {
            return operand1 + operand2;
        }

        public static double Subtract(double operand1, double operand2)
        {
            return operand1 - operand2;
        }

        public static double Multiply(double operand1, double operand2)
        {
            return operand1 * operand2;
        }

        public static double Divide(double operand1, double operand2)
        {
            if (operand2 == 0)
            {
                throw new DivideByZeroException("Cannot divide by zero.");
            }

            return operand1 / operand2;
        }
    }
}
