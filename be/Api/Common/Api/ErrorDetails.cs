using Newtonsoft.Json;

namespace Api.Common.Api;

public class ErrorDetails
{
    public int StatusCode { get; set; }
    public string Message { get; set; }
    public string Exception { get; set; }
    public string ReadFriendlyException { get; set; }
    public override string ToString()
    {
        return JsonConvert.SerializeObject(this);
    }
}
