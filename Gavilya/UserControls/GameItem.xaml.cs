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
using PeyrSharp.Env;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace Gavilya.UserControls;

/// <summary>
/// Logique d'interaction pour GameItem.xaml
/// </summary>
public partial class GameItem : UserControl
{
	public GameInfo GameInfo { get; set; }
	public DispatcherTimer Timer { get; set; }
	public bool IsChecked { get; set; }
	public GameItem(GameInfo gameInfo)
	{
		InitializeComponent();
		GameInfo = gameInfo;
		LoadInformations(); // Load the game's info

		Timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) }; // Define the timer
		Timer.Tick += Timer_Tick; // Add the event
	}

	bool gameStarted = false;
	private void Timer_Tick(object sender, EventArgs e)
	{
		string processName = (!string.IsNullOrEmpty(GameInfo.ProcessName)) ? GameInfo.ProcessName : System.IO.Path.GetFileNameWithoutExtension(GameInfo.FileLocation); // Get the process name

		Definitions.GameInfoPage.DisplayTotalTimePlayed((Definitions.GameInfoPage.GameInfo == null) ? GameInfo.TotalTimePlayed : Definitions.GameInfoPage.GameInfo.TotalTimePlayed); // Refresh
		Definitions.GameInfoPage2.DisplayTotalTimePlayed((Definitions.GameInfoPage2.GameInfo == null) ? GameInfo.TotalTimePlayed : Definitions.GameInfoPage2.GameInfo.TotalTimePlayed); // Refresh

		if (Global.IsProcessRunning(processName)) // If the game is running
		{
			gameStarted = true; // The game has started
			GameInfo.TotalTimePlayed += 1; // Increment the time played
		}
		else
		{
			if (gameStarted) // If the game has been started
			{
				GameSaver.Save(Definitions.Games); // Save
				if (!GameInfo.AlwaysCheckIfRunning)
				{
					Timer.Stop();
				}
			}
		}
	}

	private void LoadInformations()
	{
		GameName.Text = GameInfo.Name; // Set the name
		GameNameToolTip.Content = GameInfo.Name;
		FavIconTxt.Visibility = GameInfo.IsFavorite ? Visibility.Visible : Visibility.Hidden; // Set the favorite icon
	}

	private void GameBtn_Click(object sender, RoutedEventArgs e)
	{
		Definitions.GameInfoPage2.InitializeUI(GameInfo, this);
		Definitions.GamesListPage.GamePage.Navigate(Definitions.GameInfoPage2);
		CheckedChanged();
	}

	private void CheckedChanged()
	{
		foreach (UIElement uIElement in Definitions.GamesListPage.GameList.Children) // For each UIElement in the list
		{
			if (uIElement is GameItem gameItem) // If the UIElement is a GameItem
			{
				gameItem.GameBtn.Background = Definitions.TransparentColor; // Change the background color
				gameItem.PlayBtn.Visibility = Visibility.Hidden;
				gameItem.IsChecked = false;
			}
		}

		GameBtn.Background = new SolidColorBrush { Color = Color.FromRgb(40, 40, 60) }; // Change the background color
		PlayBtn.Visibility = Visibility.Visible; // Show
		IsChecked = true;
	}

	private void PlayBtn_Click(object sender, RoutedEventArgs e)
	{
		try
		{
			if (GameInfo.IsSteam && !Global.CanLaunchSteamGame())
			{
				return; // If the user can't launch the game, stop
			}

			if (!GameInfo.IsUWP && !GameInfo.IsSteam) // If EXE/Win32 game
			{
				Process p = new();
				p.StartInfo.WorkingDirectory = Path.GetDirectoryName(GameInfo.FileLocation);
				p.StartInfo.FileName = GameInfo.FileLocation;
				p.Start();
			}
			else
			{
				Process.Start("explorer.exe", GameInfo.FileLocation); // Start the game
			}

			GameInfo.LastTimePlayed = Sys.UnixTime; // Set the last time played
			Definitions.Games[Definitions.Games.IndexOf(GameInfo)].LastTimePlayed = GameInfo.LastTimePlayed; // Update the games
			GameSaver.Save(Definitions.Games); // Save the changes

			Timer.Start();
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, Properties.Resources.Error, MessageBoxButton.OK, MessageBoxImage.Error);
		}
	}

	private void Grid_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
	{
		PlayBtn.Visibility = Visibility.Visible; // Show
	}

	private void Grid_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
	{
		if (!IsChecked)
		{
			PlayBtn.Visibility = Visibility.Hidden; // Hide 
		}
	}
}
