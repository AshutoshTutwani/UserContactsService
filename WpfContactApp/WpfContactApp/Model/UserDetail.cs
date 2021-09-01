// ***********************************************************************
// Assembly         : WpfContactApp
// Author           : ashutosh.tutwani
// Created          : 08-24-2021
//
// Last Modified By : ashutosh.tutwani
// Last Modified On : 08-31-2021
// ***********************************************************************

using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;
using Prism.Mvvm;

namespace WpfContactApp.Model
{
  /// <summary>
  /// Class UserDetail.
  /// Implements the <see cref="Prism.Mvvm.BindableBase" />
  /// </summary>
  /// <seealso cref="Prism.Mvvm.BindableBase" />
  public class UserDetail : BindableBase
  {
    #region Fields

    /// <summary>
    /// Stores The is selected
    /// </summary>
    private bool _isSelected;

    /// <summary>
    /// Stores The selected email
    /// </summary>
    private string _selectedEmail;

    /// <summary>
    /// Stores The selected number
    /// </summary>
    private string _selectedNumber;

    /// <summary>
    /// Stores The phone numbers
    /// </summary>
    private ObservableCollection<string> _phoneNumbers = new ObservableCollection<string>();

    /// <summary>
    /// Stores The edited email
    /// </summary>
    private string _editedEmail;

    /// <summary>
    /// Stores The edited number
    /// </summary>
    private string _editedNumber;

    /// <summary>
    /// Stores The emails
    /// </summary>
    private ObservableCollection<string> _emails = new ObservableCollection<string>();

    /// <summary>
    /// Stores The date
    /// </summary>
    private DateTime? _date;

    /// <summary>
    /// Stores The first name
    /// </summary>
    private string _firstName;

    /// <summary>
    /// Stores The last name
    /// </summary>
    private string _lastName;

    private string _dateOfBirth;

    #endregion

    #region Properties

    /// <summary>
    /// Gets or sets the identifier.
    /// </summary>
    /// <value>The identifier.</value>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the first name.
    /// </summary>
    /// <value>The first name.</value>
    public string FirstName
    {
      get => _firstName;
      set
      {
        _firstName = value;
        this.RaisePropertyChanged();
        this.RaisePropertyChanged(nameof(this.Name));
      }
    }

    /// <summary>
    /// Gets or sets the last name.
    /// </summary>
    /// <value>The last name.</value>
    public string LastName
    {
      get => _lastName;
      set
      {
        _lastName = value;
        this.RaisePropertyChanged();
        this.RaisePropertyChanged(nameof(this.Name));
      }
    }

    /// <summary>
    /// Gets or sets the date.
    /// </summary>
    /// <value>The date.</value>
    [JsonIgnore]
    public DateTime? Date
    {
      get => _date;
      set
      {
        _date = value;
        this.RaisePropertyChanged();
        if (this._date.HasValue)
        {
          this.DateOfBirth = this._date.Value.ToString("yyyy-MM-dd");
          this.RaisePropertyChanged(nameof(this.DateOfBirth));
        }
      }
    }

    /// <summary>
    /// Gets or sets the date of birth.
    /// </summary>
    /// <value>The date of birth.</value>
    public string DateOfBirth
    {
      get => _dateOfBirth;
      set
      {
        _dateOfBirth = value;
        if (!string.IsNullOrEmpty(value))
        {
          _date = DateTime.ParseExact(value, "yyyy-MM-dd", CultureInfo.InvariantCulture);
          this.RaisePropertyChanged(nameof(this.Date));
        }
      }
    }

    /// <summary>
    /// Gets or sets the emails.
    /// </summary>
    /// <value>The emails.</value>
    public ObservableCollection<string> Emails
    {
      get => _emails ?? new ObservableCollection<string>();
      set
      {
        _emails = value;
        this.RaisePropertyChanged();
      }
    }

    /// <summary>
    /// Gets or sets the phone numbers.
    /// </summary>
    /// <value>The phone numbers.</value>
    public ObservableCollection<string> PhoneNumbers
    {
      get => _phoneNumbers ?? new ObservableCollection<string>();
      set
      {
        _phoneNumbers = value;
        this.RaisePropertyChanged();
      }
    }

    /// <summary>
    /// Gets the name.
    /// </summary>
    /// <value>The name.</value>
    [JsonIgnore]
    public string Name => this.FirstName + " " + this.LastName;

    /// <summary>
    /// Gets or sets a value indicating whether this instance is selected.
    /// </summary>
    /// <value><c>true</c> if this instance is selected; otherwise, <c>false</c>.</value>
    [JsonIgnore]
    public bool IsSelected
    {
      get => _isSelected;
      set
      {
        _isSelected = value;
        this.RaisePropertyChanged();
      }
    }

    /// <summary>
    /// Gets or sets the selected email.
    /// </summary>
    /// <value>The selected email.</value>
    [JsonIgnore]
    public string SelectedEmail
    {
      get => _selectedEmail;
      set
      {
        _selectedEmail = value;
        this.RaisePropertyChanged();
      }
    }

    /// <summary>
    /// Gets or sets the edited email.
    /// </summary>
    /// <value>The edited email.</value>
    [JsonIgnore]
    public string EditedEmail
    {
      get => _editedEmail;
      set
      {
        _editedEmail = value;
        this.RaisePropertyChanged();
      }
    }

    /// <summary>
    /// Gets or sets the edited number.
    /// </summary>
    /// <value>The edited number.</value>
    [JsonIgnore]
    public string EditedNumber
    {
      get => _editedNumber;
      set
      {
        _editedNumber = value;
        this.RaisePropertyChanged();
      }
    }

    /// <summary>
    /// Gets or sets the selected number.
    /// </summary>
    /// <value>The selected number.</value>
    [JsonIgnore]
    public string SelectedNumber
    {
      get => _selectedNumber;
      set
      {
        _selectedNumber = value;
        this.RaisePropertyChanged();
      }
    }

    #endregion
  }
}