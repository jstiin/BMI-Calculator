using System;
using System.Drawing;
using System.Windows.Forms;

namespace BMI_Calculator
{
    public partial class Form1 : Form
    {
        private const string metric = "Metric";
        private const string imperial = "Imperial";
        private const string mask1metric = "Height in cm";
        private const string mask3metric = "Weight in kg";
        private const string mask1imperial = "Height in ft";
        private const string mask2imperial = "in";
        private const string mask3imperial = "Weight in lbs";
        private readonly CalculatorController controller;

        public Form1()  // Form1 constructor
        {
            InitializeComponent();  // Initializes the components

            // Centers most of the elements horizontally on the form
            comboBox1.Left = (this.ClientSize.Width / 2) - (comboBox1.Width / 2);
            label1.Left = (this.ClientSize.Width / 2) - (label1.Width / 2);
            label2.Left = (this.ClientSize.Width / 2) - (label2.Width / 2);
            label3.Left = (this.ClientSize.Width / 2) - (label3.Width / 2);
            label4.Left = (this.ClientSize.Width / 2) - (label4.Width / 2);

            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;   // Sets the combobox style to DropDownList
            comboBox1.Items.Add(metric);    // Adds "Metric" to the combobox
            comboBox1.Items.Add(imperial);  // Adds "Imperial" to the combobox
            comboBox1.SelectedIndex = 0;    // Sets the default selected item to "Metric"

            // Subscribes the GotFocus events to the maskedTextBoxes
            maskedTextBox1.GotFocus += MaskedTextBox1_GotFocus;
            maskedTextBox2.GotFocus += MaskedTextBox2_GotFocus;
            maskedTextBox3.GotFocus += MaskedTextBox3_GotFocus;

            controller = new CalculatorController(this);    // Initializes the controller with the current form as the necessary parameter
        }

        // ComboBox SelectedIndexChanged event
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Unsubscribes the leave events from the maskedTextBoxes to prevent overlapping
            maskedTextBox1.Leave -= MaskedTextBox1_Leave;
            maskedTextBox2.Leave -= MaskedTextBox2_Leave;
            maskedTextBox3.Leave -= MaskedTextBox3_Leave;

            if (comboBox1.SelectedIndex == 0) // If "Metric" is selected in the combobox
            {
                maskedTextBox1.Mask = mask1metric;  // Set the mask of maskedTextBox1 to "Height in cm" / mask1metric
                maskedTextBox2.Visible = false; // Hide maskedTextBox2
                maskedTextBox3.Mask = mask3metric;  // Set the mask of maskedTextBox3 to "Weight in kg" / mask3metric

                // Subscribes the leave events to the maskedTextBoxes
                maskedTextBox1.Leave += MaskedTextBox1_Leave;
                maskedTextBox3.Leave += MaskedTextBox3_Leave;
            }
            else if (comboBox1.SelectedIndex == 1)  // If "Imperial" is selected in the combobox
            {
                maskedTextBox1.Mask = mask1imperial;    // Set the mask of maskedTextBox1 to "Height in ft" / mask1imperial
                maskedTextBox2.Visible = true;  // Show maskedTextBox2
                maskedTextBox2.Mask = mask2imperial;    // Set the mask of maskedTextBox2 to "in" / mask2imperial
                maskedTextBox3.Mask = mask3imperial;    // Set the mask of maskedTextBox3 to "Weight in lbs" / mask3imperial

                // Subscribes the leave events to the maskedTextBoxes
                maskedTextBox1.Leave += MaskedTextBox1_Leave;
                maskedTextBox2.Leave += MaskedTextBox2_Leave;
                maskedTextBox3.Leave += MaskedTextBox3_Leave;
            }
        }

        // Designer-generated code (unused)
        private void label1_Click(object sender, EventArgs e) { }
        private void label2_Click(object sender, EventArgs e) { }
        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e) { }
        private void maskedTextBox2_MaskInputRejected(object sender, MaskInputRejectedEventArgs e) { }

        // MaskedTextBoxes GotFocus events
        private void MaskedTextBox2_GotFocus(object sender, EventArgs e) { maskedTextBox2.Mask = string.Empty; }  // If maskedTextBox2 is selected, set the mask to empty
        private void MaskedTextBox1_GotFocus(object sender, EventArgs e) { maskedTextBox1.Mask = string.Empty; }  // If maskedTextBox1 is selected, set the mask to empty
        private void MaskedTextBox3_GotFocus(object sender, EventArgs e) { maskedTextBox3.Mask = string.Empty; }  // If maskedTextBox3 is selected, set the mask to empty

        // MaskedTextBox2_Leave event
        private void MaskedTextBox2_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(maskedTextBox2.Text))
            {
                maskedTextBox2.Mask = mask2imperial;    // If the maskedTextBox2 (height input) is empty, set the mask to "in" / mask2imperial.
                                                        // Since the textbox isn't visible when "Metric" is selected, it will only use the mask2imperial mask.
            }
        }

        // MaskedTextBox1_Leave event
        private void MaskedTextBox1_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(maskedTextBox1.Text) && comboBox1.SelectedIndex == 0)
            {
                maskedTextBox1.Mask = mask1metric;  // If the maskedTextBox1 (height input) is empty and the combobox is set to "Metric", set the mask to "Height in cm" / mask1metric
            }
            else if (string.IsNullOrEmpty(maskedTextBox1.Text) && comboBox1.SelectedIndex == 1)
            {
                maskedTextBox1.Mask = mask1imperial; // If the maskedTextBox1 (height input) is empty and the combobox is set to "Imperial", set the mask to "Height in ft" / mask1imperial
            }
        }

        // MaskedTextBox3_Leave event
        private void MaskedTextBox3_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(maskedTextBox3.Text) && comboBox1.SelectedIndex == 0)
            {
                maskedTextBox3.Mask = mask3metric;  // If the maskedTextBox3 (weight input) is empty and the combobox is set to "Metric", set the mask to "Weight in kg" / mask3metric
            }
            else if (string.IsNullOrEmpty(maskedTextBox3.Text) && comboBox1.SelectedIndex == 1)
            {
                maskedTextBox3.Mask = mask3imperial;    // If the maskedTextBox3 (weight input) is empty and the combobox is set to "Imperial", set the mask to "Weight in lbs" / mask3imperial
            }
        }

        // Button click event
        private void button1_Click(object sender, EventArgs e)
        {
            float bmi = 0; // Sets the variable "bmi" initially to 0

            if (comboBox1.SelectedIndex == 0) { bmi = controller.BMImetric(); } // If "Metric" is selected in the combobox, calculate BMI using metric system and store the result in the variable "bmi"
            else if (comboBox1.SelectedIndex == 1) { bmi = controller.BMIimperial(); } // If "Imperial" is selected in the combobox, calculate BMI using imperial system and store the result in the variable "bmi"

            if (!string.IsNullOrEmpty(maskedTextBox1.Text) && !string.IsNullOrEmpty(maskedTextBox3.Text) && comboBox1.SelectedIndex == 0)
            {
                label4.Text = bmi.ToString();   // If the height and weight fields are not empty and the combobox is set to "Metric", display the BMI value in the label
            }
            else if (!string.IsNullOrEmpty(maskedTextBox1.Text) && !string.IsNullOrEmpty(maskedTextBox2.Text) && !string.IsNullOrEmpty(maskedTextBox3.Text) && comboBox1.SelectedIndex == 1)
            {
                label4.Text = bmi.ToString();  // If the height and weight fields are not empty and the combobox is set to "Imperial", display the BMI value in the label
            }

            UpdateLabel(bmi); // Calls the updater method. Refer to the method for more information
        }

        // MaskedTextBox getters
        public MaskedTextBox GetMaskedTextBox1() { return maskedTextBox1; }
        public MaskedTextBox GetMaskedTextBox2() { return maskedTextBox2; }
        public MaskedTextBox GetMaskedTextBox3() { return maskedTextBox3; }

        // Method to update the label color based on the BMI value. (Also updates the text if BMI value < 0) 
        public void UpdateLabel(float bmi)
        {
            if (bmi < 0)
            {
                label4.Text = "Invalid input";
                label4.ForeColor = Color.Black;
            }
            else if (bmi < 18.5)
            {
                label4.ForeColor = Color.MediumBlue;
            }
            else if (bmi >= 18.5 && bmi <= 24.99)
            {
                label4.ForeColor = Color.Green;
            }
            else if (bmi >= 25 && bmi <= 29.99)
            {
                label4.ForeColor = Color.DarkOrange;
            }
            else
            {
                label4.ForeColor = Color.Red;
            }
        }
    }
}
