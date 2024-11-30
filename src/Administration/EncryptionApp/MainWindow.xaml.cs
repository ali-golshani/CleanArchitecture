using System.Windows;

namespace EncryptionApp;

public partial class MainWindow : Window
{
    private readonly Data data;

    public MainWindow()
    {
        InitializeComponent();

        data = new Data();
        this.DataContext = data;

        data.Error += OnError;
    }

    private void OnError(object? sender, Exception exp)
    {
        MessageBox.Show(this, exp.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
    }
}