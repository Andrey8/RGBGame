using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;



namespace RGBGame.Game.Testers
{
	[TestFixture]
	class GameServiceTester
	{
		static Color[][] table1 = new Color[][] {
			new [] { Color.Red, Color.Green, Color.Red, Color.Red, Color.Red },
			new [] { Color.Green, Color.Blue, Color.Blue, Color.Red, Color.Green },
			new [] { Color.Green, Color.Red, Color.Blue, Color.Green, Color.Red }
		};

		static Color[][] table2 = new Color[][] {
			new [] { Color.Blue, Color.Green, Color.Red, Color.Blue, Color.Blue, Color.Red, Color.Green },
			new [] { Color.Red, Color.Blue, Color.Green, Color.Green, Color.Green, Color.Red, Color.Red },
			new [] { Color.Red, Color.Red, Color.Green, Color.Red, Color.Green, Color.Green, Color.Green },
			new [] { Color.Red, Color.Green, Color.Blue, Color.Blue, Color.Green, Color.Red, Color.Green },
			new [] { Color.Red, Color.Red, Color.Red, Color.Blue, Color.Red, Color.Blue, Color.Blue }
		};



		[Test]
		[TestCase( 2, 4, 1, new[] { Color.Red, Color.Green, Color.Green } )]
		[TestCase( 2, 4, 2, new[] { Color.Green, Color.Blue, Color.Red } )]
		[TestCase( 2, 4, 3, new[] { Color.Blue, Color.Blue, Color.None } )]
		[TestCase( 2, 4, 4, new[] { Color.Green, Color.None, Color.None } )]
		[TestCase( 2, 4, 5, new[] { Color.Green, Color.Red, Color.None } )]
		public void TestDoGameStep11( int i, int j, int columnIndex, Color[] expectedColumn )
		{
			var gameBoard = new GameBoard< Color >( table1 );
			var gameService = new GameService();

			gameService.DoGameStep( gameBoard, gameBoard[ i, j ] );

			var actualColumn = TestHelper.ConvertToColorsArray( gameBoard.GetColumn( columnIndex ) );

			CollectionAssert.AreEqual( actualColumn, expectedColumn );
		}

		[Test]
		public void TestPlayingSimulation1()
		{
			var gameBoard = new GameBoard<Color>( table1 );
			var gameService = new GameService();

			int steps = gameService.DoPlayingSimulation( gameBoard );

			Assert.AreEqual( 5, steps );
		}
	}
}
