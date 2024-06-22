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

namespace LabWork37
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
            string filePath = @"C:\Temp\ispp21";
            DirectoryInfo folder = new DirectoryInfo(filePath);//коллекция
            filesList = folder.GetFiles().ToList();//метод кот извлекает массив файлинфо, а потом из массива делается список
            FilesDataGrid.ItemsSource = filesList;
            RecordCountTextBlock.Text = $"Показано {filesList.Count} из {filesList.Count} записей";
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = SearchTextBox.Text.ToLower();//ищет по любым букавкам

            if (string.IsNullOrEmpty(searchText))//если строка пустая то так и оставляет список
            {
                FilesDataGrid.ItemsSource = filesList;
            }

            else
            {//имя файда по любым букавкам которые сожержатся в текстбоксе выводит в виде списка
                var filteredList = filesList.Where(file => file.Name.ToLower().Contains(searchText)).ToList();
                FilesDataGrid.ItemsSource = filteredList;//выводит в строку файлы которые прошли фильтр
                RecordCountTextBlock.Text = $"Показано {filteredList.Count} из {filesList.Count} записей";

                if (filteredList.Count == 0)
                {
                    MessageBox.Show("Подходящих записей не найдено.");
                }
            }
        }

        private void ResetFilterButton_Click(object sender, RoutedEventArgs e)
        {
            SearchTextBox.Text = "";//если нажать кнопку то все сбрасывается
            SearchTextBox_TextChanged(null, null);
        }
    }
}