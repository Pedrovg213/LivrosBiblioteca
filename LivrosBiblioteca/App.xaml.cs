﻿using System.Runtime.InteropServices;

namespace LivrosBiblioteca;

public partial class App : Application
{
	public App ()
	{
		InitializeComponent( );

		MainPage = new AppShell( );
	}
}
