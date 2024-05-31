
namespace Calculator
{
    public partial class MainPage : ContentPage
    {

        string currentText = "";
        string previousAnswer = "";

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
            string answer = Calculator.Calculate(this.ResultText.Text);
            this.CalculationText.Text = answer;
            this.ResultText.Text = "";
            currentText = string.Empty;
            previousAnswer = answer;
        }

        void OnSelection(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string buttonPressed = button.Text;
            currentText += buttonPressed;

            this.ResultText.Text += buttonPressed;
        }

        void OnPrevious(object sender, EventArgs e)
        {
            this.ResultText.Text += previousAnswer;
        }

        void OnRemoval(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.ResultText.Text)) 
            {
                this.ResultText.Text = this.ResultText.Text.Substring(0, this.ResultText.Text.Length - 1); 
            }
        }

    }
}
