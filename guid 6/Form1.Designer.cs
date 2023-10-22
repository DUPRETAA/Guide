namespace guid_6
{
    partial class Form12
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.mapView = new GMap.NET.WindowsForms.GMapControl();
            this.SuspendLayout();
            // 
            // mapView
            // 
            this.mapView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mapView.AutoSize = true;
            this.mapView.Bearing = 0F;
            this.mapView.CanDragMap = true;
            this.mapView.EmptyTileColor = System.Drawing.Color.Navy;
            this.mapView.GrayScaleMode = false;
            this.mapView.HelperLineOption = GMap.NET.WindowsForms.HelperLineOptions.DontShow;
            this.mapView.LevelsKeepInMemmory = 5;
            this.mapView.Location = new System.Drawing.Point(0, -1);
            this.mapView.MarkersEnabled = true;
            this.mapView.MaxZoom = 2;
            this.mapView.MinZoom = 2;
            this.mapView.MouseWheelZoomEnabled = true;
            this.mapView.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
            this.mapView.Name = "mapView";
            this.mapView.NegativeMode = false;
            this.mapView.PolygonsEnabled = true;
            this.mapView.RetryLoadTile = 0;
            this.mapView.RoutesEnabled = true;
            this.mapView.ScaleMode = GMap.NET.WindowsForms.ScaleModes.Integer;
            this.mapView.SelectedAreaFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(65)))), ((int)(((byte)(105)))), ((int)(((byte)(225)))));
            this.mapView.ShowTileGridLines = false;
            this.mapView.Size = new System.Drawing.Size(800, 395);
            this.mapView.TabIndex = 0;
            this.mapView.Zoom = 0D;
            this.mapView.OnMarkerClick += new GMap.NET.WindowsForms.MarkerClick(this.mapView_OnMarkerClick);
            this.mapView.Load += new System.EventHandler(this.mapView_Load);
            this.mapView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.mapView_MouseDoubleClick);
            // 
            // Form12
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.mapView);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form12";
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private GMap.NET.WindowsForms.GMapControl mapView;
    }
}

