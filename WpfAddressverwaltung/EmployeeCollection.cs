using System.Collections.Specialized;
using System.Diagnostics;
using WpfAddressverwaltung.Classes.DataSaving;

namespace WpfAddressverwaltung;

public partial class MainWindow {
	/// <summary>
	/// Handles the CollectionChanged event of the Employees collection.
	/// Saves the updated Employees collection to the repository asynchronously.
	/// </summary>
	/// <param name="sender">The sender of the event.</param>
	/// <param name="e">The event arguments.</param>
	private async void Employees_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e) {
		try {
			// Save the updated Employees collection to the repository
			await EmployeeRepository.SaveEmployeesAsync(this._employees);
		}
		catch (Exception exception) {
			// Log the exception
			Debug.WriteLine(exception);
		}
	}
}