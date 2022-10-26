namespace Publisher.Models;

public class MyMessage
{
    private string _sender;
    private string _message;

    public MyMessage(string sender, string message)
    {
        this._sender = sender;
        this._message = message;
    }
}