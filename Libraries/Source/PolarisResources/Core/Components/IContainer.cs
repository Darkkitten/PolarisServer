#region Usings ...

using System;
using System.Collections.Generic;

#endregion

/// <summary>
/// </summary>
public interface IContainer
{
    #region Public Methods and Operators

    /// <summary>
    /// </summary>
    /// <param name="serviceType">
    /// </param>
    /// <returns>
    /// </returns>
    IEnumerable<object> GetAllInstances(Type serviceType);

    /// <summary>
    /// </summary>
    /// <param name="serviceType">
    /// </param>
    /// <param name="key">
    /// </param>
    /// <returns>
    /// </returns>
    object GetInstance(Type serviceType, string key = null);

    /// <summary>
    /// </summary>
    /// <param name="key">
    /// </param>
    /// <typeparam name="T">
    /// </typeparam>
    /// <returns>
    /// </returns>
    T GetInstance<T>(string key = null);

    #endregion
}
