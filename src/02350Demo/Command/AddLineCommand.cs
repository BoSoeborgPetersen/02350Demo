namespace _02350Demo.Command;

public record AddLineCommand(ObservableCollection<Line> Lines, Line Line) : IUndoRedoCommand
{
    public void Do() => Lines.Add(Line);
    public void Undo() => Lines.Remove(Line);
}
