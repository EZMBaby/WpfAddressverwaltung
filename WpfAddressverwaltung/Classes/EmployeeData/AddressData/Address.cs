using System.ComponentModel;

namespace WpfAddressverwaltung.Classes.EmployeeData.AddressData;

/// <summary>
/// Represents an address with street, street number, city, postal code, and address type.
/// </summary>
public class Address : INotifyPropertyChanged
{
    /// <summary>
    /// Initializes a new instance of the Address class.
    /// </summary>
    /// <param name="street">The street name.</param>
    /// <param name="streetNumber">The street number.</param>
    /// <param name="city">The city name.</param>
    /// <param name="postalCode">The postal code.</param>
    /// <param name="addressType">The type of address (e.g., home, work, etc.).</param>
    public Address(string street, string streetNumber, string city, string postalCode, AddressTypeEnum addressType)
    {
        // Initialize the private fields with the provided values
        this._street = street;
        this._streetNumber = streetNumber;
        this._city = city;
        this._postalCode = postalCode;
        this._addressType = addressType;
    }

    // Private fields to store the address data
    private string _street;
    private string _streetNumber;
    private string _city;
    private string _postalCode;
    private AddressTypeEnum _addressType;

    /// <summary>
    /// Occurs when a property value changes.
    /// </summary>
    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>
    /// Gets or sets the street name.
    /// </summary>
    public string Street
    {
        get => this._street;
        set
        {
            // Update the private field and notify property changed
            this._street = value;
            this.OnPropertyChanged(nameof(this.Street));
        }
    }

    /// <summary>
    /// Gets or sets the street number.
    /// </summary>
    public string StreetNumber
    {
        get => this._streetNumber;
        set
        {
            // Update the private field and notify property changed
            this._streetNumber = value;
            this.OnPropertyChanged(nameof(this.StreetNumber));
        }
    }

    /// <summary>
    /// Gets or sets the city name.
    /// </summary>
    public string City
    {
        get => this._city;
        set
        {
            // Update the private field and notify property changed
            this._city = value;
            this.OnPropertyChanged(nameof(this.City));
        }
    }

    /// <summary>
    /// Gets or sets the postal code.
    /// </summary>
    public string PostalCode
    {
        get => this._postalCode;
        set
        {
            // Update the private field and notify property changed
            this._postalCode = value;
            this.OnPropertyChanged(nameof(this.PostalCode));
        }
    }

    /// <summary>
    /// Gets or sets the type of address.
    /// </summary>
    public AddressTypeEnum AddressType
    {
        get => this._addressType;
        set
        {
            // Update the private field and notify property changed
            this._addressType = value;
            this.OnPropertyChanged(nameof(this.AddressType));
        }
    }

    /// <summary>
    /// Raises the PropertyChanged event.
    /// </summary>
    /// <param name="propertyName">The name of the property that changed.</param>
    protected virtual void OnPropertyChanged(string propertyName)
    {
        // Invoke the PropertyChanged event handler
        this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}