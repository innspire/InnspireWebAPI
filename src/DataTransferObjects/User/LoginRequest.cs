namespace InnspireWebAPI.DataTransferObjects.User;

public record LoginRequest(string UserName, string Password)
{
}

public record LoginResponse(bool Success, LoginSuccessResponse? Result)
{
}

public record LoginSuccessResponse(string Username, string FirstName, string LastName, string Token)
{
}