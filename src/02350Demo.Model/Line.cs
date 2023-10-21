using CommunityToolkit.Mvvm.ComponentModel;

namespace _02350Demo.Model
{
    // The Line class has a reference to 2 shapes, that it connects.
    public class Line : ObservableObject 
    {
        // Normally Auto-Implemented Properties (http://msdn.microsoft.com/en-us/library/bb384054.aspx) would be used, 
        //  but in this case additional work has to be done when the property is changed, 
        //  which is to raise an INotifyPropertyChanged event that notifies the View (GUI) that this model property has changed, 
        //  so the graphical representation can be updated.

        // The reason no string is given to the 'OnPropertyChanged' method is because, 
        //  it uses the compiler to get the name of the calling property, 
        //  which in this case is the name of the property that has changed.
        // Java:
        //  private Shape from;
        // 
        //  public Shape getFrom(){
        //    return from;
        //  }
        //
        //  public void setFrom(Shape value){
        //    from = value;
        //    OnPropertyChanged();
        //  }
        private Shape from;
        public Shape From { get { return from; } set { from = value; OnPropertyChanged(); } }

        // The reason no string is given to the 'OnPropertyChanged' method is because, 
        //  it uses the compiler to get the name of the calling property, 
        //  which in this case is the name of the property that has changed.
        // Java:
        //  private Shape to;
        // 
        //  public Shape getTo(){
        //    return to;
        //  }
        //
        //  public void setTo(Shape value){
        //    to = value;
        //    OnPropertyChanged();
        //  }
        private Shape to;
        public Shape To { get { return to; } set { to = value; OnPropertyChanged(); } }
    }
}
