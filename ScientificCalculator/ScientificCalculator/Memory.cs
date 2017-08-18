using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;

namespace ScientificCalculator
{
    class Memory
    {
        string equation = "";
        double result = 0;
        string op = "";
        double firstNumber = 0, secondNumber = 0;
        bool second = false, done = false;
        Arithm Operate = Arithm.Instance;
        char lastchar='\0';
        //to handle events

        public delegate void MemoryEvent(object sender, EventArgs e);
        public event MemoryEvent updateEquation;
        public event MemoryEvent updateOutput;
        public event MemoryEvent calcDone;
        
        
        /// <summary>
        /// Making the class Singleton
        /// </summary>
        #region
        private static Memory instance = new Memory();
        private Memory()
        {

        }
        public static Memory Instance
        {
            get { return instance; }
            set { instance = value; }
        }
        #endregion

        /// <summary>
        /// function called when a number key is pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void newNumber(object sender, EventArgs e)
        {
            done = false;
            Button b = new Button();
            b = (Button)sender;
            equation = equation + b.Text;
            if (updateEquation != null)
                updateEquation(this, e);

        }
        public void deleteChar(object sender,EventArgs e)
        {
            if (equation.Length == 0)
                return;
            lastchar = equation[equation.Length - 1];
            if (lastchar.GetType() == typeof(int))
                equation = equation.Remove(equation.Length - 1, 1);
            else
            {
                equation = equation.Remove(equation.Length - 1, 1);
                op = "";
                second = false;
            }
            if (updateEquation != null)
                updateEquation(this, e);
        }
        public string Equation
        {
            get { return equation; }
            set { ;}
        }

        public double Result
        {
            get { return result; }
            set { result = value; }
        }

        /// <summary>
        /// When a new arithmetic operator is entered
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void newSign(object sender, EventArgs e)
        {
            done = false;
            if (second == true)
            {
                getSecondNumber();
                firstNumber = Arithm.Instance.basicOperation(op, firstNumber, secondNumber);
                secondNumber = 0;
                equation = firstNumber.ToString();
                if (updateEquation != null)
                {
                    updateEquation(this, e);
                }
            }
            if (second == false)
            {
                if (equation.Length == 0)
                {
                    firstNumber = result;
                    equation = firstNumber.ToString();
                }
                else
                    firstNumber = double.Parse(equation);
                second = true;
            }
            Button b = new Button();
            b = (Button)sender;
            op = b.Text;
            equation = equation + b.Text;
            
            if (updateEquation != null)
            {
                updateEquation(this, e);
            }
            if (updateOutput != null)
            {
                updateOutput(this, e);
            }

        }

        /// <summary>
        /// function called when equals sign is pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void EqualsSignPressed(object sender, EventArgs e)
        {
            Trace.WriteLine("= sign pressed");
            Debug.WriteLine("Debug Message");
            if (op != "")
            {
                getSecondNumber();
                result = Arithm.Instance.basicOperation(op, firstNumber, secondNumber);
            }
            else
            {
                firstNumber = double.Parse(equation);
                result = firstNumber;
                equation = "";
                if (calcDone != null)
                    calcDone(this, null);

            }
            if(!done)
               done = true;
            firstNumber = secondNumber = 0;
            second = false;
            equation = "";
            if (calcDone != null)
                calcDone(this, e);
        }

        /// <summary>
        /// function to retrieve the second operand from the equation string
        /// </summary>
        private void getSecondNumber()
        {
            string temp = equation;
            int len = (firstNumber.ToString()).Length;
            temp=temp.Remove(0, len+1);
            if (temp.Length < 1)
                secondNumber = 0;
            else
                secondNumber = Convert.ToDouble(temp);
        }

        /// <summary>
        /// Clear All parameters and variables
        /// </summary>
        public void clearAll()
        {
            op = "";
            firstNumber = secondNumber = 0;
            second = done = false;
            result = 0;
            equation = "";
            if (calcDone != null)
                calcDone(this, null);
        }

        public void AnsKeyPressed()
        {
            if (second == true)
            {
                secondNumber = result;
                equation = firstNumber.ToString() + op + secondNumber.ToString();
            }
            else
            {
                firstNumber = result;
                equation = firstNumber.ToString();
            }
            if (updateEquation != null)
                updateEquation(this, null);
        }

    }
}
