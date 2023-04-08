using System;
using System.Collections.Generic;
using System.IO;

namespace WeightedAverageCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Tuple<float, float>> grades = new List<Tuple<float, float>>();
            string path = "averages.txt";

            Console.WriteLine("Write \'next\' in either the grade or weight field to go onto the next subject.")
            Console.WriteLine("Write \'exit\' to stop the program.")

            // Loop for every subject
            while (true)
            {
                grades.Clear();
                Console.Write("Subject: ");
                string subject = Console.ReadLine();

                // Loop for every grade
                while (true)
                {
                    Console.Write("Grade: ");
                    string strGrade = Console.ReadLine();

                    Console.Write("Weight: ");
                    string strWeight = Console.ReadLine();

                    // Remove + and - from grades before converting to numbers
                    float modifier = 0;
                    if (strGrade.EndsWith("+"))
                    {
                        modifier = 0.5f;
                        strGrade = strGrade.Substring(0, strGrade.Length - 1);
                    }
                    else if (strGrade.EndsWith("-"))
                    {
                        modifier = -0.25f;
                        strGrade = strGrade.Substring(0, strGrade.Length - 1);
                    }

                    float grade, weight;
                    if (float.TryParse(strGrade, out grade) && float.TryParse(strWeight, out grade))
                    {
                        grade = modifier + float.Parse(strGrade);
                        weight = modifier + float.Parse(strWeight);
                        grades.Add(Tuple.Create(grade, weight));
                    }
                    else
                        break;
                }

                // Calculate the average from the current subject
                int i = 0;
                float weightsSum = 0;
                float gradesSum = 0;
                while (i < grades.Count)
                {
                    gradesSum += grades[i].Item1 * grades[i].Item2;
                    weightsSum += grades[i].Item2;
                    i++;
                }
                float average = gradesSum / weightsSum;

                // Write to file
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
                sw.WriteLine(subject + ": " + tempAverage);
                sw.Close();

                Console.WriteLine(average);
            }
        }
    }
}
