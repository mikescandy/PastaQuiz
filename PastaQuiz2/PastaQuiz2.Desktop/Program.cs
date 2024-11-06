using System;
using Avalonia;
using Avalonia.ReactiveUI;
using PastaQuiz2.Views;

namespace PastaQuiz2.Desktop;

sealed class Program
{
    // Initialization code. Don't use any Avalonia, third-party APIs or any
    // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
    // yet and stuff might break.
    [STAThread]
    public static void Main(string[] args) => BuildAvaloniaApp()
        .StartWithClassicDesktopLifetime(args);

    // Avalonia configuration, don't remove; also used by visual designer.
    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .WithInterFont()
            .LogToTrace()
            .AfterSetup(_=> MainWindow.SoundPlayer = new SoundPlayerDesktop())
            .UseReactiveUI();
}

public class SoundPlayerDesktop: ISoundPlayer
{
    public void Play(string url)
    {
        
    }
}