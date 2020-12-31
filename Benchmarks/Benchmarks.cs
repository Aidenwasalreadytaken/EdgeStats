using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using LinqStatistics;
using System;
using System.Collections.Generic;
using System.Linq;
using WebStatistics;

namespace Benchmarks
{
    [SimpleJob(RuntimeMoniker.NetCoreApp31, baseline: true)]
    [RPlotExporter]
    public class Benchmarks
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<Benchmarks>();
        }

        [Params(1000, 10000)]
        public int SAMPLE_SIZE;

        [Benchmark]
        public void IncreasingSampleNormalStatistics()
        {
            IEnumerable<double> samples = GenerateSamples(SAMPLE_SIZE);
            LinkedList<double> increasingSample = new LinkedList<double>();
            increasingSample.AddLast(0.0);
            foreach (double sample in samples)
            {
                increasingSample.AddLast(sample);
                increasingSample.StandardDeviation();
                increasingSample.Average();
            }
        }

        [Benchmark]
        public void IncreasingSampleWebStatistics()
        {
            IEnumerable<double> samples = GenerateSamples(SAMPLE_SIZE);
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

        [Benchmark]
        public void JumpingSampleNormalStatistics()
        {
            IEnumerable<double> firstSamples = GenerateSamples(SAMPLE_SIZE);
            firstSamples.StandardDeviationP();
            firstSamples.Average();
            IEnumerable<double> secondSamples = firstSamples.Concat(GenerateSamples(SAMPLE_SIZE));
            secondSamples.StandardDeviationP();
            secondSamples.Average();
        }

        [Benchmark]
        public void JumpingSampleWebStatistics()
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

        [Benchmark]
        public void FixedSampleNormalStatistics()
        {
            IEnumerable<double> firstSamples = GenerateSamples(SAMPLE_SIZE);
            firstSamples.StandardDeviationP();
            firstSamples.Average();
        }

        [Benchmark]
        public void FixedSampleWebStatistics()
        {
            IncrementalStandardDeviation decrementalStatistics = new IncrementalStandardDeviation();
            IEnumerable<double> firstSamples = GenerateSamples(SAMPLE_SIZE);
            foreach (double sample in firstSamples)
            {
                decrementalStatistics.AddSample(sample);
            }
            double stat = decrementalStatistics.StandardDeviation;
            stat = decrementalStatistics.Mean;
        }

        public IEnumerable<double> GenerateSamples(int sampleSize)
        {
            for (int ii = 0; ii < sampleSize; ii++)
            {
                yield return ii;
            }
        }
    }
}
