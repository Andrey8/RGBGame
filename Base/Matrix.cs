using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace RGBGame.Base
{
	public class Matrix< T >
	{
		int height;
		public int Height { get { return height; } }

		int width;
		public int Width { get { return width; } }

		T[ , ] data;



		public Matrix( int height, int width )
		{
			this.height = height;
			this.width = width;
			this.data = new T[ height, width ];

			for ( int i = 0; i < height; ++i )
				for ( int j = 0; j < width; ++j )
					data[ i, j ] = default( T );
		}

		public Matrix( T[][] rows )
			: this( rows.Length, rows[ 0 ].Length )
		{
			if ( !BaseHelper.IsCorrectRows( rows ) )
			{
				throw new ArgumentException( "ERROR : invalid table initialization." );
			}

			for ( int i = 0; i < height; ++i )
				for ( int j = 0; j < width; ++j )
					data[ i, j ] = rows[ i ][ j ];
		}

		public T this[ int i, int j ]
		{
			get 
			{ 
				if ( !IsInRange( i, j ) )
				{
					throw new ArgumentOutOfRangeException();
				}

				return data[ i - 1, j - 1 ];
			}
			set 
			{
				if ( !IsInRange( i, j ) )
				{
					throw new ArgumentOutOfRangeException();
				}

				data[ i - 1, j - 1 ] = value; 
			}
		}

		public override string ToString()
		{
			var result = new StringBuilder();
			for ( int i = height; i >= 1; --i )
			{
				for ( int j = 1; j <= width; ++j )
				{
					result.Append( data[ i - 1, j - 1 ].ToString() + " " );
				}

				result.Append( '\n' );
			}

			return result.ToString();
		}



		bool IsInRange( int i, int j )
		{
			return ( i >= 1 && i <= height &&
					 j >= 1 && j <= width );
		}
	}	
}
