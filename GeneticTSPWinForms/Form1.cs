using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
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
        private StreamReader fileWithInstanceReader;
        private int numberOfCities;
        private int[,] citiesPositions;
        private double[,] distancesBetweenCities;
        private int citiesPositionsToGuiRatio;
        private List<int> globalTour;
        private List<List<int>> tourPopulation;
        private double globalTourLength;
        private CancellationTokenSource gaThreadCancellationToken;
        public Form1()
        {
            InitializeComponent();
            globalTourLength = Double.MaxValue;
            globalTour = new List<int>();
        }

        private void loadInstanceButton_Click(object sender, EventArgs e)
        {
            fileWithInstanceReader = new StreamReader(filenameTextBox.Text);
            numberOfCities = int.Parse(fileWithInstanceReader.ReadLine());
            citiesPositions = new int[numberOfCities, 2];
            distancesBetweenCities = new double[numberOfCities, numberOfCities];

            for (int i = 0; i < numberOfCities; i++)
            {
                string temp = fileWithInstanceReader.ReadLine();
                citiesPositions[i, 0] = int.Parse(temp.Split(' ')[1]);
                citiesPositions[i, 1] = int.Parse(temp.Split(' ')[2]);
            }
            fileWithInstanceReader.Close();
            int x = Int32.MinValue;
            int y = Int32.MinValue;
            citiesPositionsToGuiRatio = 1;
            for (int i = 0; i < numberOfCities; i++)
            {
                if (citiesPositions[i, 0] > x)
                {
                    x = citiesPositions[i, 0];
                }
                if (citiesPositions[i, 1] > y)
                {
                    y = citiesPositions[i, 1];
                }
            }
            int xyMax;
            if (x > y) xyMax = x;
            else xyMax = y;
            citiesPositionsToGuiRatio = xyMax / 500;
            if (xyMax % 500 != 0)
            {
                citiesPositionsToGuiRatio++;
            }
            Image cityImage = new Bitmap(tourDiagram.Width, tourDiagram.Height);
            Graphics graphics = Graphics.FromImage(cityImage);
            for (int i = 0; i < numberOfCities; i++)
            {
                graphics.DrawEllipse(Pens.Black, citiesPositions[i, 0] / citiesPositionsToGuiRatio - 2, citiesPositions[i, 1] / citiesPositionsToGuiRatio - 2, 4, 4);
            }
            tourDiagram.Image = cityImage;
            for (int i = 0; i < numberOfCities; i++)
            {
                for (int j = 0; j < numberOfCities; j++)
                {
                    distancesBetweenCities[i, j] = Math.Sqrt(
                        Math.Pow(citiesPositions[i, 0] - citiesPositions[j, 0], 2) +
                        Math.Pow(citiesPositions[i, 1] - citiesPositions[j, 1], 2));
                }
            }
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            if (startButton.Text.Equals("Start"))
            {
                startButton.Text = "Stop";
                gaThreadCancellationToken = new CancellationTokenSource();

                Task tMain = Task.Run(() =>
                {
                    globalTour.Clear();
                    globalTourLength = Double.MaxValue;
                    for (int k = 0; k < numberOfCities; k++)
                    {
                        if (gaThreadCancellationToken.Token.IsCancellationRequested)
                            break;
                        List<int> tour = new List<int>();
                        List<int> notUsedCities = new List<int>();
                        double tourLength = 0;
                        for (int i = 0; i < numberOfCities; i++)
                        {
                            notUsedCities.Add(i);
                        }

                        int firstNode = k;
                        tour.Add(firstNode);
                        notUsedCities.Remove(firstNode);
                        while (notUsedCities.Count > 0)
                        {
                            int actualNodeNumber = tour[tour.Count - 1];
                            double minLength = Double.MaxValue;
                            int chosenNode = -1;

                            foreach (int nodeNumber in notUsedCities)
                            {
                                double length = distancesBetweenCities[actualNodeNumber, nodeNumber];
                                if (length < minLength)
                                {
                                    minLength = length;
                                    chosenNode = nodeNumber;
                                }
                            }

                            tourLength += minLength;
                            tour.Add(chosenNode);
                            notUsedCities.Remove(chosenNode);
                        }

                        tourLength += distancesBetweenCities[tour[0], tour[tour.Count - 1]];
                        tour.Add(tour[0]);

                        if (tourLength < globalTourLength)
                        {
                            globalTourLength = tourLength;
                            globalTour.Clear();
                            foreach (int city in tour)
                            {
                                globalTour.Add(city);
                            }
                            DrawTour();
                            drawnTourLengthLabel.BeginInvoke(new MethodInvoker(() =>
                            {
                                drawnTourLengthLabel.Text = globalTourLength.ToString(CultureInfo.CurrentCulture);
                            }));
                        }
                    }
                    startButton.BeginInvoke(new MethodInvoker(() =>
                    {
                        startButton.Text = "Start";
                    }));
                }, gaThreadCancellationToken.Token);
            }
            else
            {
                gaThreadCancellationToken.Cancel();
            }
        }

        private void DrawTour()
        {
            Image cityImage = new Bitmap(tourDiagram.Width, tourDiagram.Height);
            Graphics graphics = Graphics.FromImage(cityImage);
            for (int i = 0; i < numberOfCities; i++)
            {
                graphics.DrawEllipse(Pens.Black, citiesPositions[i, 0] / citiesPositionsToGuiRatio - 2, citiesPositions[i, 1] / citiesPositionsToGuiRatio - 2, 4, 4);
            }
            for (int i = 0; i < globalTour.Count - 1; i++)
            {
                graphics.DrawLine(Pens.Black, citiesPositions[globalTour[i], 0] / citiesPositionsToGuiRatio, citiesPositions[globalTour[i], 1] / citiesPositionsToGuiRatio,
                    citiesPositions[globalTour[i + 1], 0] / citiesPositionsToGuiRatio, citiesPositions[globalTour[i + 1], 1] / citiesPositionsToGuiRatio);
            }
            tourDiagram.Image = cityImage;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            tourCityListTextBox.Text = globalTourLength.ToString(CultureInfo.CurrentCulture);
        }
    }
}
