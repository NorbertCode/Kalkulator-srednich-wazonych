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

            Console.WriteLine("Write \'next\' in either the grade or weight field to go onto the next subject.");
            Console.WriteLine("Write \'exit\' to stop the program.");

            // Loop for every subject
            while (true)
            {
                grades.Clear();
                Console.Write("Subject: ");
                string subject = Console.ReadLine();

                if (subject == "exit")
                    break;

                // Loop for every grade
                while (true)
                {
                    Console.Write("Grade: ");
                    string strGrade = Console.ReadLine();
                    if (strGrade == "next")
                        break;

                    Console.Write("Weight: ");
                    string strWeight = Console.ReadLine();
                    if (strWeight == "next")
                        break;

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
                        Console.WriteLine("Invalid input. Values were omitted.");
                }

                // Calculate the average from the current subject
                float gradesSum = 0;
                float weightsSum = 0;
                foreach (Tuple<float, float> grade in grades)
                {
                    gradesSum += grade.Item1 * grade.Item2;
                    weightsSum += grade.Item2;
                }
                decimal average = Math.Round((decimal)(gradesSum / weightsSum), 2);

                // Write to file
                StreamWriter sw = new StreamWriter(path, true);
                sw.WriteLine(subject + ": " + average);
                sw.Close();

                Console.WriteLine(average);
            }
        }
    }
}
