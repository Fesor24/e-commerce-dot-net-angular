namespace API.Errors
{
    public class ApiResponse
    {
        public ApiResponse(int statusCode, string message = null)
        {
            StatusCode= statusCode;
            Message = message ??= DefaultErrorMessageForStatusCode(statusCode);
        }

        

        public int StatusCode { get; set; }
        public string Message { get; set; }

        private string DefaultErrorMessageForStatusCode(int statusCode)
        {
            return statusCode switch
            {
                400 => "You have made a bad request",
                401 => "You are not authorised",
                404 => "Reource not found",
                500 => "Bumped into an internal server error",
                _ => null

            };
        }
    }
}
