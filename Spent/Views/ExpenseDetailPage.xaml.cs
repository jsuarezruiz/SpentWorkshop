using Xamarin.Forms;

namespace Spent
{
    public partial class ExpenseDetailPage : ContentPage
	{
		public Expense Expense { get; set; }

		public ExpenseDetailPage(Expense expense)
		{
			InitializeComponent();

			Expense = expense;
			BindingContext = this;
        }

        private void OnCrashButtonClicked(object sender, System.EventArgs e)
        {
            throw new System.Exception("Testing Analytics!");
        }
    }
}