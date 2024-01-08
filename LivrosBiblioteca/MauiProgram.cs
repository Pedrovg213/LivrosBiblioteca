using Microsoft.Extensions.Logging;
using Mopups.Hosting;

namespace LivrosBiblioteca;
public static class MauiProgram
{
	public static MauiApp CreateMauiApp ()
	{
		MauiAppBuilder builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>( )
			.ConfigureMopups( )
			.ConfigureFonts( fonts =>
			{
				fonts.AddFont( "pala.ttf", "Palatino" );
				fonts.AddFont( "palab.ttf", "PalatinoBold" );
				fonts.AddFont( "PALAI.TTF", "PalatinoItalico" );
			} );


#if DEBUG
		builder.Logging.AddDebug( );
#endif

		return builder.Build( );
	}
}
