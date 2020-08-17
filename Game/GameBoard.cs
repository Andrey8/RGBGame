using System;
using System.Collections.Generic;
using System.Linq;

using RGBGame.Base;



namespace RGBGame.Game
{
    public class GameBoard< T > : UnfilledTable< T >
    {
        public GameBoard( int height, int width ) : base( height, width )
        {

        }

		public GameBoard( T[][] rows ) : base( rows )
		{

		}
		

		        
		public void ShiftBallsDown()
        {
			for ( int j = 1; j <= Width; ++j )
			{
				var indices = new List< int >();
				for ( int i = 1; i <= Height; ++i )
				{					
					if ( !base[ i, j ].IsEmpty )
					{
						indices.Add( i );
					}
				}

				if ( indices.Count == 0 )
				{
					continue;
				}

				var newColumn = new TableCell< T > [ Height ];
				for ( int i = 1; i <= indices.Count; ++i )
				{
					T value = base[ indices[ i - 1 ], j ].Value;
					newColumn[ i - 1 ] = new TableCell< T >( value, i, j, this );
				}
				for ( int i = indices.Count + 1; i <= Height; ++i )
				{
					newColumn[ i - 1 ] = new TableCell< T >( i, j, this );
				}

				SetColumn( j, newColumn );
			}
        }

        public void ShiftBallsLeft()
        {
			var indices = new List< int >();
			for ( int j = 1; j <= base.Width; ++j )
			{
				if ( !IsColumnEmpty( j ) )
				{
					indices.Add( j );
				}
			}

			var cloneTable = ( UnfilledTable< T > ) base.Clone();
			for ( int j = 1; j <= indices.Count; ++j )
			{
				this.SetColumn( j, cloneTable.GetColumn( indices[ j - 1 ] ) );
			}

			for ( int j = indices.Count + 1; j <= Width; ++j )
			{
				ClearColumn( j );
			}
        }
        
        public List< Cluster< T > > GetGeneratedClusters()
        {
            var clustered = new HashSet<TableCell< T >>();

            var result = new List< Cluster< T > >();
            foreach ( var startCell in base.GetAllCells() )
            {
				if ( startCell.IsEmpty )
				{
					continue;
				}

				T startColor = startCell.Value;
				
				if ( clustered.Contains( startCell ) )
				{
				    continue;
				}
				
				var marked = new HashSet<TableCell< T >>();
				marked.Add( startCell );
				var cellsLevel = new HashSet<TableCell<T>>();
				cellsLevel.Add( startCell );
				while ( cellsLevel.Count != 0 )
				{
				    var tempCells = new HashSet<TableCell<T>>();
				    foreach ( var cell in cellsLevel )
				    {
				        foreach ( var neighbour in cell.GetNeighbours() )
				        {
				            if ( !neighbour.IsEmpty && neighbour.Value.Equals( startColor ) )
				            {
				                tempCells.Add( neighbour );
				            }
				        }
				    }
				
				    tempCells.ExceptWith( marked );
				    cellsLevel = tempCells;
				    marked.UnionWith( cellsLevel );
				}
				
				clustered.UnionWith( marked );
				if ( marked.Count >= 2 )
				{
					result.Add( new Cluster< T >( marked.ToList(), this ) );
				}
            }

            return result;
        }
		
		public void ClearCluster( TableCell< T > clusterCell )
		{
			var clusters = GetGeneratedClusters();
			Cluster< T > clusterToClear = null;
			foreach ( var cluster in clusters )
			{
				if ( cluster.Contains( clusterCell ) )
				{
					clusterToClear = cluster;

					break;
				}
			}

			if ( clusterToClear == null )
			{
				throw new ArgumentException( "There is no such cell in this cluster." );
			}

			foreach ( var cell in clusterToClear.Cells )
			{
				cell.Clear();
			}
		}



		void ShiftColumnLeftAndLeaveEmpty( int j )
		{
			for ( int i = 1; i <= base.Height; ++i )
			{
				base[ i, j - 1 ] = base[ i, j ];
			}

			ClearColumn( j );
		}

		bool IsColumnEmpty( int j )
		{			
			for ( int i = 1; i <= base.Height; ++i )
			{
				if ( !base[ i, j ].IsEmpty )
				{
					return false;
				}
			}

			return true;
		}
    }
}
