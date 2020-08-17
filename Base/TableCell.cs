using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace RGBGame.Base
{
	public class TableCell< T >
	{
		T value;
		public T Value 
		{ 
			get { return value; }
		}
		public readonly int i;
		public readonly int j;
		UnfilledTable< T > table;
		bool isEmpty;
		public bool IsEmpty { get { return isEmpty; } }



		public TableCell( int i, int j, UnfilledTable< T > table )
		{
			this.value = default( T );
			this.i = i;
			this.j = j;
			this.table = table;

			this.isEmpty = true;
		}

		public TableCell( T value, int i, int j, UnfilledTable< T > table )
		{
			this.value = value;
			this.i = i;
			this.j = j;
			this.table = table;

			this.isEmpty = false;
		}

		public List< TableCell< T > > GetNeighbours()
		{
			int height = table.Height;
			int width = table.Width;

			var result = new List<TableCell<T>>();

			if ( !IsBorder() )
			{
				result.Add( table[ i, j - 1 ] );
				result.Add( table[ i + 1, j ] );
				result.Add( table[ i, j + 1 ] );
				result.Add( table[ i - 1, j ] );

				return result;
			}
			else if ( IsBottomBorder() && !IsCorner() )
			{
				result.Add( table[ 1, j - 1 ] );				
				result.Add( table[ 1, j + 1 ] );
				if ( height >= 2 )
				{
					result.Add( table[ 2, j ] );
				}

				return result;
			}
			else if ( IsLeftBorder() && !IsCorner() )
			{
				result.Add( table[ i + 1, 1 ] );				
				result.Add( table[ i - 1, 1 ] );
				if ( width >= 2 )
				{
					result.Add( table[ i, 2 ] );
				}

				return result;
			}
			else if ( IsUpperBorder() && !IsCorner() )
			{
				result.Add( table[ height, j - 1 ] );
				result.Add( table[ height, j + 1 ] );
				if ( height >= 2 )
				{
					result.Add( table[ height - 1, j ] );
				}

				return result;
			}
			else if ( IsRightBorder() && !IsCorner() )
			{				
				result.Add( table[ i + 1, width ] );
				result.Add( table[ i - 1, width ] );
				if ( width >= 2 )
				{
					result.Add( table[ i, width - 1 ] );
				}

				return result;
			}
			else if ( IsCorner() )
			{
				if ( i == 1 && j == 1 )
				{
					if ( height >= 2 )
					{
						result.Add( table[ 2, 1 ] );
					}
					if ( width >= 2 )
					{
						result.Add( table[ 1, 2 ] );
					}

					return result;
				}
				else if ( i == height && j == 1 )
				{
					if ( height >= 2 )
					{
						result.Add( table[ height - 1, 1 ] );
					}
					if ( width >= 2 )
					{
						result.Add( table[ height, 2 ] );
					}

					return result;
				}
				else if ( i == height && j == width )
				{
					if ( height >= 2 )
					{
						result.Add( table[ height - 1, width ] );
					}
					if ( width >= 2 )
					{
						result.Add( table[ height, width - 1 ] );
					}

					return result;
				}
				else if ( i == 1 && j == width )
				{
					if ( height >= 2 )
					{
						result.Add( table[ 2, width ] );
					}
					if ( width >= 2 )
					{
						result.Add( table[ 1, width - 1 ] );
					}

					return result;
				}
			}

			throw new NotImplementedException();
		}

		public void Clear()
		{
			this.value = default( T );
			this.isEmpty = true;
		}



		bool IsBorder()
		{
			return IsLeftBorder() || IsUpperBorder() || IsRightBorder() || IsBottomBorder();
		}

		bool IsLeftBorder()
		{
			return j == 1;
		}

		bool IsUpperBorder()
		{
			return i == table.Height;
		}

		bool IsRightBorder()
		{
			return j == table.Width;
		}

		bool IsBottomBorder()
		{
			return i == 1;
		}

		bool IsCorner()
		{
			return ( ( i == 1 && j == 1 ) ||
					 ( i == table.Height && j == 1 ) ||
					 ( i == table.Height && j == table.Width ) ||
					 ( i == 1 && j == table.Width ) );
		}
	}
}
