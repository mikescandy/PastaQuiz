using System.Runtime.InteropServices.JavaScript;
using System.Runtime.Versioning;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Browser;
using Avalonia.ReactiveUI;
using PastaQuiz2;
using PastaQuiz2.Views;

[assembly: SupportedOSPlatform("browser")]

internal sealed partial class Program
{
    private static Task Main(string[] args) => BuildAvaloniaApp()
        .WithInterFont()
        .UseReactiveUI()
        .AfterSetup(_=> MainWindow.SoundPlayer = new SoundPlayerBrowser())
        .StartBrowserAppAsync("out");

    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>();
}

internal static partial class EmbedInterop
{
     [JSImport("globalThis.playSound")]
     public static partial void PlaySound(string url);
}

public class SoundPlayerBrowser: ISoundPlayer
{
     public void Play(string url) => EmbedInterop.PlaySound(url);

    // public void Play(string url)
    // {
    //    
    // }
}