using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using WpfAddressverwaltung.Classes.DataSaving;
using WpfAddressverwaltung.Classes.EmployeeData;
using WpfAddressverwaltung.Classes.EmployeeData.AddressData;
using WpfAddressverwaltung.Classes.EmployeeData.PhoneData;

namespace WpfAddressverwaltung;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow {
	private ObservableCollection<Employee> _employees = null!;
	public  IEnumerable<PhoneTypeEnum>     EnumerablePhoneTypes => Enum.GetValues<PhoneTypeEnum>();

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
		this.Loaded += async (_, _) => {
			// Load employees from the repository asynchronously
			this._employees = await EmployeeRepository.LoadEmployeesAsync();

			// Subscribe to the CollectionChanged event to handle changes to the employees collection
			this._employees.CollectionChanged += this.Employees_CollectionChanged;

			// Set the DataContext to the employees collection
			this.DataContext = this._employees;
		};

		// Handle the Closing event to save employees asynchronously
		this.Closing += async (_, _) => {
			// Save employees asynchronously when the window is closing
			await EmployeeRepository.SaveEmployeesAsync(this._employees);
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
		// Initialize empty collections for phone numbers and addresses
		ObservableCollection<PhoneNumber> phoneNumbers = [];
		ObservableCollection<Address>     addresses    = [];

		// Generate a random id
		int id = 1;
		if (this._employees.Count > 0) {
			id = this._employees.Max(e => e.Id) + 1;
		}

		// Extract personal details from input fields
		string firstName = this.FirstNameInput.Text; // Get first name from input field
		string lastName  = this.LastNameInput.Text;  // Get last name from input field
		string position  = this.PositionInput.Text;  // Get position from input field

		string birthdayDay = this.DayInput.Text;
		if (birthdayDay.Length == 1) {
			birthdayDay = "0" + birthdayDay;
		}

		string birthdayMonth = this.MonthInput.Text;
		if (birthdayMonth.Length == 1) {
			birthdayMonth = "0" + birthdayMonth;
		}

		string birthdayYear = this.YearInput.Text;

		string birthday =
			$"{birthdayDay}." + $"{birthdayMonth}." +
			$"{birthdayYear}"; // Format birthday from input fields

		// Extract address details from input fields
		string street       = this.StreetInput.Text;       // Get street from input field
		string streetNumber = this.StreetNumberInput.Text; // Get street number from input field
		string city         = this.CityInput.Text;         // Get city from input field
		string postalCode   = this.PostalInput.Text;       // Get postal code from input field
		AddressTypeEnum
			addressType = (AddressTypeEnum)this.AddressTypeComboBox.SelectedValue; // Get address type from input field

		// Extract phone number details from input fields
		string        prefix    = this.PrefixInput.Text; // Get prefix from input field
		string        phone     = this.PhoneInput.Text;  // Get phone number from input field
		PhoneTypeEnum phoneType = (PhoneTypeEnum)this.PhoneTypeComboBox.SelectedValue;

		// Validate address input fields
		if (this.ValidateInput([street, streetNumber, city, postalCode])) {
			// Create a new Address instance and add it to the addresses collection
			addresses.Add(new Address(street, streetNumber, city, postalCode, addressType));
		}

		// Validate phone number input fields
		if (this.ValidateInput([prefix, phone])) {
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
		this.StreetInput.Text                  = ""; // Clear street input field
		this.StreetNumberInput.Text            = ""; // Clear street number input field
		this.CityInput.Text                    = ""; // Clear city input field
		this.PostalInput.Text                  = ""; // Clear postal code input field
		this.AddressTypeComboBox.SelectedIndex = 1;  // Clear address type input field

		// Clear phone number input fields
		this.PrefixInput.Text                = ""; // Clear prefix input field
		this.PhoneInput.Text                 = ""; // Clear phone number input field
		this.PhoneTypeComboBox.SelectedIndex = 1;  // Clear phone type input field
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
		EmployeeEditor editor = new(selectedRow, this);

		// Hide the DataGrid to make room for the Editor view
		this.EmployeeInfoGrid.Visibility = Visibility.Collapsed;
		this.NewEntryGrid.Visibility     = Visibility.Collapsed;

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
		// If an item is selected, make the EditButton/DeleteButton visible
		this.EditButton.Visibility = this.EmployeeInfoGrid.SelectedItem != null
			? Visibility.Visible
			:
			// If no item is selected, collapse the DeleteButton
			Visibility.Collapsed;

		this.DeleteButton.Visibility = this.EmployeeInfoGrid.SelectedItem != null
			? Visibility.Visible
			:
			// If no item is selected, collapse the DeleteButton
			Visibility.Collapsed;
	}

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

	private void DayInput_OnTextChanged(object sender, TextChangedEventArgs e) {
		if (this.DayInput.Text.Length > 1) {
			this.MonthInput.Focus();
		}
	}

	private void MonthInput_OnTextChanged(object sender, TextChangedEventArgs e) {
		if (this.MonthInput.Text.Length > 1) {
			this.YearInput.Focus();
		}
	}

	private void YearInput_OnTextChanged(object sender, TextChangedEventArgs e) {
		if (this.YearInput.Text.Length > 3) {
			this.StreetInput.Focus();
		}
	}

	private void DeleteButton_OnClick(object sender, RoutedEventArgs e) {
		var selectedRows = this.EmployeeInfoGrid.SelectedItems.Cast<Employee>().ToList();
		foreach (var employee in selectedRows) {
			this._employees.Remove(employee);
		}
	}

	private void SearchEmployees(string query) {
		// Wenn das Suchfeld leer ist, zeige alle Mitarbeiter an
		if (string.IsNullOrWhiteSpace(query)) {
			EmployeeInfoGrid.ItemsSource = _employees.ToList();
			return;
		}

		IEnumerable<Employee> filteredEmployees = _employees;

		// Überprüfen, ob die Suchanfrage den ID-Filter enthält
		if (query.StartsWith("#:")) {
			if (int.TryParse(query.Substring(2), out int id)) {
				filteredEmployees = _employees.Where(e => e.Id == id);
			}
		}

		// Überprüfen, ob die Suchanfrage den Addressen-Filter enthält
		else if (query.StartsWith("addr:")) {
			string addressQuery = query.Substring(5).ToLower();
			filteredEmployees = _employees.Where(e =>
				e.Address.Any(a => a.Street.ToLower().Contains(addressQuery) ||
								   a.City.ToLower().Contains(addressQuery) ||
								   a.PostalCode.ToLower().Contains(addressQuery)));
		}

		// Überprüfen, ob die Suchanfrage den Positions-Filter enthält
		else if (query.StartsWith("pos:")) {
			string positionQuery = query.Substring(4).ToLower();
			filteredEmployees = _employees.Where(e => e.Position.ToLower().Contains(positionQuery));
		}

		// Überprüfen, ob die Suchanfrage den Telefonnummern-Filter enthält
		else if (query.StartsWith("phone:")) {
			string phoneQuery = query.Substring(6).ToLower();
			filteredEmployees = _employees.Where(e => 
				e.Phone.Any(p => p.PhonePrefix.ToLower().Contains(phoneQuery) ||
								 p.PhoneSuffix.ToLower().Contains(phoneQuery)));
		}
		
		// Standard-String-Suche für Vorname und Nachname
		else {
			string nameQuery = query.ToLower();
			filteredEmployees = _employees.Where(e =>
				e.FirstName.ToLower().Contains(nameQuery) ||
				e.LastName.ToLower().Contains(nameQuery));
		}

		// Aktualisiere die ItemsSource des DataGrids
		EmployeeInfoGrid.ItemsSource = filteredEmployees.ToList();
	}

	private void SearchButton_Click(object sender, RoutedEventArgs e) {
		SearchEmployees(this.SearchInput.Text);
		this.SearchInput.Text = "";
		this.SearchButton.Content = "Reset";
	}

	private void SearchInput_OnTextChanged(object sender, TextChangedEventArgs e) {
		if (this.SearchInput.Text.Length > 0) {
			this.SearchButton.Content = "Suchen";
		}
		else {
			this.SearchButton.Content = "Reset";
		}
	}
}