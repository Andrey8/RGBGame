using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;



namespace RGBGame.Base.Testers
{
	[TestFixture]
	class BaseHelperTester
	{
		[Test]
		[TestCase( 2, 3, new[] { 47, 8, 28, 2 } )]
		[TestCase( 3, 4, new[] { 8, 10, 28, 24 } )]
		[TestCase( 4, 2, new[] { 43, 59, 90 } )]
		[TestCase( 1, 4, new[] { 88, 2, 28 } )]
		[TestCase( 3, 1, new[] { 59, 43, 5 } )]
		[TestCase( 2, 5, new[] { 88, 28, 24 } )]
		[TestCase( 1, 1, new[] { 5, 4 } )]
		[TestCase( 1, 5, new[] { 13, 53 } )]
		[TestCase( 4, 1, new[] { 62, 21 } )]
		[TestCase( 4, 5, new[] { 24, 10 } )]		
		public void TestCellNeighboursValues( int i, int j, int[] expectedValues )
		{
			var t1 = new UnfilledTable<int>( new[] {
                new [] { 3, 4, 2, 53, 88 },
                new [] { 5, 47, 71, 28, 13 },
                new [] { 21, 59, 8, 1, 24 },
                new [] { 43, 62, 90, 10, 81 } } );

			var neightbours = t1[ i, j ].GetNeighbours();

			var values = new List<int>();
			neightbours.ForEach( cell => values.Add( cell.Value ) );

			Assert.IsFalse( values.Except( expectedValues ).Any() );			
		}
	}
}
