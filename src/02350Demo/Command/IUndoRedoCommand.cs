namespace _02350Demo.Command;

public interface IUndoRedoCommand
{
    void Do();
    void Undo();
}
