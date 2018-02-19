/*
 * Created by SharpDevelop.
 * User: Elite
 * Date: 2/18/2018
 * Time: 10:00 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Net;
using HtmlAgilityPack;
using System.Collections.Generic;
using System.Linq;

namespace p_usernameScraper {
	
	/// <summary>
	/// Where the account name scraping happens.
	/// </summary>
	public class Scraper {	
		
		/// <summary>
		/// The stage that provides most of the information we need.
		/// </summary>
		protected Stage stage;
		
		/// <summary>
		/// The summoners scraped with this scraper.
		/// </summary>
		private List<string> summonersScraped = new List<string>();
		
		/// <summary>
		/// The public value of total amount of usernames scraped.
		/// </summary>
		public long totalScraped {
			
			get { return this.amountScraped; }
			
		}
		
		/// <summary>
		/// The private value of total amount of usernames scraped.
		/// </summary>
		private long amountScraped = 0;
		
		/// <summary>
		/// The constructor for the scraper.
		/// </summary>
		/// <param name="stage">The stage to provide information.</param>
		public Scraper (Stage stage) {
			
			this.stage = stage;
			
		}
		
		/// <summary>
		/// Scrape a username.
		/// </summary>
		/// <param name="fromUsername">The username to continue off of.</param>
		public void scrape (string fromUsername) {
			
			string htmlData = "";
			HtmlDocument doc = null;
			Uri toScrapeFromUri = stage.getUri(fromUsername);
			WebRequest rq = WebRequest.Create(toScrapeFromUri);
			rq.Method = "GET";
			List<string> latestSummonersScraped = new List<string>();
			long count = 0;
			
			using (HttpWebResponse resp = (HttpWebResponse) rq.GetResponse()) {
				
				using (StreamReader sr = new StreamReader(resp.GetResponseStream(), Encoding.UTF8)) {
				
					htmlData = sr.ReadToEnd();
					doc = new HtmlDocument();
					doc.LoadHtml(htmlData);
				
				};
				
			};
			
			doc.OptionEmptyCollection = false;
			HtmlNodeCollection hnc = doc.DocumentNode.SelectNodes("//div[@class=\"FollowPlayers Names\"]");
			string lastUsername = "";
			List<HtmlNode> htmlNodes = new List<HtmlNode>();
			List<int> whitespaceIndex = Stage.getWhitespaceIndex(100);
			
			try {
			
				foreach (HtmlNode hn in doc.DocumentNode.SelectNodes("//div[@class=\"FollowPlayers Names\"]")) {
				
        			foreach (HtmlNode hn0 in hn.SelectNodes(".//div[@class=\"SummonerName\"]")) {
					
						lastUsername = hn0.SelectSingleNode("a").InnerHtml;
					
						/* if (lastUsername.Equals(fromUsername))
							break;*/
					
						if ((!(summonersScraped.Contains(lastUsername)))) {
						
							if (!(lastUsername.Any(x => Char.IsWhiteSpace(x)))) {
								
								if (stage.isLoginUsername(lastUsername)) {
							
									latestSummonersScraped.Add(lastUsername);
									summonersScraped.Add(lastUsername);
									count++;
									amountScraped++;
								
								}
							
							}
						
						}
					
           			}
				
  				}
			}
			
			catch (Exception e) {
				
				if (summonersScraped.Count > 0) {
					
					Console.WriteLine(Environment.NewLine + "=== EXCEPTION ===");
					Console.WriteLine("Sorry, but we ran into an exception.");
					Console.WriteLine("We will try to fix it, it's likely normal.");
					Console.WriteLine("However, if the application is frozen here and you can read this,");
					Console.WriteLine("it's likely not normal and you should restart the application.");
					Console.WriteLine("Thank you, and again, I apologize for this inconvenience.");
					scrape(summonersScraped[new Random().Next(0, summonersScraped.Count)]);
					
				}
				
				else {
				
					Console.WriteLine(Environment.NewLine + "=== EXCEPTION ===");
					Console.WriteLine("Sorry, but my username scraper ran into an exception here at the scraping area.");
					Console.WriteLine("This is likely due to you selecting an inactive/incorrect username.");
					Console.WriteLine("Please chose a new username to input that is active and correct.");
					Console.WriteLine("Thank you, and again, I apologize for this inconvenience.");
					Console.WriteLine("If this is not the case, please snapshot this and send it to me.");
					Console.WriteLine("This application will now exit in 30 seconds.");
					Console.WriteLine(Environment.NewLine + e.ToString());
					Thread.Sleep(30000);
					Environment.Exit(0);
				
				}
				
			}
			
			/*
			foreach (HtmlNode hn in hnc) {
				
				hnc0 = hn.SelectNodes(".//div[@class=\"SummonerName\"]");
				
				foreach (HtmlNode hn1 in hnc0) {
					
					hn0 = hn1.SelectSingleNode("a");
					
					lastUsername = hn.LastChild.InnerHtml;
					
				}
				
			}
			*/
			
			string nextUsername = lastUsername;
			string textDocumentPrint = "";
			
			if (count == 0) {
				
				nextUsername = summonersScraped[new Random().Next(0, summonersScraped.Count)];
				
			}
				
			foreach (string s in latestSummonersScraped) {
				
				/*if ((s.Contains(" ")) || (!(stage.isLoginUsername(s))) || (!(Stage.containsSpecialCharacters(s))))
					break;*/
				
				Console.WriteLine(s);
				textDocumentPrint += (Environment.NewLine + s);
					
			}
				
			using (StreamWriter sw = File.AppendText(Program.fileName)) {
					
				// byte[] toWrite = Encoding.ASCII.GetBytes(textDocumentPrint);
				sw.Write(textDocumentPrint);
				// fs.Close(); // TODO:: IDK IF THIS IS REQUIRED I DONT THINK SO IN FACT I VERY HIGHLY DOUBT IT IS SO IM COMMENTING IT OUT
					
			};
			
			Console.WriteLine("=================================");
			Console.WriteLine("TOTAL USERNAMES SCRAPED: " + amountScraped + (amountScraped < 10000 ? "." : "!"));
			Console.WriteLine("=================================");
			Console.WriteLine("NULLED: POLYGONNL");
			Console.WriteLine("=================================");
			
			Thread.Sleep(15);
			
			scrape(nextUsername);
			
		}
		
	}
	
}