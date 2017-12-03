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
        private Tour globalTour;
        private List<Tour> tourPopulation;
        private List<Tour> tempPopulation;
        private double globalTourLength;
        private CancellationTokenSource gaThreadCancellationToken;
        private int populationSize;
        private int iterations;
        private Random rand;

        //kufflowe zmienne
        //test
        private int TOP_SURVIVORS = 50; //0-100 ile % najlepszych z poprzedniego pokolenia przejdzie dalej
        private const int BOTTOM_SURVIVORS = 0; //0-100 ile % najgorszych z poprzedniego pokolenia przejdzie dalej
        private int MUTATION_PROBABILITY = 10; // 0-100 % SZANSA NA MUTACJĘ W 1 ROZWIAZANIU
        private int MAX_MUTATIONS = 2; //MAKSYMALNA LICZBA MUTACJI W 1 ROZWIAZANIU W 1 KROKU
        private const int MAX_CROSSOVER_LEN = 10; //0-100: PROCENT DLUGOSCI ROZWIAZANIA BEDACY MAKSYMALNA DLUGOSCIA SEGMENTU PRZY CROSSOVER
        private const int duplicatesRemovalInterval = 2;
        //List<List<int>> Routes = new List<List<int>>();
        //List<List<int>> newRoutes = new List<List<int>>();

        public Form1()
        {
            InitializeComponent();
            globalTourLength = Double.MaxValue;
            globalTour = new Tour();
            tourPopulation = new List<Tour>();
            tempPopulation = new List<Tour>();
            rand = new Random();
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
                populationSize = int.Parse(populationSizeTextBox.Text);
                iterations = int.Parse(iterationsTextBox.Text);
                MAX_MUTATIONS = int.Parse(mutationSizeTextBox.Text);
                MUTATION_PROBABILITY = int.Parse(mutationTextBox.Text);
                int duplicatesTimer = 0;
                Task tMain = Task.Run(() =>
                {
                    globalTour.Clear();
                    tourPopulation.Clear();
                    globalTourLength = Double.MaxValue;
                    Populate();
                    CalculateCostAllToursInPolulation();
                    //tourPopulation = tourPopulation.OrderBy(o => o.GetDistance()).ToList();
                    tourPopulation.Sort();
                    for (int k = 0; k < iterations; k++)
                    {
                        duplicatesTimer++;
                        if (gaThreadCancellationToken.IsCancellationRequested)
                            break;
                        SimulateGeneration();
                        CalculateCostAllToursInPolulation();
                        //tourPopulation = tourPopulation.OrderBy(o => o.GetDistance()).ToList();
                        tourPopulation.Sort();
                        if (globalTourLength > tourPopulation[0].GetDistance())
                        {
                            Tour Changed = findReplacement(tourPopulation[0], globalTour);
                            globalTour = tourPopulation[0];
                            globalTourLength = tourPopulation[0].GetDistance();
                            DrawTour(Changed);
                            drawnTourLengthLabel.BeginInvoke(new MethodInvoker(() =>
                            {
                                drawnTourLengthLabel.Text = "Drawn tour length: " + globalTourLength;
                            }));
                            labelLastSolution.BeginInvoke(new MethodInvoker(() =>
                            {
                                labelLastSolution.Text = "Best solution found @: " + k;
                            }));
                        }
                        iterationLabel.BeginInvoke(new MethodInvoker(() =>
                        {
                            iterationLabel.Text = "Iteration: " + k;
                        }));
                        if (duplicatesTimer > duplicatesRemovalInterval)
                        {
                            duplicatesTimer = 0;
                            Tour Prev = tourPopulation[0];
                            Tour Next;
                            for (int i = 1; i < tourPopulation.Count; i++)
                            {
                                Next = tourPopulation[i];
                                if (Prev.GetDistance() == Next.GetDistance())
                                {
                                    if (Prev.Hash() == Next.Hash())
                                    {
                                        Mutate(tourPopulation[i]);
                                    }
                                    else
                                    {
                                        Prev = Next;
                                    }
                                }
                                else
                                {
                                    Prev = Next;
                                }
                            }
                            CalculateCostAllToursInPolulation();
                            //tourPopulation = tourPopulation.OrderBy(o => o.GetDistance()).ToList();
                            tourPopulation.Sort();
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

        private void DrawTour(Tour whatsNew)
        {
            Image cityImage = new Bitmap(tourDiagram.Width, tourDiagram.Height);
            Graphics graphics = Graphics.FromImage(cityImage);
            for (int i = 0; i < numberOfCities; i++)
            {
                graphics.DrawEllipse(Pens.Black, citiesPositions[i, 0] / citiesPositionsToGuiRatio - 2, citiesPositions[i, 1] / citiesPositionsToGuiRatio - 2, 4, 4);
            }
            Pen Color = Pens.Black;

            for (int i = 0; i < globalTour.Count - 1; i++)
            {
                if (whatsNew.Count > 0) 
                if (whatsNew.IndexOf(globalTour[i + 1]) != -1)
                {
                    Color = Pens.Red;
                }
                else
                {
                    Color = Pens.Black;
                }
                graphics.DrawLine(Color, citiesPositions[globalTour[i], 0] / citiesPositionsToGuiRatio, citiesPositions[globalTour[i], 1] / citiesPositionsToGuiRatio,
                    citiesPositions[globalTour[i + 1], 0] / citiesPositionsToGuiRatio, citiesPositions[globalTour[i + 1], 1] / citiesPositionsToGuiRatio);
            }
            if (whatsNew.Count > 0)
                if (whatsNew.IndexOf(globalTour[0]) != -1)
            {
                Color = Pens.Red;
            }
            else
            {
                Color = Pens.Black;
            }
            graphics.DrawLine(Color, citiesPositions[globalTour[0], 0] / citiesPositionsToGuiRatio,
                citiesPositions[globalTour[0], 1] / citiesPositionsToGuiRatio,
                citiesPositions[globalTour[globalTour.Count - 1], 0] / citiesPositionsToGuiRatio,
                citiesPositions[globalTour[globalTour.Count - 1], 1] / citiesPositionsToGuiRatio);
            //tourDiagram.Image = cityImage;
            tourDiagram.BeginInvoke(new MethodInvoker(() =>
            {
                tourDiagram.Image = cityImage;
            }));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            tourCityListTextBox.Text = "";
            foreach (int i in globalTour)
            {
                tourCityListTextBox.AppendText(i.ToString());
                tourCityListTextBox.AppendText(Environment.NewLine);
            }
        }

        private Tour WhatChanged(Tour B)
        {
            Tour C = new Tour();

            for (int i = 0; i < globalTour.Count; i++)
            {
                int next;
                if (i < globalTour.Count - 1)
                {
                    next = globalTour[i + 1];
                }
                else
                {
                    next = globalTour[0];
                }
                int where = B.IndexOf(globalTour[i]); //do tutaj
            }

            return C;
        }

        private int FindPartner()
        { //losuje indeks partnera ze sklonnoscia do nizszych wartosci
            int n = tourPopulation.Count;
            int maxRand = (n + 1) * (n + 1);
            int r = rand.Next(1, maxRand);

            return n - (int)Math.Floor(Math.Sqrt(r));
        }

        private void Mutate(Tour Tour)
        { //dokonuje mutacji na zadanym rozwiazaniu
            int maxRand = numberOfCities;
            for (int i = 0; i < MAX_MUTATIONS; i++)
            {
                int a = rand.Next(maxRand);
                int b = a;
                while (a == b)
                {
                    b = rand.Next(0, maxRand);
                }
                int tmp = Tour[a];
                Tour[a] = Tour[b];
                Tour[b] = tmp;
            }
        }

        private void Crossover(Tour parent1, Tour parent2, Tour child)
        { //Robi crossover rozwiazania A i B
            int unique = 0;
            child.Clear();
            int index = 0;
            int start = rand.Next(0, numberOfCities * 6 / 10);
            int len = rand.Next(numberOfCities * 3 / 10, (numberOfCities - 1 - start));
            List<int> S;
            S = parent1.GetRange(start, len);
            int i = 0;
            while (index < start)
            {
                if (!S.Contains(parent2[i]))
                {
                    child.Add(parent2[i]);
                    index++;
                }
                else
                    unique++;
                i++;
            }
            child.AddRange(S);
            index += len;
            while (index < parent1.Count())
            {
                if (!S.Contains(parent2[i]))
                {
                    unique++;
                    child.Add(parent2[i]);
                    index++;
                }
                else
                    unique++;
                i++;
            }
            if (unique == numberOfCities - len)
                Mutate(child);
        }

        private void Breed(Tour A)
        { //generuje nowego osobnika z 2 wybranych losowo z populacji i dodaje go do nowej populacji
            int a,b;
            do
            {
                a = FindPartner();
            } while (tourPopulation[a]==A);
            do
            {
                b = FindPartner();
            } while (a==b || tourPopulation[b]==A);
            Crossover(tourPopulation[a], tourPopulation[b],A);
        }

        private void SimulateGeneration()
        { //robi jeden krok populacji, nowa populacja ma rozmiar identyczny z poprzednią
            tempPopulation.Clear();
            int FitToCopy = tourPopulation.Count * TOP_SURVIVORS / 100;
            int UnfitToCopy = BOTTOM_SURVIVORS;
            int i;
            for (i = 0; i < FitToCopy; i++)
            { //kopiowanie najbardziej fit
                if (i != 0 && rand.Next(1, 100) <= MUTATION_PROBABILITY)
                {
                    Mutate(tourPopulation[i]);
                }
                tempPopulation.Add(tourPopulation[i]);

            }
            for (; i < tourPopulation.Count - UnfitToCopy; i++)
            {
                Breed(tourPopulation[i]);
                tempPopulation.Add(tourPopulation[i]);
            }
            for (; i < tourPopulation.Count; i++)
            {
                if (rand.Next(1, 100) <= MUTATION_PROBABILITY * 2)
                {
                    Tour C = new Tour();
                    C.AddRange(tourPopulation[i].ToArray());
                    Mutate(C);
                    tempPopulation.Add(C);
                }
            }
            tourPopulation.Clear();
            tourPopulation.AddRange(tempPopulation);
        }

        private void Populate() //tworzenie poczatkowej populacji - greedy dla kazdego miasta + zapelnienie reszty ich krzyzowkami i mutacjami
        {
            for (int city = 0; city < numberOfCities; city++)
            {
                Greedy(city);
            }
            CalculateCostAllToursInPolulation(); //musimy miec po czym sortowac
            //tourPopulation = tourPopulation.OrderBy(o=>o.GetDistance()).ToList();
            tourPopulation.Sort(); //sortujemy, zeby reszta tworzonych zaraz tras preferowala tworzenie sie z lepszych tras
            while (tourPopulation.Count < populationSize)
            {
                Tour Tour = new Tour();
                Breed(Tour);
                Mutate(Tour);
                tourPopulation.Add(Tour);
            }
        }

        private void CalculateCostAllToursInPolulation()
        {
            foreach (var tour in tourPopulation)
            {
                tour.UpdateDistance(distancesBetweenCities);
            }
        }

        private void Greedy(int firstCity)
        {
            Tour tour = new Tour();
            List<int> notUsedCities = new List<int>();
            double tourLength = 0;
            for (int i = 0; i < numberOfCities; i++)
            {
                notUsedCities.Add(i);
            }

            tour.Add(firstCity);
            notUsedCities.Remove(firstCity);
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
            tourPopulation.Add(tour);
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private Tour findReplacement(Tour Next, Tour Prev)
        {
            Tour B = new Tour();
            int k, p;
            for (int i = 0; i < Prev.Count; i++)
            {
                k = Next.IndexOf(Prev[i]);
                p = k;
                if (k == numberOfCities - 1)
                {
                    p = 0;
                }
                else
                {
                    p += 1;
                }
                if (i != Next.Count - 1)
                {
                    if (Next[p] != Prev[i + 1])
                    {
                        B.Add(Next[p]);
                    }
                }
                else
                {
                    if (Next[p] != Prev[0])
                    {
                        B.Add(Next[p]);
                    }
                }
            }
            return B;
        }
    }
}
