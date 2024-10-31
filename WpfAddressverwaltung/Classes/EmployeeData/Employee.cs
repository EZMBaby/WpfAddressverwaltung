using System.Collections.ObjectModel;
using System.ComponentModel;
using WpfAddressverwaltung.Classes.EmployeeData.PhoneData;

namespace WpfAddressverwaltung.Classes.EmployeeData;

/// <summary>
/// Represents an employee with properties for first name, last name, position, birthday, address, and phone number.
/// </summary>
public class Employee : INotifyPropertyChanged
{
    // Private fields to store the employee's properties
    private string _firstName;
    private string _lastName;
    private string _position;
    private string _birthday;
    private ObservableCollection<Address> _address;
    private ObservableCollection<PhoneNumber> _phone;

    /// <summary>
    /// Initializes a new instance of the Employee class.
    /// </summary>
    /// <param name="firstName">The employee's first name.</param>
    /// <param name="lastName">The employee's last name.</param>
    /// <param name="position">The employee's position.</param>
    /// <param name="birthday">The employee's birthday.</param>
    /// <param name="address">The employee's address.</param>
    /// <param name="phone">The employee's phone number.</param>
    /// <param name="id">The employee's ID.</param>
    public Employee(string firstName, string lastName, string position, string birthday,
                    ObservableCollection<Address> address, ObservableCollection<PhoneNumber> phone, int id)
    {
        // Initialize the private fields with the provided values
        this._firstName = firstName;
        this._lastName = lastName;
        this._position = position;
        this._birthday = birthday;
        this._address = address;
        this._phone = phone;
        this.Id = id;
    }

    // Event handler for property changes
    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>
    /// Gets or sets the employee's ID.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the employee's first name.
    /// </summary>
    public string FirstName
    {
        get => this._firstName;
        set
        {
            // Update the private field and notify of property change
            this._firstName = value;
            this.OnPropertyChanged(nameof(this.FirstName));
        }
    }

    /// <summary>
    /// Gets or sets the employee's last name.
    /// </summary>
    public string LastName
    {
        get => this._lastName;
        set
        {
            // Update the private field and notify of property change
            this._lastName = value;
            this.OnPropertyChanged(nameof(this.LastName));
        }
    }

    /// <summary>
    /// Gets or sets the employee's position.
    /// </summary>
    public string Position
    {
        get => this._position;
        set
        {
            // Update the private field and notify of property change
            this._position = value;
            this.OnPropertyChanged(nameof(this.Position));
        }
    }

    /// <summary>
    /// Gets or sets the employee's birthday.
    /// </summary>
    public string Birthday
    {
        get => this._birthday;
        set
        {
            // Update the private field and notify of property change
            this._birthday = value;
            this.OnPropertyChanged(nameof(this.Birthday));
        }
    }

    /// <summary>
    /// Gets or sets the employee's address.
    /// </summary>
    public ObservableCollection<Address> Address
    {
        get => this._address;
        set
        {
            // Update the private field and notify of property change
            this._address = value;
            this.OnPropertyChanged(nameof(this.Address));
        }
    }

    /// <summary>
    /// Gets or sets the employee's phone number.
    /// </summary>
    public ObservableCollection<PhoneNumber> Phone
    {
        get => this._phone;
        set
        {
            // Update the private field and notify of property change
            this._phone = value;
            this.OnPropertyChanged(nameof(this.Phone));
        }
    }

    /// <summary>
    /// Notifies of a property change.
    /// </summary>
    /// <param name="propertyName">The name of the property that changed.</param>
    protected void OnPropertyChanged(string propertyName)
    {
        // Invoke the PropertyChanged event
        this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}