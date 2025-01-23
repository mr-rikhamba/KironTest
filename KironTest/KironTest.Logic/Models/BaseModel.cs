namespace KironTest.Logic.Models;

public abstract class BaseModel
{
}

public class BaseResponseModel : BaseModel
{
    public bool IsSuccessful { get; set; } = true;
    public string ResponseMessage { get; set; }
}
public class BaseResponseModel<T> : BaseModel
{
    public bool IsSuccessful { get; set; } = true;
    public string ResponseMessage { get; set; }
    public T ResponseData { get; set; }
}