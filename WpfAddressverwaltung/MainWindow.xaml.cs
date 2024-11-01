using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using WpfAddressverwaltung.Classes.DataSaving;
using WpfAddressverwaltung.Classes.EmployeeData;

namespace WpfAddressverwaltung;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow {
	private ObservableCollection<Employee> _employees = null!;

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
	/// Handles the TextChanged event of the DayInput control.
	/// When the text length exceeds 1, it sets the focus to the MonthInput control.
	/// </summary>
	/// <param name="sender">The sender object.</param>
	/// <param name="e">The TextChangedEventArgs instance.</param>
	private void DayInput_OnTextChanged(object sender, TextChangedEventArgs e) {
		// Check if the text length exceeds 1
		if (this.DayInput.Text.Length > 1) {
			// Set the focus to the MonthInput control
			this.MonthInput.Focus();
		}
	}

	/// <summary>
	/// Handles the TextChanged event of the MonthInput control.
	/// When the text length exceeds 1, it sets the focus to the YearInput control.
	/// </summary>
	/// <param name="sender">The sender object.</param>
	/// <param name="e">The TextChangedEventArgs instance.</param>
	private void MonthInput_OnTextChanged(object sender, TextChangedEventArgs e) {
		// Check if the text length exceeds 1
		if (this.MonthInput.Text.Length > 1) {
			// Set the focus to the YearInput control
			this.YearInput.Focus();
		}
	}

	/// <summary>
	/// Handles the TextChanged event of the YearInput control.
	/// When the text length exceeds 3, it sets the focus to the StreetInput control.
	/// </summary>
	/// <param name="sender">The sender object.</param>
	/// <param name="e">The TextChangedEventArgs instance.</param>
	private void YearInput_OnTextChanged(object sender, TextChangedEventArgs e) {
		// Check if the text length exceeds 3
		if (this.YearInput.Text.Length > 3) {
			// Set the focus to the StreetInput control
			this.StreetInput.Focus();
		}
	}

	/// <summary>
	/// Handles the Click event of the DeleteButton control.
	/// Removes the selected employees from the _employees collection.
	/// </summary>
	/// <param name="sender">The sender object.</param>
	/// <param name="e">The RoutedEventArgs instance.</param>
	private void DeleteButton_OnClick(object sender, RoutedEventArgs e) {
		// Get the selected employees from the EmployeeInfoGrid
		List<Employee> selectedRows = this.EmployeeInfoGrid.SelectedItems.Cast<Employee>().ToList();

		// Iterate through the selected employees and remove them from the _employees collection
		foreach (Employee? employee in selectedRows) {
			this._employees.Remove(employee);
		}
	}
}