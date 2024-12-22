using System;
using System.Windows.Forms;

namespace BMI_Calculator
{
    internal static class Program
    {

        [STAThread]
        static void Main()  // Main method
        {
            Application.EnableVisualStyles();   // Enables visual styles
            Application.SetCompatibleTextRenderingDefault(false);   // Sets the compatible text rendering default to false

            Form1 form1 = new Form1();  // Initializes a new Form1 object
            CalculatorController controller = new CalculatorController(form1);  // Initializes a new CalculatorController object with the Form1 object as the parameter

            Application.Run(form1); // Runs the application with the Form1 object
        }
    }
}
