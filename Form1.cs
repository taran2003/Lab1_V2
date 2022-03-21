using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab1_V2
{
    public partial class Form1 : Form
    {
        List<List<Drawing>> states;
        List<Tweet> tweets;
        Dictionary<string, double> sentiments;
        public Form1()
        {
            InitializeComponent();

        }

        private void CreateDicctionry(string path)
        {
            System.Globalization.CultureInfo.CurrentCulture = System.Globalization.CultureInfo.InvariantCulture;
            List<string> buf = FileWork.Read(path);
            sentiments = new Dictionary<string, double>();
            foreach (var item in buf)
            {
                string[] rofl = item.Split(',');
                sentiments.Add(rofl[0], double.Parse(rofl[1]));
            }
        }

        private void CreateTweet(string path)
        {
            CreateDicctionry("sentiments.csv");
            List<string> textOfTweet = FileWork.Read(path);
            tweets = new List<Tweet>();
            foreach (var item in textOfTweet)
            {
                tweets.Add(new Tweet(item, sentiments));
            }
        }

        private void LoadStates(State state)
        {
            states = new List<List<Drawing>>();
            for (int z = 0; z < state.states.Count(); z++)
            {
                List<Drawing> buf = new List<Drawing>();
                for (int j = 0; j < state.states[z].Count(); j++)
                {
                    buf.Add(new Drawing(state.states[z][j]));
                }
                states.Add(buf);
            }
        }

        private List<List<Tweet>> SortByState()
        {
            List<List<Tweet>> tweetsInState = new List<List<Tweet>>();
            foreach (var path in states)
            {
                List<Tweet> buf = new List<Tweet>();
                for (int i = 0; i < path.Count(); i++)
                {
                    for (int j = 0; j < tweets.Count; j++)
                    {
                        if (path[i].state.IsVisible(tweets[j].point))
                        {
                            buf.Add(tweets[j]);
                        }
                    }
                }
                tweetsInState.Add(buf);
            }
            return tweetsInState;
        }

        private double AvarageValue(List<Tweet> tweets)
        {
            if (tweets.Count == 0)
            {
                return double.NaN;
            }
            double result=0.0;
            for (int i = 0; i < tweets.Count; i++)
            {
                result += tweets[i].value;
            }
            return result/tweets.Count;
        }

        private void legend(Graphics map)
        {
            Rectangle rectangle = new Rectangle(2040, -4400, 20, 20);
            map.FillRectangle(new SolidBrush(Color.FromArgb(122, 122, 122)), rectangle);
            map.DrawRectangle(new Pen(Color.Black), rectangle);
            map.DrawString(@":NaN", new Font("Arial", 10f), new SolidBrush(Color.Black), 2060, -4400);
            rectangle = new Rectangle(2040, -4375, 20, 20);
            map.FillRectangle(new SolidBrush(Color.FromArgb(225, 0, 0)), rectangle);
            map.DrawRectangle(new Pen(Color.Black), rectangle);
            map.DrawString(@":<=-0.1", new Font("Arial", 10f), new SolidBrush(Color.Black), 2060, -4375);
            rectangle = new Rectangle(2040, -4350, 20, 20);
            map.FillRectangle(new SolidBrush(Color.FromArgb(225, 69, 69)), rectangle);
            map.DrawRectangle(new Pen(Color.Black), rectangle);
            map.DrawString(@":>-0.1 & <=0.0", new Font("Arial", 10f), new SolidBrush(Color.Black), 2060, -4350);
            rectangle = new Rectangle(2040, -4325, 20, 20);
            map.FillRectangle(new SolidBrush(Color.FromArgb(225, 138, 138)), rectangle);
            map.DrawRectangle(new Pen(Color.Black), rectangle);
            map.DrawString(@":>0.0 & <=0.1", new Font("Arial", 10f), new SolidBrush(Color.Black), 2060, -4325);
            rectangle = new Rectangle(2040, -4300, 20, 20);
            map.FillRectangle(new SolidBrush(Color.FromArgb(123, 237, 129)), rectangle);
            map.DrawRectangle(new Pen(Color.Black), rectangle);
            map.DrawString(@":>0.1 & <=0.2", new Font("Arial", 10f), new SolidBrush(Color.Black), 2060, -4300);
            rectangle = new Rectangle(2040, -4275, 20, 20);
            map.FillRectangle(new SolidBrush(Color.FromArgb(0, 255, 0)), rectangle);
            map.DrawRectangle(new Pen(Color.Black), rectangle);
            map.DrawString(@":>0.2", new Font("Arial", 10f), new SolidBrush(Color.Black), 2060, -4275);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Graphics map = this.pictureBox1.CreateGraphics();
            State state = new State("states.json");
            map.TranslateTransform(-300.0f, 4400.0F);
            List<List<Tweet>> tweetsInState = new List<List<Tweet>>();
            LoadStates(state);
            CreateTweet("movie_tweets2014.txt");
            tweetsInState = SortByState();
            //List<double> value =new List<double>();
            List<SolidBrush> brushes = new List<SolidBrush>();
            foreach (var item in tweetsInState)
            {
                double value = AvarageValue(item);
                if(double.IsNaN(value))
                { 
                brushes.Add(new SolidBrush(Color.FromArgb(122,122,122)));
                }
                else if(value<=-0.1)
                {
                    brushes.Add(new SolidBrush(Color.FromArgb(225, 0, 0)));
                }
                else if (value <= 0.0&&value>-0.1)
                {
                    brushes.Add(new SolidBrush(Color.FromArgb(225, 69, 69)));
                }
                else if (value > 0.0&& value <= 0.1)
                {
                    brushes.Add(new SolidBrush(Color.FromArgb(225, 138, 138)));
                }
                else if (value>0.1&& value<=0.2)
                {
                    brushes.Add(new SolidBrush(Color.FromArgb(123, 237, 129)));
                }
                else if (value > 0.2)
                {
                    brushes.Add(new SolidBrush(Color.FromArgb(0, 255,0)));
                }
            }
            Drawing.Draw(states, map, brushes);
            legend(map);
        }
    }
}
