using LivrosBiblioteca.Servicos;

namespace LivrosBiblioteca;

public partial class App : Application
{
	public App ()
	{
		InitializeComponent( );

		MainPage = new AppShell( );

		DataBase.IniciarDataBase( );

		MainPage.Unloaded += MainPagseUnloaded;
	}

	private void MainPagseUnloaded ( object sender, EventArgs e ) =>
		DataBase.EncerrarDataBase( );
}
