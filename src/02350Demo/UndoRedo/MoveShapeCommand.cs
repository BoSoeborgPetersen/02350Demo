namespace _02350Demo.UndoRedo;

public record MoveShapeCommand(ShapeViewModel Shape, Vector Offset) : IUndoRedoCommand
{
    public void Do() => Shape.Position += Offset;
    public void Undo() => Shape.Position -= Offset;
}
