// Import the System.ComponentModel namespace to use the INotifyPropertyChanged interface

using System.ComponentModel;

// Define the namespace for the PhoneNumber class
namespace WpfAddressverwaltung.Classes.EmployeeData.PhoneData;

/// <summary>
/// Represents a phone number with properties for phone type, prefix, and suffix.
/// </summary>
public class PhoneNumber : INotifyPropertyChanged {
	/// <summary>
	/// Initializes a new instance of the PhoneNumber class.
	/// </summary>
	/// <param name="phoneType">The type of phone number (e.g., home, work, mobile).</param>
	/// <param name="phonePrefix">The prefix of the phone number.</param>
	/// <param name="phoneSuffix">The suffix of the phone number.</param>
	public PhoneNumber(PhoneTypeEnum phoneType, string phonePrefix, string phoneSuffix) {
		// Initialize the private fields with the provided values
		this._phoneType   = phoneType;
		this._phonePrefix = phonePrefix;
		this._phoneSuffix = phoneSuffix;
	}

	// Private fields to store the phone number properties
	private PhoneTypeEnum _phoneType;
	private string        _phonePrefix;
	private string        _phoneSuffix;

	// Event handler for property changes
	public event PropertyChangedEventHandler? PropertyChanged;

	/// <summary>
	/// Gets or sets the phone type.
	/// </summary>
	public PhoneTypeEnum PhoneType {
		get => this._phoneType;
		set {
			// Update the private field and notify of property change
			this._phoneType = value;
			this.OnPropertyChanged(nameof(this.PhoneType));
		}
	}

	/// <summary>
	/// Gets or sets the phone prefix.
	/// </summary>
	public string PhonePrefix {
		get => this._phonePrefix;
		set {
			// Update the private field and notify of property change
			this._phonePrefix = value;
			this.OnPropertyChanged(nameof(this.PhonePrefix));
		}
	}

	/// <summary>
	/// Gets or sets the phone suffix.
	/// </summary>
	public string PhoneSuffix {
		get => this._phoneSuffix;
		set {
			// Update the private field and notify of property change
			this._phoneSuffix = value;
			this.OnPropertyChanged(nameof(this.PhoneSuffix));
		}
	}

	/// <summary>
	/// Notifies of a property change.
	/// </summary>
	/// <param name="propertyName">The name of the property that changed.</param>
	protected void OnPropertyChanged(string propertyName) {
		// Invoke the PropertyChanged event
		this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
}