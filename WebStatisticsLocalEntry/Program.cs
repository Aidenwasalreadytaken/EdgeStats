﻿using LinqStatistics;
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
            CalcStatisticsOfJumpingSample();
            CalcStatisticsOfIncreasingSample();
        }
        public static void PrintStats(BaseStandardDeviation webStats)
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
            IEnumerable<double> samples = GenerateSamples((int)Math.Sqrt(SAMPLE_SIZE));
            LinkedList<double> increasingSample = new LinkedList<double>();
            increasingSample.AddLast(0.0);
            foreach (double sample in samples)
            {
                increasingSample.AddLast(sample);
                increasingSample.StandardDeviation();
                increasingSample.Average();
            }
        }

        public static void IncreasingSampleWebStatistics()
        {
            IEnumerable<double> samples = GenerateSamples((int)Math.Sqrt(SAMPLE_SIZE));
            IncrementalStandardDeviation stats = new IncrementalStandardDeviation();
            stats.AddSample(0.0);
            double stat;
            foreach (double sample in samples)
            {
                stats.AddSample(sample);
                stat = stats.Mean;
                stat = stats.StandardDeviation;
            }
        }

        public static void CalcStatisticsOfJumpingSample()
        {
            JumpingSampleNormalStatistics();
            JumpingSampleWebStatistics();
        }

        public static void JumpingSampleNormalStatistics()
        {
            IEnumerable<double> firstSamples = GenerateSamples(SAMPLE_SIZE);
            firstSamples.StandardDeviationP();
            firstSamples.Average();
            IEnumerable<double> secondSamples = firstSamples.Concat(GenerateSamples(SAMPLE_SIZE));
            secondSamples.StandardDeviationP();
            secondSamples.Average();
        }

        public static void JumpingSampleWebStatistics()
        {
            IncrementalStandardDeviation decrementalStatistics = new IncrementalStandardDeviation();
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
