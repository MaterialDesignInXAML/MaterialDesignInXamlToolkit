using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;

namespace MDIX_Testing;

public partial class App : Application
{
    public static IHost? AppHost { get; private set; }

	public App()
	{
		AppHost = Host.CreateDefaultBuilder()
			.ConfigureServices((hostContext, services) =>
			{
				services.AddSingleton(services => new MainWindow()
				{
					DataContext = services.GetRequiredService<MainWindowViewModel>()
				});

				services.AddSingleton<MainWindowViewModel>();
			})
			.Build();
	}

    protected override async void OnStartup(StartupEventArgs e)
    {
		await AppHost!.StartAsync();

		var startupForm = AppHost.Services.GetRequiredService<MainWindow>();
		startupForm.Show();

        base.OnStartup(e);
    }

    protected override async void OnExit(ExitEventArgs e)
    {
		await AppHost!.StopAsync();

        base.OnExit(e);
    }
}
