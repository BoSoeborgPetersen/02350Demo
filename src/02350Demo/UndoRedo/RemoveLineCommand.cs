namespace _02350Demo.UndoRedo;

public record RemoveLineCommand(ObservableCollection<LineViewModel> Lines, LineViewModel LineToRemove) : IUndoRedoCommand
{
    public void Do() => Lines.Remove(LineToRemove);
    public void Undo() => Lines.Add(LineToRemove);
}
