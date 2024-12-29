using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Selection_Sort
{
    public partial class MainWindow : Window
    {
        private readonly int[] array = { 64, 25, 12, 22, 11, 90, 30, 55 };
        private const int BarWidth = 50;

        public MainWindow()
        {
            InitializeComponent();
            DrawArray();
        }

        private void DrawArray(int highlightedIndex = -1, int comparingIndex = -1)
        {
            SortingCanvas.Children.Clear();
            for (int i = 0; i < array.Length; i++)
            {
                var rect = new Rectangle
                {
                    Width = BarWidth - 5,
                    Height = array[i] * 3,
                    Fill = i == highlightedIndex ? Brushes.Red :
                           i == comparingIndex ? Brushes.Yellow :
                           Brushes.Blue
                };
                Canvas.SetLeft(rect, i * BarWidth);
                Canvas.SetTop(rect, SortingCanvas.ActualHeight - rect.Height);
                SortingCanvas.Children.Add(rect);
            }
        }

        private async void StartSorting_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                button.IsEnabled = false; 
                await SelectionSort();
                button.IsEnabled = true; 
            }
        }

        private async Task SelectionSort()
        {
            for (int i = 0; i < array.Length - 1; i++)
            {
                int minIndex = i;
                DrawArray(highlightedIndex: i);

                for (int j = i + 1; j < array.Length; j++)
                {
                    DrawArray(highlightedIndex: i, comparingIndex: j);
                    await Task.Delay(500);

                    if (array[j] < array[minIndex])
                    {
                        minIndex = j;
                    }
                }

                if (minIndex != i)
                {
                    (array[i], array[minIndex]) = (array[minIndex], array[i]);
                    DrawArray();
                    await Task.Delay(500);
                }
            }

            DrawArray();
            MessageBox.Show("Sorting Completed!");
        }
    }
}
