using System;
using System.Collections.Generic;
using System.Linq;



namespace RGBGame.Game
{
	public static class GameHelper
	{
		public static Cluster<T> GetBiggestCluster< T >( List< Cluster< T > > clusters )
		{
			var max = 0;
			var maxClusters = new List<Cluster<T>>();
			foreach ( var cluster in clusters )
			{
				if ( cluster.Cells.Count > max )
				{
					maxClusters.Clear();
					maxClusters.Add( cluster );

					max = cluster.Cells.Count;
				}
				else if ( cluster.Cells.Count == max )
				{
					maxClusters.Add( cluster );
				}
			}

			return maxClusters.First();
		}

		public static char ConvertToChar( Color color )
		{
			switch ( color )
			{
				case Color.Red:
				{
					return 'R';
				}
				case Color.Green:
				{
					return 'G';
				}
				case Color.Blue:
				{
					return 'B';
				}
				default:
				{
					throw new ArgumentException( "Wrong color to convert to char." );
				}
			}
		}

		public static int GetOneStepScore( int removedBalls )
		{
			return ( removedBalls - 2 ) * ( removedBalls - 2 );
		}

		public static Color[] ConvertToColorsArray( string row )
		{
			var colors = new List<Color>();
			foreach ( var ch in row.ToCharArray() )
			{
				switch ( ch )
				{
					case 'R':
					{
						colors.Add( Color.Red );

						break;
					}
					case 'G':
					{
						colors.Add( Color.Green );

						break;
					}
					case 'B':
					{
						colors.Add( Color.Blue );

						break;
					}
					default:
					{
						throw new ArgumentException( "The row contains unexpected character." );
					}
				}
			}

			return colors.ToArray();
		}
	}
}
