namespace _02350Demo.ViewModel;

public partial class LineViewModel : ObservableObject
{
    [ObservableProperty]
    ShapeViewModel from;

    [ObservableProperty]
    ShapeViewModel to;
}
