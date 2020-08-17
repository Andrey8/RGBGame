using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;



namespace RGBGame.Base.Testers
{
	[TestFixture]
	class UnfilledTableTester
	{
		static int[][] goodRows1 = new int[][] {
			new [] { 3, 4, 2, 53 },
            new [] { 5, 47, 71, 28 },
            new [] { 21, 59, 8, 1 }
		};

		static int[][] goodRows2 = new int[][] {
			new [] { 3, 28 },
			new [] { 1, 43 },
			new [] { 54, 73 },
			new [] { 35, 9 },
			new [] { 2, 24 }
		};

		static UnfilledTable<int> table1 = new UnfilledTable<int>( goodRows1 );
		static UnfilledTable<int> table2 = new UnfilledTable<int>( goodRows2 );



		[Test]
		public void TestConstructorsForInts()
		{
			var badRows1 = new int[][] {
				new [] { 3, 4, 2 },
                new [] { 5, 47, 71, 28 },
                new [] { 21, 59, 1 }
			};

			var badRows2 = new int[][] {
				new [] { 3, 4, 2, 87 },
                new [] { 5, 47, 71, 28 },
                new [] { 21, 59, 1 }
			};

			var badRows3 = new int[][] {
				new [] { 3, 4, 2, 87 },
                new [] { 5, 47, 71 },
                new [] { 21, 59 }
			};

			var table1 = new UnfilledTable<int>( goodRows1 );

			var table2 = new UnfilledTable<int>( goodRows2 );

			Assert.Catch<ArgumentException>( () => { var t = new UnfilledTable<int>( badRows1 ); } );
			Assert.Catch<ArgumentException>( () => { var t = new UnfilledTable<int>( badRows2 ); } );
			Assert.Catch<ArgumentException>( () => { var t = new UnfilledTable<int>( badRows3 ); } );
		}

		[Test]
		[TestCase( 59, 3, 2 )]
		[TestCase( 8, 3, 3 )]
		[TestCase( 47, 2, 2 )]
		[TestCase( 21, 3, 1 )]
		public void TestIndexerGetterForCorrectIndices1( int expected, int i, int j )
		{
			Assert.AreEqual( expected, table1[ i, j ].Value );
		}

		static int[] ConvertToValuesArray( TableCell< int >[] cells )
		{
			int size = cells.Length;
			var result = new int[ size ];
			for ( int i = 0; i < size; ++i )
			{
				result[ i ] = cells[ i ].Value;
			}

			return result;
		}
		
		[Test]
		[TestCase( 1, new[] { 3, 5, 21 } )]
		[TestCase( 2, new[] { 4, 47, 59 } )]
		[TestCase( 3, new[] { 2, 71, 8 } )]
		[TestCase( 4, new[] { 53, 28, 1 } )]
		public void TestGetColumn( int j, int[] column )
		{
			var valuesArray = ConvertToValuesArray( table1.GetColumn( j ) );
			Assert.IsTrue( valuesArray.SequenceEqual( column ) );
		}

		static TableCell< int >[] ConvertToCellsArray( int[] values )
		{
			int size = values.Length;
			var result = new List<TableCell<int>>();
			for ( int i = 0; i < size; ++i )
			{
				result.Add( new TableCell< int >( values[ i ], 0, 0, null ) );
			}

			return result.ToArray();
		}

		[Test]
		[TestCase( 1, new[] { 10, 20, 30 } )]
		[TestCase( 3, new[] { 543, 6, 2345 } )]
		public void TestSetColumn( int j, int[] newColumn )
		{
			table1.SetColumn( j, ConvertToCellsArray( newColumn ) );

			var valuesArray = ConvertToValuesArray( table1.GetColumn( j ) );
			Assert.IsTrue( valuesArray.SequenceEqual( newColumn ) );
		}

		static bool AreEqualTables( UnfilledTable< int > t1, UnfilledTable< int > t2 )
		{
			int h1 = t1.Height;
			int w1 = t1.Width;
			int h2 = t2.Height;
			int w2 = t2.Width;

			if ( h1 != h2 || w1 != w2 )
			{
				return false;
			}

			for ( int i = 1; i <= h1; ++i )
			{
				for ( int j = 1; j <= w1; ++j )
				{
					if ( t1[ i, j ].Value != t2[ i, j ].Value )
					{
						return false;
					}
				}
			}

			return true;
		}

		[Test]
		public void TestClone()
		{
			Assert.IsTrue( AreEqualTables( table1, ( UnfilledTable< int > ) table1.Clone() ) );
			Assert.IsTrue( AreEqualTables( table2, ( UnfilledTable< int > ) table2.Clone() ) );
		}

		static bool IsEmptyColumn( int j, UnfilledTable< int > table )
		{
			for ( int i = 1; i <= table.Height; ++i )
			{
				if ( !table[ i, j ].IsEmpty )
				{
					return false;
				}
			}

			return true;
		}

		[Test]
		[TestCase( 1 )]
		[TestCase( 2 )]
		[TestCase( 3 )]
		[TestCase( 4 )]
		[TestCase( 5 )]
		public void TestClearColumn( int j )
		{
			var t1 = new UnfilledTable<int>( new[] {
                new [] { 3, 4, 2, 53, 88 },
                new [] { 5, 47, 71, 28, 13 },
                new [] { 21, 59, 8, 1, 24 },
                new [] { 43, 62, 90, 10, 81 } } );

			t1.ClearColumn( j );

			Assert.IsTrue( IsEmptyColumn( j, t1 ) );
		}

		[Test]
		public void TestConstructorOfEmptyTable()
		{
			var table = new UnfilledTable<int>( 10, 15 );

			for ( int j = 1; j <= table.Height; ++j )
			{
				Assert.IsTrue( IsEmptyColumn( j, table ) );
			}
		}


	}
}
