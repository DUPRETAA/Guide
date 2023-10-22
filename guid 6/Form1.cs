using GMap.NET.MapProviders;
using GMap.NET;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Windows.Media.Imaging;
using System.IO;
using System.Collections;

namespace guid_6
{

    public partial class Form12 : System.Windows.Forms.Form
    {
        private bool move = true;
        private bool move2 = true;
        private GMapOverlay _markerOverlay;

        public Form12()
        {
            InitializeComponent();
        }

        private void mapView_Load(object sender, EventArgs e)
        {
            // способ работы карты
            GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerAndCache;
            // горизонтальное положение карты
            mapView.Bearing = 0;
            // какая карта используется
            mapView.MapProvider = GMapProviders.GoogleMap;
            GMaps.Instance.Mode = AccessMode.ServerOnly;
            // минимальный и максимальный зум
            mapView.MinZoom = 8;
            mapView.MaxZoom = 17;
            // зум по умолчанию
            mapView.Zoom = 8;
            mapView.Position = new GMap.NET.PointLatLng(58.82796, 56.11404);// точка в центре карты при открытии
            // способ приблежения
            mapView.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionWithoutCenter;
            // можно ли перемещать карту
            mapView.CanDragMap = true;
            // кнопка с помощью которой происходит перемещаение карты
            mapView.DragButton = MouseButtons.Left;
            // скрытие внешней сетки карты
            mapView.ShowTileGridLines = false;
            // убрать красный крест из центра экарана
            mapView.ShowCenter = false;
            // язык карты - русский
            GMapProvider.Language = LanguageType.Russian;
            // для запросов
            GMapProvider.WebProxy = WebRequest.GetSystemWebProxy();
            GMapProvider.WebProxy.Credentials = CredentialCache.DefaultCredentials;

            OpenConnection();
            DataView datatable = Select("select title, latitude, longitude, photo from sights");


            if (_markerOverlay != null) _markerOverlay.Markers.Clear();


            for (int i = 0; i < datatable.Count; i++)
            {
                byte[] image = (byte[])datatable[i][3];
                PointLatLng point = new PointLatLng(double.Parse(datatable[i][1].ToString()), double.Parse(datatable[i][2].ToString()));
                AddMarker(datatable[i][0].ToString(), point, image);
            }
        }

        private void AddMarker(string title, PointLatLng point, byte[] photo)
        {
            if (_markerOverlay == null)
            {
                _markerOverlay = new GMapOverlay("markers");
                mapView.Overlays.Add(_markerOverlay);
            }
            //Новый маркер в заданной точке с заданным изображением. Изображение должно быть в одной папке и исполняемым файлом
            GMapMarker marker = new GMarkerGoogle(point, new Bitmap("marker.png"));
            //Всегда показывать подсказку
            marker.ToolTipMode = MarkerTooltipMode.OnMouseOver;
            //Создаём подсказку
            var tooltip = new CustomToolTip(marker);
            //Задаём метод для отрисовывания подсказки
            tooltip.Render += g =>
            {
                ToolTipRender(tooltip, marker, g, photo);
            };
            //Назначаем подсказку маркеру
            marker.ToolTip = tooltip;
            //Получение текста для подсказки
            GeoCoderStatusCode geoCoderStatusCode;
            Placemark? pm = GMapProviders.GoogleMap.GetPlacemark(point, out geoCoderStatusCode);
            marker.ToolTipText = $"{title}";
            _markerOverlay.Markers.Add(marker);
        }

        private void ToolTipRender(GMapToolTip tooltip, GMapMarker marker, Graphics g, byte[] photo)
        {
            //Размер текста подсказки
            SizeF size = g.MeasureString(marker.ToolTipText, tooltip.Font);
            //Прямоугольник для текста и изображения
            float x;
            if (size.Width < 320) x = 320; else x = size.Width;
            var rect = new RectangleF(marker.Size.Width + marker.LocalPosition.X, marker.LocalPosition.Y, x, size.Height+180);
            //Заполнение фона
            g.FillRectangle(tooltip.Fill, rect);
            //Рисование текста
            g.DrawString(marker.ToolTipText, tooltip.Font, tooltip.Foreground, new PointF(rect.Location.X, marker.LocalPosition.Y));
            //Рисование изображения
            Stream stream = new MemoryStream(photo);
            g.DrawImage(new Bitmap(Image.FromStream(stream), (int)320, (int)180), new PointF(rect.Location.X, rect.Location.Y + size.Height));
        }

        public void OpenConnection()
        {
            MySqlConnection _connection = new MySqlConnection("server=localhost;port=3306;username=root;password=root;database=guid");
            if (_connection.State == System.Data.ConnectionState.Closed)
                _connection.Open();
        }

        public void CloseConnection()
        {
            MySqlConnection _connection = new MySqlConnection("server=localhost;port=3306;username=root;password=root;database=guid");
            if (_connection.State == System.Data.ConnectionState.Open)
                _connection.Close();
        }

        public DataView Select(string sqlString)
        {
            var ret = new DataView();
            OpenConnection();
            try
            {
                var table = new DataTable();
                var adapter = new MySqlDataAdapter();
                var command = new MySqlCommand(sqlString, GetConnection());

                adapter.SelectCommand = command;
                adapter.Fill(table);

                ret = table.DefaultView;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                throw;
            }
            finally
            {
                CloseConnection();
            }

            return ret;
        }

        public MySqlConnection GetConnection()
        {
            MySqlConnection _connection = new MySqlConnection("server=localhost;port=3306;username=root;password=root;database=guid");
            return _connection;
        }

        private void mapView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (move && move2)
            {
                mapView.Position = new GMap.NET.PointLatLng(58.82796, 56.11404);// точка в центре карты при открытии
                mapView.Zoom = 8;
                move = true;
                move2 = true;
            }   
            else
            {
                move = true;
                move2 = true;
            }
        }

        private void mapView_OnMarkerClick(GMap.NET.WindowsForms.GMapMarker item, MouseEventArgs e)
        {
            if (!move) move2 = false;

            if (!move && !move2)
            {
                OpenConnection();
                DataView datatable = Select($"select description from sights where title = '{item.ToolTipText}'");
                CloseConnection();

                MessageBox.Show(datatable[0][0].ToString(), "Дополнительная информация", MessageBoxButtons.OK);
                move = true;
                move2 = true;
            }

            
            move = false;
            
            
        }
    }
}
