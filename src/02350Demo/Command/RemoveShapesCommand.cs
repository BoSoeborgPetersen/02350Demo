namespace _02350Demo.Command;

public record RemoveShapesCommand(ObservableCollection<Shape> Shapes, ObservableCollection<Line> Lines, List<Shape> ShapesToRemove, List<Line> LinesToRemove) : IUndoRedoCommand
{
    public void Do() { LinesToRemove.ForEach(x => Lines.Remove(x)); ShapesToRemove.ForEach(x => Shapes.Remove(x)); }
    public void Undo() { ShapesToRemove.ForEach(x => Shapes.Add(x)); LinesToRemove.ForEach(x => Lines.Add(x)); }
}
