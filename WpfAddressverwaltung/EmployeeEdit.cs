using System.Windows;
using System.Windows.Controls;
using WpfAddressverwaltung.Classes.EmployeeData;

namespace WpfAddressverwaltung;

public partial class MainWindow {
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
}