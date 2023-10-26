namespace _02350Demo.Command;

public class UndoRedoController
{
    private readonly Stack<IUndoRedoCommand> undoStack = new();
    private readonly Stack<IUndoRedoCommand> redoStack = new();

    public static UndoRedoController Instance { get; } = new();

    private UndoRedoController() { }

    public void AddAndExecute(IUndoRedoCommand command)
    {
        undoStack.Push(command);
        redoStack.Clear();
        command.Do();
    }

    public bool CanUndo() => undoStack.Any();

    public void Undo()
    {
        if (undoStack.Any())
        {
            var command = undoStack.Pop();
            redoStack.Push(command);
            command.Undo();
        }
    }

    public bool CanRedo() => redoStack.Any();

    public void Redo()
    {
        if (redoStack.Any())
        {
            var command = redoStack.Pop();
            undoStack.Push(command);
            command.Do();
        }
    }
}