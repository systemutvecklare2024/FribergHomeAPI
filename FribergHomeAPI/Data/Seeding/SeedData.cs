using System.Text.Json;
using FribergHomeAPI.Models;
using Microsoft.EntityFrameworkCore;
using static FribergHomeAPI.Models.PropertyTypes;

namespace FribergHomeAPI.Data.Seeding
{
	public class SeedData
	{
		// Author: Christoffer
		public static async Task SeedAsync(ApplicationDbContext ctx)
		{
			// Order matters!
			if (!ctx.Agencies.Any())
			{
				await SeedAgencies(ctx);
			}

			if (!ctx.Muncipalities.Any())
			{
				await SeedMuncipality(ctx);
			}

			if (!ctx.Properties.Any())
			{
				await SeedProperties(ctx);
			}
		}

		// Author: Christoffer
		public static async Task SeedAgencies(ApplicationDbContext ctx)
		{
			ctx.Agencies.Add(new Models.RealEstateAgency
			{
				Name = "BengtRealtorzAB",
				Presentation = "Vi säljer osv",
				LogoUrl = "https://picsum.photos/seed/property1/800/600",
				Agents = new[] {
					new Models.RealEstateAgent {
						FirstName = "Bengt",
						LastName = "Bengtzon",
						Email = "Bengan@BengtRealtzorzAB.se",
						PhoneNumber = "112",
						ImageUrl = "https://randomuser.me/api/portraits/men/8.jpg"
					},
					new Models.RealEstateAgent {
						FirstName = "Berit",
						LastName = "Bengtzon",
						Email = "Brittan@BengtRealtzorzAB.se",
						PhoneNumber = "112",
						ImageUrl = "https://randomuser.me/api/portraits/women/7.jpg"
					},
				}
			});
			await ctx.SaveChangesAsync();
		}

		//Author: Glate
		public static async Task SeedMuncipality(ApplicationDbContext ctx)
		{
			string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "Seeding", "sveriges_kommuner.json");
			string jsonContent = File.ReadAllText(filePath);
			if (jsonContent == null)
			{
				return;
			}

			List<string> names = JsonSerializer.Deserialize<List<string>>(jsonContent) ?? [];

			if (names.Any())
			{
				foreach (string name in names)
				{
					ctx.Muncipalities.Add(new Muncipality
					{
						Name = name
					});
				}
			}
			await ctx.SaveChangesAsync();
		}

		//Author: Glate
		public static async Task SeedProperties(ApplicationDbContext ctx)
		{
			var list = new List<Property>
			{
				new Property{
				ListingPrice = 1000000,
				LivingSpace = 1000,
				SecondaryArea = 10,
				LotSize = 1000,
				Description = "Ett stort objekt, huserade tidigare hockeylag",
				NumberOfRooms = 100,
				MonthlyFee = 10000,
				OperationalCostPerYear = 100000,
				YearBuilt = 1970,
				PropertyType = PropertyType.House,
				Images = new List<PropertyImage>
				{
					new PropertyImage { ImgURL = "https://images.ohmyhosting.se/RXjFakX2ag9Aj3gQ8StqoqopjOM=/3840x1546/smart/filters:quality(85)/https%3A%2F%2Fnaidenbygg.se%2Fwp-content%2Fuploads%2F2023%2F02%2Fcoop-arena-naiden-arena3.jpg"},
					new PropertyImage {ImgURL = "https://cdn1-photos.shl.se/photos/22/05/00de89423e81ec324e758e9626a26477/thumb_2560.jpg?ixlib=js-3.8.0&auto=format&fp-debug=0&fp-y=0.5&fp-x=0.5&crop=focalpoint&fit=crop&ar=16%3A9&w=927&s=9c4b708ae814e966dd71b61dc1bab5ba" }
				},
				Address = new Address
				{
					Street = "Midgårdsvägen 4",
					PostalCode = "97334",
					City = "Luleå",
				},
				Muncipality = await ctx.Muncipalities.FirstAsync(m => m.Name == "Luleå"),
				RealEstateAgent = await ctx.Agents.FirstAsync(b => b.FirstName == "Berit")
			},
			new Property
			{
				ListingPrice = 200000,
				LivingSpace = 200,
				SecondaryArea = 20,
				LotSize = 20,
				Description = "Renoveringsobjekt. Gammalt crackhus. Perfekt för dig som vill sätta egen prägel och skapa ditt drömhem!",
				NumberOfRooms = 2,
				MonthlyFee = 2000,
				OperationalCostPerYear = 20000,
				YearBuilt = 1982,
				PropertyType = PropertyType.TownHouse,
				Images = new List<PropertyImage>
				{
					new PropertyImage { ImgURL = "https://miro.medium.com/v2/resize:fit:720/format:webp/1*yUhE4CUSnTP2e-mC3zx-qA.png"},
					new PropertyImage {ImgURL = "https://i2-prod.cornwalllive.com/news/cornwall-news/article599640.ece/ALTERNATES/s1200e/1_Newquay.jpg" }
				},
				Address = new Address
				{
					Street = "Muskotvägen 24",
					PostalCode = "18460",
					City = "Åkersberga",
				},
				Muncipality = await ctx.Muncipalities.FirstAsync(m => m.Name == "Stockholm"),
				RealEstateAgent = await ctx.Agents.FirstAsync(b => b.FirstName == "Bengt")
			}};
			ctx.Properties.AddRange(list);
			await ctx.SaveChangesAsync();
		}
	}
}