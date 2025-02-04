﻿/*
MIT License

Copyright (c) Léo Corporation

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE. 
*/
using Gavilya.Classes;
using Gavilya.Windows;
using Microsoft.Win32;
using System.Windows;
using System.Windows.Controls;

namespace Gavilya.Pages.FirstRunPages;

/// <summary>
/// Logique d'interaction pour ImportGamesPage.xaml
/// </summary>
public partial class ImportGamesPage : Page
{
	readonly FirstRun FirstRun;
	public ImportGamesPage(FirstRun firstRun)
	{
		InitializeComponent();
		FirstRun = firstRun; // Define
	}

	private void NextPage()
	{
		FirstRun.ChangePage(Enums.FirstRunPages.Finish); // Change page
	}

	private void ImportBtn_Click(object sender, RoutedEventArgs e)
	{
		OpenFileDialog openFileDialog = new(); // Create an OpenFileDialog
		openFileDialog.Filter = $"{Properties.Resources.GavFiles}|*.gav"; // Extension
		openFileDialog.Title = Properties.Resources.ImportGames; // Title

		if (openFileDialog.ShowDialog() ?? true) // If the user opend a file
		{
			GameSaver.Import(openFileDialog.FileName, true); // Import
		}

		FirstRun.ChangePage(Enums.FirstRunPages.SelectImportedGames); // Change page
	}

	private void SkipBtn_Click(object sender, RoutedEventArgs e)
	{
		NextPage(); // Change page
	}
}
