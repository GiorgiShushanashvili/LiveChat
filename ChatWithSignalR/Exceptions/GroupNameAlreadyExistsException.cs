namespace ChatWithSignalR.Exceptions;

public class GroupNameAlreadyExistsException:Exception
{
    public GroupNameAlreadyExistsException() : base("This Group Already Exists"){}
}