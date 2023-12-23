namespace _02350Demo.Service;

public sealed class MouseService
{
    readonly UndoRedoController undoRedoController = UndoRedoController.Instance;

    public static MouseService Instance { get; } = new();

    MouseService() { }

    public ObservableCollection<LineViewModel> Lines;
    bool isAddingLine;
    public bool IsAddingLine { get { return isAddingLine; } set { isAddingLine = value; WeakReferenceMessenger.Default.Send<IsAddingLineMessage>(); } }
    ShapeViewModel addingLineFrom;
    Point initialShapePosition;
    Point initialMousePosition;

    public void Down(ShapeViewModel shape, MouseButtonEventArgs e)
    {
        if (!IsAddingLine)
        {
            initialShapePosition = shape.Position;
            initialMousePosition = MousePosition(shape.Position, e);

            e.MouseDevice.Target.CaptureMouse();
        }
    }

    public void Move(ShapeViewModel shape, MouseEventArgs e)
    {
        if (Mouse.Captured != null && !IsAddingLine)
        {
            shape.Position = initialShapePosition + (MousePosition(shape.Position, e) - initialMousePosition);
        }
    }

    public void Up(ShapeViewModel shape, MouseButtonEventArgs e)
    {
        if (!IsAddingLine)
        {
            var mousePosition = MousePosition(shape.Position, e);
            shape.Position = initialShapePosition;

            undoRedoController.AddAndExecute(new MoveShapeCommand(shape, mousePosition - initialMousePosition));

            e.MouseDevice.Target.ReleaseMouseCapture();
        }
        else
        {
            if (addingLineFrom == null) { addingLineFrom = shape; addingLineFrom.IsSelected = true; }
            else if (addingLineFrom.Number != shape.Number)
            {
                undoRedoController.AddAndExecute(new AddLineCommand(Lines, new() { From = addingLineFrom, To = shape }));
                addingLineFrom.IsSelected = false;
                IsAddingLine = false;
                addingLineFrom = null;
            }
        }
    }

    static Point MousePosition(Point offset, MouseEventArgs e) => offset + (Vector)Mouse.GetPosition(e.MouseDevice.Target);
}
