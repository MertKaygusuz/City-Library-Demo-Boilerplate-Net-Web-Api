using System;
using System.Collections.Generic;

namespace CityLibraryInfrastructure.ExceptionHandling.Dtos;

[Serializable]
public class ErrorDto
{
    public ErrorDto(int status = 400)
    {
        Status = status;
    }

    /// <summary>
    /// Create new instance and set the errors to "CustomError" key in Errors dictionary
    /// </summary>
    public ErrorDto(IEnumerable<string> errors, int status = 400)
    {
        Errors.Add("CustomError", errors);
        Status = status;
    }

    /// <summary>
    /// Create new instance and set only one error text to "CustomError" key in Errors dictionary
    /// </summary>
    public ErrorDto(string errorText, int status = 400)
    {
        Errors.Add("CustomError", new List<string>() {errorText});
        Status = status;
    }
    
    public int Status { get; set; }

    public string InternalErrorMessage { get; set; }

    public string InternalStackTrace { get; set; }

    public string InternalSource { get; set; }

    public Dictionary<string, IEnumerable<string>> Errors { get; set; } = new();
}