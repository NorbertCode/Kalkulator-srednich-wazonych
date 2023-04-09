using System;
using System.Collections.Generic;
using System.IO;

namespace WeightedAverageCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = "averages.txt";

            Console.WriteLine("Write \'next\' in either the grade or weight field to go onto the next subject.");
            Console.WriteLine("Write \'exit\' to stop the program.");

            // Loop for every subject
            while (true)
            {
                Console.Write("Subject: ");
                string subject = Console.ReadLine();

                if (subject == "exit")
                    break;

                float gradesSum = 0f;
                float weightsSum = 0f;

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

                    float grade = 0f, weight = 0f;
                    if (float.TryParse(strGrade, out grade) && float.TryParse(strWeight, out weight))
                    {
                        grade += modifier;

                        gradesSum += grade * weight;
                        weightsSum += weight;
                    }
                    else
                        Console.WriteLine("Invalid input. Values were omitted.");
                }

                if (weightsSum == 0)
                {
                    Console.WriteLine("Not enough values to calculate average.");
                    continue;
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
