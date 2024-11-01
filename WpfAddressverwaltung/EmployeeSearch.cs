using System.Windows;
using System.Windows.Controls;
using WpfAddressverwaltung.Classes.EmployeeData;

namespace WpfAddressverwaltung;

public partial class MainWindow {
	/// <summary>
	/// Searches for employees based on the provided query string.
	/// </summary>
	/// <param name="query">The search query string.</param>
	private void SearchEmployees(string query) {
		// Check if the search query is empty or null
		if (string.IsNullOrWhiteSpace(query)) {
			// If the query is empty, display all employees
			this.EmployeeInfoGrid.ItemsSource = this._employees.ToList();
			return;
		}

		// Initialize the filtered employees collection with all employees
		IEnumerable<Employee> filteredEmployees = this._employees;

		// Check if the query starts with the ID filter prefix
		if (query.StartsWith("#:")) {
			// Try to parse the ID from the query
			if (int.TryParse(query.AsSpan(2), out int id)) {
				// Filter employees by ID
				filteredEmployees = this._employees.Where(employee => employee.Id == id);
			}
		}

		// Check if the query starts with the address filter prefix
		else if (query.StartsWith("addr:")) {
			// Extract the address query from the query string
			string addressQuery = query[5..].ToLower();

			// Filter employees by address
			filteredEmployees = this._employees.Where(e =>
				e.Address.Any(address =>
					address.Street.Contains(addressQuery, StringComparison.CurrentCultureIgnoreCase) ||
					address.City.Contains(addressQuery, StringComparison.CurrentCultureIgnoreCase) ||
					address.PostalCode.Contains(addressQuery, StringComparison.CurrentCultureIgnoreCase) ||
					address.StreetNumber.Contains(addressQuery, StringComparison.CurrentCultureIgnoreCase)));
		}

		// Check if the query starts with the position filter prefix
		else if (query.StartsWith("pos:")) {
			// Extract the position query from the query string
			string positionQuery = query[4..].ToLower();

			// Filter employees by position
			filteredEmployees = this._employees.Where(employee =>
				employee.Position.Contains(positionQuery, StringComparison.CurrentCultureIgnoreCase));
		}

		// Check if the query starts with the phone number filter prefix
		else if (query.StartsWith("phone:")) {
			// Extract the phone number query from the query string
			string phoneQuery = query[6..].ToLower();

			// Filter employees by phone number
			filteredEmployees = this._employees.Where(employee =>
				employee.Phone.Any(phone =>
					phone.PhonePrefix.Contains(phoneQuery, StringComparison.CurrentCultureIgnoreCase) ||
					phone.PhoneSuffix.Contains(phoneQuery, StringComparison.CurrentCultureIgnoreCase) ||
					(phone.PhonePrefix.ToLower() + phone.PhoneSuffix.ToLower()).Contains(phoneQuery)));
		}

		// If none of the above filters match, perform a standard string search on first and last name
		else {
			// Extract the name query from the query string
			string nameQuery = query.ToLower();

			// Filter employees by first and last name
			filteredEmployees = this._employees.Where(employee =>
				employee.FirstName.Contains(nameQuery, StringComparison.CurrentCultureIgnoreCase) ||
				employee.LastName.Contains(nameQuery, StringComparison.CurrentCultureIgnoreCase));
		}

		// Update the ItemsSource of the DataGrid with the filtered employees
		this.EmployeeInfoGrid.ItemsSource = filteredEmployees.ToList();
	}


	/// <summary>
	/// Handles the Click event of the SearchButton.
	/// Triggers the search functionality and resets the search input field.
	/// </summary>
	/// <param name="sender">The sender of the event.</param>
	/// <param name="e">The event arguments.</param>
	private void SearchButton_Click(object sender, RoutedEventArgs e) {
		// Call the SearchEmployees method with the current search input text
		this.SearchEmployees(this.SearchInput.Text);

		// Clear the search input field
		this.SearchInput.Text = "";

		// Reset the SearchButton content to "Reset"
		this.SearchButton.Content = "Reset";
	}

	/// <summary>
	/// Handles the TextChanged event of the SearchInput control.
	/// Updates the content of the SearchButton based on the text length of the SearchInput.
	/// </summary>
	/// <param name="sender">The sender of the event.</param>
	/// <param name="e">The event arguments.</param>
	private void SearchInput_OnTextChanged(object sender, TextChangedEventArgs e) {
		// Update the SearchButton content based on the SearchInput text length
		// If the text length is greater than 0, set the content to "Suchen", otherwise set it to "Reset"
		this.SearchButton.Content = this.SearchInput.Text.Length > 0 ? "Suchen" : "Reset";
	}
}