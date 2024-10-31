﻿using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;
using WpfAddressverwaltung.Classes.DataSaving;
using WpfAddressverwaltung.Classes.EmployeeData;

namespace WpfAddressverwaltung;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window {
	private ObservableCollection<Employee> _employees;

	/// <summary>
	/// Initializes a new instance of the MainWindow class.
	/// Loads the employees from the repository asynchronously and assigns them to the _employees collection.
	/// Subscribes to the CollectionChanged event of the _employees collection.
	/// Sets the DataContext to the _employees collection.
	/// Ensures that employees are saved asynchronously when the window is closing.
	/// </summary>
	public MainWindow() {
		// Initialize the component
		this.InitializeComponent();

		// Handle the Loaded event to load employees from the repository
		this.Loaded += async (sender, args) => {
			// Load employees from the repository asynchronously
			this._employees = await EmployeeRepository.LoadEmployeesAsync();

			// Subscribe to the CollectionChanged event to handle changes to the employees collection
			this._employees.CollectionChanged += this.Employees_CollectionChanged;

			// Set the DataContext to the employees collection
			this.DataContext = this._employees;
		};

		// Handle the Closing event to save employees asynchronously
		this.Closing += async (sender, args) => {
			// Save employees asynchronously when the window is closing
			await EmployeeRepository.SaveEmployeesAsync(this._employees!);
		};
	}


	/// <summary>
	/// Handles the Click event of the SaveButton.
	/// Generates a random id and extracts the input data from the input fields.
	/// Creates a new Employee instance with the extracted data and adds it to the _employees collection.
	/// Clears the input fields.
	/// </summary>
	/// <param name="sender">The sender of the event.</param>
	/// <param name="e">The event arguments.</param>
	private void SaveButton_Click(object sender, RoutedEventArgs e) {
		// Create a new instance of the Random class to generate a random id
		Random random = new Random();

		// Initialize empty collections for phone numbers and addresses
		ObservableCollection<PhoneNumber> phoneNumbers = [];
		ObservableCollection<Address>     addresses    = [];

		// Generate a random id between 1 and 9999
		int id = random.Next(1, 9999);

		// Extract personal details from input fields
		string firstName = this.FirstNameInput.Text; // Get first name from input field
		string lastName  = this.LastNameInput.Text;  // Get last name from input field
		string position  = this.PositionInput.Text;  // Get position from input field
		string birthday =
			$"{this.DayInput.Text}." + $"{this.MonthInput.Text}." +
			$"{this.YearInput.Text}"; // Format birthday from input fields

		// Extract address details from input fields
		string street       = this.StreetInput.Text;       // Get street from input field
		string streetNumber = this.StreetNumberInput.Text; // Get street number from input field
		string city         = this.CityInput.Text;         // Get city from input field
		string postalCode   = this.PostalInput.Text;       // Get postal code from input field
		string addressType  = this.AddressTypeInput.Text;  // Get address type from input field

		// Extract phone number details from input fields
		string prefix    = this.PrefixInput.Text;    // Get prefix from input field
		string phone     = this.PhoneInput.Text;     // Get phone number from input field
		string phoneType = this.PhoneTypeInput.Text; // Get phone type from input field

		// Validate address input fields
		if (this.ValidateInput([street, streetNumber, city, postalCode, addressType])) {
			// Create a new Address instance and add it to the addresses collection
			addresses.Add(new Address(street, streetNumber, city, postalCode, addressType));
		}

		// Validate phone number input fields
		if (this.ValidateInput([prefix, phone, phoneType])) {
			// Create a new PhoneNumber instance and add it to the phoneNumbers collection
			phoneNumbers.Add(new PhoneNumber(phoneType, prefix, phone));
		}

		// Create a new Employee instance with the extracted data and add it to the _employees collection
		this._employees.Add(new Employee(firstName, lastName, position, birthday, addresses, phoneNumbers, id));

		// Clear all input fields
		this.ClearInputFields();
	}

	/// <summary>
	/// Validates an array of input strings to ensure none of them are null or empty.
	/// </summary>
	/// <param name="inputData">The array of input strings to validate.</param>
	/// <returns>True if all input strings are non-null and non-empty, false otherwise.</returns>
	private bool ValidateInput(string[] inputData) {
		// Use LINQ's All method to check if all input strings are non-null and non-empty
		return inputData.All(input => !String.IsNullOrEmpty(input));
	}

	/// <summary>
	/// Clears all input fields on the form.
	/// </summary>
	private void ClearInputFields() {
		// Clear personal details input fields
		this.FirstNameInput.Text = ""; // Clear first name input field
		this.LastNameInput.Text  = ""; // Clear last name input field
		this.PositionInput.Text  = ""; // Clear position input field
		this.DayInput.Text       = ""; // Clear day input field
		this.MonthInput.Text     = ""; // Clear month input field
		this.YearInput.Text      = ""; // Clear year input field

		// Clear address input fields
		this.StreetInput.Text       = ""; // Clear street input field
		this.StreetNumberInput.Text = ""; // Clear street number input field
		this.CityInput.Text         = ""; // Clear city input field
		this.PostalInput.Text       = ""; // Clear postal code input field
		this.AddressTypeInput.Text  = ""; // Clear address type input field

		// Clear phone number input fields
		this.PrefixInput.Text    = ""; // Clear prefix input field
		this.PhoneInput.Text     = ""; // Clear phone number input field
		this.PhoneTypeInput.Text = ""; // Clear phone type input field
	}

	/// <summary>
	/// Handles the Click event of the EditButton.
	/// Replaces the DataGrid with the Editor view for the selected Employee.
	/// </summary>
	/// <param name="sender">The sender of the event.</param>
	/// <param name="e">The event arguments.</param>
	private void EditButton_Click(object sender, RoutedEventArgs e) {
		// Get the selected Employee from the DataGrid
		Employee? selectedRow = (Employee)this.EmployeeInfoGrid.SelectedItem;

		// Create a new EmployeeEditor instance for the selected Employee
		EmployeeEditor editor = new EmployeeEditor(selectedRow, this);

		// Hide the DataGrid to make room for the Editor view
		this.EmployeeInfoGrid.Visibility = Visibility.Collapsed;

		// Position the Editor view in the MainGrid
		Grid.SetColumn(editor, 1);

		// Add the Editor view to the MainGrid
		this.MainGrid.Children.Add(editor);

		// Hide the EditButton to prevent multiple clicks
		this.EditButton.Visibility = Visibility.Collapsed;
	}

	/// <summary>
	/// Handles the SelectionChanged event of the EmployeeInfoGrid.
	/// Toggles the visibility of the EditButton based on the selection state of the grid.
	/// </summary>
	/// <param name="sender">The sender of the event.</param>
	/// <param name="e">The event arguments.</param>
	private void EmployeeInfoGrid_OnSelectionChanged(object sender, SelectionChangedEventArgs e) {
		// Check if an item is selected in the grid
		if (this.EmployeeInfoGrid.SelectedItem != null) {
			// If an item is selected, make the EditButton visible
			this.EditButton.Visibility = Visibility.Visible;
		}
		else {
			// If no item is selected, collapse the EditButton
			this.EditButton.Visibility = Visibility.Collapsed;
		}
	}

	/// <summary>
	/// Handles the CollectionChanged event of the Employees collection.
	/// Saves the updated Employees collection to the repository asynchronously.
	/// </summary>
	/// <param name="sender">The sender of the event.</param>
	/// <param name="e">The event arguments.</param>
	private async void Employees_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e) {
		// Save the updated Employees collection to the repository
		await EmployeeRepository.SaveEmployeesAsync(this._employees);
	}

	/// <summary>
	/// Adds a new employee to the collection.
	/// </summary>
	/// <param name="employee">The employee to add.</param>
	/// <returns>A completed task.</returns>
	private Task AddEmployee(Employee employee) {
		// Add the new employee to the collection, triggering the CollectionChanged event.
		this._employees.Add(employee);
		return Task.CompletedTask;
	}

	/// <summary>
	/// Removes an employee from the collection.
	/// </summary>
	/// <param name="employee">The employee to remove.</param>
	/// <returns>A completed task.</returns>
	private Task DeleteEmployee(Employee employee) {
		// Remove the employee from the collection, triggering the CollectionChanged event.
		this._employees.Remove(employee);
		return Task.CompletedTask;
	}

	/// <summary>
	/// Updates an employee in the collection by saving the entire collection to the repository.
	/// </summary>
	/// <param name="employee">The employee to update (not actually used in this implementation).</param>
	/// <returns>A task representing the asynchronous save operation.</returns>
	private async Task UpdateEmployee(Employee employee) {
		// Save the updated employees collection to the repository asynchronously.
		await EmployeeRepository.SaveEmployeesAsync(this._employees);
	}
}