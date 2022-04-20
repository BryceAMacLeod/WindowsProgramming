using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestingDemo_GrpA
{
    public class MathModule
    {
        public double Add (double num1, double num2)
        {
            return num1 + num2;
        }

        public double Subtract(double num1, double num2)
        {
            return num1 - num2;
        }

        public double Multiply(double num1, double num2)
        {
            return num1 * num2;
        }

        public double Divide(int num1, int num2)
        {
            try
            {
                return num1 / num2;
            }
            catch(DivideByZeroException ex)
            {
                throw new DivideByZeroException();
            }
        }
    }
}
