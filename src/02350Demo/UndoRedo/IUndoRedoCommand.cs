namespace _02350Demo.UndoRedo;

public interface IUndoRedoCommand
{
    void Do();
    void Undo();
}
