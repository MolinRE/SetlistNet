using System;
using System.Text.Json.Serialization;

namespace SetlistNet.Models.Abstract;

/// <summary>
/// This is an abstract class, that represents a set of items, returned by API
/// </summary>
public abstract class ApiArrayResult
{
    /// <summary>
    /// Gets or sets the total amount of items matching the query
    /// </summary>
    [JsonPropertyName("total")]
    public int Total { get; set; }

    /// <summary>
    /// Gets or sets current page
    /// </summary>
    [JsonPropertyName("page")]
    public int Page { get; set; }

    /// <summary>
    /// Gets or sets the amount of items you get per page
    /// </summary>
    [JsonPropertyName("itemsPerPage")]
    public int ItemsPerPage { get; set; }

    /// <summary>
    /// Gets or sets the property "type" of an object
    /// </summary>
    [JsonPropertyName("type")]
    public string? ApiType { get; set; }

    /// <summary>
    /// Gets the total amount of pages returned by API
    /// </summary>
    public int TotalPages
    {
        get
        {
            if (ItemsPerPage == 0)
            {
                return 0;
            }

            if (ItemsPerPage > Total)
            {
                return 1;
            }

            return (int)Math.Ceiling((double)Total / ItemsPerPage);
        }
    }
}