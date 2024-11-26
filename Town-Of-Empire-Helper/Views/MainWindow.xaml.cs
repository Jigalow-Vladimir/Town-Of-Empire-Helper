using System.Windows;
using Town_Of_Empire_Helper.ViewModels;

namespace Town_Of_Empire_Helper;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private GameVM _gameVM;

    public MainWindow()
    {
        InitializeComponent();

        _gameVM = new GameVM();
        mainGrid.DataContext = _gameVM;
    }
}