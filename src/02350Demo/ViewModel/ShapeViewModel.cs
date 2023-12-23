namespace _02350Demo.ViewModel;

public partial class ShapeViewModel : ObservableRecipient, IRecipient<IsAddingLineMessage>
{
    readonly MouseService mouse = MouseService.Instance;

    static int counter = 0;

    public int Number { get; } = ++counter;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(CanvasCenter))]
    Point position = new(200, 200);

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(Center))]
    [NotifyPropertyChangedFor(nameof(CanvasCenter))]
    Size size = new(100, 100);

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(SelectedColor))]
    bool isSelected;

    public Vector Center => (Vector)Size / 2;
    public Point CanvasCenter => Position + Center;
    public Brush SelectedColor => IsSelected ? Brushes.Red : Brushes.Yellow;
    public double ModeOpacity => mouse.IsAddingLine ? 0.4 : 1.0;

    public ShapeViewModel() { IsActive = true; }

    [RelayCommand]
    void MouseDown(MouseButtonEventArgs e) => mouse.Down(this, e);
    [RelayCommand]
    void MouseMove(MouseEventArgs e) => mouse.Move(this, e);
    [RelayCommand]
    void MouseUp(MouseButtonEventArgs e) => mouse.Up(this, e);

    public void Receive(IsAddingLineMessage message) => OnPropertyChanged(nameof(ModeOpacity));
}
