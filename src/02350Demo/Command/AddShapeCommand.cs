namespace _02350Demo.Command;

public record AddShapeCommand(ObservableCollection<Shape> Shapes, Shape Shape) : IUndoRedoCommand
{
    public void Do() => Shapes.Add(Shape);
    public void Undo() => Shapes.Remove(Shape);
}
