using System;
using System.Collections.Generic;
using NUnit.Framework;



namespace RGBGame.Base.Testers
{
	[TestFixture]
	class MatrixTester
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

			var m1 = new Matrix<int>( goodRows1 );			
			
			var m2 = new Matrix<int>( goodRows2 );
						
			Assert.Catch<ArgumentException>( () => { var m = new Matrix<int>( badRows1 ); } );
			Assert.Catch<ArgumentException>( () => { var m = new Matrix<int>( badRows2 ); } );
			Assert.Catch<ArgumentException>( () => { var m = new Matrix<int>( badRows3 ); } );
		}

		[Test]
		public void TestSize1()
		{
			var m1 = new Matrix<int>( goodRows1 );
			Assert.AreEqual( 3, m1.Height );
			Assert.AreEqual( 4, m1.Width );
		}

		[Test]
		public void TestSize2()
		{
			var m2 = new Matrix<int>( goodRows2 );
			Assert.AreEqual( 5, m2.Height );
			Assert.AreEqual( 2, m2.Width );
		}

		[Test]
		[TestCase( 59, 3, 2 )]
		[TestCase( 28, 2, 4 )]
		[TestCase( 47, 2, 2 )]
		[TestCase( 5, 2, 1 )]
		public void TestIndexerGetterForCorrectIndices1( int expected, int i, int j )
		{
			Matrix<int> m1 = new Matrix<int>( goodRows1 );

			Assert.AreEqual( expected, m1[ i, j ] );
		}

		void ChangeMatrixByIndexerSetter1( Matrix< int > m, int i, int j, int value )
		{
			m[ i, j ] = value;
		}

		[Test]
		[TestCase( 1, 2, 50 )]
		[TestCase( 3, 4, 100 )]
		[TestCase( 2, 2, 200 )]
		public void TestIndexerSetterForCorrectIndices1( int i, int j, int value )
		{
			Matrix<int> m1 = new Matrix<int>( goodRows1 );

			ChangeMatrixByIndexerSetter1( m1, i, j, value );

			Assert.AreEqual( m1[ i, j ], value );
		}

		[Test]
		[TestCase( 4, 2 )]
		[TestCase( 3, 5 )]
		[TestCase( 0, 2 )]
		[TestCase( 1, 0 )]
		[TestCase( 0, 0 )]
		public void TestIndexerForIncorrectIndices1( int i, int j )
		{
			Matrix<int> m1 = new Matrix<int>( goodRows1 );

			Assert.Catch<ArgumentOutOfRangeException>( () => { m1[ i, j ] = 51; } );
			Assert.Catch<ArgumentOutOfRangeException>( () => { m1[ i, j ] = 17; } );
			Assert.Catch<ArgumentOutOfRangeException>( () => { m1[ i, j ] = 8; } );

			Assert.Catch<ArgumentOutOfRangeException>( () => { var x = m1[ i, j ]; } );
		}

		

	}
}
