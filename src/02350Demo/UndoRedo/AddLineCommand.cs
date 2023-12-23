namespace _02350Demo.UndoRedo;

public record AddLineCommand(ObservableCollection<LineViewModel> Lines, LineViewModel Line) : IUndoRedoCommand
{
    public void Do() => Lines.Add(Line);
    public void Undo() => Lines.Remove(Line);
}
