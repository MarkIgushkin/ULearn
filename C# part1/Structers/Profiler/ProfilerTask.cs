using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Profiling
{
	public class ProfilerTask : IProfiler
	{
		public List<ExperimentResult> Measure(IRunner runner, int repetitionsCount)
		{
            var result = new List<ExperimentResult>();
            foreach (var size in Constants.FieldCounts)
            {
                var classResult = GetTimeResult(runner, true, size, repetitionsCount);
                var structResult = GetTimeResult(runner, false, size, repetitionsCount);
                result.Add(new ExperimentResult(size, classResult, structResult));
            }
            return result;
		}

        public double GetTimeResult(IRunner runner, bool isClass, int size, int count)
        {
            var timer = new Stopwatch();
            runner.Call(isClass, size, 1);
            timer.Start();
            runner.Call(isClass, size, count);
            timer.Stop();
            var objectTime = (double)timer.ElapsedMilliseconds / count;
            return objectTime;
        }
    }
}