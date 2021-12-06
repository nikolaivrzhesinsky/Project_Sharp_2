using System;
using System.Threading;
using System.Diagnostics;

namespace SpeedFlow
{
    class Program
    {
		static void Main()
		{
			int count = 10;			
			CountdownEvent countEventObject = new CountdownEvent(10);
			Stopwatch timer = new Stopwatch();

			timer.Start();
			for (int i = 0; i < count; i++)
			{
				var t = new Thread(ThreadFunc);
				t.Start(countEventObject);
			}
			countEventObject.Wait();
			timer.Stop();

			Console.WriteLine($"threads=: {timer.ElapsedMilliseconds}");

			
			countEventObject = new CountdownEvent(10);

			timer.Restart();
			for (int i = 0; i < count; i++)
			{
				ThreadPool.QueueUserWorkItem(ThreadFunc, countEventObject);
			}
			countEventObject.Wait();
			timer.Stop();

			Console.WriteLine($"threadpool=: {timer.ElapsedMilliseconds}");
		}

		static void ThreadFunc(object obj)
		{
			((CountdownEvent)obj).Signal();
		}

	}
}
