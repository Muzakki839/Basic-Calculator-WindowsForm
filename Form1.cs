using System;
using System.Data;
using System.Windows.Forms;

namespace BasicCalculator
{
    public partial class Form1 : Form
    {
        DataTable table = new DataTable();
        string input = string.Empty;        // String storing user input
        bool isResultDisplay = false;    // Flag to track if the result is displayed

        bool isStart = true;

        public Form1()
        {
            InitializeComponent();
            input = "0";
            textBox1.Text = input;
        }

        private void ButtonNumber_Click(object sender, EventArgs e)
        {
            if (isStart)
            {
                Clear();
                isStart = false;
            }

            if (isResultDisplay)
            {
                input = string.Empty;
                isResultDisplay = false;
            }

            Button button = (Button)sender;
            input += button.Text;
            textBox1.Text += button.Text;
        }

        private void ButtonOperator_Click(object sender, EventArgs e)
        {
            isStart = false;
            if (isResultDisplay)
            {
                isResultDisplay = false;
            }

            Button button = (Button)sender;
            switch (button.Text)
            {
                default:
                    input += button.Text;
                    textBox1.Text += button.Text;
                    break;
                case "%":
                    input += "/100";
                    textBox1.Text += button.Text;
                    break;
            }
            
        }

        private void ButtonEquals_Click(object sender, EventArgs e)
        {
            Evaluate();
        }

        private void Evaluate()
        {
            if (!string.IsNullOrEmpty(input))
            {
                try
                {
                    object result = table.Compute(input, "");
                    textBox1.Text = result.ToString();
                    input = result.ToString();
                    isResultDisplay = true;
                }
                catch (Exception)
                {
                    textBox1.Text = "ERROR";
                    input = string.Empty;
                }

                isStart = true;
            }
        }

        private void ButtonClear_Click(object sender, EventArgs e)
        {
            Clear();
            textBox1.Text = "0";
        }

        private void Clear()
        {
            textBox1.Text = "";
            input = string.Empty;
            isResultDisplay = false;

            isStart = true;
        }

        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Developer:\nMuzakki Abdul Aziz\nMuhammad Aziz\nErik Anindiya");
        }
    }
}
