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
using System.Windows;

namespace Gavilya.Windows;

/// <summary>
/// Interaction logic for DescriptionWindow.xaml
/// </summary>
public partial class DescriptionWindow : Window
{
	readonly bool isFromEdit = false;
	AddGame AddGame { get; init; }
	EditGame EditGame { get; init; }
	public DescriptionWindow(string description, AddGame addGame)
	{
		InitializeComponent();
		descriptionTxt.Text = description; // Set description
		isFromEdit = false; // Is from Add
		AddGame = addGame; // Define
	}

	public DescriptionWindow(string description, EditGame editGame)
	{
		InitializeComponent();
		descriptionTxt.Text = description; // Set description
		isFromEdit = true; // Is from edit
		EditGame = editGame; // Define
	}

	private void Button_Click(object sender, RoutedEventArgs e)
	{
		WindowState = WindowState.Minimized; // Minimize
	}

	private void Button_Click_1(object sender, RoutedEventArgs e)
	{
		Close(); // Close the window
	}

	private void ApplyBtn_Click(object sender, RoutedEventArgs e)
	{
		if (!isFromEdit) // Is from Add
		{
			AddGame.GameDescription = descriptionTxt.Text; // Set to the description text
		}
		else
		{
			EditGame.GameDescription = descriptionTxt.Text; // Set to the description text
		}
		Close(); // Close
	}

	private void CancelBtn_Click(object sender, RoutedEventArgs e)
	{
		Close(); // Close
	}
}
