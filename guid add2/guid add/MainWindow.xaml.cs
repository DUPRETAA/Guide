using Microsoft.Win32;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Генератор_заданий;
using forms = System.Windows.Forms;

namespace guid_add
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int counter = 0;
        private DataBase db = new DataBase();
        MySqlCommand command = new MySqlCommand();
        string image = "";


        public MainWindow()
        {
            InitializeComponent();
        }

        private void title_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (((TextBox)sender).Text == "Название")
            {
                counter += 1;
                ((TextBox)sender).Text = "";
            }
        }

        private void description_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (((TextBox)sender).Text == "Описание")
            {
                counter += 1;
                ((TextBox)sender).Text = "";
            }
        }

        private void longitude_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (((TextBox)sender).Text == "Долгота")
            {
                counter += 1;
                ((TextBox)sender).Text = "";
            }
        }

        private void latitude_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (((TextBox)sender).Text == "Широта")
            {
                counter += 1;
                ((TextBox)sender).Text = "";
            }
        }

        private void title_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(((TextBox)sender).Text)) { ((TextBox)sender).Text = "Название"; counter -= 1; }

        }

        private void description_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(((TextBox)sender).Text)) { ((TextBox)sender).Text = "Описание"; counter -= 1; }

        }

        private void longitude_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(((TextBox)sender).Text)) { ((TextBox)sender).Text = "Долгота"; counter -= 1; }

        }

        private void latitude_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(((TextBox)sender).Text)) { ((TextBox)sender).Text = "Широта"; counter -= 1; }

        }
        
        private void submit_Click(object sender, RoutedEventArgs e)
        {
            Exception ex = new Exception();
            try
            {
                if (counter == 4 && image != "")
                {
                    db.OpenConnection();
                    using (
                    command = new MySqlCommand($"insert into sights (title, description, latitude, longitude, photo) values ('{title.Text}', '{description.Text}', '{latitude.Text}', '{longitude.Text}', @image);", db.GetConnection())
                    ) 
                    {
                        command.Parameters.Add("@image", MySqlDbType.LongBlob).Value = File.ReadAllBytes(image);
                        command.ExecuteNonQuery();
                    }
                    db.CloseConnection();

                    title.Text = "Название";
                    description.Text = "Описание";
                    latitude.Text = "Широта";
                    longitude.Text = "Долгота";
                    counter = 0;
                }
                else
                {
                    MessageBox.Show("Заполните все поля");
                }
            }
            catch
            {
                MessageBox.Show($"Ошибка: {ex.ToString()}");
                db.CloseConnection();
            }
        }

        private void DataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.Column.Header.ToString() == "id_sight")
            {
                e.Column.Visibility = Visibility.Hidden;
            }

            if (e.Column.Header.ToString() == "title")
            {
                e.Column.Header = "Название";
                e.Column.MaxWidth = 150;

                Style style = new Style(typeof(DataGridCell));
                style.Setters.Add(new Setter(DataGridCell.ContentTemplateProperty, Resources["templ"]));
                e.Column.CellStyle = style;
            }
            if (e.Column.Header.ToString() == "description")
            {
                e.Column.Header = "Описание";
                e.Column.MaxWidth = 300;

                Style style = new Style(typeof(DataGridCell));
                style.Setters.Add(new Setter(DataGridCell.ContentTemplateProperty, Resources["templ2"]));
                e.Column.CellStyle = style;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
           if (MessageBox.Show("Вы уверены?", "Предупреждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                string selectedRows = "'";
                foreach (DataRowView index in sights.SelectedItems)
                {
                    selectedRows = selectedRows + index.Row[0].ToString() + "', '";
                }

                var command = new MySqlCommand($"delete from sights where id_sight in ({selectedRows.Remove(selectedRows.Length - 3)})", db.GetConnection());
                db.OpenConnection();
                command.ExecuteNonQuery();
                db.CloseConnection();

                UpdateTables();
            }
        }

        public void UpdateTables()
        {
            sights.ItemsSource = db.Select("select id_sight, title, description from sights");
        }

        private void sights_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateTables();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            forms.OpenFileDialog openFileDialog = new forms.OpenFileDialog();
            openFileDialog.Multiselect = false;
            openFileDialog.Filter = "jpg files (*.jpg)|*.jpg";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (openFileDialog.ShowDialog() == forms.DialogResult.OK)
            {
                foreach (string filename in openFileDialog.FileNames)
                {
                    image = filename;
                }
            }
        }
    }
}
