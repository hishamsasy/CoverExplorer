using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.IO;
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
using Path = System.IO.Path;

namespace CoverExplorer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<TreeViewItemViewModel> ProjectItems { get; set; } = new ObservableCollection<TreeViewItemViewModel>();
        public MainWindow()
        {
            InitializeComponent();
            ProjectTreeView.ItemsSource = ProjectItems;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "C# Project Files (*.csproj)|*.csproj"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                string projectFilePath = openFileDialog.FileName;
                MainProjectPath.Text = projectFilePath;
                LoadProjectFiles(projectFilePath);
            }
        }

        private void LoadProjectFiles(string projectFilePath)
        {
            var projectDirectory = Path.GetDirectoryName(projectFilePath);
            var csFiles = Directory.GetFiles(projectDirectory, "*.cs", SearchOption.AllDirectories);

            ProjectItems.Clear();

            foreach (var file in csFiles)
            {
                var relativePath = Path.GetRelativePath(projectDirectory, file);
                AddFileToTreeView(relativePath);
            }
        }

        private void AddFileToTreeView(string relativePath)
        {
            var pathParts = relativePath.Split(Path.DirectorySeparatorChar);
            TreeViewItemViewModel? currentItem = null;

            foreach (var part in pathParts)
            {
                if (currentItem == null)
                {
                    currentItem = FindOrCreateItem(ProjectItems, part);
                }
                else
                {
                    currentItem = FindOrCreateItem(currentItem.Children, part);
                }
            }
        }

        private TreeViewItemViewModel FindOrCreateItem(ObservableCollection<TreeViewItemViewModel> items, string header)
        {
            var item = items.FirstOrDefault(i => i.Header == header);
            if (item == null)
            {
                item = new TreeViewItemViewModel { Header = header };
                items.Add(item);
            }
            return item;
        }

        private void RunOperation_Click(object sender, RoutedEventArgs e)
        {
            var selectedItems = GetSelectedItems(ProjectItems);
            // Perform your operation on selectedItems
        }

        private ObservableCollection<string> GetSelectedItems(ObservableCollection<TreeViewItemViewModel> items)
        {
            var selectedItems = new ObservableCollection<string>();

            foreach (var item in items)
            {
                if (item.IsChecked)
                {
                    selectedItems.Add(item.Header);
                }
                if (item.Children.Any())
                {
                    var childSelectedItems = GetSelectedItems(item.Children);
                    foreach (var childItem in childSelectedItems)
                    {
                        selectedItems.Add(Path.Combine(item.Header, childItem));
                    }
                }
            }
            return selectedItems;
        }
    }

    public class TreeViewItemViewModel : DependencyObject
    {
        public string Header { get; set; }
        public ObservableCollection<TreeViewItemViewModel> Children { get; set; } = new ObservableCollection<TreeViewItemViewModel>();

        public bool IsChecked
        {
            get { return (bool)GetValue(IsCheckedProperty); }
            set { SetValue(IsCheckedProperty, value); }
        }

        public static readonly DependencyProperty IsCheckedProperty =
            DependencyProperty.Register("IsChecked", typeof(bool), typeof(TreeViewItemViewModel), new PropertyMetadata(false));
    }
}