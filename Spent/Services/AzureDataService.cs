using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;
using Xamarin.Forms;

[assembly: Dependency(typeof(Spent.AzureDataService))]
namespace Spent
{
	public class AzureDataService : IDataService
	{
		private bool _isInitialized;
        private IMobileServiceSyncTable<Expense> _expensesTable;

		public MobileServiceClient MobileService { get; set; }

		public AzureDataService()
		{
			MobileService = new MobileServiceClient(AppSettings.AzureMobileApp, null);
		}

		async Task Initialize()
		{
			if (_isInitialized)
				return;

			var store = new MobileServiceSQLiteStore(AppSettings.DatabaseName);
			store.DefineTable<Expense>();
			await MobileService.SyncContext.InitializeAsync(store, new MobileServiceSyncHandler());
            _expensesTable = MobileService.GetSyncTable<Expense>();

            _isInitialized = true;
		}

		public async Task AddExpenseAsync(Expense ex)
		{
			await Initialize();

			await _expensesTable.InsertAsync(ex);
			await SyncExpenses();
		}

		public async Task<IEnumerable<Expense>> GetExpensesAsync()
		{
			await Initialize();

			await SyncExpenses();

			return await _expensesTable.ToEnumerableAsync();
		}

		async Task SyncExpenses()
		{
			try
			{
				await MobileService.SyncContext.PushAsync();
				await _expensesTable.PullAsync($"all{typeof(Expense).Name}", _expensesTable.CreateQuery());
			}
			catch (Exception)
			{
				System.Diagnostics.Debug.WriteLine(
                    "An error syncing occurred. That is OK, as we have offline sync.");
			}
		}
	}
}