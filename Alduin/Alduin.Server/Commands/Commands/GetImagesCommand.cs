using Alduin.Server.Commands;

public class GetImagesCommand
{
    public GetImagesVariables newVariables { get; set; }
    public BaseCommands newBaseCommand { get; set; }
}

public class GetImagesVariables
{
    public string imagePath { get; set; }
}

