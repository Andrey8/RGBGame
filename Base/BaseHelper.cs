using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace RGBGame.Base
{
	static class BaseHelper
	{
		internal static bool IsCorrectRows<T>( T[][] rows )
		{
			if ( rows.Length == 0 )
			{
				return false;
			}

			int l1 = rows[ 0 ].Length;
			for ( int i = 1; i < rows.Length; ++i )
			{
				if ( rows[ i ].Length != l1 )
				{
					return false;
				}
			}

			return true;
		}

		internal static T[][] GetRows<T>( UnfilledTable<T> table )
		{
			var result = new List<T[]>();
			for ( int i = 1; i <= table.Height; ++i )
			{
				var row = new List<T>();
				Array.ForEach( GetRow( table, i ), x => row.Add( x.Value ) );

				result.Add( row.ToArray() );
			}

			return result.ToArray();
		}

		internal static TableCell<T>[] GetRow< T >( UnfilledTable< T > table, int i )
		{
			var result = new TableCell<T>[ table.Width ];

			for ( int j = 0; j < table.Width; ++j )
			{
				result[ j ] = table[ i, j + 1 ];
			}

			return result;
		}

		internal static void ClearCell< T >( UnfilledTable< T > table, int i, int j )
		{
			table[ i, j ].Clear();
		}
	}
}
