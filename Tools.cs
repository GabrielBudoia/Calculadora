using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculadora
{
    public class Tools
    {
        private double _value = 0;
        private string _operation;

        public double GetValue() 
        {
            return _value;
        }

        public string GetOperation()
        {
            return _operation;
        }


        public void DefineResult(double result ) 
        {
            _value = result;
        }

        public void DefineOperation(string op)
        {
            _operation = op;
        }

        


        public double Calculate(double newValue) 
        {
            if (string.IsNullOrEmpty(_operation)) 
            
            {
                _value = newValue;
                return _value;
            }
                switch (_operation)
                {
                    case "Root":
                        if(newValue < 0)
                        {
                            throw new ArgumentException("Cannot calculate the square root of a negative number.");
                        }
                    _value = Math.Sqrt(newValue);
                         break;

                    case "Plus":
                        _value += newValue;
                        break;
                    case "Minus":
                        _value -= newValue;
                        break;
                    case "Times":
                        _value *= newValue;
                        break;
                    case "Divide":
                        if (newValue != 0)
                        {
                            _value /= newValue;
                        }
                        else
                        {
                            throw new DivideByZeroException("Cannot divide by zero.");
                            
                    }
                        break;
                default:
                    throw new InvalidOperationException("Invalid operation.");
                    

            }
            return _value;
        }
        
            
    }
}
