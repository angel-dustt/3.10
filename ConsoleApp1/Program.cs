using System;
using System.Diagnostics;
using System.Threading;

class Program
{
    static void Main()
    {
        Thread firstThread = new Thread(OpenNotepad);
        Thread secondThread = new Thread(RunCommand);

        firstThread.Start();
        secondThread.Start();

        firstThread.Join();
        secondThread.Join();

        Console.WriteLine("Все потоки завершены.");
    }

    static void OpenNotepad()
    {
        Console.WriteLine("[Поток 1] Запуск потока.");
        Console.WriteLine("[Поток 1] Запуск Notepad.");

        Process app = Process.Start("notepad.exe");

        Console.WriteLine("[Поток 1] Ожидание 3 секунды...");
        Thread.Sleep(3000);

        Console.WriteLine("[Поток 1] Закрытие Notepad.");

        if (!app.HasExited)
        {
            app.Kill();
            app.WaitForExit();
        }

        Console.WriteLine("[Поток 1] Поток завершён.");
    }

    static void RunCommand()
    {
        Console.WriteLine("[Поток 2] Запуск потока.");
        Console.WriteLine("[Поток 2] Запуск командной строки.");

        ProcessStartInfo settings = new ProcessStartInfo
        {
            FileName = "cmd.exe",
            Arguments = "/c dir",
            RedirectStandardOutput = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        Process command = Process.Start(settings);

        string output = command.StandardOutput.ReadToEnd();
        command.WaitForExit();

        Console.WriteLine("[Поток 2] Результат команды dir:");
        Console.WriteLine(output);
        Console.WriteLine("[Поток 2] Поток завершён.");
    }
}