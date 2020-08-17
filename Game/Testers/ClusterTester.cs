using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;



namespace RGBGame.Game.Testers
{
	[TestFixture]
	class ClusterTester
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
		[TestCase( 3, 3, 2, 2 )]
		[TestCase( 2, 4, 1, 3 )]
		[TestCase( 3, 1, 2, 1 )]
		public void TestMarkedCellSearching1( int i, int j, int expectedI, int expectedJ )
		{
			var gb = new GameBoard<Color>( table1 );
			var clusters = gb.GetGeneratedClusters();
			var cluster = TestHelper.GetCluster( gb, i, j );

			var markedCell = cluster.MarkedCell;

			Assert.AreEqual( expectedI, markedCell.i );
			Assert.AreEqual( expectedJ, markedCell.j );
		}

		[Test]
		[TestCase( 3, 6, 2, 3 )]
		[TestCase( 5, 3, 2, 1 )]
		[TestCase( 4, 4, 4, 3 )]
		[TestCase( 2, 7, 1, 6 )]
		[TestCase( 5, 6, 5, 6 )]
		[TestCase( 1, 4, 1, 4 )]
		public void TestMarkedCellSearching2( int i, int j, int expectedI, int expectedJ )
		{
			var gb = new GameBoard<Color>( table2 );
			var clusters = gb.GetGeneratedClusters();
			var cluster = TestHelper.GetCluster( gb, i, j );

			var markedCell = cluster.MarkedCell;

			Assert.AreEqual( expectedI, markedCell.i );
			Assert.AreEqual( expectedJ, markedCell.j );
		}
	}
}
