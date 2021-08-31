// ***********************************************************************
// Assembly         : WpfContactApp
// Author           : ashutosh.tutwani
// Created          : 08-26-2021
//
// Last Modified By : ashutosh.tutwani
// Last Modified On : 08-31-2021
// ***********************************************************************

using System;
using System.Linq;
using System.Windows;
using Prism.Commands;
using Prism.Mvvm;
using WpfContactApp.Model;
using WpfContactApp.Services;

namespace WpfContactApp.ViewModels
{
  /// <summary>
  /// Class AddUserVM.
  /// Implements the <see cref="Prism.Mvvm.BindableBase" />
  /// </summary>
  /// <seealso cref="Prism.Mvvm.BindableBase" />
  public class AddUserVM : BindableBase
  {
    #region Fields

    /// <summary>
    /// Stores The user detail
    /// </summary>
    private UserDetail _userDetail;

    /// <summary>
    /// Stores The add number command
    /// </summary>
    private DelegateCommand _addNumberCommand;

    /// <summary>
    /// Stores The add mail command
    /// </summary>
    private DelegateCommand _addMailCommand;

    /// <summary>
    /// Stores The save command
    /// </summary>
    private DelegateCommand _saveCommand;

    /// <summary>
    /// Stores The user service
    /// </summary>
    private readonly UserService _userService;

    /// <summary>
    /// Stores The add user to collection
    /// </summary>
    private readonly Action<UserDetail> _addUserToCollection;

    #endregion

    #region Ctor

    /// <summary>
    /// Initializes a new instance of the <see cref="AddUserVM"/> class.
    /// </summary>
    /// <param name="userService">The user service.</param>
    /// <param name="addUserToCollection">The add user to collection.</param>
    public AddUserVM(UserService userService, Action<UserDetail> addUserToCollection)
    {
      this.UserDetail = new UserDetail();
      this._userService = userService;
      this._addUserToCollection = addUserToCollection;
    }

    #endregion

    #region Properties

    /// <summary>
    /// Gets or sets the user detail.
    /// </summary>
    /// <value>The user detail.</value>
    public UserDetail UserDetail
    {
      get => _userDetail;
      set
      {
        _userDetail = value;
        this.RaisePropertyChanged();
      }
    }

    /// <summary>
    /// Gets the add number command.
    /// </summary>
    /// <value>The add number command.</value>
    public DelegateCommand AddNumberCommand => this._addNumberCommand ?? (this._addNumberCommand = new DelegateCommand(AddNumber));

    /// <summary>
    /// Gets the add mail command.
    /// </summary>
    /// <value>The add mail command.</value>
    public DelegateCommand AddMailCommand => this._addMailCommand ?? (this._addMailCommand = new DelegateCommand(AddMail));

    /// <summary>
    /// Gets the save command.
    /// </summary>
    /// <value>The save command.</value>
    public DelegateCommand SaveCommand => this._saveCommand ?? (this._saveCommand = new DelegateCommand(AddNewUser));

    #endregion

    #region Methods

    /// <summary>
    /// Adds the mail.
    /// </summary>
    private void AddMail()
    {
      this._userDetail.Emails.Add(this._userDetail.EditedEmail);
      this._userDetail.SelectedEmail = this._userDetail.Emails.LastOrDefault();
    }

    /// <summary>
    /// Adds the new user.
    /// </summary>
    private async void AddNewUser()
    {
      if (string.IsNullOrEmpty(this._userDetail.FirstName))
      {
        MessageBox.Show("First Name should not be empty. Please enter the first name.", "Operation Failed", MessageBoxButton.OK,
          MessageBoxImage.Warning);
        return;
      }

      if (string.IsNullOrEmpty(this._userDetail.LastName))
      {
        MessageBox.Show("Last Name should not be empty. Please enter the last name.", "Operation Failed", MessageBoxButton.OK,
          MessageBoxImage.Warning);
        return;
      }

      // insert the new user by through API
      var id = await _userService.InsertUser(this._userDetail);
      if (id != null)
      {
        this._addUserToCollection.Invoke(this._userDetail);
        MessageBox.Show("User is added successfully", "Operation Successful", MessageBoxButton.OK, MessageBoxImage.Information);
      }
      else
      {
        MessageBox.Show("User cannot be added. Please try again.", "Operation Failed", MessageBoxButton.OK,
          MessageBoxImage.Warning);
      }
    }

    /// <summary>
    /// Adds the number.
    /// </summary>
    private void AddNumber()
    {
      this._userDetail.PhoneNumbers.Add(this._userDetail.EditedNumber);
      this._userDetail.SelectedNumber = this._userDetail.PhoneNumbers.LastOrDefault();
    }

    #endregion
  }
}