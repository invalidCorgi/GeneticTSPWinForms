﻿using System;
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
        private List<Tour> tourPopulation;
        private List<Tour> newTourPopulation;
        private double globalTourLength;
        private CancellationTokenSource gaThreadCancellationToken;
        private int populationSize;
        private int iterations;
        private Random rand;
        
        //kufflowe zmienne
        private const int TOP_SURVIVORS = 10; //0-100 ile % najlepszych z poprzedniego pokolenia przejdzie dalej
        private const int BOTTOM_SURVIVORS = 10; //0-100 ile % najgorszych z poprzedniego pokolenia przejdzie dalej
        private const int MUTATION_PROBABILITY = 10; // 0-100 % SZANSA NA MUTACJĘ W 1 ROZWIAZANIU
        private const int MAX_MUTATIONS = 10; //MAKSYMALNA LICZBA MUTACJI W 1 ROZWIAZANIU W 1 KROKU
        private const int MAX_CROSSOVER_LEN = 10; //0-100: PROCENT DLUGOSCI ROZWIAZANIA BEDACY MAKSYMALNA DLUGOSCIA SEGMENTU PRZY CROSSOVER
        //List<List<int>> Routes = new List<List<int>>();
        //List<List<int>> newRoutes = new List<List<int>>();

        public Form1()
        {
            InitializeComponent();
            globalTourLength = Double.MaxValue;
            globalTour = new List<int>();
            tourPopulation = new List<Tour>();
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

                Task tMain = Task.Run(() =>
                {
                    globalTour.Clear();
                    tourPopulation.Clear();
                    globalTourLength = Double.MaxValue;
                    for (int i = 0; i < populationSize; i++)
                    {
                        tourPopulation.Add(new Tour());
                        tourPopulation[i].MakeRandomTour(numberOfCities,rand);
                        tourPopulation[i].UpdateDistance(distancesBetweenCities);
                    }
                    tourPopulation.Sort();
                    for (int k = 0; k < 1; k++)
                    {
                        globalTour = tourPopulation[0];
                        globalTourLength = tourPopulation[0].GetDistance();
                        DrawTour();
                        drawnTourLengthLabel.BeginInvoke(new MethodInvoker(() =>
                        {
                            drawnTourLengthLabel.Text = "Drawn tour length: "+globalTourLength;
                        }));
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
            graphics.DrawLine(Pens.Black, citiesPositions[globalTour[0], 0] / citiesPositionsToGuiRatio,
                citiesPositions[globalTour[0], 1] / citiesPositionsToGuiRatio,
                citiesPositions[globalTour[globalTour.Count - 1], 0] / citiesPositionsToGuiRatio,
                citiesPositions[globalTour[globalTour.Count - 1], 1] / citiesPositionsToGuiRatio);
            tourDiagram.Image = cityImage;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            tourCityListTextBox.Text = globalTourLength.ToString(CultureInfo.CurrentCulture);
        }

        //kufflowe funckjce

        private int FindPartner()
        { //losuje indeks partnera
            int n = tourPopulation.Count();
            int maxRand = n * n * n * n;
            int r = rand.Next(1, maxRand);
            return (int) (Math.Floor(Math.Sqrt(Math.Sqrt(r))) - 1);
        }

        private void Mutate(List<int> Route)
        { //dokonuje mutacji na zadanym rozwiazaniu
            int maxRand = Route.Count();
            for (int i = 0; i < MAX_MUTATIONS; i++)
            {
                int a = rand.Next(0, maxRand);
                int b = a;
                while (a == b)
                {
                    b = rand.Next(0, maxRand);
                }
                int tmp = Route[a];
                Route[a] = Route[b];
                Route[b] = tmp;
            }
        }

        private Tour Crossover(Tour A, Tour B)
        { //Robi crossover rozwiazania A i B
            Tour C = new Tour();
            int index = 0;
            bool CurrentA = true;
            while (index < A.Count() - 1)
            {
                int len = rand.Next(1, (numberOfCities * MAX_CROSSOVER_LEN / 100));
                int newIndex = index + len;
                if (newIndex >= A.Count())
                {
                    newIndex = A.Count() - 1;
                }
                for (; index <= newIndex; index++)
                {
                    if (CurrentA)
                    {
                        C.Add(A[index]);
                    }
                    else
                    {
                        C.Add(B[index]);
                    }
                }
                CurrentA = !CurrentA;
            }
            return C;
        }

        private Tour Breed()
        { //generuje nowego osobnika z 2 wybranych losowo z populacji i dodaje go do nowej populacji
            int a = FindPartner();
            int b = a;
            while (a == b)
            {
                b = FindPartner();
            }
            Tour C;
            C = Crossover(tourPopulation[a], tourPopulation[b]);
            return C;
        }

        /*int CalculateCost(List<int> Route)
        { //ASDFG
            return magia //kod na obliczanie ceny danej drogi
}

        void SortRoutes()
        { //kod na sortowanie listy list Routes po kluczu CalculateCost

        }*/

        private void SimulateGeneration()
        { //robi jeden krok populacji, nowa populacja ma rozmiar identyczny z poprzednią
            newTourPopulation.Clear();
            //SortRoutes();
            tourPopulation.Sort();
            int FitToCopy = tourPopulation.Count() * TOP_SURVIVORS / 100;
            int UnfitToCopy = tourPopulation.Count() * BOTTOM_SURVIVORS / 100;
            int i;
            for (i = 0; i < FitToCopy; i++)
            { //kopiowanie najbardziej fit
                if (i != 0 && rand.Next(1, 100) <= MUTATION_PROBABILITY)
                {
                    Tour C = tourPopulation[i];
                    Mutate(C);
                    newTourPopulation.Add(C);
                }
                newTourPopulation.Add(tourPopulation[i]);
            }
            for (; i < tourPopulation.Count() - UnfitToCopy; i++)
            {
                newTourPopulation.Add(Breed());
            }
            for (; i < tourPopulation.Count; i++)
            {
                if (rand.Next(1, 100) <= MUTATION_PROBABILITY)
                {
                    Tour C = tourPopulation[i];
                    Mutate(C);
                    newTourPopulation.Add(C);
                }
            }
            tourPopulation = newTourPopulation;
        }

        private void Populate()
        { //dodaje nowe rozwiazania zeby pierwotny rozmiar populacji sie zgadzal
          //tutaj kod heurystyki generujacej poczatkowe
            while (tourPopulation.Count() < populationSize)
            {
                Tour C = Breed();
                Mutate(C);
                tourPopulation.Add(C);
            }
        }
    }
}
