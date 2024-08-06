namespace ChatWithSignalR.Exceptions;

public class UserNotExistsException:Exception
{
    public UserNotExistsException():base("User With This UserName DOesnot Exists"){}
}