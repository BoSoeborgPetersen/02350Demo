namespace _02350Demo.UndoRedo;

public record AddShapeCommand(ObservableCollection<ShapeViewModel> Shapes, ShapeViewModel Shape) : IUndoRedoCommand
{
    public void Do() => Shapes.Add(Shape);
    public void Undo() => Shapes.Remove(Shape);
}
