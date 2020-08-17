using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

using RGBGame.Base;



namespace RGBGame.Game.Testers
{
	struct Position
	{
		int i;
		int j;

		internal Position( int i, int j )
		{
			this.i = i;
			this.j = j;
		}
	}

	class DistinctPositionsArray
	{
		Position[] positions;

		internal DistinctPositionsArray( Position[] positions )
		{
			if ( positions.Length != positions.Distinct().Count() )
			{
				throw new ArgumentException();
			}

			this.positions = positions;
		}

		public override bool Equals( object other )
		{
			return !positions.Except( ( ( DistinctPositionsArray ) other ).positions ).Any();
		}

		public override int GetHashCode()
		{
			return positions.GetHashCode();
		}
	}



	[TestFixture]
	class GameBoardTester
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

		static GameBoard<Color> gb1 = new GameBoard<Color>( table1 );
		static GameBoard<Color> gb2 = new GameBoard<Color>( table2 );

		[Test]
		public void TestConstructors()
		{
			Assert.DoesNotThrow( () => { new GameBoard<Color>( 10, 15 ); } );
			Assert.DoesNotThrow( () => { new GameBoard<Color>( table1 ); } );
		}

		static Position GetPosition( TableCell< Color > cell )
		{
			if ( !cell.IsEmpty )
			{
				return new Position( cell.i, cell.j );
			}

			return new Position( 0, 0 );
		}

		static Position[] GetPositionsArray( Cluster< Color > cluster )
		{
			var result = new List<Position>();
			cluster.Cells.ForEach( x => result.Add( GetPosition( x ) ) );

			return result.ToArray();
		}

		

		static DistinctPositionsArray[] GetConvertedPositionArrays( Position[][] data )
		{
			var result = new List< DistinctPositionsArray >();
			Array.ForEach( data, x => result.Add( new DistinctPositionsArray( x ) ) );

			return result.ToArray();
		}

		[Test]
		public void TestClustersGenerating1()
		{
			var clusters = gb1.GetGeneratedClusters();

			var actual = new List<Position[]>();
			clusters.ForEach( x => actual.Add( GetPositionsArray( x ) ) );

			var expected = new Position[][] {
				new [] { new Position( 2, 1 ), new Position( 3, 1 ) },
				new [] { new Position( 2, 2 ), new Position( 2, 3 ), new Position( 3, 3 ) },
				new [] { new Position( 1, 3 ), new Position( 1, 4 ), new Position( 1, 5 ), new Position( 2, 4 ) }
			};

			Assert.AreEqual( 3, clusters.Count );
			CollectionAssert.AreEquivalent( expected, actual );			
		}

		[Test]
		public void TestClustersGenerating2()
		{
			var clusters = gb2.GetGeneratedClusters();

			var actual = new List<Position[]>();
			clusters.ForEach( x => actual.Add( GetPositionsArray( x ) ) );

			var expected = new Position[][] {
				new [] { new Position( 2, 1 ), new Position( 3, 1 ), new Position( 3, 2 ), new Position( 4, 1 ), new Position( 5, 1 ), new Position( 5, 2 ), new Position( 5, 3 ) },
				new [] { new Position( 4, 4 ), new Position( 4, 3 ), new Position( 5, 4 ) },
				new [] { new Position( 1, 4 ), new Position( 1, 5 ) },
				new [] { new Position( 5, 6 ), new Position( 5, 7 ) },
				new [] { new Position( 1, 6 ), new Position( 2, 6 ), new Position( 2, 7 ) },
				new [] { new Position( 3, 5 ), new Position( 2, 5 ), new Position( 2, 4 ), new Position( 2, 3 ), new Position( 3, 3 ), new Position( 4, 5 ), new Position( 3, 6 ), new Position( 3, 7 ), new Position( 4, 7 ) }
			};
			
			Assert.AreEqual( 6, clusters.Count );

			var array1 = GetConvertedPositionArrays( expected );
			var array2 = GetConvertedPositionArrays( actual.ToArray() );
			CollectionAssert.AreEquivalent( array1, array2 );
		}

		static void ClearCellInGameBoard( int i, int j, GameBoard< Color > gb )
		{
			gb[ i, j ] = new TableCell<Color>( i, j, gb );
		}

		[Test]
		[TestCase( 1, new[] { Color.Red, Color.Green, Color.None } )]
		[TestCase( 2, new[] { Color.Blue, Color.Red, Color.None } )]
		[TestCase( 3, new[] { Color.Red, Color.Blue, Color.None } )]
		[TestCase( 4, new[] { Color.Red, Color.None, Color.None } )]
		[TestCase( 5, new[] { Color.Red, Color.None, Color.None } )]
		public void TestShiftBallsDown1( int j, Color[] expected )
		{
			var gb = new GameBoard< Color >( table1 );

			ClearCellInGameBoard( 2, 1, gb );
			ClearCellInGameBoard( 1, 2, gb );
			ClearCellInGameBoard( 2, 3, gb );
			ClearCellInGameBoard( 2, 4, gb );
			ClearCellInGameBoard( 3, 4, gb );
			ClearCellInGameBoard( 1, 5, gb );
			ClearCellInGameBoard( 2, 5, gb );

			gb.ShiftBallsDown();

			var actual = TestHelper.ConvertToColorsArray( gb.GetColumn( j ) );
			
			CollectionAssert.AreEqual( actual, expected );			
		}

		[Test]
		[TestCase( new[] { 2, 4 }, 1, new[] { Color.Red, Color.Green, Color.Green } )]
		[TestCase( new[] { 2, 4 }, 2, new[] { Color.Red, Color.Blue, Color.Blue } )]
		[TestCase( new[] { 2, 4 }, 3, new[] { Color.Red, Color.Green, Color.Red } )]
		[TestCase( new[] { 2, 4 }, 4, new[] { Color.None, Color.None, Color.None } )]
		[TestCase( new[] { 2, 4 }, 5, new[] { Color.None, Color.None, Color.None } )]
		public void TestShiftBallsLeft1( int[] indicesToClear, int j, Color[] expected )
		{
			var gb = new GameBoard<Color>( table1 );

			foreach ( int index in indicesToClear )
			{
				gb.ClearColumn( index );
			}

			gb.ShiftBallsLeft();

			var actual = TestHelper.ConvertToColorsArray( gb.GetColumn( j ) );

			CollectionAssert.AreEqual( actual, expected );
		}
		
		[Test]
		[TestCase( 1, new[] { Color.Red, Color.Green, Color.Green } )]
		[TestCase( 2, new[] { Color.Green, Color.None, Color.Red } )]
		[TestCase( 3, new[] { Color.Red, Color.None, Color.None } )]
		[TestCase( 4, new[] { Color.Red, Color.Red, Color.Green } )]
		[TestCase( 5, new[] { Color.Red, Color.Green, Color.Red } )]
		public void TestClearCluster11( int j, Color[] expected )
		{
			var gb = new GameBoard<Color>( table1 );

			gb.ClearCluster( gb[ 2, 3 ] );

			var actual = TestHelper.ConvertToColorsArray( gb.GetColumn( j ) );

			CollectionAssert.AreEqual( actual, expected );
		}

		[Test]
		[TestCase( 1, new[] { Color.Red, Color.Green, Color.Green } )]
		[TestCase( 2, new[] { Color.Green, Color.Blue, Color.Red } )]
		[TestCase( 3, new[] { Color.None, Color.Blue, Color.Blue } )]
		[TestCase( 4, new[] { Color.None, Color.None, Color.Green } )]
		[TestCase( 5, new[] { Color.None, Color.Green, Color.Red } )]
		public void TestClearCluster12( int j, Color[] expected )
		{
			var gb = new GameBoard<Color>( table1 );

			gb.ClearCluster( gb[ 2, 4 ] );

			var actual = TestHelper.ConvertToColorsArray( gb.GetColumn( j ) );

			CollectionAssert.AreEqual( actual, expected );
		}
	}
}
