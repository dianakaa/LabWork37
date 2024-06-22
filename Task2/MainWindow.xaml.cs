using System.IO;
using System.Windows;

namespace Task2
{
    public partial class MainWindow : Window
    {
        private List<FileInfo> filesList;

        public MainWindow()
        {
            InitializeComponent();
            LoadFiles();
        }

        private void LoadFiles()
        {
            DirectoryInfo directory = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.Desktop));
            var files = directory.GetFiles();

            FilesDataGrid.ItemsSource = files.Select(f => new
            {
                FileName = f.Name,
                Extension = f.Extension,
                Path = f.FullName,
                Size = f.Length,
                CreationDate = f.CreationTime,
                ModificationDate = f.LastWriteTime
            }).ToList();
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            double fileSize = Convert.ToDouble(FileSizeTextBox.Text);
            bool isGreaterOrEqual = GreaterOrEqualRadioButton.IsChecked == true;
            bool isLessOrEqual = LessOrEqualRadioButton.IsChecked == true;

            if (AllFilesRadioButton.IsChecked == true)
            {
                FilesDataGrid.ItemsSource = ((List<object>)FilesDataGrid.ItemsSource).ToList();
            }
            else
            {
                FilesDataGrid.ItemsSource = ((List<object>)FilesDataGrid.ItemsSource).Where(f =>
                {
                    var size = Convert.ToDouble(((dynamic)f).Size);
                    if (isGreaterOrEqual && size >= fileSize)
                    {
                        return true;
                    }
                    else if (isLessOrEqual && size <= fileSize)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }).ToList();
            }

            if (((List<object>)FilesDataGrid.ItemsSource).Count == 0)
            {
                MessageBox.Show("Нет подходящих записей.");
            }
        }
    }
}