namespace ChatWithSignalR.Exceptions;

public class UserNotAddException:Exception
{
    public UserNotAddException() : base("User Can't Be Added"){}
}