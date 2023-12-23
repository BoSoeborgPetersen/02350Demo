namespace _02350Demo.ViewModel;

public partial class MainViewModel : ObservableRecipient, IRecipient<UndoRedoChangedMessage>
{
    readonly MouseService mouse = MouseService.Instance;
    readonly UndoRedoController undoRedo = UndoRedoController.Instance;

    public ObservableCollection<ShapeViewModel> Shapes { get; set; }
    public ObservableCollection<LineViewModel> Lines { get; set; }

    public MainViewModel()
    {
        Shapes = [
            new() { Position = new(30, 40), Size = new(80, 80) },
            new() { Position = new(230, 340), Size = new(100, 100) },
            new() { Position = new(330, 40), Size = new(90, 90) }
        ];
        Lines = [
            new() { From = Shapes[0], To = Shapes[1] },
            new() { From = Shapes[1], To = Shapes[2] }
        ];
        mouse.Lines = Lines;
        IsActive = true;
    }

    [RelayCommand(CanExecute = nameof(CanUndo))]
    void Undo() => undoRedo.Undo();
    bool CanUndo() => undoRedo.CanUndo();

    [RelayCommand(CanExecute = nameof(CanRedo))]
    void Redo() => undoRedo.Redo();
    bool CanRedo() => undoRedo.CanRedo();

    [RelayCommand]
    void AddShape() => undoRedo.AddAndExecute(new AddShapeCommand(Shapes, new()));

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(RemoveShapeCommand))]
    ShapeViewModel selectedShape;

    [RelayCommand(CanExecute = nameof(CanRemoveShape))]
    void RemoveShape()
    {
        var linesToRemove = Lines.Where(l => SelectedShape.Number == l.From.Number || SelectedShape.Number == l.To.Number).ToList();
        undoRedo.AddAndExecute(new RemoveShapeCommand(Shapes, Lines, SelectedShape, linesToRemove));
    }
    bool CanRemoveShape() => SelectedShape != null;

    [RelayCommand]
    void AddLine() => mouse.IsAddingLine = true;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(RemoveLineCommand))]
    LineViewModel selectedLine;

    [RelayCommand(CanExecute = nameof(CanRemoveLine))]
    void RemoveLine() => undoRedo.AddAndExecute(new RemoveLineCommand(Lines, SelectedLine));
    bool CanRemoveLine() => SelectedLine != null;

    public void Receive(UndoRedoChangedMessage message) { UndoCommand.NotifyCanExecuteChanged(); RedoCommand.NotifyCanExecuteChanged(); }
}