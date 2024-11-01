using System.Windows;
using System.Windows.Controls;
using WpfAddressverwaltung.Classes.EmployeeData;
using WpfAddressverwaltung.Classes.EmployeeData.AddressData;
using WpfAddressverwaltung.Classes.EmployeeData.PhoneData;

namespace WpfAddressverwaltung;

/// <summary>
/// A partial class representing the EmployeeEditor control.
/// </summary>
public partial class EmployeeEditor {
	// Reference to the parent MainWindow instance.
	private MainWindow _parentWindow;

	/// <summary>
	/// Initializes a new instance of the EmployeeEditor class.
	/// </summary>
	/// <param name="employee">The employee to be edited.</param>
	/// <param name="parentWindow">The parent MainWindow instance.</param>
	public EmployeeEditor(Employee employee, MainWindow parentWindow) {
		InitializeComponent();
		this.DataContext   = employee; // Set the DataContext to the given employee.
		this._parentWindow = parentWindow;
	}

	/// <summary>
	/// Handles the AddAddress button click event.
	/// </summary>
	/// <param name="sender">The sender object.</param>
	/// <param name="e">The RoutedEventArgs instance.</param>
	private void AddAddress_Click(object sender, RoutedEventArgs e) {
		// Create a new AddressDialog instance.
		AddressDialog addressDialog = new();

		// Show the dialog and check if it was successfully closed.
		if (addressDialog.ShowDialog() == true) {
			// Get the new address from the dialog.
			Address? newAddress = addressDialog.NewAddress;

			// Get the current employee from the DataContext.
			Employee? employee = (Employee)this.DataContext;

			// Add the new address to the employee's address collection if it's not null.
			if (newAddress != null) employee.Address.Add(newAddress);
		}
	}

	/// <summary>
	/// Handles the AddPhone button click event.
	/// </summary>
	/// <param name="sender">The sender object.</param>
	/// <param name="e">The RoutedEventArgs instance.</param>
	private void AddPhone_Click(object sender, RoutedEventArgs e) {
		// Create a new PhoneNumberDialog instance.
		PhoneNumberDialog phoneDialog = new();

		// Show the dialog and check if it was successfully closed.
		if (phoneDialog.ShowDialog() == true) {
			// Get the new phone number from the dialog.
			PhoneNumber? newPhone = phoneDialog.NewPhoneNumber;

			// Get the current employee from the DataContext.
			Employee? employee = (Employee)this.DataContext;

			// Add the new phone number to the employee's phone collection if it's not null.
			if (newPhone != null) employee.Phone.Add(newPhone);
		}
	}

	/// <summary>
	/// Handles the SaveButton click event.
	/// </summary>
	/// <param name="sender">The sender object.</param>
	/// <param name="e">The RoutedEventArgs instance.</param>
	private void SaveButton_Click(object sender, RoutedEventArgs e) {
		// Collapse the EmployeeEditor control.
		this.Visibility = Visibility.Collapsed;

		// Show the EmployeeInfoGrid in the parent MainWindow.
		this._parentWindow.EmployeeInfoGrid.Visibility = Visibility.Visible;
		this._parentWindow.NewEntryGrid.Visibility     = Visibility.Visible;

		// Clear the selected item in the EmployeeInfoGrid.
		this._parentWindow.EmployeeInfoGrid.SelectedItem = null;
	}

	/// <summary>
	/// Handles the DeletePhone button click event.
	/// </summary>
	/// <param name="sender">The sender object.</param>
	/// <param name="e">The RoutedEventArgs instance.</param>
	private void DeletePhone_Click(object sender, RoutedEventArgs e) {
		// Check if the sender is a Button with a PhoneNumber Tag.
		if (sender is Button { Tag: PhoneNumber phoneNumber }) {
			// Get the current employee from the DataContext.
			Employee? employee = (Employee)this.DataContext;

			// Remove the phone number from the employee's phone collection.
			employee.Phone.Remove(phoneNumber);
		}
	}

	/// <summary>
	/// Handles the DeleteAddress button click event.
	/// </summary>
	/// <param name="sender">The sender object.</param>
	/// <param name="e">The RoutedEventArgs instance.</param>
	private void DeleteAddress_Click(object sender, RoutedEventArgs e) {
		// Check if the sender is a Button with an Address Tag.
		if (sender is Button { Tag: Address address }) {
			// Get the current employee from the DataContext.
			Employee? employee = (Employee)this.DataContext;

			// Remove the address from the employee's address collection.
			employee.Address.Remove(address);
		}
	}
}