using System.Windows;
using WpfAddressverwaltung.Classes.EmployeeData;

namespace WpfAddressverwaltung;

/// <summary>
/// Represents a dialog for entering a new phone number.
/// </summary>
public partial class PhoneNumberDialog {
	
	/// <summary>
	/// Gets or sets the newly entered phone number.
	/// </summary>
	public PhoneNumber? NewPhoneNumber { get; private set; }

	/// <summary>
	/// Initializes a new instance of the PhoneNumberDialog class.
	/// </summary>
	public PhoneNumberDialog() {
		// Initialize the dialog's UI components
		this.InitializeComponent();
	}

	/// <summary>
	/// Handles the Click event of the AddButton.
	/// Creates a new PhoneNumber instance from the input fields and closes the dialog.
	/// </summary>
	/// <param name="sender">The sender object.</param>
	/// <param name="e">The RoutedEventArgs instance.</param>
	private void AddButton_Click(object sender, RoutedEventArgs e) {
		// Get the input values from the dialog's fields
		string phoneNumberType = this.PhoneTypeInput.Text;
		string phoneNumberPrefix = this.PhonePrefixInput.Text;
		string phoneNumberSuffix = this.PhoneSuffixInput.Text;

		// Check if any of the input fields are empty
		if (string.IsNullOrEmpty(phoneNumberType) || 
			string.IsNullOrEmpty(phoneNumberPrefix) ||
			string.IsNullOrEmpty(phoneNumberSuffix)) 
		{
			// If any field is empty, do not create a new phone number
			return;
		}
		
		// Create a new PhoneNumber instance from the input values
		this.NewPhoneNumber = new PhoneNumber(phoneNumberType, phoneNumberPrefix, phoneNumberSuffix);
		
		// Set the dialog result to true and close the dialog
		this.DialogResult = true; // Setze Dialogergebnis auf true
		this.Close();               // Schließe das Fenster
	}

	/// <summary>
	/// Handles the Click event of the CancelButton.
	/// Closes the dialog without creating a new phone number.
	/// </summary>
	/// <param name="sender">The sender object.</param>
	/// <param name="e">The RoutedEventArgs instance.</param>
	private void CancelButton_Click(object sender, RoutedEventArgs e) {
		// Set the dialog result to false and close the dialog
		this.DialogResult = false;
		this.Close();
	}
}