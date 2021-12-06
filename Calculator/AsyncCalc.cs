using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CalcLibrary;

namespace Calculator
{
    public static class AsyncCalc
    {
        public static async Task<double> Diff(double num1, double num2)
        {
            return await Task.Run(() => Calc.Diff(num1, num2)); 
        }
        public static async Task<double> Divide(double num1, double num2)
        {
            return await Task.Run(() => Calc.Divide(num1, num2)); 
        }
        public static async Task<double> Multiply(double num1, double num2)
        {
            return await Task.Run(() => Calc.Multiply(num1, num2)); 
        }
        public static async Task<double> Sqrt(double num1)
        {
            return await Task.Run(() => Calc.Sqrt(num1)); 
        }   
        public static async Task<double> Sum(double num1, double num2)
        {
            return await Task.Run(() => Calc.Sum(num1, num2));
        }
    }
}
