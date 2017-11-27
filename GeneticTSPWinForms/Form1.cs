using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GeneticTSPWinForms
{
    public partial class Form1 : Form
    {
        private StreamReader file;
        private int numberOfNodes;
        private int[,] nodes;
        private int ratio;
        private List<int> globalRoute;
        private double globalRouteLength;
        private CancellationTokenSource tokenSource;
        private CancellationToken token;
        public Form1()
        {
            InitializeComponent();
            globalRouteLength = Double.MaxValue;
            globalRoute = new List<int>();
        }

        private void loadInstanceButton_Click(object sender, EventArgs e)
        {
            file = new StreamReader(filenameTextBox.Text);
            numberOfNodes = int.Parse(file.ReadLine());
            nodes = new int[numberOfNodes, 2];
            for (int i = 0; i < numberOfNodes; i++)
            {
                string temp = file.ReadLine();
                nodes[i, 0] = int.Parse(temp.Split(' ')[1]);
                nodes[i, 1] = int.Parse(temp.Split(' ')[2]);
            }
            file.Close();
            int x = Int32.MinValue;
            int y = Int32.MinValue;
            ratio = 1;
            for (int i = 0; i < numberOfNodes; i++)
            {
                if (nodes[i,0]>x)
                {
                    x = nodes[i, 0];
                }
                if (nodes[i, 1] > y)
                {
                    y = nodes[i, 1];
                }
            }
            //filenameTextBox.Text = x + " " + y;
            if (x>y)
            {
                ratio = x / 500;
                if (x % 500 != 0)
                {
                    ratio++;
                }
            }
            else
            {
                ratio = y / 500;
                if (y % 500 != 0)
                {
                    ratio++;
                }
            }
            Image cityImage = new Bitmap(tourDiagram.Width, tourDiagram.Height);
            Graphics graphics = Graphics.FromImage(cityImage);
            for (int i = 0; i < numberOfNodes; i++)
            {
                graphics.DrawEllipse(Pens.Black, nodes[i, 0] / ratio - 2, nodes[i, 1] / ratio - 2, 4, 4);
            }
            tourDiagram.Image = cityImage;
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            if (startButton.Text.Equals("Start"))
            {
                startButton.Text = "Stop";
                tokenSource = new CancellationTokenSource();
                token = tokenSource.Token;

                Task tMain = Task.Run(() =>
                {
                    globalRoute.Clear();
                    globalRouteLength = Double.MaxValue;
                    /*for (int i = 0; i < 29; i++)
                    {
                        globalRoute.Add(i);
                    }
                    globalRoute.Add(0);*/
                    for (int k = 0; k < numberOfNodes; k++)
                    {
                        if (token.IsCancellationRequested)
                            break;
                        List<int> route = new List<int>();
                        List<int> notUsedNodes = new List<int>();
                        double routeLength = 0;
                        for (int i = 0; i < numberOfNodes; i++)
                        {
                            notUsedNodes.Add(i);
                        }

                        int firstNode = k;
                        route.Add(firstNode);
                        notUsedNodes.Remove(firstNode);
                        while (notUsedNodes.Count > 0)
                        {
                            int actualNodeNumber = route[route.Count - 1];
                            int actualNodeX = nodes[actualNodeNumber, 0];
                            int actualNodeY = nodes[actualNodeNumber, 1];
                            double minLength = Double.MaxValue;
                            int chosenNode = -1;

                            foreach (int nodeNumber in notUsedNodes)
                            {
                                int x = nodes[nodeNumber, 0];
                                int y = nodes[nodeNumber, 1];
                                double length = Math.Sqrt(Math.Pow(actualNodeX - x, 2) + Math.Pow(actualNodeY - y, 2));
                                if (length < minLength)
                                {
                                    minLength = length;
                                    chosenNode = nodeNumber;
                                }
                            }

                            routeLength += minLength;
                            route.Add(chosenNode);
                            notUsedNodes.Remove(chosenNode);
                        }

                        route.Add(route[0]);
                        routeLength += Math.Sqrt(Math.Pow(nodes[route[0], 0] - nodes[route[route.Count - 1], 0], 2) +
                                                 Math.Pow(nodes[route[0], 1] - nodes[route[route.Count - 1], 1], 2));

                        if (routeLength < globalRouteLength)
                        {
                            globalRouteLength = routeLength;
                            globalRoute.Clear();
                            for (int i = 0; i < route.Count; i++)
                            {
                                globalRoute.Add(route[i]);
                            }
                            Task tDr = Task.Run(() =>
                            {
                                DrawTour();
                            });
                        }
                    }
                    startButton.BeginInvoke(new MethodInvoker(() =>
                    {
                        startButton.Text = "Start";
                    }));
                }, token);
            }
            else
            {
                tokenSource.Cancel();
                //startButton.Text = "Start";
            }
            //tMain.Wait();
            //DrawTour();
            //filenameTextBox.Text = globalRoute.Count.ToString();
        }

        private void DrawTour()
        {
            Image cityImage = new Bitmap(tourDiagram.Width, tourDiagram.Height);
            Graphics graphics = Graphics.FromImage(cityImage);
            for (int i = 0; i < numberOfNodes; i++)
            {
                graphics.DrawEllipse(Pens.Black, nodes[i, 0] / ratio - 2, nodes[i, 1] / ratio - 2, 4, 4);
            }
            for (int i = 0; i < globalRoute.Count - 1; i++)
            {
                graphics.DrawLine(Pens.Black, nodes[globalRoute[i], 0] / ratio, nodes[globalRoute[i], 1] / ratio,
                    nodes[globalRoute[i + 1], 0] / ratio, nodes[globalRoute[i + 1], 1] / ratio);
            }
            tourDiagram.Image = cityImage;
        }
    }
}
