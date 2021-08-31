// ***********************************************************************
// Assembly         : WpfContactApp
// Author           : ashutosh.tutwani
// Created          : 08-25-2021
//
// Last Modified By : ashutosh.tutwani
// Last Modified On : 08-31-2021
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WpfContactApp.Model;

namespace WpfContactApp.Services
{
  /// <summary>
  /// Class UserService.
  /// </summary>
  public class UserService
  {
    #region Constants & Fields

    /// <summary>
    /// Store the Base URL
    /// </summary>
    private const string BaseURL = "http://localhost:5000/api/";

    #endregion

    /// <summary>
    /// Deletes the user.
    /// </summary>
    /// <param name="userId">The user identifier.</param>
    internal async Task<bool> DeleteUser(int userId)
    {
      using (var client = new HttpClient())
      {
        try
        {
          // set url
          var requestUri = $"user/{userId}";
          client.BaseAddress = new Uri(BaseURL);
          // get response
          var responseMessage = await client.DeleteAsync(requestUri);
          // is response is success, then return
          return responseMessage.IsSuccessStatusCode;
        }
        catch (Exception)
        {
          return false;
        }
      }
    }

    /// <summary>
    /// Updates the user.
    /// </summary>
    /// <param name="userDetail">The user detail.</param>
    internal async Task<bool> UpdateUser(UserDetail userDetail)
    {
      using (var client = new HttpClient())
      {
        try
        {
          // set url
          const string requestUri = "user/update";
          client.BaseAddress = new Uri(BaseURL);
          var json = JsonConvert.SerializeObject(userDetail);
          var content = new StringContent(json, Encoding.UTF8, "application/json");
          // get response
          var responseMessage = await client.PutAsync(requestUri, content);
          // is response is success, then return
          return responseMessage.IsSuccessStatusCode;
        }
        catch (Exception)
        {
          return false;
        }
      }
    }

    /// <summary>
    /// Inserts the user.
    /// </summary>
    /// <param name="userDetail">The user detail.</param>
    internal async Task<int?> InsertUser(UserDetail userDetail)
    {
      using (var client = new HttpClient())
      {
        try
        {
          // set url
          const string requestUri = "user/insert";
          client.BaseAddress = new Uri(BaseURL);
          var json = JsonConvert.SerializeObject(userDetail);
          var content = new StringContent(json, Encoding.UTF8, "application/json");
          // get response
          var responseMessage = await client.PostAsync(requestUri, content);
          // is response is success, then return
          if (!responseMessage.IsSuccessStatusCode)
          {
            return null;
          }

          var result = await responseMessage.Content.ReadAsStringAsync();
          if (int.TryParse(result, out var id))
          {
            userDetail.Id = id;
            return id;
          }

          return null;
        }
        catch (Exception)
        {
          return null;
        }
      }
    }



    /// <summary>
    /// Gets all the users.
    /// </summary>
    internal async Task<IEnumerable<UserDetail>> GetAllUsers()
    {
      using (var client = new HttpClient())
      {
        try
        {
          // set url
          const string requestUri = "user/getall";
          client.BaseAddress = new Uri(BaseURL);

          // get response
          var responseMessage = await client.GetAsync(requestUri);
          // is response is success, then return
          if (!responseMessage.IsSuccessStatusCode)
          {
            return null;
          }

          var result = await responseMessage.Content.ReadAsStringAsync();
          return JsonConvert.DeserializeObject<IEnumerable<UserDetail>>(result);
        }
        catch (Exception)
        {
          return null;
        }
      }
    }
  }
}