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

namespace Gavilya.Enums;

/// <summary>
/// The pages f the <see cref="MainWindow"/>.
/// </summary>
public enum GavilyaPages
{
	/// <summary>
	/// The <see cref="Pages.GamesCardsPages"/>.
	/// </summary>
	Cards,

	/// <summary>
	/// The <see cref="Pages.RecentGamesPage"/>.
	/// </summary>
	Recent,

	/// <summary>
	/// The <see cref="Pages.GamesListPage"/>.
	/// </summary>
	List,

	/// <summary>
	/// The page is unknown.
	/// </summary>
	Underteminated
}

public enum GavilyaWindowPages
{
	/// <summary>
	/// The <see cref="Pages.HomePage"/>.
	/// </summary>
	Home,

	/// <summary>
	/// The <see cref="Pages.LibraryPage"/>.
	/// </summary>
	Library,

	/// <summary>
	/// The <see cref="Pages.ProfilePage"/>.
	/// </summary>
	Profile,

	/// <summary>
	/// The <see cref="Pages.RecentGamesPage"/>.
	/// </summary>
	Recent
}
