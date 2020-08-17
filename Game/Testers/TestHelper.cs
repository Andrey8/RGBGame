using System;
using System.Collections.Generic;
using System.Linq;

using RGBGame.Base;



namespace RGBGame.Game.Testers
{
	public static class TestHelper
	{
		public static Color[] ConvertToColorsArray( TableCell<Color>[] cells )
		{
			var result = new List<Color>();
			foreach ( var cell in cells )
			{
				if ( !cell.IsEmpty )
				{
					result.Add( cell.Value );
				}
				else
				{
					result.Add( Color.None );
				}
			}

			return result.ToArray();
		}

		public static Cluster< T > GetCluster< T >( GameBoard< T > board, int i, int j )
		{
			var clusters = board.GetGeneratedClusters();
			Cluster<T> result = null;
			foreach ( var cluster in clusters )
			{
				if ( cluster.Contains( board[ i, j ] ) )
				{
					result = cluster;

					break;
				}
			}

			if ( result == null )
			{
				throw new ArgumentException( "There is no such cell in this cluster." );
			}

			return result;
		}
	}
}
