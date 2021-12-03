using System.Collections.Generic;

namespace TimeLog.Api.Documentation.Models.RestDocumentationHelpers.Core;

/// <summary>
///     Rest path structure for swagger 2.0 format.
/// </summary>
public class RestPath
{
    #region Constructor

    public RestPath(string name, IReadOnlyList<RestAction> actions)
    {
        Name = name;
        Actions = actions;
    }

    #endregion

    #region Variables

    public string Name { get; }

    public IReadOnlyList<RestAction> Actions { get; }

    #endregion
}