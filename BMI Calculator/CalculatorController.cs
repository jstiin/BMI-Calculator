using System;

namespace BMI_Calculator
{
    internal class CalculatorController
    {
        private Form1 form1; // Form1 field
        public CalculatorController(Form1 form1)    // CalculatorController constructor
        {
            this.form1 = form1; // Assigns the form1 parameter to the form1 field
        }

        // BMI calculation method for metric units
        public float BMImetric()
        {
            try
            {
                float height = float.Parse(form1.GetMaskedTextBox1().Text); // converts the text in maskedTextBox1 to float
                float weight = float.Parse(form1.GetMaskedTextBox3().Text); // converts the text in maskedTextBox3 to float

                if (height < 0 || weight < 0)   // throws ArithmeticException if height or weight is less than 0
                {
                    throw new ArithmeticException();
                }

                height /= 100;  // converts height from cm to m

                float bmi = weight / (height * height); // calculates BMI
                bmi = (float)Math.Round(bmi, 2);    // rounds the BMI to 2 decimal places

                return bmi; // returns the BMI
            }
            catch (FormatException) { return -1; }  // returns -1 if FormatException is caught
            catch (ArithmeticException) { return -2; }  // returns -2 if ArithmeticException is caught
        }

        // BMI calculation method for imperial units
        public float BMIimperial()
        {
            try
            {
                float heightFT = float.Parse(form1.GetMaskedTextBox1().Text);   // converts the text in maskedTextBox1 to float
                float heightIN = float.Parse(form1.GetMaskedTextBox2().Text);   // converts the text in maskedTextBox2 to float
                float weight = float.Parse(form1.GetMaskedTextBox3().Text);     // converts the text in maskedTextBox3 to float

                if (heightFT < 0 || weight < 0 || heightIN < 0) // throws ArithmeticException if height or weight is less than 0
                {
                    throw new ArithmeticException();
                }

                heightFT *= 12; // converts height from ft to in
                heightFT += heightIN;   // adds the inches to the height

                float bmi = (weight / (heightFT * heightFT)) * 703; // calculates BMI using imperial formula
                bmi = (float)Math.Round(bmi, 2);    // rounds the BMI to 2 decimal places

                return bmi; // returns the BMI
            }
            catch (FormatException) { return -1; }  // returns -1 if FormatException is caught
            catch (ArithmeticException) { return -2; } // returns -2 if ArithmeticException is caught
        }
    }
}
