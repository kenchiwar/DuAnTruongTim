namespace DuAnTruongTim.Models;

public class ResultApi
{
    public bool result { get; set; }
    public string message { get; set; }
    public ResultApi() { }
    public ResultApi(bool result, string message)
    {
        this.result = result;
        this.message = message;
    }
    public ResultApi(bool result) { this.result = result; }

}