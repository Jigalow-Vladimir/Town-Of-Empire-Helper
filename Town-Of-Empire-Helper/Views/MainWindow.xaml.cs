using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
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