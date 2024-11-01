using System.Windows;
using WpfAddressverwaltung.Classes.EmployeeData.AddressData;

namespace WpfAddressverwaltung;

/// <summary>
/// A dialog window for adding a new address.
/// </summary>
public partial class AddressDialog : Window {
	/// <summary>
	/// The newly created address, or null if the dialog is cancelled.
	/// </summary>
	public Address? NewAddress { get; private set; }

	/// <summary>
	/// Initializes a new instance of the AddressDialog class.
	/// </summary>
	public AddressDialog() {
		// Initialize the dialog's components
		this.InitializeComponent();
	}

	/// <summary>
	/// Handles the Click event of the AddButton.
	/// </summary>
	/// <param name="sender">The source of the event.</param>
	/// <param name="e">The RoutedEventArgs instance containing the event data.</param>
	private void AddButton_Click(object sender, RoutedEventArgs e) {
		// Get the input values from the dialog's fields
		AddressTypeEnum addressType  = (AddressTypeEnum)this.AddressTypeComboBox.SelectedValue;
		string          street       = this.AddressStreetInput.Text;
		string          streetNumber = this.AddressStreetNumberInput.Text;
		string          postalCode   = this.AddressPostalInput.Text;
		string          city         = this.AddressCityInput.Text;

		// Check if any of the input fields are empty
		if (string.IsNullOrEmpty(street) ||
			string.IsNullOrEmpty(streetNumber) ||
			string.IsNullOrEmpty(postalCode) ||
			string.IsNullOrEmpty(city)) {
			// If any field is empty, do not create a new address
			return;
		}

		// Create a new address with the input values
		this.NewAddress = new Address(street, streetNumber, city, postalCode, addressType);

		// Set the dialog result to true and close the dialog
		this.DialogResult = true; // Setze Dialogergebnis auf true
		this.Close();             // Schließe das Fenster
	}

	/// <summary>
	/// Handles the Click event of the CancelButton.
	/// </summary>
	/// <param name="sender">The source of the event.</param>
	/// <param name="e">The RoutedEventArgs instance containing the event data.</param>
	private void CancelButton_Click(object sender, RoutedEventArgs e) {
		// Set the dialog result to false and close the dialog
		this.DialogResult = false;
		this.Close();
	}
}