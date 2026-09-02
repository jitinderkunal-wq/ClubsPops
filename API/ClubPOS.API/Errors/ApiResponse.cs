namespace ClubPOS.API.Errors;

public class ApiResponse
{
    public ApiResponse(int statusCode, string? message = null)
    {
        StatusCode = statusCode;
        Message = message ?? GetDefaultMessageForStatusCode(statusCode);
    }

    public int StatusCode { get; set; }
    public string? Message { get; set; }

    private string GetDefaultMessageForStatusCode(int statusCode)
    {
        return statusCode switch
        {
            200 => "Done",
            400 => "You have made a bad request",
            401 => "You are not Authorized",
            403 => "You do not have permission to access this resource",
            404 => "Requested resource was not found",
            500
              => "Errors occured, contact administrator",
            _ => "",
        };
    }
}