using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;


namespace Lab1_V2
{
    class Tweet
    {
        public PointF point;
        string[] tweet;
        public double value;

        public Tweet(string tweet, Dictionary<string, double> sentiments)
        {
            List<string> buf;
            buf = editor(tweet);
            System.Globalization.CultureInfo.CurrentCulture = System.Globalization.CultureInfo.InvariantCulture;
            point = new PointF((float.Parse(buf[1])+200)*16, (float.Parse(buf[0])+200)*(-16));
            this.tweet = buf[2].Split(' ');
            value = SetValue(sentiments);
        }

        private double SetValue(Dictionary<string, double> sentiments)
        {
            double result= 0.0;
            for (int i = 0; i < tweet.Length; i++)
            {
                if(sentiments.ContainsKey(tweet[i]))
                {
                    result += sentiments.GetValueOrDefault(tweet[i]);
                }
            }
            return result;
        }

        private List<string> editor(string toEdit)
        {
            List<string> result = new List<string>();
            string[] buf = toEdit.Split('\t');
            buf[0] = buf[0].Trim(new char[] { '[', ']' });
            buf[0] = buf[0].Replace(" ", "");
            result.AddRange(buf[0].Split(','));
            result.Add(buf[buf.Length-1]);
            return result;
        }
    }
}
