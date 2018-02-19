/*
 * Created by SharpDevelop.
 * User: Elite
 * Date: 2/18/2018
 * Time: 9:46 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace p_usernameScraper {
	
	/// <summary>
	/// The front line of whats happening.
	/// </summary>
	public class Stage {
		
		/// <summary>
		/// The starting username.
		/// </summary>
		public string startingPoint;
		/// <summary>
		/// The starting uri.
		/// </summary>
		public Uri startingUri;
		/// <summary>
		/// The current region.
		/// </summary>
		public Region currentRegion;
		/// <summary>
		/// The scraper.
		/// </summary>
		public Scraper scraper;
		
		/// <summary>
		/// Stage constructor.
		/// </summary>
		/// <param name="startingPoint">The starting username</param>
		public Stage (string startingPoint) {
			
			this.startingPoint = startingPoint;
			Console.ResetColor();
			
		}
		
		/// <summary>
		/// The start function, essentially initialization.
		/// </summary>
		/// <returns>Returns this current stage. Useful for declaration statements.</returns>
		/// <param name="region">Describes the region of the starting point.</param>
		public Stage start (Region region) {
			
			this.startingUri = getUri(startingPoint, region);
			Console.WriteLine(@"Starting URI: " + startingUri.ToString());
			this.currentRegion = region;
			
			scraper = new Scraper(this);
			// ignoreExceptions(() => scraper.scrape(startingPoint));
			scraper.scrape(startingPoint);
			
			return this;
			
		}
		
		/// <summary>
		/// Check if a username is a login username.
		/// </summary>
		/// <param name="username">The username.</param>
		/// <returns>If the username is a login username or not.</returns>
		public bool isLoginUsername (string username) {
			
			// TODO:: CONTINUE THIS
			
			return true;
			
		}
		
		/// <summary>
		/// Get a op.gg Uri from a username and a region.
		/// </summary>
		/// <param name="username">The username.</param>
		/// <param name="region">The region.</param>
		/// <returns>The uri crafted from the username and region.</returns>
		public static Uri getUri (string username, Region region) {
			
			return new Uri(@"https://" + region + @".op.gg/summoner/userName=" + username.Replace(' ', '+'));
			
		}
		
		/// <summary>
		/// Get a op.gg Uri from a username and information from this stage.
		/// </summary>
		/// <param name="username">The username.</param>
		/// <returns>The uri crafted from the username and information from this stage.</returns>
		public Uri getUri (string username) {
			
			return new Uri(@"https://" + this.currentRegion + @".op.gg/summoner/userName=" + username.Replace(' ', '+'));
			
		}
		
		/// <summary>
		/// Ignore exceptions. Usage: ignoreExceptions(() => /* Code goes here */);
		/// </summary>
		/// <param name="a">The action to ignore exceptions for.</param>
		public static void ignoreExceptions (Action a) {
			
			try {
				
				a.Invoke();
				
			}
			
			catch {
				
				
				
			}
			
		}
		
		/// <summary>
		/// Check if a string is only whitespace.
		/// </summary>
		/// <param name="data">The string.</param>
		/// <returns>Whether the string is only whitespace or not.</returns>
		public static bool isOnlyWhitespace (String data) {
			
			foreach (char c in data)	
				if (!(c == ' '))
					return false;
			
			return true;
			
		}
		
		/*
		
		/// <summary>
		/// Get the whitespace anomaly index. It is in a pattern, +2 +2 +1. Starts at 0 which is a whitespace index number so that is manually input.
		/// </summary>
		/// <param name="until">When to stop.</param>
		/// <returns>The whitespace index.</returns>
		public static int[] getWhitespaceIndex (int until) {
			
			int count = 0;
			int count0 = 0;
			int[] returnValue;
			returnValue[0] = 0;
			
			for (int i = 0; i < until; i++) {
				
				if ((count == 0) || (count == 1)) {
					
					count0 += 2;
					returnValue[i] = count0;
					
				}
				
				else if (count == 2) {
					
					
					count0 += 1;
					returnValue[i] = count0;
					count = -1;
					
				}
				
				count++;
				
			}
			
			return returnValue;
			
		}
		
		*/ // Old non-functional int[] method of getWhitespaceIndex
		
		/// <summary>
		/// Get the whitespace anomaly index. It is in a pattern, +2 +2 +1. Starts at 0 which is a whitespace index number so that is manually input.
		/// </summary>
		/// <param name="until">When to stop.</param>
		/// <returns>The whitespace index.</returns>
		public static List<int> getWhitespaceIndex (int until) {
			
			int count = 0;
			int count0 = 0;
			List<int> returnValue = new List<int>();
			returnValue.Add(0);
			
			for (int i = 0; i < until; i++) {
				
				if ((count == 0) || (count == 1)) {
					
					count0 += 2;
					returnValue.Add(count0);
					
				}
				
				else if (count == 2) {
					
					
					count0 += 1;
					returnValue.Add(count0);
					count = -1;
					
				}
				
				count++;
				
			}
			
			return returnValue;
			
		}
		
		/// <summary>
		/// Check if string contains special characters.
		/// </summary>
		/// <param name="data">The string to check.</param>
		/// <returns>Whether the string contains special characters or not.</returns>
		public static bool containsSpecialCharacters (string data) {
			
			bool returnValue = false;
			
			Regex r = new Regex("^[-'a-zA-Z]*$");
			if (r.IsMatch(data))
				returnValue = true;
			
			return returnValue;
			
		}
		
	}
	
}