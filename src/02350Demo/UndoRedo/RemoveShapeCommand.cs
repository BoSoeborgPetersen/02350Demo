namespace _02350Demo.UndoRedo;

public record RemoveShapeCommand(ObservableCollection<ShapeViewModel> Shapes, ObservableCollection<LineViewModel> Lines, ShapeViewModel ShapeToRemove, List<LineViewModel> LinesToRemove) : IUndoRedoCommand
{
    public void Do() { LinesToRemove.ForEach(l => Lines.Remove(l)); Shapes.Remove(ShapeToRemove); }
    public void Undo() { Shapes.Add(ShapeToRemove); LinesToRemove.ForEach(Lines.Add); }
}
