using System;
using System.Collections.Generic;
using System.Linq;

using RGBGame.Base;



namespace RGBGame.Game
{
	public class Cluster< T >
	{
		List<TableCell<T>> cells;
		public List<TableCell<T>> Cells
		{
			get { return cells; }
		}
		readonly UnfilledTable<T> table;
		TableCell<T> markedCell;
		public TableCell<T> MarkedCell
		{
			get { return markedCell; }
		}



		public Cluster( List<TableCell<T>> cells, UnfilledTable< T > table )
		{
			this.cells = cells;
			this.table = table;
			this.markedCell = GetSearchedMarkedCell( cells, table );
		}

		public bool Contains( TableCell< T > cell )
		{
			return cells.Contains( cell );
		}



		TableCell<T> GetSearchedMarkedCell( List<TableCell<T>> cells, UnfilledTable<T> table )
		{
			int minColumnIndex = table.Width;
			var leftCells = new List<TableCell<T>>();
			foreach ( var cell in cells )
			{
				if ( cell.j < minColumnIndex )
				{
					leftCells.Clear();
					leftCells.Add( cell );

					minColumnIndex = cell.j;
				}
				else if ( cell.j == minColumnIndex )
				{
					leftCells.Add( cell );
				}
			}

			var i = leftCells.Min( cell => cell.i );

			return table[ i, minColumnIndex ];
		}
	}
}
