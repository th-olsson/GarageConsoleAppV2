using static GarageConsoleApp.UI.ConsoleUI;

namespace GarageConsoleApp.UI.Menus
{
    internal static class MenuRunner
    {
        // Menu with fixed options
        internal static void Run(string title, string exitLabel,
            params (string Label, Action Action)[] options)
            => Run(title, exitLabel, () => options);

        // Menu where the options are fetched every lap
        internal static void Run(string title, string exitLabel,
            Func<(string Label, Action Action)[]> getOptions)
        {
            int selected = 0;

            while (true)
            {
                var options = getOptions();

                // Exit is always the last option
                string[] labels = new string[options.Length + 1];

                for (int i = 0; i < options.Length; i++)
                {
                    labels[i] = options[i].Label;
                }

                labels[options.Length] = exitLabel;
                int exitIndex = options.Length;

                // Keep the marker in range if the number of options has shrunk
                selected = Math.Min(selected, exitIndex);

                Title(title);
                selected = Choose(null, labels, selected, separateLast: true);

                if (selected == exitIndex)
                    return;

                Console.WriteLine();
                options[selected].Action();
            }
        }
    }
}
