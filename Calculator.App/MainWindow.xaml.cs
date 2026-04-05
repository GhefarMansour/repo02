using System.Windows;
using Calculator.App.ViewModels;

namespace Calculator.App;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new CalculatorViewModel();
    }
}
