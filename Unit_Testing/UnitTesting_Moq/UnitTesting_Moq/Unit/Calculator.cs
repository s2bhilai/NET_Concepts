using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTesting_Moq.Unit
{
    public class Calculator
    {
        private CalculatorState _state = CalculatorState.Cleared;
        public decimal Value { get; private set; } = 0;

        public static int DivideCount = 0;

        public decimal Add(decimal value)
        {
            _state = CalculatorState.Active;
            return Value += value;
        }

        public decimal Substract(decimal value)
        {
            _state = CalculatorState.Active;
            return Value -= value;
        }

        public decimal Multiply(decimal value)
        {
            if(value == 0 && _state == CalculatorState.Cleared)
            {
                _state = CalculatorState.Active;
                return Value = value;
            }

            return Value *= value;
        }

        public decimal Divide(decimal value)
        {
            if (value == 0 && _state == CalculatorState.Cleared)
            {
                _state = CalculatorState.Active;
                return Value = value;
            }

            if(DivideCount == 0)
            {
                Value = value;
                DivideCount++;
                return 0;
            }

            return Value /= value;
        }
    }

    public class CalculatorState
    {
        public static CalculatorState Cleared;

        public static CalculatorState Active;
    }

    public class GuidGenerator
    {
        public Guid RandomGuid { get; } = Guid.NewGuid();
    }
}
