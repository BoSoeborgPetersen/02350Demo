using _02350Demo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02350Demo.Command
{
    // Undo/Redo command for moving a Shape.
    public class MoveShapeCommand : IUndoRedoCommand
    {
        // Regions can be used to make code foldable (minus/plus sign to the left).
        #region Fields

        // The 'shape' field holds an existing shape, 
        //  and the reference points to the same object, 
        //  as one of the objects in the MainViewModels 'Shapes' ObservableCollection.
        // This shape is moved by changing its coordinates (X and Y), 
        //  and if undone the coordinates are changed back to the original coordinates.
        private Shape shape;

        // The 'beforeX' field holds the X coordinate of the shape before it is moved.
        private double beforeX;
        // The 'beforeY' field holds the Y coordinate of the shape after it is moved.
        private double beforeY;
        // The 'afterX' field holds the X coordinate of the shape before it is moved.
        private double afterX;
        // The 'afterY' field holds the Y coordinate of the shape after it is moved.
        private double afterY;

        #endregion

        #region Constructor

        // For changing the current state of the diagram.
        public MoveShapeCommand(Shape _shape, double _beforeX, double _beforeY, double _afterX, double _afterY) 
        {
            shape = _shape;
            beforeX = _beforeX;
            beforeY = _beforeY;
            afterX = _afterX;
            afterY = _afterY;
        }

        #endregion

        #region Methods

        // For doing and redoing the command.
        public void Execute()
        {
            shape.CanvasCenterX = afterX;
            shape.CanvasCenterY = afterY;
        }

        // For undoing the command.
        public void UnExecute()
        {
            shape.CanvasCenterX = beforeX;
            shape.CanvasCenterY = beforeY;
        }

        #endregion
    }
}
