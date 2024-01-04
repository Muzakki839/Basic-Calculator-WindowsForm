using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace BasicCalculator
{
    public partial class Form1 : Form
    {
        DataTable table = new DataTable();
        string input = string.Empty;

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

            Button button = (Button)sender;
            switch (button.Text)
            {
                default:
                    input += button.Text;
                    textBox1.Text += button.Text;
                    break;
                case "(":
                    // Jika sebelumnya kurung buka tidak diikuti operator, tambahkan operator kali secara otomatis
                    if (!string.IsNullOrEmpty(input) && (char.IsDigit(input.Last()) || input.EndsWith(")")))
                    {
                        input += "*";
                        textBox1.Text += "*";
                    }

                    input += button.Text;
                    textBox1.Text += button.Text;
                    break;
            }

        }

        private void ButtonOperator_Click(object sender, EventArgs e)
        {
            isStart = false;

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
            // Check if there are open parentheses without corresponding closing parentheses
            int openParenthesisCount = input.Count(c => c == '(');
            int closeParenthesisCount = input.Count(c => c == ')');

            for (int i = 0; i < openParenthesisCount - closeParenthesisCount; i++)
            {
                input += ")";
                textBox1.Text += ")";
            }

            // Eval
            if (!string.IsNullOrEmpty(input))
            {
                try
                {
                    object result = table.Compute(input, "");
                    textBox1.Text = result.ToString();
                    input = result.ToString();
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

            isStart = true;
        }

        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Developer:\nMuzakki Abdul Aziz\nMuhammad Aziz\nErik Anindiya");
        }
    }
}
