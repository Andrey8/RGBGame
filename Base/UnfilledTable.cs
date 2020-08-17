using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace RGBGame.Base
{
	public class UnfilledTable< T > : ICloneable
	{
		Matrix<TableCell<T>> data;
		public readonly int Height;
		public readonly int Width;
		


		public UnfilledTable( int height, int width )
		{
			this.data = new Matrix< TableCell< T > >( height, width );
			this.Height = height;
			this.Width = width;

			for ( int i = 1; i <= Height; ++i )
				for ( int j = 1; j <= Width; ++j )
					data[ i, j ] = new TableCell<T>( i, j, this );
		}

		public UnfilledTable( T[][] rows )
			: this( rows.Length, rows[ 0 ].Length )
		{
			if ( !BaseHelper.IsCorrectRows( rows ) )
			{
				throw new ArgumentException( "ERROR : invalid table initialization." );
			}

			for ( int i = 1; i <= Height; ++i )
				for ( int j = 1; j <= Width; ++j )
					data[ i, j ] = new TableCell< T >( rows[ i - 1 ][ j - 1 ], i, j, this );
		}
		
		public TableCell<T> this[ int i, int j ]
		{
			get
			{
				if ( !IsInRange( i, j ) )
				{
					throw new ArgumentOutOfRangeException();
				}

				return data[ i, j ];
			}
			set
			{
				if ( !IsInRange( i, j ) )
				{
					throw new ArgumentOutOfRangeException();
				}

				data[ i, j ] = value;
			}
		}

		public List<TableCell<T>> GetAllCells()
		{
			var result = new List<TableCell<T>>();
			for ( int i = 1; i <= data.Height; ++i )
			{
				for ( int j = 1; j <= data.Width; ++j )
				{
					result.Add( data[ i, j ] );
				}
			}

			return result;
		}
		
		public virtual object Clone()
		{
			var result = new UnfilledTable<T>( Height, Width );
			for ( int i = 1; i <= Height; ++i )
			{
				for ( int j = 1; j <= Width; ++j )
				{
					var cell = data[ i, j ];
					if ( !cell.IsEmpty )
					{
						result[ i, j ] = new TableCell<T>( cell.Value, i, j, result );
					}
					else
					{
						result[ i, j ] = new TableCell<T>( i, j, result );
					}					
				}
			}

			return result;
		}				

		public void ClearColumn( int j )
		{
			for ( int i = 1; i <= Height; ++i )
			{
				data[ i, j ].Clear();
			}
		}

		public TableCell< T >[] GetColumn( int j )
		{
			var result = new List< TableCell<T> >();
			for ( int i = 1; i <= Height; ++i )
			{
				result.Add( this[ i, j ] );
			}

			return result.ToArray();
		}
				
		public void SetColumn( int j, TableCell< T >[] newColumn )
		{
			for ( int i = 1; i <= Height; ++i )
			{
				var cell = newColumn[ i - 1 ];
				if ( !cell.IsEmpty )
				{
					this[ i, j ] = new TableCell<T>( cell.Value, i, j, this );
				}
				else
				{
					this[ i, j ] = new TableCell<T>( i, j, this );
				}
			}

			//var column1 = this.GetColumn( 1 );
		}

		public bool IsEmpty()
		{
			for ( int i = 1; i <= Height; ++i )
			{
				for ( int j = 1; j <= Width; ++j )
				{
					if ( !data[ i, j ].IsEmpty )
					{
						return false;
					}
				}
			}

			return true;
		}



		bool IsInRange( int i, int j )
		{
			return ( i >= 1 && i <= Height &&
					 j >= 1 && j <= Width );
		}
	}
}
