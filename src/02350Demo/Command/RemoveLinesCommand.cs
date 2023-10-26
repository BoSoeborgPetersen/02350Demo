namespace _02350Demo.Command;

public record RemoveLinesCommand(ObservableCollection<Line> Lines, List<Line> LinesToRemove) : IUndoRedoCommand
{
    public void Do() => LinesToRemove.ForEach(x => Lines.Remove(x));
    public void Undo() => LinesToRemove.ForEach(x => Lines.Add(x));
}
