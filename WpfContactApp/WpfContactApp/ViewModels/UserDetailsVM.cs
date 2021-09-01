// ***********************************************************************
// Assembly         : WpfContactApp
// Author           : ashutosh.tutwani
// Created          : 08-25-2021
//
// Last Modified By : ashutosh.tutwani
// Last Modified On : 08-31-2021
// ***********************************************************************

using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using Prism.Commands;
using WpfContactApp.Model;
using WpfContactApp.Services;
using Prism.Mvvm;

namespace WpfContactApp.ViewModels
{
  /// <summary>
  /// Class UserDetailsVM.
  /// Implements the <see cref="Prism.Mvvm.BindableBase" />
  /// </summary>
  /// <seealso cref="Prism.Mvvm.BindableBase" />
  public class UserDetailsVM : BindableBase
  {
    #region Fields

    /// <summary>
    /// Stores The user service
    /// </summary>
    private readonly UserService _userService;

    /// <summary>
    /// Stores All users
    /// </summary>
    private ObservableCollection<UserDetail> _allUsers = new ObservableCollection<UserDetail>();

    /// <summary>
    /// Stores The selected user
    /// </summary>
    private UserDetail _selectedUser;

    /// <summary>
    /// Stores The add new user command
    /// </summary>
    private DelegateCommand _addNewUserCommand;

    /// <summary>
    /// Stores The update number command
    /// </summary>
    private DelegateCommand _updateNumberCommand;

    /// <summary>
    /// Stores The update mail command
    /// </summary>
    private DelegateCommand _updateMailCommand;

    /// <summary>
    /// Stores The update user command
    /// </summary>
    private DelegateCommand _updateUserCommand;

    /// <summary>
    /// Stores The delete user command
    /// </summary>
    private DelegateCommand _deleteUserCommand;

    /// <summary>
    /// Stores The search command
    /// </summary>
    private DelegateCommand _searchCommand;

    /// <summary>
    /// Stores The search text
    /// </summary>
    private string _searchText;

    /// <summary>
    /// Stores The clear search command
    /// </summary>
    private DelegateCommand _clearSearchCommand;

    #endregion

    #region Ctor

    /// <summary>
    /// Initializes a new instance of the <see cref="UserDetailsVM"/> class.
    /// </summary>
    public UserDetailsVM()
    {
      this._userService = new UserService();
      this.GetAllUsers();
    }

    #endregion

    #region Properties

    /// <summary>
    /// Gets the add new user command.
    /// </summary>
    /// <value>The add new user command.</value>
    public DelegateCommand AddNewUserCommand =>
      this._addNewUserCommand ?? (this._addNewUserCommand = new DelegateCommand(AddNewUser));

    /// <summary>
    /// Gets the search command.
    /// </summary>
    /// <value>The search command.</value>
    public DelegateCommand SearchCommand => this._searchCommand ?? (this._searchCommand = new DelegateCommand(Search));

    /// <summary>
    /// Gets the clear search command.
    /// </summary>
    /// <value>The clear search command.</value>
    public DelegateCommand ClearSearchCommand =>
      this._clearSearchCommand ?? (this._clearSearchCommand = new DelegateCommand(ClearSearch));

    /// <summary>
    /// Gets the update user command.
    /// </summary>
    /// <value>The update user command.</value>
    public DelegateCommand UpdateUserCommand =>
      this._updateUserCommand ?? (this._updateUserCommand = new DelegateCommand(UpdateUser));

    /// <summary>
    /// Gets the update mail command.
    /// </summary>
    /// <value>The update mail command.</value>
    public DelegateCommand UpdateMailCommand =>
      this._updateMailCommand ?? (this._updateMailCommand = new DelegateCommand(UpdateMail));

    /// <summary>
    /// Gets the delete user command.
    /// </summary>
    /// <value>The delete user command.</value>
    public DelegateCommand DeleteUserCommand =>
      this._deleteUserCommand ?? (this._deleteUserCommand = new DelegateCommand(DeleteUser));

    /// <summary>
    /// Gets the update number command.
    /// </summary>
    /// <value>The update number command.</value>
    public DelegateCommand UpdateNumberCommand =>
      this._updateNumberCommand ?? (this._updateNumberCommand = new DelegateCommand(UpdateNumber));

    /// <summary>
    /// Gets or sets the selected user.
    /// </summary>
    /// <value>The selected user.</value>
    public UserDetail SelectedUser
    {
      get => _selectedUser;
      set
      {
        _selectedUser = value;

        if (value != null)
        {
          foreach (var user in AllUsers)
          {
            user.IsSelected = false;
          }

          _selectedUser.IsSelected = true;
        }

        this.RaisePropertyChanged();
      }
    }

    /// <summary>
    /// Gets or sets all users.
    /// </summary>
    /// <value>All users.</value>
    public ObservableCollection<UserDetail> AllUsers
    {
      get => _allUsers;
      set
      {
        _allUsers = value;
        this.RaisePropertyChanged();
      }
    }

    /// <summary>
    /// Gets or sets the search text.
    /// </summary>
    /// <value>The search text.</value>
    public string SearchText
    {
      get => _searchText;
      set
      {
        _searchText = value;
        this.RaisePropertyChanged();
      }
    }

    #endregion

    #region Methods

    /// <summary>
    /// Adds the new user.
    /// </summary>
    private void AddNewUser()
    {
      var view = new AddUserView()
      {
        Owner = Application.Current.MainWindow,
        WindowStartupLocation = WindowStartupLocation.CenterOwner,
        DataContext = new AddUserVM(new UserService(), AddUserToCollection)
      };

      view.Show();
    }

    /// <summary>
    /// Adds the user to collection.
    /// </summary>
    /// <param name="userDetail">The user detail.</param>
    internal void AddUserToCollection(UserDetail userDetail)
    {
      this.AllUsers.Add(userDetail);
      this.SelectedUser = userDetail;
    }

    /// <summary>
    /// Gets all users.
    /// </summary>
    private async void GetAllUsers()
    {
      var users = await _userService.GetAllUsers();
      if (users == null)
      {
        return;
      }

      this.AllUsers = new ObservableCollection<UserDetail>(users);
      this.SelectedUser = this.AllUsers.FirstOrDefault();

      if (this.SelectedUser == null) return;

      this.SelectedUser.SelectedEmail = this.SelectedUser.Emails.FirstOrDefault();
      this.SelectedUser.SelectedNumber = this.SelectedUser.PhoneNumbers.FirstOrDefault();
    }

    /// <summary>
    /// Deletes the user.
    /// </summary>
    private async void DeleteUser()
    {
      if (await _userService.DeleteUser(this.SelectedUser.Id))
      {
        MessageBox.Show("Selected user is deleted successfully", "Operation Successful", MessageBoxButton.OK,
          MessageBoxImage.Information);
      }
      else
      {
        MessageBox.Show("Selected user cannot be deleted. Please try again.", "Operation Failed", MessageBoxButton.OK,
          MessageBoxImage.Warning);
      }
    }

    /// <summary>
    /// Clears the search.
    /// </summary>
    private void ClearSearch()
    {
      this.GetAllUsers();
    }

    /// <summary>
    /// Searches this instance.
    /// </summary>
    private void Search()
    {
      this.AllUsers = new ObservableCollection<UserDetail>(this.AllUsers.Where(user =>
        user.FirstName.ToLowerInvariant() == this._searchText.ToLowerInvariant() ||
        user.LastName.ToLowerInvariant() == this._searchText.ToLowerInvariant() ||
        user.Name.ToLowerInvariant() == this._searchText.ToLowerInvariant()));
      this.SelectedUser = this.AllUsers.FirstOrDefault();
    }

    /// <summary>
    /// Updates the mail.
    /// </summary>
    private void UpdateMail()
    {
      if (string.IsNullOrEmpty(this.SelectedUser.EditedEmail))
      {
        return;
      }

      this.SelectedUser.Emails.Remove(this.SelectedUser.SelectedEmail);
      this.SelectedUser.Emails.Add(this.SelectedUser.EditedEmail);
      this.SelectedUser.SelectedEmail = this.SelectedUser.EditedEmail;
    }

    /// <summary>
    /// Updates the number.
    /// </summary>
    private void UpdateNumber()
    {
      if (string.IsNullOrEmpty(this.SelectedUser.EditedNumber))
      {
        return;
      }

      this.SelectedUser.PhoneNumbers.Remove(this.SelectedUser.SelectedNumber);
      this.SelectedUser.PhoneNumbers.Add(this.SelectedUser.EditedNumber);
      this.SelectedUser.SelectedNumber = this.SelectedUser.EditedNumber;
    }

    /// <summary>
    /// Updates the user.
    /// </summary>
    private async void UpdateUser()
    {
      if (string.IsNullOrEmpty(this._selectedUser.FirstName))
      {
        MessageBox.Show("First Name should not be empty. Please enter the first name.", "Operation Failed", MessageBoxButton.OK,
          MessageBoxImage.Warning);
        return;
      }

      if (string.IsNullOrEmpty(this._selectedUser.LastName))
      {
        MessageBox.Show("Last Name should not be empty. Please enter the last name.", "Operation Failed", MessageBoxButton.OK,
          MessageBoxImage.Warning);
        return;
      }

      if (await _userService.UpdateUser(this.SelectedUser))
      {
        MessageBox.Show("Selected user is updated successfully", "Operation Successful", MessageBoxButton.OK,
          MessageBoxImage.Information);
      }
      else
      {
        MessageBox.Show("Selected user cannot be updated. Please try again.", "Operation Failed", MessageBoxButton.OK,
          MessageBoxImage.Warning);
      }
    }

    #endregion
  }
}