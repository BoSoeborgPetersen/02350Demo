namespace _02350Demo.Model;

public partial class Shape : ObservableObject
{
    private static int counter = 0;

    public int Number { get; } = ++counter;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(CanvasCenter))]
    private Point position = new(200, 200);

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(Center))]
    [NotifyPropertyChangedFor(nameof(CanvasCenter))]
    private Size size = new(100, 100);

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(SelectedColor))]
    private bool isSelected;

    public Vector Center => new(Size.Width / 2, Size.Height / 2);
    public Point CanvasCenter => Position + Center;
    public Brush SelectedColor => IsSelected ? Brushes.Red : Brushes.Yellow;

    public override string ToString() => Number.ToString();
}
