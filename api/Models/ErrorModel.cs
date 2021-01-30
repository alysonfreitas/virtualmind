using Microsoft.Extensions.Logging;

namespace API.Models
{
    public class ErrorModel
    {
        public bool Error { get { return true; } }
        public string Message { get; set; }

        public ErrorModel(ILogger logger, string message)
        {
            logger.LogError(message);
            Message = message;
        }
    }
}