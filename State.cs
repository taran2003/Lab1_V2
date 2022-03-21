using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.IO;
using System.Drawing.Drawing2D;
using System.Drawing;
namespace Lab1_V2
{
    class State
    {
        public List<List<List<PointF>>> states;
        public List<string> nameOfState;
        // long lat = [float, float]
        // polygon = [longlat] | [[longlat]]
        // state = [poly]
        public  State(string path)
        {
            StreamReader cin = new StreamReader(path);
            JsonDocument document = JsonDocument.Parse(cin.ReadToEnd());
            List<PointF> points = new List<PointF>();
            List<List<PointF>> polygons = new List<List<PointF>>();
            nameOfState = new List<string>();
            states = new List<List<List<PointF>>>();
            foreach (var state in document.RootElement.EnumerateObject())
            {
                nameOfState.Add(state.Name);
                foreach (var shape in state.Value.EnumerateArray())
                {
                    var polygon = shape;
                    if(polygon[0][0].ValueKind != JsonValueKind.Number)
                    {
                        polygon = polygon[0];
                    }
                    foreach (var point in polygon.EnumerateArray())
                    {
                        points.Add(new PointF((float)point[0].GetDouble(), (float)point[1].GetDouble()));
                    }
                    polygons.Add(points);
                    points = new List<PointF>();
                }
                states.Add(polygons);
                polygons = new List<List<PointF>>();
            }
            cin.Close();
        }
    }
}
