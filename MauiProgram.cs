using Microsoft.Extensions.Logging;

namespace MauiAppMinhasCompras;

public static class MauiProgram
{
    // Método principal que inicializa e configura o nosso aplicativo .NET MAUI
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>() // Registra a nossa classe App como a aplicação principal
            .ConfigureFonts(fonts =>
            {
                // Registra as fontes personalizadas para o app
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        // Configuração de logs para depuração enquanto estivermos desenvolvendo (Modo DEBUG)
#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
