using LinqStatistics;
using System;
using System.Collections.Generic;
using System.Linq;
using WebStatistics;

namespace LocalEntry
{
    class Program
    {
        public const int SAMPLE_SIZE = 100000000;

        static void Main(string[] args)
        {
            CalcStatisticsOfIncreasingSample();
        }
        public static void PrintStats(BaseStatistics webStats)
        {
            Console.WriteLine("!!! Mean: " + webStats.Mean);
            Console.WriteLine("!!! Var: " + webStats.Variance);
            Console.WriteLine("!!! Std: " + webStats.StandardDeviation);
            Console.WriteLine();
        }

        public static void PrintDiff(double x, double y)
        {
            double diff = Math.Abs(x - y);
            double magnitude = Math.Log10(diff);

            Console.WriteLine($"!!! Diff: {diff}, Mag: {magnitude}");
        }

        public static void CalcStatisticsOfIncreasingSample()
        {
            IncreasingSampleNormalStatistics();
            IncreasingSampleWebStatistics();
        }

        public static void IncreasingSampleNormalStatistics()
        {
            IEnumerable<double> firstSamples = GenerateSamples(SAMPLE_SIZE);
            firstSamples.StandardDeviationP();
            IEnumerable<double> secondSamples = firstSamples.Concat(GenerateSamples(SAMPLE_SIZE));
            secondSamples.StandardDeviationP();
            secondSamples.Average();
            firstSamples.Average();
        }

        public static void IncreasingSampleWebStatistics()
        {
            IncrementalStatistics decrementalStatistics = new IncrementalStatistics();
            IEnumerable<double> firstSamples = GenerateSamples(SAMPLE_SIZE);
            foreach (double sample in firstSamples)
            {
                decrementalStatistics.AddSample(sample);
            }
            double stat = decrementalStatistics.StandardDeviation;
            stat = decrementalStatistics.Mean;

            IEnumerable<double> secondSamples = GenerateSamples(SAMPLE_SIZE);
            foreach (double sample in secondSamples)
            {
                decrementalStatistics.AddSample(sample);
            }
            stat = decrementalStatistics.StandardDeviation;
            stat = decrementalStatistics.Mean;
        }

        public static IEnumerable<double> GenerateSamples(int sampleSize)
        {
            for (int ii = 0; ii < sampleSize; ii++)
            {
                yield return ii;
            }
        }
    }
}
