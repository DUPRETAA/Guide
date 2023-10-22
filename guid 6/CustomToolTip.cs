using GMap.NET.WindowsForms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace guid_6
{
    internal class CustomToolTip : GMapToolTip
    {
        public CustomToolTip(GMapMarker marker)
        : base(marker)
        {
        }
        /// <summary>
        /// Метод для пользовательской прорисовки подсказки
        /// </summary>
        public Action<Graphics> Render { get; set; }
        public CustomToolTip(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        #region Overrides of GMapToolTip

        public override void OnRender(Graphics g)
        {
            if (Render == null)
            {
                base.OnRender(g);
                return;
            }
            Render(g);
        }
        #endregion
    }
}
