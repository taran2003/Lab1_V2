using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Drawing2D;
using System.Drawing;

namespace Lab1_V2
{
    class Drawing
    {
        public GraphicsPath state;
        public PointF center;
        public Drawing(List<PointF>  points)
        {
            PointF[] point = new PointF[points.Count];
            Byte[] type = new Byte[points.Count];
            for (int i = 0; i < points.Count; i++)
            {
                point[i].X = (points[i].X+200)*16;
                point[i].Y = (points[i].Y + 200) * (-16);
                type[i] = 1;
            }
            state = new GraphicsPath(point,type);
        }
        
        static public void Draw(List<List<Drawing>> states, Graphics map, List<SolidBrush> brushes)
        {
            Pen pen = new Pen(Color.Black, 2.0f);
            for(int j=0;j< states.Count;j++)
            {
                for (int i = 0; i < states[j].Count(); i++)
                {
                    map.FillPath(brushes[j], states[j][i].state);
                    map.DrawPath(pen, states[j][i].state);
                }
            }
        }
    }
}
