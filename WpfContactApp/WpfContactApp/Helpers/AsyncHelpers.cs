// ***********************************************************************
// Assembly         : WpfContactApp
// Author           : ashutosh.tutwani
// Created          : 08-31-2021
//
// Last Modified By : ashutosh.tutwani
// Last Modified On : 08-31-2021
// ***********************************************************************

using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace WpfContactApp.Helpers
{
  /// <summary>
  /// Class AsyncHelpers.
  /// </summary>
  public class AsyncHelpers
  {
    #region Fields

    /// <summary>
    /// Stores The actions list
    /// </summary>
    private readonly ConcurrentDictionary<Action, CancellationToken> _actionsList = new ConcurrentDictionary<Action, CancellationToken>();

    #endregion

    #region Methods

    /// <summary>
    /// Adds the action.
    /// </summary>
    /// <param name="action">The action.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    public void AddAction(Action action, CancellationToken cancellationToken)
    {
      this._actionsList.TryAdd(action, cancellationToken);
    }

    /// <summary>
    /// Runs all tasks.
    /// </summary>
    /// <returns>Task.</returns>
    public async Task RunAllTasks()
    {
      while (this._actionsList.Any())
      {
        var action = this._actionsList.LastOrDefault();
        await this.InvokeAsync(action.Key, action.Value);
        this._actionsList.TryRemove(action.Key, out _);
      }
    }

    /// <summary>
    /// invoke as an asynchronous operation.
    /// </summary>
    /// <param name="actionToInvoke">The action to invoke.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>Task.</returns>
    public async Task InvokeAsync(Action actionToInvoke,
      CancellationToken cancellationToken)
    {
      if (cancellationToken.IsCancellationRequested)
      {
        return;
      }

      await Task.Run(actionToInvoke, cancellationToken);
    } // InvokeAsync

    #endregion
  }
}