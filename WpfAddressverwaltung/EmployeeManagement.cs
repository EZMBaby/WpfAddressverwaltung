using System.Collections.ObjectModel;
using System.Windows;
using WpfAddressverwaltung.Classes.EmployeeData;
using WpfAddressverwaltung.Classes.EmployeeData.AddressData;
using WpfAddressverwaltung.Classes.EmployeeData.PhoneData;

namespace WpfAddressverwaltung;

public partial class MainWindow {
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
			id = this._employees.Max(employee => employee.Id) + 1;
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
		this._employees.Add(
			new Employee(firstName, lastName, position, birthday, addresses, phoneNumbers, id)
		);

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
}