using CommunityToolkit.Mvvm.ComponentModel;
using System.Windows.Media;

namespace _02350Demo.Model
{
    // The Shape class descripes a shape with a position (X and Y), and a size (Width and Height).
    public class Shape : ObservableObject
    {
        // For a description of the Getter/Setter Property syntax ("{ get { ... } set { ... } }") see the Line class.
        // The static integer counter field is used to set the integer Number property to a unique number for each Shape object.
        private static int counter = 0;

        // The Number integer property holds a unique integer for each Shape object to identify them in the View (GUI) layer.
        // The "{ get; }" syntax describes that a private field 
        //  and default getter method should be generated.
        public int Number { get; }

        private double x = 200;
        // The reason no string is given to the 'OnPropertyChanged' method is because, 
        //  it uses the compiler to get the name of the calling property, 
        //  which in this case is the name of the property that has changed.
        // The second call to 'OnPropertyChanged' notifies observers that another property has changed, 
        //  which requires the name of that property to be given, which is done with the nameof built-in method. 
        //  At design time, it gets a string equal to the name of the variable,
        //  which ensures that refactoring the variable will not break this functionality.
        // Java:
        //  private double x;
        // 
        //  public double getX(){
        //    return x;
        //  }
        //
        //  public void setX(double value){
        //    x = value;
        //    OnPropertyChanged();
        //    OnPropertyChanged("CanvasCenterX");
        //  }
        public double X { get { return x; } set { x = value; OnPropertyChanged(); OnPropertyChanged(nameof(CanvasCenterX)); } }

        private double y = 200;
        public double Y { get { return y; } set { y = value; OnPropertyChanged(); OnPropertyChanged(nameof(CanvasCenterY)); } }

        private double width = 100;
        public double Width { get { return width; } set { width = value; OnPropertyChanged(); OnPropertyChanged(nameof(CanvasCenterX)); OnPropertyChanged(nameof(CenterX)); } }

        private double height = 100;
        public double Height { get { return height; } set { height = value; OnPropertyChanged(); OnPropertyChanged(nameof(CanvasCenterY)); OnPropertyChanged(nameof(CenterY)); } }

        // The CenterX and CenterY properties are used by the Shape animation to define the point of rotation.
        // NOTE: These derived properties are diffent from the Shape properties with the same names, 
        //        from the 02350SuperSimpleDemo, see above for an explanation.
        // This method uses an expression-bodied member (http://www.informit.com/articles/article.aspx?p=2414582) to simplify a method that only returns a value;
        // Java:
        //  public double getCenterX(){
        //    return X + Width / 2;
        //  }
        public double CenterX => Width / 2;

        // Same as for the 'CenterX' derived property.
        public double CenterY => Height / 2;

        // Derived properties. 
        // Corresponds to making a Getter method in Java (for instance 'public int GetCenterX()'), 
        //  that does not have its own private field, but is calculated from other fields and properties. } }
        // The CanvasCenterX and CanvasCenterY derived properties are used by the Line class, 
        //  so it can be drawn from the center of one Shape to the center of another Shape.
        // NOTE: In the 02350SuperSimpleDemo these derived properties are called CenterX and CenterY, 
        //        but in this demo we need both these and derived properties for the coordinates of the Shape, 
        //        relative to the upper left corner of the Shape. This is an example of a breaking change, 
        //        that is changed during the lifetime of an application, because the requirements change.

        public double CanvasCenterX { get { return X + CenterX; } set { X = value - CenterX; OnPropertyChanged(nameof(X)); } }

        public double CanvasCenterY { get { return Y + CenterY; } set { Y = value - CenterY; OnPropertyChanged(nameof(Y)); } }

        // ViewModel properties.
        // These properties should be in the ViewModel layer, but it is easier for the demo to put them here, 
        //  to avoid unnecessary complexity.
        // NOTE: This breaks the seperation of layers of the MVVM architecture pattern.
        //       To avoid this a ShapeViewModel class should be created that wraps all Shape objects, 
        //        but it adds to the complexity of the ViewModel layer and this demo, and therefore a simpler solution was chosen for the demo.
        //        (this requires this project to be a WPF class library with references to WPF classes, instead of a regular class library).
        //       To learn how to avoid this and create an application with a more pure MVVM architecture pattern, 
        //        please ask the Teaching Assistants.
        private bool isSelected;
        public bool IsSelected { get { return isSelected; } set { isSelected = value; OnPropertyChanged(); OnPropertyChanged(nameof(SelectedColor)); } }
        public Brush SelectedColor => IsSelected ? Brushes.Red : Brushes.Yellow;

        // Constructor.
        // The constructor is in this case used to set the default values for the properties.
        public Shape()
        {
            // This just means that the integer field called counter is incremented before its value is used to set the Number integer property.
            Number = ++counter;
        }

        // By overwriting the ToString() method, the default representation of the class is changed from the full namespace (Java: package) name, 
        //  to the value of the Number integer property, which is meant to be unique for each Shape object.
        // The ToString() method is inheritied from the Object class, that all classes inherit from.
        public override string ToString() => Number.ToString();
    }
}
