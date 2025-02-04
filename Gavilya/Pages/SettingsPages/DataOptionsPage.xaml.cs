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
using Gavilya.Enums;
using PeyrSharp.Env;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace Gavilya.Pages.SettingsPages;

/// <summary>
/// Interaction logic for DataOptionsPage.xaml
/// </summary>
public partial class DataOptionsPage : Page
{
	public DataOptionsPage()
	{
		InitializeComponent();
	}

	private void ResetSettingsBtn_Click(object sender, RoutedEventArgs e)
	{
		if (MessageBox.Show(Properties.Resources.ResetSettingsMsg, Properties.Resources.MainWindowTitle, MessageBoxButton.YesNoCancel, MessageBoxImage.Question) == MessageBoxResult.Yes)
		{
			Definitions.Settings = new()
			{
				Language = "_default",
				IsFirstRun = false, // Default is true but there is no need to show the "Welcome" prompt to the user again.
				IsMaximized = false,
				PageId = 0,
				CurrentProfileIndex = 0,
				MakeAutoSave = true,
				AutoSaveDay = 1,
				SavePath = $@"{FileSys.AppDataPath}\Gavilya\Backups",
				DefaultGavilyaHomePage = GavilyaWindowPages.Home,
				MaxNumberRecentGamesShown = 4,
				ShowMoreUnplayedGamesRecommanded = true,
				HideSearchBar = false,
				NumberOfSearchResultsToDisplay = 3,
				FpsCounterOpacity = 1,
				UpdatesAvNotification = true,
				UnusedGameNotification = true
			};

			SettingsSaver.Save(); // Save changes

			MessageBox.Show(Properties.Resources.GavilyaNeedsRestartChanges, Properties.Resources.ResetSettings, MessageBoxButton.OK, MessageBoxImage.Information);
			Process.Start(Directory.GetCurrentDirectory() + @"\Gavilya.exe");
			Environment.Exit(0); // Quit
		}
	}
}
