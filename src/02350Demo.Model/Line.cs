namespace _02350Demo.Model;

public partial class Line : ObservableObject
{
    [ObservableProperty]
    private Shape from;

    [ObservableProperty]
    private Shape to;
}
