using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScientificCalculator
{
    public partial class Form1 : Form
    {
        Memory memoryObj = Memory.Instance;

        public Form1()
        {
            InitializeComponent();
            subscribeToEvents();

        }

        /// <summary>
        ///Event Definitions 
        /// </summary>
        #region
        void subscribeToEvents()
        {
            Memory.Instance.updateEquation += Instance_updateEquation;
            Memory.Instance.updateOutput += Instance_updateResult;
            Memory.Instance.calcDone += Instance_calcDone;
        }

        void Instance_calcDone(object sender, EventArgs e)
        {
            equation.Text = Memory.Instance.Equation;
            Output.Text = Memory.Instance.Result.ToString();
        }

        void Instance_updateResult(object sender, EventArgs e)
        {
            Output.Text = Memory.Instance.Result.ToString();
        }

        void Instance_updateEquation(object sender, EventArgs e)
        {
            equation.Text = Memory.Instance.Equation;
        }
        #endregion

        private void button2_Click(object sender, EventArgs e)
        {
            memoryObj.newNumber(sender, e);
        }
        private void basicOp(object sender, EventArgs e)
        {
            memoryObj.newSign(sender, e);
        }

        private void EqualsPressed(object sender, EventArgs e)
        {
            memoryObj.EqualsSignPressed(sender, e);
        }

        private void backSpace(object sender, EventArgs e)
        {
            memoryObj.deleteChar(sender, e);
        }

        private void clear(object sender, EventArgs e)
        {
            memoryObj.clearAll();
        }

        private void AnsKey(object sender, EventArgs e)
        {
            memoryObj.AnsKeyPressed();
        }



    }
}
