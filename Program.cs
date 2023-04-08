using System;
using System.Collections.Generic;
using System.IO;

namespace Kalkulator_średnich_ważonych
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Tuple<float, float>> grades = new List<Tuple<float, float>>();
            string path = "srednie.txt";
            while (true)
            {
                grades.Clear();
                Console.Write("Przedmiot: ");
                string clas = Console.ReadLine();

                while (true)
                {
                    Console.Write("Ocena: ");
                    string stGrade = Console.ReadLine();
                    Console.Write("Waga: ");
                    string stWeight = Console.ReadLine();
                    float intGrade, intWeight;
                    float temp = 0;
                    if (stGrade.EndsWith("+"))
                    {
                        temp = 0.5f;
                        stGrade = stGrade.Substring(0, stGrade.Length - 1);
                    }
                    else if (stGrade.EndsWith("-"))
                    {
                        temp = -0.25f;
                        stGrade = stGrade.Substring(0, stGrade.Length - 1);
                    }

                    if (float.TryParse(stGrade, out intGrade) && float.TryParse(stWeight, out intGrade))
                    {
                        intGrade = temp + int.Parse(stGrade);
                        intWeight = temp + int.Parse(stWeight);
                        grades.Add(Tuple.Create(intGrade, intWeight));
                    }
                    else
                        break;
                }
                int i = 0;
                float weights = 0;
                float average = 0;
                while (i < grades.Count)
                {
                    average += grades[i].Item1 * grades[i].Item2;
                    weights += grades[i].Item2;
                    i++;
                }
                average /= weights;
                StreamWriter sw = new StreamWriter(path, true);
                string tempAverage = "0";
                try
                {
                    tempAverage = average.ToString().Substring(0, 4);
                }
                catch(ArgumentOutOfRangeException e)
                {
                    try
                    {
                        tempAverage = average.ToString().Substring(0, 3);
                    }
                    catch(ArgumentOutOfRangeException f)
                    {
                        tempAverage = average.ToString().Substring(0, 1);
                    }
                }
                sw.WriteLine(clas + ": " + tempAverage);
                sw.Close();
                Console.WriteLine(average);
            }
        }
    }
}
