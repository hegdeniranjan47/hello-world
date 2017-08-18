using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScientificCalculator
{
    class Arithm
    {
        public delegate Arithm ArithmEvent(object sender, EventArgs e);
        public event ArithmEvent updateSign;
        public event ArithmEvent updateResult;
        private static Arithm instance = new Arithm();
        private Arithm()
        {

        }
        public static Arithm Instance
        {
            get { return instance; }
            set { instance = value; }
        }

        public double basicOperation(string op, double number1, double number2)
        {
            if (updateResult != null)
                updateResult(this, null);
            if (updateSign != null)
                updateSign(this, null);
            switch (op)
            {
                case "+":
                    return number1 + number2;
                case "-":
                    return number1 - number2;
                case "X":
                    return number1 * number2;
                case "÷":
                    return number1 / number2;
                default:
                    return 0;
            }
        }
    }
}
