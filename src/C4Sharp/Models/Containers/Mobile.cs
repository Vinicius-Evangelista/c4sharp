namespace C4Sharp.Models.Containers;

public record Mobile(string Alias, string Label, string Technology, string Description)
    : Container(Alias, Label, ContainerType.Mobile, Technology, Description);

public record Mobile<T>(string Technology, string Description)
    : Container<T>(ContainerType.Mobile, Technology, Description);