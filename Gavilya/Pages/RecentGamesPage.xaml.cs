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
using Gavilya.UserControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Gavilya.Pages;

/// <summary>
/// Logique d'interaction pour RecentGamesPage.xaml
/// </summary>
public partial class RecentGamesPage : Page
{
	public RecentGamesPage()
	{
		InitializeComponent();
		LoadGames();
	}

	public void LoadGames()
	{
		Dispatcher.Invoke(new Action(() =>
		{
			try
			{
				GamePresenter.Children.Clear(); // Clear the games

				if (Definitions.Games.Count > 0) // If there is games
				{
					GamePresenter.Visibility = Visibility.Visible; // Visible
					WelcomeHost.Visibility = Visibility.Collapsed; // Hide

					Dictionary<GameInfo, int> keyValuePairs = new(); // Create a dictionnary

					foreach (GameInfo gameInfo in Definitions.Games) // For each games
					{
						if (!keyValuePairs.ContainsKey(gameInfo))
						{
							keyValuePairs.Add(gameInfo, gameInfo.LastTimePlayed); // Add the game and the last time played to the dictionnary
						}
					}

					var items = from pair in keyValuePairs orderby pair.Value descending select pair; // Sort

					int c = 0;
					Definitions.HomePage.RecentBar.Children.Clear(); // Clear all items
					foreach (KeyValuePair<GameInfo, int> pair1 in items) // For each item
					{
						var gameCard = new GameCard(pair1.Key, GavilyaPages.Recent, true);
						if (Definitions.Settings.PageId != 1)
						{
							GamePresenter.Children.Add(gameCard); // Add the game 
						}
						if (c < Definitions.Settings.MaxNumberRecentGamesShown.Value)
						{
							Definitions.HomePage.RecentBar.Children.Add(new FavoriteGameCard(pair1.Key, gameCard));
						}
						c++;
					}

					if (Definitions.Games.Count >= 5) // If there are more than 5 games
					{
						// Show a notification about the least used game
						var gameInfo = items.Last().Key;

						// Get the associated game card from the game info
						var gameCard = GamePresenter.Children.OfType<GameCard>().FirstOrDefault(x => x.GameInfo == gameInfo);

						Definitions.LeastUsedGames = new(); // Create a new list
						Definitions.LeastUsedGames.Add(gameInfo, gameCard); // Add the game info and the game card
					}

					Definitions.HomePage.RecentPlaceholder.Visibility = Visibility.Collapsed; // Hide
					Definitions.HomePage.RecentBar.Visibility = Visibility.Visible;
				}
				else
				{
					GamePresenter.Visibility = Visibility.Collapsed; // Hide
					WelcomeHost.Visibility = Visibility.Visible; // Visible

					WelcomeHost.Children.Add(new WelcomeRecentGames()); // Add "WelcomeRecentGames"
					Definitions.HomePage.RecentBar.Visibility = Visibility.Collapsed; // Hide
					Definitions.HomePage.RecentPlaceholder.Visibility = Visibility.Visible; // Show
				}
			}
			catch { }
		}));
	}
}
