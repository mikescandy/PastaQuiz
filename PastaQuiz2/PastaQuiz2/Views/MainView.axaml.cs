using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.JavaScript;
using System.Threading.Tasks;
using Avalonia.Animation;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using Avalonia.Threading;

namespace PastaQuiz2.Views;
public record Pasta(int id, string Name) {

	public string ImageFileName => $"{Name.ToLower()}.jpg";
	public string AudioFileName => $"{Name.ToLower()}.mp3";
}

public partial class MainView : UserControl
{
	

  int count = 0;

	private List<Pasta> Paste = new List<Pasta>();

	private Pasta AnswerA;
	private Pasta AnswerB;
	private Pasta AnswerC;
	private Pasta AnswerD;
	private Pasta Question;
    private int previousId=-1;

    private List<int> previous = new List<int>();
    public MainView()
	{
		InitializeComponent();

        var i = 0;
        Paste.Add(new Pasta(i++, "Anellini"));
        Paste.Add(new Pasta(i++, "Bucatini"));
        Paste.Add(new Pasta(i++, "Conchiglie"));
        Paste.Add(new Pasta(i++, "Farfalle"));
        Paste.Add(new Pasta(i++, "Fettuccine"));
        Paste.Add(new Pasta(i++, "Fusilli"));
        Paste.Add(new Pasta(i++, "Gnocchi"));
        Paste.Add(new Pasta(i++, "Lasagne"));
        Paste.Add(new Pasta(i++, "Mafaldine"));
        Paste.Add(new Pasta(i++, "Pappardelle"));
        Paste.Add(new Pasta(i++, "Penne"));
        Paste.Add(new Pasta(i++, "Ravioli"));
        Paste.Add(new Pasta(i++, "Rigatoni"));
        Paste.Add(new Pasta(i++, "Spaghetti"));
        Paste.Add(new Pasta(i++, "Stelline"));
        Paste.Add(new Pasta(i++, "Tagliatelle"));
        Paste.Add(new Pasta(i++, "Tortellini"));

        Init();
		
    }

	private void Init()
	{
        var count = 0;
        var rnd = new Random();
        var selected = new List<int>();
        while (count < 4)
        {
            var id = rnd.Next(0, Paste.Count);
            if (selected.Contains(id))
            {
            }
            else
            {
                selected.Add(id);
                count++;
            }
        }

        var notfoundq = true;
        while(notfoundq)
        {
            var questionId = selected[rnd.Next(0, 4)];

            if (!previous.Contains(questionId)) {
                Question = Paste.First(m => m.id == questionId);
                previous.Add(questionId);
                notfoundq = false;
            }
            else {

            }
        }

        PastaBtn1.Content = Paste.First(m => m.id == selected[0]).Name;
        PastaBtn2.Content = Paste.First(m => m.id == selected[1]).Name;
        PastaBtn3.Content = Paste.First(m => m.id == selected[2]).Name;
        PastaBtn4.Content = Paste.First(m => m.id == selected[3]).Name;

        PastaImg.Source = new Bitmap(AssetLoader.Open(new Uri($"avares://PastaQuiz2/Assets/Images/{Question.ImageFileName}")));
        //new Bitmap($"\"avares://PastaQuiz2/Assets/{Question.ImageFileName}");

        //PastaImg.Source = ImageSource.FromFile(Question.ImageFileName);
	}
    private bool canexecute = true;
	private async void OnCounterClicked(object sender, EventArgs e)
	{
        
		
    }

	private async void PastaBtn4_OnClick(object? sender, RoutedEventArgs e)
	{
		if (!canexecute) return;
		lock(this)
		{
			canexecute = false;
		}
		var selectedPasta = Paste.First(m => m.Name == (string)((Button)sender).Content);
        
		//using var stream = await FileSystem.OpenAppPackageFileAsync(selectedPasta.AudioFileName);
        
		// var audioPlayer = AudioManager.Current.CreatePlayer(stream);
		// audioPlayer.Volume = 1;
		// audioPlayer.Play();
 
			MainWindow.SoundPlayer.Play($"Media/{selectedPasta.AudioFileName}");
	 
	
		
		if (selectedPasta.Name == Question.Name)
		{
			if (previous.Count>5)
			{
				previous.RemoveAt(0);
			}
			
			
			// var source = new SKFileLottieImageSource();
			
			var rnd = new Random();
			var i = rnd.Next(0, 5);
			
			Dispatcher.UIThread.Post(async () => {
				// source.File = $"confetti{i}.json";
				Animation.Path=$"avares://PastaQuiz2/Assets/Animations/confetti{i}.json";
				Animation.RepeatCount = 1;
				
				Animation.IsVisible = true;
				await Task.Delay(2000);
				Animation.IsVisible = false;
				Init();
				canexecute = true;
				count++;
			});
			
			// // source.File = $"confetti{i}.json";
			// Animation.Path="avares://PastaQuiz2/Assets/Animations/confetti{i}.json";
			// Animation.RepeatCount = 1;
			//
			// Animation.IsVisible = true;
			// await Task.Delay(2000);
			// Animation.IsVisible = false;
   //          
			// Init();
		}
		else
		{
			canexecute = true;
			count++;
		}


		//	if (count == 1)
		//	CounterBtn.Text = $"Clicked {count} time";
		//else
		//	CounterBtn.Text = $"Clicked {count} times";

		//SemanticScreenReader.Announce(CounterBtn.Text);
	}
}