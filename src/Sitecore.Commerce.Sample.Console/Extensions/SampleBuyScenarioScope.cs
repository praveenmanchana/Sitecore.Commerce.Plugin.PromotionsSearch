namespace Sitecore.Commerce.Extensions
{
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    public class SampleBuyScenarioScope : IDisposable
    {
        private static int TabCount = 1;
        private Stopwatch _watch = new Stopwatch();
        private string _scenarioName;
        private bool _disposed;

        [MethodImpl(MethodImplOptions.NoInlining)]
        public SampleBuyScenarioScope()
        {
            var st = new StackTrace();
            var sf = st.GetFrame(1);

            this._scenarioName = sf.GetMethod().ReflectedType.Name;
            this._watch.Start();

            ConsoleExtensions.WriteColoredLine(ConsoleColor.DarkCyan, $"{new string('>', (TabCount++) * 2)} [Begin Buy Scenario] {this._scenarioName}");
        }

        public void Dispose()
        {
            if (this._disposed)
            {
                return;
            }

            this._watch.Stop();
            ConsoleExtensions.WriteColoredLine(ConsoleColor.DarkCyan, $"{new string('<', (--TabCount) * 2)} [End Buy Scenario] {this._scenarioName} : {this._watch.Elapsed}");
            this._disposed = true;
        }
    }
}
