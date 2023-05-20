using System;

namespace NewFindACouple
{
    public partial class Form1 : Form
    {
        private string previousButtonTag = string.Empty;
        private Button previousButton;

        private void Button_Click(object sender, EventArgs eventArgs)
        {
            var button = (Button)sender;
            if (previousButtonTag != string.Empty && previousButtonTag != "-")
            {
                if (button.Tag == null || previousButton.Tag == null)
                {
                    throw new NullReferenceException();
                }

                if (previousButtonTag == button.Tag.ToString())
                {
                    previousButton.Text = previousButton.Tag.ToString();
                    button.Text = button.Tag.ToString();
                    previousButton.Tag = "-";
                    button.Tag = "-";
                }
            }
            previousButtonTag = string.Copy(button.Tag.ToString());
        } 

        public void AddGrid(int size)
        {

            int[] arrayForNumbers = new int[size * size];

            for (int i = 0; i < size * size; i++)
            {
                arrayForNumbers[i] = i;
            }

            for (int i = 0; i < size; i++)
            {
                var random = new Random();
                var firstIndex = random.Next(0, size * size);
                var secondIndex = random.Next(0, size * size);
                if (firstIndex != secondIndex)
                {
                    var copy = arrayForNumbers[firstIndex];
                    arrayForNumbers[firstIndex] = arrayForNumbers[secondIndex];
                    arrayForNumbers[secondIndex] = copy;
                }
            }

            int k = 0;
            int top = 10;
            int left = 10;

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; ++j)
                {
                    Button button = new Button();
                    button.Left = left;
                    button.Top = top;
                    button.Location = new Point(left, top);

                    this.Controls.Add(button);
                    top += button.Height + 2;
                    button.Name = "btn" + i + '.' + j;
                    button.Tag = arrayForNumbers[k].ToString();
                    left += 100;
                    ++k;
                    button.Click += Button_Click;
                    
                }

                top += 40;
                left = 10;
            }
        }

        public Form1(int size)
        {
            InitializeComponent();
            AddGrid(size);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}