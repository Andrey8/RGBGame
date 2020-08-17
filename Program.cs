using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using NUnitLite;

using RGBGame.Base;
using RGBGame.Game;



namespace RGBGame
{
    static class Program
    {
		static void DoTask()
		{
			try
			{
				var gameService = new GameService();
				string inputFilePath = "InputFile.txt";
				string outputFilePath = "OutputFile.txt";
				
				var gameBoards = gameService.ReadData( inputFilePath );

				File.WriteAllText( outputFilePath, string.Empty );

				gameService.DoPlayingSimulationAndReportInfo( gameBoards, outputFilePath );
				Console.WriteLine( "New {0} file has created or existing file has overwritten.", outputFilePath );
			}
			catch ( Exception exc )
			{
				Console.WriteLine( exc.Message );
			}
		}

        [STAThread]
        static void Main( string[] args )
        {
			//( new AutoRun() ).Execute( args );
			DoTask();
        }
    }
}
