namespace _02350Demo.ViewModel;

public class MainViewModel : ObservableRecipient
{
    private readonly UndoRedoController undoRedoController = UndoRedoController.Instance;

    private bool isAddingLine;
    private Shape addingLineFrom;
    private Point initialMousePosition;
    private Point initialShapePosition;
    public double ModeOpacity => isAddingLine ? 0.4 : 1.0;

    public ObservableCollection<Shape> Shapes { get; set; }
    public ObservableCollection<Line> Lines { get; set; }

    public RelayCommand UndoCommand { get; }
    public RelayCommand RedoCommand { get; }

    public RelayCommand AddShapeCommand { get; }
    public RelayCommand ShapesSelectionChangedCommand { get; }
    public RelayCommand<IList> RemoveShapeCommand { get; }
    public RelayCommand AddLineCommand { get; }
    public RelayCommand LinesSelectionChangedCommand { get; }
    public RelayCommand<IList> RemoveLinesCommand { get; }

    public RelayCommand<MouseButtonEventArgs> MouseDownShapeCommand { get; }
    public RelayCommand<MouseEventArgs> MouseMoveShapeCommand { get; }
    public RelayCommand<MouseButtonEventArgs> MouseUpShapeCommand { get; }

    public MainViewModel()
    {
        Shapes = new() {
            new Shape() { Position = new(30, 40), Size = new(80, 80) },
            new Shape() { Position = new(140, 230), Size = new(100, 100) }
        };
        Lines = new() {
            new Line() { From = Shapes[0], To = Shapes[1] }
        };

        UndoCommand = new(Undo, undoRedoController.CanUndo);
        RedoCommand = new(Redo, undoRedoController.CanRedo);

        AddShapeCommand = new(AddShape);
        ShapesSelectionChangedCommand = new(() => RemoveShapeCommand.NotifyCanExecuteChanged());
        RemoveShapeCommand = new(RemoveShape, (_shapes) => _shapes.Count == 1);
        AddLineCommand = new(AddLine);
        LinesSelectionChangedCommand = new(() => RemoveLinesCommand.NotifyCanExecuteChanged());
        RemoveLinesCommand = new(RemoveLines, (_edges) => _edges.Count >= 1);

        MouseDownShapeCommand = new(MouseDownShape);
        MouseMoveShapeCommand = new(MouseMoveShape);
        MouseUpShapeCommand = new(MouseUpShape);
    }

    private void Undo()
    {
        undoRedoController.Undo();
        UndoCommand.NotifyCanExecuteChanged();
        RedoCommand.NotifyCanExecuteChanged();
    }

    private void Redo()
    {
        undoRedoController.Redo();
        UndoCommand.NotifyCanExecuteChanged();
        RedoCommand.NotifyCanExecuteChanged();
    }

    private void AddShape()
    {
        undoRedoController.AddAndExecute(new AddShapeCommand(Shapes, new()));
        UndoCommand.NotifyCanExecuteChanged();
    }

    private void RemoveShape(IList _shapes)
    {
        var shapesToRemove = _shapes.Cast<Shape>().ToList();
        var linesToRemove = Lines.Where(x => shapesToRemove.Any(y => y.Number == x.From.Number || y.Number == x.To.Number)).ToList();
        undoRedoController.AddAndExecute(new RemoveShapesCommand(Shapes, Lines, shapesToRemove, linesToRemove));
        UndoCommand.NotifyCanExecuteChanged();
    }

    private void AddLine()
    {
        isAddingLine = true;
        OnPropertyChanged(nameof(ModeOpacity));
    }

    private void RemoveLines(IList _lines)
    {
        undoRedoController.AddAndExecute(new RemoveLinesCommand(Lines, _lines.Cast<Line>().ToList()));
        UndoCommand.NotifyCanExecuteChanged();
    }

    private void MouseDownShape(MouseButtonEventArgs e)
    {
        if (!isAddingLine)
        {
            var shape = TargetShape(e);
            var mousePosition = RelativeMousePosition(e);

            initialMousePosition = mousePosition;
            initialShapePosition = shape.Position;

            e.MouseDevice.Target.CaptureMouse();
        }
    }

    private void MouseMoveShape(MouseEventArgs e)
    {
        if (Mouse.Captured != null && !isAddingLine)
        {
            var shape = TargetShape(e);
            var mousePosition = RelativeMousePosition(e);

            shape.Position = initialShapePosition + (mousePosition - initialMousePosition);
        }
    }

    private void MouseUpShape(MouseButtonEventArgs e)
    {
        if (isAddingLine)
        {
            var shape = TargetShape(e);
            if (addingLineFrom == null) { addingLineFrom = shape; addingLineFrom.IsSelected = true; }
            else if (addingLineFrom.Number != shape.Number)
            {
                undoRedoController.AddAndExecute(new AddLineCommand(Lines, new() { From = addingLineFrom, To = shape }));
                UndoCommand.NotifyCanExecuteChanged();
                addingLineFrom.IsSelected = false;
                isAddingLine = false;
                addingLineFrom = null;
                OnPropertyChanged(nameof(ModeOpacity));
            }
        }
        else
        {
            var shape = TargetShape(e);
            var mousePosition = RelativeMousePosition(e);

            shape.Position = initialShapePosition;

            undoRedoController.AddAndExecute(new MoveShapeCommand(shape, new(mousePosition.X - initialMousePosition.X, mousePosition.Y - initialMousePosition.Y)));
            UndoCommand.NotifyCanExecuteChanged();

            e.MouseDevice.Target.ReleaseMouseCapture();
        }
    }

    private static Shape TargetShape(MouseEventArgs e)
    {
        var shapeVisualElement = (FrameworkElement)e.MouseDevice.Target;
        return (Shape)shapeVisualElement.DataContext;
    }

    private static Point RelativeMousePosition(MouseEventArgs e)
    {
        var shapeVisualElement = (FrameworkElement)e.MouseDevice.Target;
        var canvas = FindParentOfType<Canvas>(shapeVisualElement);
        return Mouse.GetPosition(canvas);
    }

    private static T FindParentOfType<T>(DependencyObject o)
    {
        dynamic parent = VisualTreeHelper.GetParent(o);
        return parent.GetType().IsAssignableFrom(typeof(T)) ? parent : FindParentOfType<T>(parent);
    }
}