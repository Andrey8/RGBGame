using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

using RGBGame.Base;



namespace RGBGame.Game
{
	public class GameService
	{
		public GameBoard<Color>[] ReadData( String inputFilePath )
		{
			var sr = new StreamReader( inputFilePath );

			string line = sr.ReadLine();
			int gamesCount = 0;
			if ( !Int32.TryParse( line, out gamesCount ) )
			{
				throw new Exception();
			}
			sr.ReadLine();

			var gameBoards = new List<GameBoard<Color>>();
			for ( int i = 1; i <= gamesCount; ++i )
			{
				var rows = new List<Color[]>();
				line = sr.ReadLine();
				while ( line != null && !line.Equals( "" ) )
				{
					var array = line.ToArray();
					rows.Add( GameHelper.ConvertToColorsArray( line ) );

					line = sr.ReadLine();
				}
				rows.Reverse();

				gameBoards.Add( new GameBoard<Color>( rows.ToArray() ) );
			}

			return gameBoards.ToArray();
		}

		public void DoPlayingSimulationAndReportInfo( GameBoard<Color>[] gameBoards, String outputFilePath )
		{
			var sw = new StreamWriter( outputFilePath );

			for ( int i = 0; i < gameBoards.Length; ++i )
			{
				var board = gameBoards[ i ];

				sw.WriteLine( "Game {0}:", i + 1 );

				var clusters = board.GetGeneratedClusters();
				int moveNumber = 1;
				int totalScore = 0;
				while ( clusters.Count >= 1 )
				{
					var biggestCluster = GameHelper.GetBiggestCluster( clusters );
					var cell = biggestCluster.MarkedCell;

					int ballsToRemove = biggestCluster.Cells.Count;
					int score = GameHelper.GetOneStepScore( ballsToRemove );
					sw.WriteLine( "Move {0} at ({1}, {2}) : removed {3} balls of color {4}, got {5} points.",
						moveNumber, cell.i, cell.j, ballsToRemove, GameHelper.ConvertToChar( cell.Value ), score );

					DoGameStep( board, cell );

					clusters = board.GetGeneratedClusters();

					++moveNumber;
					totalScore += score;
				}

				int remainingBalls = 0;
				foreach ( var cell in board.GetAllCells() )
				{
					if ( !cell.IsEmpty )
					{
						++remainingBalls;
					}
				}

				if ( remainingBalls == 0 )
				{
					totalScore += 1000;
				}

				sw.WriteLine( "Final score: {0}, with {1} balls remaining.",
					totalScore, remainingBalls );
				sw.WriteLine();
			}

			sw.Close();
		}



		public void DoGameStep( GameBoard< Color > board, TableCell<Color> cell )
		{
			board.ClearCluster( cell );
			board.ShiftBallsDown();
			board.ShiftBallsLeft();
		}

		public int DoPlayingSimulation( GameBoard<Color> board )
		{
			var clusters = board.GetGeneratedClusters();
			int stepsCount = 0;
			while ( clusters.Count >= 1 )
			{
				var biggestCluster = GameHelper.GetBiggestCluster( clusters );
				var cell = biggestCluster.MarkedCell;

				DoGameStep( board, cell );

				clusters = board.GetGeneratedClusters();

				++stepsCount;
			}

			return stepsCount;
		}
	}
}
