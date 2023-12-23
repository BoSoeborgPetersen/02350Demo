namespace _02350Demo.UndoRedo;

public sealed class UndoRedoController
{
    readonly Stack<IUndoRedoCommand> undoStack = new();
    readonly Stack<IUndoRedoCommand> redoStack = new();

    public static UndoRedoController Instance { get; } = new();

    UndoRedoController() { }

    public void AddAndExecute(IUndoRedoCommand command)
    {
        undoStack.Push(command);
        redoStack.Clear();
        command.Do();
        SendUndoRedoChangedMessage();
    }

    public bool CanUndo() => undoStack.Any();

    public void Undo()
    {
        if (CanUndo())
        {
            var command = undoStack.Pop();
            redoStack.Push(command);
            command.Undo();
            SendUndoRedoChangedMessage();
        }
    }

    public bool CanRedo() => redoStack.Any();

    public void Redo()
    {
        if (CanRedo())
        {
            var command = redoStack.Pop();
            undoStack.Push(command);
            command.Do();
            SendUndoRedoChangedMessage();
        }
    }

    void SendUndoRedoChangedMessage() => WeakReferenceMessenger.Default.Send<UndoRedoChangedMessage>();
}