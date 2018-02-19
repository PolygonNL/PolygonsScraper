/*
 * Created by SharpDevelop.
 * User: Elite
 * Date: 2/18/2018
 * Time: 9:43 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO;

namespace p_usernameScraper {
		
	/// <summary>
	/// The console application.
	/// </summary>
	internal class Program {
		
		/// <summary>
		/// The stage.
		/// </summary>
		internal static Stage st;
		
		/// <summary>
		/// The filename of the scraped usernames results text file.
		/// </summary>
		internal static string fileName;
		
		/// <summary>
		/// The main console application method.
		/// </summary>
		/// <param name="args">The arguements that would hypothetically be accessed in command prompt after the executable name.</param>
		public static void Main (string[] args) {
			
			Console.WriteLine("op.gg Username Scraper by Polygon");
			Console.WriteLine("Speed optimized");
			Console.WriteLine("NA Only");
			Console.WriteLine("Btw the timeout is only 15ms so probably put on a proxy.");
			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine("You might get out of memory exception after a while so");
			Console.WriteLine("dedicate more memory to this through task manager to avoid that.");
			Console.ResetColor();
			Console.WriteLine("Also, the GUI is pretty glitchy but you probably won't be staring at");
			Console.WriteLine("that anyways." + Environment.NewLine);
			setup();
			Console.Write(Environment.NewLine + "Starting Point: ");
			Console.ForegroundColor = ConsoleColor.Cyan;
			st = new Stage(Console.ReadLine()).start(Region.NA);
			
		}
		
		/// <summary>
		/// Setting up the text file for the results.
		/// </summary>
		private static void setup () {
			
			Console.WriteLine("Setting up text file...");
			fileName = DateTime.Today.Year + @"-" + DateTime.Today.Month + @"-" + DateTime.Today.Day + @" " + DateTime.Now.Hour + @";" + DateTime.Now.Minute + @" scraped usernames.txt";
			File.Create(fileName).Close();
			Console.WriteLine("Done.");		          
			
		}
		
	}
	
}