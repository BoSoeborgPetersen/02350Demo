namespace _02350Demo.Command;

public record MoveShapeCommand(Shape Shape, Vector Offset) : IUndoRedoCommand
{
    public void Do() => Shape.Position += Offset;
    public void Undo() => Shape.Position -= Offset;
}
