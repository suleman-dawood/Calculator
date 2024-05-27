

using System.Diagnostics;

namespace Calculator
{
    public partial class MainPage : ContentPage
    {

        string currentText = "";

        public MainPage()
        {
            InitializeComponent();
            OnClear(this, null);
        }

        void OnClear(object sender, EventArgs e)
        {
            this.ResultText.Text = "";
            currentText = string.Empty;
        }

        void OnCalculate(object sender, EventArgs e)
        {
            
            double answer = Calculator.Calculate(currentText);
            Debug.WriteLine(answer);
            this.CalculationText.Text = $"{answer}";
            this.ResultText.Text = "";
            currentText = string.Empty;
            
        }

        void OnSelection(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string buttonPressed = button.Text;
            currentText += buttonPressed;

            this.ResultText.Text += buttonPressed;
        }

    }
}
