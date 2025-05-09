using System.Text.Json;
using FribergHomeAPI.Models;
using Microsoft.EntityFrameworkCore;
using static FribergHomeAPI.Models.PropertyTypes;
using Microsoft.AspNetCore.Identity;
using FribergHomeAPI.Constants;
using Microsoft.Identity.Client.Extensions.Msal;
using System.Reflection;

namespace FribergHomeAPI.Data.Seeding
{
	public class SeedData
	{
		// Author: Christoffer
		public static async Task SeedAsync(ApplicationDbContext ctx, RoleManager<IdentityRole> roleManager, UserManager<ApiUser> userManager)
		{
			await IdentitySeeder.SeedAsync(ctx, roleManager, userManager);

			// Order matters!
			if (!ctx.Agencies.Any())
			{
				await SeedAgencies(ctx, userManager);
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
		public static async Task SeedAgencies(ApplicationDbContext ctx, UserManager<ApiUser> userManager)
		{
			await using var transaction = await ctx.Database.BeginTransactionAsync();

			try
			{
				//BengtRealtorzAB
				var bengt = new Models.RealEstateAgent
				{
					FirstName = "Bengt",
					LastName = "Bengtzon",
					Email = "Bengan@BengtRealtzorzAB.se",
					PhoneNumber = "112",
					ImageUrl = "https://randomuser.me/api/portraits/men/8.jpg",
					ApiUserId = "0c64a282-46e7-437d-b36c-60a7c6d71a68"
				};

				await IdentitySeeder.CreateUser(new IdentitySeeder.NewUser(
					Id: bengt.ApiUserId,
					Email: bengt.Email,
					UserName: bengt.Email,
					FirstName: bengt.FirstName,
					LastName: bengt.LastName,
					Password: "Bengt123!"), ApiRoles.Agent, userManager);

				var berit = new Models.RealEstateAgent
				{
					FirstName = "Berit",
					LastName = "Bengtzon",
					Email = "Brittan@BengtRealtzorzAB.se",
					PhoneNumber = "112",
					ImageUrl = "https://randomuser.me/api/portraits/women/7.jpg",
					ApiUserId = "6148f50d-a317-413d-99fc-0c3a3680eb62"
				};

				await IdentitySeeder.CreateUser(new IdentitySeeder.NewUser(
					Id: berit.ApiUserId,
					Email: berit.Email,
					UserName: berit.Email,
					FirstName: berit.FirstName,
					LastName: berit.LastName,
					Password: "Berit123!"), ApiRoles.Agent, userManager);


				ctx.Agencies.Add(new Models.RealEstateAgency
				{
					Name = "BengtRealtorzAB",
					Presentation = "Vi säljer osv",
					LogoUrl = "https://picsum.photos/seed/property1/800/600",
					Agents = new[] {
						bengt,
						berit,
					}
				});

				//ChristRealtorsAB
				var christer = new Models.RealEstateAgent
				{
					FirstName = "Christer",
					LastName = "Christersson",
					Email = "christer@ChristRealtors.se",
					PhoneNumber = "911",
					ImageUrl = "https://randomuser.me/api/portraits/men/82.jpg",
					ApiUserId = "de9feeda-c5ad-43d0-83db-9dbf3b241369"
				};

				await IdentitySeeder.CreateUser(new IdentitySeeder.NewUser(
					Id: christer.ApiUserId,
					Email: christer.Email,
					UserName: christer.Email,
					FirstName: christer.FirstName,
					LastName: christer.LastName,
					Password: "Christer123!"), ApiRoles.Agent, userManager);

				var christina = new Models.RealEstateAgent
				{
					FirstName = "Christina",
					LastName = "Christersson",
					Email = "christina@ChristRealtors.se",
					PhoneNumber = "911",
					ImageUrl = "https://randomuser.me/api/portraits/women/24.jpg",
					ApiUserId = "6cc53b1a-c6ec-4d9b-a71c-00b8565d1971"
				};

				await IdentitySeeder.CreateUser(new IdentitySeeder.NewUser(
					Id: christina.ApiUserId,
					Email: christina.Email,
					UserName: christina.Email,
					FirstName: christina.FirstName,
					LastName: christina.LastName,
					Password: "Christina123!"), ApiRoles.SuperAgent, userManager);

				var christin = new Models.RealEstateAgent
				{
					FirstName = "Christin",
					LastName = "Christersson",
					Email = "christin@ChristRealtors.se",
					PhoneNumber = "911",
					ImageUrl = "https://randomuser.me/api/portraits/women/16.jpg",
					ApiUserId = "22e4b80d-a88a-428d-aef6-07f273b980dd"
				};

				await IdentitySeeder.CreateUser(new IdentitySeeder.NewUser(
					Id: christin.ApiUserId,
					Email: christin.Email,
					UserName: christin.Email,
					FirstName: christin.FirstName,
					LastName: christin.LastName,
					Password: "Christin123!"), ApiRoles.Agent, userManager);


				ctx.Agencies.Add(new Models.RealEstateAgency
				{
					Name = "ChristRealtors",
					Presentation = "Vi är christ. Vi dör för dina synder, och bostad.",
					LogoUrl = "https://godvine.com/pics/Kierra/GVA-JesusinHouse.jpg",
					Agents = new[] {
						christer,
						christina,
						christin,
					}
				});

				//Author: Tobias
				//Boporten
				var teodor = new Models.RealEstateAgent
				{
					FirstName = "Teodor",
					LastName = "Morin",
					Email = "Teodor@Boporten.se",
					PhoneNumber = "0703680058",
					ImageUrl = "https://bilder.hemnet.se/images/broker_profile_large/e9/9f/e99fea574bf6fb528cc298bd96a809c4.jpg",
					ApiUserId = "376746c3-0777-4223-aec5-2a0bc816ba76"
				};

				await IdentitySeeder.CreateUser(new IdentitySeeder.NewUser(
								Id: teodor.ApiUserId,
								Email: teodor.Email,
								UserName: teodor.Email,
								FirstName: teodor.FirstName,
								LastName: teodor.LastName,
								Password: "Teodor123!"), ApiRoles.Agent, userManager);

				var sofie = new Models.RealEstateAgent
				{
					FirstName = "Sofie",
					LastName = "Näslund",
					Email = "Sofie@Boporten.se",
					PhoneNumber = "0703869873",
					ImageUrl = "https://bilder.hemnet.se/images/broker_profile_large/94/84/94849113ae9965db565aea14e1831e1b.jpg",
					ApiUserId = "48534674-5a34-4e0e-9615-40887e3ea0f7"
				};

				await IdentitySeeder.CreateUser(new IdentitySeeder.NewUser(
								Id: sofie.ApiUserId,
								Email: sofie.Email,
								UserName: sofie.Email,
								FirstName: sofie.FirstName,
								LastName: sofie.LastName,
								Password: "Sofie123!"), ApiRoles.SuperAgent, userManager);

				var sara = new Models.RealEstateAgent
				{
					FirstName = "Sara",
					LastName = "Stenlund",
					Email = "Sara@Boporten.se",
					PhoneNumber = "0703250015",
					ImageUrl = "https://bilder.hemnet.se/images/broker_profile_large/90/27/9027434d14eccc3364235b23dc6f579f.jpg",
					ApiUserId = "7a1266cd-a1a4-425b-a734-264cd336e8a9"
				};

				await IdentitySeeder.CreateUser(new IdentitySeeder.NewUser(
								Id: sara.ApiUserId,
								Email: sara.Email,
								UserName: sara.Email,
								FirstName: sara.FirstName,
								LastName: sara.LastName,
								Password: "Sara123!"), ApiRoles.Agent, userManager);


				ctx.Agencies.Add(new Models.RealEstateAgency
				{
					Name = "Boporten",
					Presentation = "Umeås mest rekommenderade mäklare.",
					LogoUrl = "https://bilder.hemnet.se/images/broker_logo_2_2x/3c/bd/3cbda05cac9a15cdc5dcb7091b5d20fd.jpg",
					Agents = new[] {
					teodor,
					sofie,
					sara,
				}
				}
			);
				await ctx.SaveChangesAsync();
				await transaction.CommitAsync();

            } catch(Exception)
			{
				await transaction.RollbackAsync();
				throw;
			}
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

		//Author: Glate, Emelie, Fredrik
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
			},
			new Property
			{
				ListingPrice = 2595000,
				LivingSpace = 65,
				SecondaryArea = 0,
				LotSize = 2453,
				Description = "I ett av områdets allra bästa lägen, ett stenkast från havet som man når via en stig vid huset ligger detta vinterbonade fritidshus om 65 kvm med genomgående moderna ytskikt på en lättskött naturtomt om 2453 kvm.\r\nHuset har en fin planlösning med två bra sovrum, välutrustat kök, nyrenoverat badrum och stort vardagsrum med braskamin och utgång till altanen. 2018 genomgick huset en större renovering då det fick nytt falsat plåttak, ny färg på utsidan och nytt badrum med dusch, handfat och förbränningstoalett. Stor härlig altan omger huset på tre sidor och garanterar dig lata dagar i solen till ljudet av vågornas brus. Huset är även anslutet till fiber via IP-only.\r\nDen lättskötta tomten består till stor del av naturtomt med berg i dagen, lite skog men även av en plan gräsyta för lek och spel samt en del vackra prydnadsbuskar och planteringar, bland annat syrenbuskar, äppelträd, hallonbuskar, vinbär och stora rhododendron. Här finns även en gäststuga om ca 10 kvm samt ett förråd om ca 6 kvm.\r\nFrån huset når man enkelt havet och en liten \"privat\" strand och vackra klippor via en stig (300m). Här finns även en ordnad båthamn med lediga platser och klubbhus, fina klippor och en mycket vacker sandstrand inom några hundra meter från huset. I området planeras även att bygga en bastu vid havet som ska vara klar 2025. Längs havet går även roslagsleden som tar dig från Täby till Grisslehamn via vackra stränder och smultronställen. Ca 6-7 km till både sommariddyllen Grisslehamn och Älmsta som erbjuder stort utbud av restauranger, affärer, och fina utflyktsmål.\r\nVälkommen på visning vid havet!",
				NumberOfRooms = 3,
				MonthlyFee = 0,
				OperationalCostPerYear = 22486,
				YearBuilt = 1968,
				PropertyType = PropertyType.VacationHouse,
				Images = new List<PropertyImage>
				{
					new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/cc/ae/ccae4f430b48ffee5748bbe4c7d8a5f4.jpg"},
					new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/44/72/44724caff56a79a736270f54091832a1.jpg"},
					new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/1b/ce/1bceabccd1846422d422eb741dca4b7f.jpg"},
					new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/08/cd/08cd91fda57662a98b42b27a32407388.jpg"},
					new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/f2/12/f212d50dc59a89ab4d3a6285e58a32e5.jpg"},
					new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/cb/89/cb89a743bde8803d0b2f000417e6b9a7.jpg"},
					new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/44/cb/44cb4aff915a214f19214e3b2e290475.jpg"},
					new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_portrait_cut/6d/91/6d9111dee90e29f7576f8c9da1914ab8.jpg"},
					new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/cc/5d/cc5d6f9077f532c80b603c442a02c40d.jpg"},
					new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/d3/48/d3485e17931157ef504715d86367b1da.jpg"},
					new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/25/bd/25bd2a90cf88922f893baa14c55f3356.jpg"},
					new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_portrait_cut/48/fe/48feaf8c33b0fa05b92748807c3f9ce8.jpg"},
					new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/95/ca/95ca3526b06ab22d9612d74a9609c51d.jpg"},
					new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/41/7d/417df741b752bab8ac9c6c9749e17ffd.jpg"},
					new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/87/3f/873f1c3c4961c8e42f00dab44c7c989b.jpg"},
					new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/bf/9b/bf9b007959ecd09072f503f9176d89e8.jpg"},
					new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/59/d5/59d534213c971cebc0bcd41d1b6bf33e.jpg" }
				},
				Address = new Address
				{
					Street = "Flisbergen 239",
					PostalCode = "764 91",
					City = "Väddö",
				},
				Muncipality = await ctx.Muncipalities.FirstAsync(m => m.Name == "Norrtälje"),
				RealEstateAgent = await ctx.Agents.FirstAsync(b => b.FirstName == "Bengt")
			},
			new Property
			{
				ListingPrice = 4995000,
				LivingSpace = 91,
				SecondaryArea = 0,
				LotSize = 0,
				Description = "En fantastisk och karaktärsfull lägenhet med braskamin, vackra golv, högt i tak, platsbyggt kök från Roma Specialsnickeri, fina pardörrar och trevligt genomgångsljus. \r\nLägenheten om 91 m² erbjuder ljust och rymligt vardagsrum med vacker fiskbensparkett, braskamin, stort fönsterparti mot innergården och vackra pardörrar mot köket. Mycket trevligt kök med stor matplats. Två stora sovrum med fina trägolv, samt två helkaklade duschrum.\r\nPopulär och lågt belånad förening med låga månadsavgifter. Mysig gemensam innergård med utemöbler och grillar.\r\nLäget på Rådstugugränd precis intill S:t Hansplan är helt perfekt om man vill vara mitt i den pulserande innerstaden med närhet till allt, endast några minuters promenad så når man hamnen, Almedalen, Östercentrum, Stora torget m.m.\r\nEn lägenhet som absolut måste upplevas på plats!",
				NumberOfRooms = 3,
				MonthlyFee = 4334,
				OperationalCostPerYear = 13212,
				YearBuilt = 1900,
				PropertyType = PropertyType.Condo,
				Images = new List<PropertyImage>
				{
					new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/34/2e/342e3fa81a2b3bb707a89592ce0dd3cc.jpg"},
					new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/d1/ff/d1ffebdf3304fe30ab7bfa08c37b0f37.jpg"},
					new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/dd/39/dd39be373ab338bf994dbe65c376e9c3.jpg"},
					new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_portrait_cut/37/1c/371c8b3414531ea33e31eab7aa16a9ae.jpg"},
					new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/8c/1b/8c1b4a2b594cb0b40e217bedd1f75ad5.jpg"},
					new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/8a/9c/8a9c898c58481a2b00f794f3b55f4f84.jpg"},
					new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/9f/db/9fdb64b0f73d58978acaae06058721ee.jpg"},
					new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/0d/c8/0dc86f5058e9799da7cae743ed6ed389.jpg"},
					new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/91/61/9161351d409d6e09ab8944a135142a0f.jpg"},
					new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/a8/14/a81485ead8b24b7e57a40e6954de9aec.jpg"},
					new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/cd/25/cd255d631465ef55eff348f05feafcff.jpg"},
					new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/51/12/5112067f5b58f3909f925f66b18e47e8.jpg"},
					new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/9b/f0/9bf04bec0b0b914b97f74facf86eab90.jpg"},
					new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/35/2d/352dca166ba78d4b6b2d0d387a8e6be7.jpg"},
					new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_portrait_cut/28/ee/28eeada2a4154eeea8701836afa38665.jpg"},
					new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/0b/73/0b73996cf716858934b2da82157153ac.jpg"}
				},
				Address = new Address
				{
					Street = "Rådstugugränd 2",
					PostalCode = "621 56",
					City = "Visby",
				},
				Muncipality = await ctx.Muncipalities.FirstAsync(m => m.Name == "Gotland"),
				RealEstateAgent = await ctx.Agents.FirstAsync(b => b.FirstName == "Bengt")
			},
			new Property
			{
				ListingPrice = 4990000,
				LivingSpace = 72,
				SecondaryArea = 0,
				LotSize = 0,
				Description = "Välkommen till en ljus och rymlig lägenhet där den öppna planlösningen mellan kök, vardagsrum och matrum skapar en social och inbjudande atmosfär. De stora fönsterpartierna släpper in rikligt med dagsljus och ger en fantastisk utsikt över Stenstadens ståtliga takåsar, medan den generösa takhöjden och de vackra takbjälkarna från tidigt 1900-tal förstärker känslan av rymd och karaktär. Det stilrena och moderna köket smälter harmoniskt samman med vardagsrummet och matrummet, vilket ger en perfekt yta för både vardagsliv och middagar i goda vänners lag. Sovrummet är ljust och rymligt med gott om plats för en dubbelsäng och tillhörande möblemang. Förvaringsmöjligheterna är utmärkta, med en stor hall som rymmer flera garderober samt ett flexibelt utrymme med mjuk heltäckningsmatta, som idag används som klädkammare men även kan fungera som gästrum eller hemmakontor. Det helkaklade badrummet är stilrent och funktionellt, utrustat med tvättmaskin för extra bekvämlighet. \r\nDen fantastiska gemensamma takterrassen, som ligger i anslutning till lägenheten, bjuder på en enastående utsikt över staden och hamninloppet – en perfekt plats att njuta av solnedgången. Bostadsrättsföreningen är stabil och välskött med god ekonomi och erbjuder fräscha gemensamma utrymmen för medlemmarna. Här bor du i ett centralt men lugnt område med allt du behöver runt knuten – charmiga caféer, restauranger, klädbutiker och matbutiker. En perfekt kombination av stadspuls och rofylld atmosfär!",
				NumberOfRooms = 2,
				MonthlyFee = 3067,
				OperationalCostPerYear = 7536,
				YearBuilt = 1912,
				PropertyType = PropertyType.Condo,
				Images = new List<PropertyImage>
				{
					new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/9e/5a/9e5a4ac7efb7c5bfa5972ebb279c6e43.jpg"},
					new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/fb/f7/fbf7c07f2977c76a939537190644e3e9.jpg"},
					new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/dd/2e/dd2e7720157747417d14e687cef2b0fd.jpg"},
					new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/3f/8b/3f8bb8714bd8504c0fc27e6593520988.jpg"},
					new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/9d/f7/9df70e0fab24a15ae3ca80bf19f96f47.jpg"},
					new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/dc/9f/dc9fbbcb8b3980b2f3382f04cfb65071.jpg"},
					new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/3f/11/3f11ade208640a669f485735e1ebb995.jpg"},
					new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/e5/7e/e57ef3863c41d2a6bd283833fba31657.jpg"},
					new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/f8/84/f8848f1594d4d31e58dc10f6a02a225c.jpg"},
					new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/80/b9/80b922b27d387c0e320ffd65bbdca703.jpg"},
					new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/14/02/1402be516e6dbdcd4007ab72b0977977.jpg"},
					new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/6c/31/6c31400439e349328085a781bc709788.jpg"},
					new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/33/6f/336fae37f8c11fd78cbdef6c9225c867.jpg"},
					new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/7d/8f/7d8fe5c460edb4598e5595cefd56c52b.jpg"},
					new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/f6/85/f68546d22389c09ccc107663e758862a.jpg"},
					new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/86/d1/86d1f26c44ee580fb9722c39f7e036ac.jpg"},
					new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/31/8e/318e27a160ad908962770ecc9757b059.jpg"},
					new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/0d/96/0d9639a1ca9e0daaa092cd359dd27450.jpg"},
					new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/53/06/5306255f8fbf3a0a153d5bd467aff274.jpg"}
				},
				Address = new Address
				{
					Street = "Lasarettsgatan 6",
					PostalCode = "441 19",
					City = "Göteborg",
				},
				Muncipality = await ctx.Muncipalities.FirstAsync(m => m.Name == "Göteborg"),
				RealEstateAgent = await ctx.Agents.FirstAsync(b => b.FirstName == "Bengt")
			},
			new Property
			{
				ListingPrice = 3200000,
				LivingSpace = 80,
				SecondaryArea = 0,
				LotSize = 710,
				Description = "Med ett av Timmernabbens mest eftertraktade lägen hittar vi detta fantastiska sommarhus, vars genuina karaktär och naturnära materialval gör det till en plats för generationer att njuta av. Huset är byggt med omsorg och expertis av Sommarnöjen, kända för sin hantverksskicklighet och noggranna urval av naturliga material. De stora sociala ytorna är perfekta för både intima familjesammankomster och fester, där de vidsträckta skjutpartierna mot havet suddar ut gränsen mellan inne och ute. Här får du panoramautsikt över Kalmarsund, med naturens skådespel som ständig följeslagare. Sovrummen är smart planerade, där det stora sovrummet har plats för dubbelsäng och de två mindre är utrustade med platsbyggda våningssängar – allt för att optimera både komfort och funktion.\r\nTimmernabben är ett levande samhälle med en rik historia inom trävaruhandel, båtvarv och fiske, vilket ger området en genuin charm och karaktär. Här bor du med närhet till både Mönsterås, Kalmar och Oskarshamn, samtidigt som du har allt du behöver i närområdet – från barnomsorg och skola till badplats och båtplats. Med sin natursköna omgivning och sin inbjudande småskalighet erbjuder Timmernabben en trygg och trivsam miljö för både barnfamiljer och den som söker avkoppling vid havet. Här får du det bästa av två världar – en rofylld tillflykt och ändå nära till stadens bekvämligheter. För sommargästerna är det enkelt att ta sig mellan Timmernabben, Kalmar och Öland.",
				NumberOfRooms = 4,
				MonthlyFee = 0,
				OperationalCostPerYear = 27247,
				YearBuilt = 2018,
				PropertyType = PropertyType.VacationHouse,
				Images = new List<PropertyImage>
				{
					new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/1024x/6d/a6/6da65a86094554a1feb657e2575cc0c4.jpg"},
					new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/1024x/33/2e/332e130a156361caf8836bd8b9e10c16.jpg"},
					new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/1024x/2e/68/2e68d20998b94cd53bac0c92cdadb5ea.jpg"},
					new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/1024x/f2/28/f22873d6ccbe4e4d9e5452033cf9d9ab.jpg"},
					new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/1024x/7e/f9/7ef90ee162299c96f98f55c13674820f.jpg"},
					new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/1024x/5d/91/5d91ba24461b8393d2f7dc6e720d2351.jpg"},
					new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/1024x/ab/ac/abacd33d0bca61a4eefe34a220e89cf1.jpg"},
					new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/1024x/43/d1/43d1b9a19c1d3ae084150598a057db80.jpg"},
					new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/1024x/4a/fb/4afb04ea883894b85092d77c06f904ff.jpg"},
					new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/1024x/a9/c5/a9c53ba9b4b8549aade44396617ab61b.jpg"},
					new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/1024x/25/78/2578a22f57ac1ad7ea03a349047cabcb.jpg"},
					new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/1024x/1c/8c/1c8c2b7a043bedf2b26872f7aa8505ba.jpg"},
					new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/1024x/08/47/0847a0b9f2201035ea8e4da7bdd83b14.jpg"},
					new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/1024x/ce/95/ce952670aebdb7eb966af1afdab586ed.jpg"},
					new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/1024x/07/7f/077f8dc03a224a1040850f8bbb0ea097.jpg"},
					new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/1024x/54/78/54788aed157453691f1ad5a645adf7c6.jpg"},
					new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/1024x/23/58/2358da74cdd5a2462bc440631d30b8d6.jpg"},
					new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/1024x/6d/0c/6d0cdefec33b54856935c0fd2f2097a1.jpg"},
					new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/1024x/80/7a/807ad6bfd30ffc77cb6e42b919223b93.jpg"}
				},
				Address = new Address
				{
					Street = "Briggen Sifs väg 8",
					PostalCode = "384 72",
					City = "Timmernabben",
				},
				Muncipality = await ctx.Muncipalities.FirstAsync(m => m.Name == "Mönsterås"),
				RealEstateAgent = await ctx.Agents.FirstAsync(b => b.FirstName == "Bengt")
			},
			new Property
			{
				ListingPrice = 4500000,
				LivingSpace = 119,
				SecondaryArea = 0,
				LotSize = 1169,
				Description = "Denna ombyggda och renoverade 1900-talsvilla är en sällsynt pärla i utkanten av charmiga Torekov. Med baksida som bjuder på en fantastisk havsutsikt mot öppet jordbrukslandskap, garanteras avkoppling och fridfullhet. Huset har varsamt bevarats och uppdaterats med kärlek, senast av nuvarande ägare. Bostaden rymmer 4 rum och kök, fördelat på två allrum, två sovrum och två badrum, dessutom finns det en sovalkov i ovanvåningen för den som behöver fler sängplatser. Nytt och lantligt kök från 2023 med stor matplats i anslutning. Extra utrymme erbjuds i det äldre gästhuset där bara fantasin sätter gränser. Isolerad garagebyggnad som fungerar perfekt som förråd eller verkstad. Torekovs idylliska atmosfär med sneda kullerstensgator och närhet till både golfbana och havet gör detta till en dröm som både permanentboende och sommaridyll.",
				NumberOfRooms = 4,
				MonthlyFee = 0,
				OperationalCostPerYear = 19500,
				YearBuilt = 1913,
				PropertyType = PropertyType.VacationHouse,
				Images = new List<PropertyImage>
				{
					new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/ec/9a/ec9adf18f71942173122892e37df98f1.jpg"},
					new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/81/80/8180ad62b4f4bc40c8a9d0a1deb3d825.jpg"},
					new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/dc/5a/dc5a584020a5c18318fa8ae0953ab80b.jpg"},
					new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/66/ba/66babc26db741e45f191f69956826627.jpg"},
					new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/3c/af/3caf9538b676b54c0648776428312abd.jpg"},
					new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_portrait_cut/1d/33/1d332c0f33ec6182cdb5dae4c61e3fa1.jpg"},
					new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_portrait_cut/8c/0c/8c0cc41632a0bcf52d1e153aa5cf596a.jpg"},
					new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/6a/98/6a988ef6d056e490fb8a447e6f4624af.jpg"},
					new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/88/85/88853750c553ea5046e99c03ba298f82.jpg"},
					new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/a5/f5/a5f553bf708961b9c12081647bd00cee.jpg"},
					new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/fd/ae/fdae838858dacb29d5871a3f95b2fed0.jpg"},
					new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/a2/b9/a2b98820a433dc18b3248870cb70d2f7.jpg"},
					new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_portrait_cut/2a/39/2a393044ecdf31f8d1d8710977c3bfaf.jpg"},
					new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_portrait_cut/c3/b5/c3b540406d4aeef09de30449cb86f848.jpg"},
					new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_portrait_cut/e4/2c/e42cb413ed16206e4a5e4be50fcd2efb.jpg"},
					new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/e0/2b/e02b2cf447446751d4e8da896953a460.jpg"},
					new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/df/bf/dfbff690952ee0f0f085aff172cb23ca.jpg"},
					new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/4e/f5/4ef524fcfb3ff7fe021ef78a35d6c390.jpg"},
					new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/28/e2/28e21e2d1db233056aef2f2c53261b5d.jpg"},
					new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_portrait_cut/aa/15/aa150dbaf2ee6a16e79ff0e8e28a38e9.jpg"},
					new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/1f/21/1f216ba83942ccd8e199fc9191f45e2e.jpg"}
				},
				Address = new Address
				{
					Street = "Erik Staels väg 12",
					PostalCode = "302 75",
					City = "Halmstad",
				},
				Muncipality = await ctx.Muncipalities.FirstAsync(m => m.Name == "Halmstad"),
				RealEstateAgent = await ctx.Agents.FirstAsync(b => b.FirstName == "Bengt")
			},
			new Property
			{
				ListingPrice = 3975000,
				LivingSpace = 60,
				SecondaryArea = 0,
				LotSize = 400,
				Description = "Välkommen till detta stilfulla och välplanerade hem som kombinerar modern design med hög komfort! Här möts du av ljusa och luftiga ytor med öppen planlösning mellan kök, matplats och vardagsrum. Stora fönsterpartier ger ett härligt ljusinsläpp och skapar en naturlig koppling till den generösa altanen och poolområdet.\r\nHuset erbjuder flera sovrum, ett elegant badrum samt smarta förvaringslösningar. Det stilrena köket har gott om arbetsytor och moderna vitvaror – perfekt för både vardag och fest.\r\nPå tomten finns även en separat gäststuga, idealisk för övernattande gäster. Den stora altanen blir en naturlig samlingsplats och poolen gör utemiljön komplett.\r\nHär får du ett bekvämt och trivsamt boende med både stil och funktion!\r\nVälkommen att boka visning – detta hem vill du inte missa!",
				NumberOfRooms = 4,
				MonthlyFee = 0,
				OperationalCostPerYear = 21200,
				YearBuilt = 2020,
				PropertyType = PropertyType.House,
				Images = new List<PropertyImage>
				{
					new PropertyImage { ImgURL = "https://www.bjurfors.se/cdn-cgi/image/format=auto,fit=scale-down,width=1265,quality=80/contentassets/c5077d2340c747768f8fe09d8f8bc5c0/cbild5kjdooi6gvt6ka40.jpg?v=1739879385"},
					new PropertyImage { ImgURL = "https://www.bjurfors.se/cdn-cgi/image/format=auto,fit=scale-down,width=1265,quality=80/contentassets/c5077d2340c747768f8fe09d8f8bc5c0/cbild5kjdookiavt6kaas.jpg?v=1744117073"},
					new PropertyImage { ImgURL = "https://www.bjurfors.se/cdn-cgi/image/format=auto,fit=scale-down,width=1265,quality=80/contentassets/c5077d2340c747768f8fe09d8f8bc5c0/cbild5kjdoovqcvt6kbkn.jpg?v=1744117074"},
					new PropertyImage { ImgURL = "https://www.bjurfors.se/cdn-cgi/image/format=auto,fit=scale-down,width=1265,quality=80/contentassets/c5077d2340c747768f8fe09d8f8bc5c0/cbild5kjdoogr0vt6ka0l.jpg?v=1744117075"},
					new PropertyImage { ImgURL = "https://www.bjurfors.se/cdn-cgi/image/format=auto,fit=scale-down,width=1265,quality=80/contentassets/c5077d2340c747768f8fe09d8f8bc5c0/cbild5kjdooctuvt6k9jj.jpg?v=1744117077"},
					new PropertyImage { ImgURL = "https://www.bjurfors.se/cdn-cgi/image/format=auto,fit=scale-down,width=1265,quality=80/contentassets/c5077d2340c747768f8fe09d8f8bc5c0/cbild5kjdoom2mvt6kae7.jpg?v=1739879395"},
					new PropertyImage { ImgURL = "https://www.bjurfors.se/cdn-cgi/image/format=auto,fit=scale-down,width=1265,quality=80/contentassets/c5077d2340c747768f8fe09d8f8bc5c0/cbild5kjdoonsavt6kahl.jpg?v=1739879397"},
					new PropertyImage { ImgURL = "https://www.bjurfors.se/cdn-cgi/image/format=auto,fit=scale-down,width=1265,quality=80/contentassets/c5077d2340c747768f8fe09d8f8bc5c0/cbild5kjdoorrcvt6kart.jpg?v=1739879399"},
					new PropertyImage { ImgURL = "https://www.bjurfors.se/cdn-cgi/image/format=auto,fit=scale-down,width=1265,quality=80/contentassets/c5077d2340c747768f8fe09d8f8bc5c0/cbild5kjdootkgvt6kava.jpg?v=1739879401"},
					new PropertyImage { ImgURL = "https://www.bjurfors.se/cdn-cgi/image/format=auto,fit=scale-down,width=1265,quality=80/contentassets/c5077d2340c747768f8fe09d8f8bc5c0/cbild5kjdoo8k4vt6k7sk.jpg?v=1739879402"},
					new PropertyImage { ImgURL = "https://www.bjurfors.se/cdn-cgi/image/format=auto,fit=scale-down,width=1265,quality=80/contentassets/c5077d2340c747768f8fe09d8f8bc5c0/cbild5kjdoojbovt6ka7e.jpg?v=1739879403"},
					new PropertyImage { ImgURL = "https://www.bjurfors.se/cdn-cgi/image/format=auto,fit=scale-down,width=1265,quality=80/contentassets/c5077d2340c747768f8fe09d8f8bc5c0/cbild5kjdoofgevt6k9t7.jpg?v=1739879450"},
					new PropertyImage { ImgURL = "https://www.bjurfors.se/cdn-cgi/image/format=auto,fit=scale-down,width=1265,quality=80/contentassets/c5077d2340c747768f8fe09d8f8bc5c0/cbild5kjbifik15lb17u3.jpg?v=1740055608"},
					new PropertyImage { ImgURL = "https://www.bjurfors.se/cdn-cgi/image/format=auto,fit=scale-down,width=1265,quality=80/contentassets/c5077d2340c747768f8fe09d8f8bc5c0/cbild5kjc95ii35m41715.jpg?v=1740055618"}
				},
				Address = new Address
				{
					Street = "Ängalagsvägen 231",
					PostalCode = "269 95",
					City = "Båstad",
				},
				Muncipality = await ctx.Muncipalities.FirstAsync(m => m.Name == "Båstad"),
				RealEstateAgent = await ctx.Agents.FirstAsync(b => b.FirstName == "Bengt")

			},
			

            // Fredriks Property
            new Property
			{
				ListingPrice = 2895000,
				LivingSpace = 111,
				SecondaryArea = 77,
				LotSize = 1127,
				Description = "Schysst Mexitegelvilla i liten håla",
				NumberOfRooms = 5,
				MonthlyFee = 0,
				OperationalCostPerYear = 23376,
				YearBuilt = 1971,
				PropertyType = PropertyType.House,
				Images = new List<PropertyImage>
				{
					new PropertyImage { ImgURL = "https://bcdn.se/cache/46406010_1440x0.webp"},
					new PropertyImage { ImgURL = "https://bcdn.se/cache/46406011_1440x0.webp"},
					new PropertyImage { ImgURL = "https://bcdn.se/cache/46406013_1440x0.webp"},
					new PropertyImage { ImgURL = "https://bcdn.se/cache/46406015_1440x0.webp"},
					new PropertyImage { ImgURL = "https://bcdn.se/cache/46406016_1440x0.webp"}
				},
				Address = new Address
				{
					Street = "Utsiktsvägen 16",
					PostalCode = "517 71",
					City = "Olsfors",
				},
				Muncipality = await ctx.Muncipalities.FirstAsync(m => m.Name == "Bollebygd"),
				RealEstateAgent = await ctx.Agents.FirstAsync(b => b.FirstName == "Berit")


			},
			//new Property
			//{
			//	ListingPrice = 995000,
			//	LivingSpace = 51,
			//	SecondaryArea = 0,
			//	LotSize = 0,
			//	Description = "Charmig tvåa i på första våningen – med närhet till både natur och centrum!",
			//	NumberOfRooms = 2,
			//	MonthlyFee = 3092,
			//	OperationalCostPerYear = 19510,
			//	YearBuilt = 1954,
			//	PropertyType = PropertyType.Condo,
			//Images = new List<PropertyImage>
			//{
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/ae/97/ae97514c7e4b014e55c05c40453efd4e.jpg"},
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/92/e6/92e6abf49597d1448075a6f8bba45528.jpg"},
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/72/6e/726ec27269d2125e6f0bb15cf4b34c65.jpg"},
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/87/1b/871be2d9f8cbe8ab6762598100e1de64.jpg"},
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/c8/af/c8af86cce2b97d1afa418b567974808a.jpg"},
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/12/bf/12bf425c54584cc45a94551d6e0bee35.jpg"},
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/31/72/3172b1c7babcf82652e70943156349b0.jpg"},
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/6a/53/6a53541b8d4d39056d04aad85e8330fe.jpg"},
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/54/01/5401bebc3bbb5987472a8fa65a4e5d9d.jpg"},
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/93/79/9379881b2c5965adc3e7e3612790bbb1.jpg"},
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/a6/d3/a6d3f205cf64f74aaa771c576f06cc74.jpg"},
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/89/da/89da491c4546548171a0dfa29a39f27a.jpg"},
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/66/30/6630e80cad952ff12e422c06b148392c.jpg"},
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/27/95/279536383eca61a0a5062183c9801e40.jpg"},
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_portrait_cut/f5/bb/f5bb00f5ebc4c26b3eef252491a29b59.jpg"},
			//},
			//Address = new Address
			//{
			//	Street = "Centralgatan 17F",
			//	PostalCode = "913 31",
			//	City = "Umeå",
			//},
			//Muncipality = await ctx.Muncipalities.FirstAsync(m => m.Name == "Umeå"),
			//RealEstateAgent = await ctx.Agents.FirstAsync(b => b.FirstName == "Teodor")
			//},
			//new Property
			//{
			//	ListingPrice = 1395000,
			//	LivingSpace = 44,
			//	SecondaryArea = 0,
			//	LotSize = 0,
			//	Description = "Välkommen till denna välplanerade och rymliga 1:a om 43,5 kvm på populära Mariehem! Här får du en bostad med smart planlösning, gott om förvaring och modern bekvämlighet.",
			//	NumberOfRooms = 1,
			//	MonthlyFee = 3958,
			//	OperationalCostPerYear = 32069,
			//	YearBuilt = 1968,
			//	PropertyType = PropertyType.Condo,
			//Images = new List<PropertyImage>
			//{
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/49/d0/49d004d951909b4e855c5ac3c659274a.jpg"},
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/25/0a/250a739fab34d246ff1d426eb028833e.jpg"},
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/37/e9/37e9e16c607238fcec35ec3ddd9904ee.jpg"},
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/6d/d9/6dd90c5b6152ab55df5e31b935219a19.jpg"},
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/1c/71/1c713270ba5fa94ae5b8c5ac418ce910.jpg"},
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/1c/71/1c713270ba5fa94ae5b8c5ac418ce910.jpg"},
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/1c/71/1c713270ba5fa94ae5b8c5ac418ce910.jpg"},
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/fb/97/fb972b087b021a08e88032b251d3e24f.jpg"},
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/fb/97/fb972b087b021a08e88032b251d3e24f.jpg"},
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/fb/97/fb972b087b021a08e88032b251d3e24f.jpg"},
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/c6/1f/c61f8e6adc838c8de49292f5c4c2d3ff.jpg"},
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/1c/b0/1cb0118ccd55552e7d7b69e51a827cd7.jpg"},
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/15/2f/152f416614390d16605ab56c87f74ebf.jpg"},
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/46/38/4638b51eb4e8f1f638f77298ecd11af8.jpg"},
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/3f/7a/3f7a931f0c0af31e9d58f4d56063d20a.jpg"},
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_portrait_cut/24/f1/24f11009b6f7b91f26312decf18437ba.jpg"},
			//},
			//Address = new Address
			//{
			//	Street = "Mariehemsvägen 33B",
			//	PostalCode = "906 52",
			//	City = "Umeå",
			//},
			//	Muncipality = await ctx.Muncipalities.FirstAsync(m => m.Name == "Umeå"),
			//	RealEstateAgent = await ctx.Agents.FirstAsync(b => b.FirstName == "Teodor")
			//},
			//new Property
			//{
			//	ListingPrice = 1350000,
			//	LivingSpace = 36,
			//	SecondaryArea = 0,
			//	LotSize = 0,
			//	Description = "Välkommen till Porfyrvägen 3A – en trivsam och välplanerad etta med närhet till både natur och stad!",
			//	NumberOfRooms = 1,
			//	MonthlyFee = 2843,
			//	OperationalCostPerYear = 37500,
			//	YearBuilt = 2016,
			//	PropertyType = PropertyType.Condo,
			//Images = new List<PropertyImage>
			//{
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/df/c1/dfc191402b2aa9335d9789bb368dbc9e.jpg"},
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/e8/3f/e83fd28061c324224c6673e76bc0b921.jpg"},
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/f2/a6/f2a69231947e011bf02bf31957b949e2.jpg"},
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/04/3d/043dac1485c63c590fb8ff61dafcf666.jpg"},
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/65/f0/65f0fce343eafb0f2afe882a5d328ffc.jpg"},
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/78/46/7846af7a84aaf72cb010a22b7a4d7467.jpg"},
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/78/46/7846af7a84aaf72cb010a22b7a4d7467.jpg"},
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/32/62/3262c6ce4ca507eaf15d6843970de922.jpg"},
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/aa/00/aa00f201c33150765f54725b02782f8c.jpg"},
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/b5/94/b594ece07dbf4f048b89a9cb4cef8e6c.jpg"},
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/d3/11/d3110b972ad77bdab18024d523518529.jpg"},
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/cf/ed/cfedbdbb7560550bc95cb8cee4265670.jpg"},
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/c9/e8/c9e8a802a6806142d281c5c8352f723e.jpg"},
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/01/7d/017d7e34031fb2b1bbd16e878dfa9e76.jpg"},
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/58/b5/58b5baaedef716c2a9dc2d540593098b.jpg"},
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_portrait_cut/e1/d6/e1d6ec0772e9cb3022d4743118525bac.jpg"},

			//},
			//Address = new Address
			//{
			//	Street = "Porfyrvägen 3A",
			//	PostalCode = "907 42",
			//	City = "Umeå",
			//},
			//	Muncipality = await ctx.Muncipalities.FirstAsync(m => m.Name == "Umeå"),
			//	RealEstateAgent = await ctx.Agents.FirstAsync(b => b.FirstName == "Teodor")
			//},
			//new Property
			//{
			//	ListingPrice = 1250000,
			//	LivingSpace = 40,
			//	SecondaryArea = 0,
			//	LotSize = 0,
			//	Description = "Välkommen längst upp i huset och denna kvadratsmarta lägenhet på populära Tomtebo.",
			//	NumberOfRooms = 1,5,
			//	MonthlyFee = 4250,
			//	OperationalCostPerYear = 31250,
			//	YearBuilt = 2017,
			//	PropertyType = PropertyType.Condo,
			//Images = new List<PropertyImage>
			//{
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/5f/e8/5fe8d41c78a0c729f995ebb033557095.jpg"},
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/b4/cb/b4cbbb6e165eb29f358794ffccc422a6.jpg"},
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/19/b2/19b29ea07c460678fbe947a8036dd50d.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/3e/e6/3ee6e0593d15334828b07a3213c695bd.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/77/09/77097a52665ba32cccbf2ef7e3768474.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/e1/41/e1411f251d848ec560007de9bff3d2f9.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/5d/3f/5d3f5f80247963218c4dbe6445855eb8.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/49/21/49219c42395e128658d69567766b00f0.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/6b/f2/6bf205cbda9d9c6fdfc8e63e8787a7b2.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/52/01/520177de0f63f2121bc0d825d5699701.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/34/0b/340b5220119b73f7bc263d0b50b7917f.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/1f/1e/1f1e2de0acc132fab2689be5708cfd96.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/7c/3f/7c3f7049808bb95439fed19bc26fcc4c.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/13/61/1361ce5744405c34d7f41619b69699c1.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/38/56/38563fa19b27e175d542e6d3833c05ce.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_portrait_cut/35/9a/359a21e63140ac1fa4359e4f14671cc0.jpg" },

			//},
			//Address = new Address
			//{
			//	Street = "Lyktvägen 79A",
			//	PostalCode = "907 53",
			//	City = "Umeå",
			//},
			//	Muncipality = await ctx.Muncipalities.FirstAsync(m => m.Name == "Umeå"),
			//	RealEstateAgent = await ctx.Agents.FirstAsync(b => b.FirstName == "Teodor")
			//},
			//new Property
			//{
			//	ListingPrice = 1495000,
			//	LivingSpace = 44,
			//	SecondaryArea = 0,
			//	LotSize = 0,
			//	Description = "Välkommen till Skolgatan 9B – ett drömboende som väntar på dig i Brf Skräddaren 4! Denna charmiga lägenhet i gårdshuset erbjuder en perfekt kombination av stil, komfort och harmoni.",
			//	NumberOfRooms = 2,
			//	MonthlyFee = 3442,
			//	OperationalCostPerYear = 9756,
			//	YearBuilt = 1923,
			//	PropertyType = PropertyType.Condo,
			//Images = new List<PropertyImage>
			//{
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/c8/52/c8525c67ac6f6923ef1cbc0a27a4f501.jpg"},
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/f8/ae/f8ae0daa64f4561b32872e0a45b91255.jpg"},
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/5c/41/5c413b0dbc069f56468b555fa7c162a6.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/c0/65/c065bfcf61f1f0964faaf120e5a1e003.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/ed/ea/edeaa716b3b29ce0120fc85820e7a88d.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/1a/47/1a47bd57900d6a1557869a712cdefed1.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/38/8f/388f2e0e5c620361dae2371aa53d97df.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/05/90/0590e9d372b5527e62b81bb82aff3db9.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/14/02/1402270360abeea0917c690a5499c514.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/1e/d8/1ed8fc2c96660ed966dff109c44af9d4.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/50/47/50473ada9dcb665bb767fd78ec6dc1b3.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/5e/7f/5e7f0d26e43c0c7e5356b8f47d53ff83.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/ca/9e/ca9e344d76cd92a45b1bf3999199afa0.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/27/e3/27e30e1e927f065d17a670a1d9ca94c6.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/4c/08/4c081a965a02814e8f68339d2fb5ff4a.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_portrait_cut/b0/f9/b0f9802fee03253cf6804f35866c7439.jpg" },

			//},
			//Address = new Address
			//{
			//	Street = "Skolgatan 9B",
			//	PostalCode = "903 22",
			//	City = "Umeå",
			//},
			//	Muncipality = await ctx.Muncipalities.FirstAsync(m => m.Name == "Umeå"),
			//	RealEstateAgent = await ctx.Agents.FirstAsync(b => b.FirstName == "Teodor")
			//},
			//new Property
			//{
			//	ListingPrice = 1745000,
			//	LivingSpace = 39,5,
			//	SecondaryArea = 0,
			//	LotSize = 0,
			//	Description = "Här har du möjlighet att förvärva en ljus och välplanerad etta på 39,5 kvadratmeter i den populära bostadsrättsföreningen Skidstaven 1.",
			//	NumberOfRooms = 1,
			//	MonthlyFee = 2292,
			//	OperationalCostPerYear = 0,
			//	YearBuilt = 1963,
			//	PropertyType = PropertyType.Condo,
			//Images = new List<PropertyImage>
			//{
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/91/fa/91fa3bf5411370d631089097746cc327.jpg"},
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/c8/1d/c81daf971a419785c751cb7901630238.jpg"},
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_portrait_cut/03/54/035427d8335191bc2fe6b58a8b4681c1.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/f5/ef/f5efbb859684d75f34c7bd3b19c66bcf.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_portrait_cut/75/0d/750d56b85042142810f9570a1af0b179.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/f1/fa/f1fabe250771fb3b380cd4675ee010e5.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/88/99/889933c6a538de5659a130394f35f7f1.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/f3/a4/f3a4d2b8c832d859d5928d5b7a6d3ee4.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/c1/c2/c1c2b1732e0fcc2772721a319afce23b.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_portrait_cut/26/96/2696bf857b744c0f40f6cdeb33bbd45e.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/6e/7e/6e7ebe7911169e6fec7b54b3e7a90d4a.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_portrait_cut/67/3a/673a2643151033ee21183885bbf45b50.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/73/8f/738f8753c4bb1462fe688020d4049d13.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/fd/eb/fdeb73f3512115b990b2260ea3e3c56f.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/2f/e8/2fe80fb1d2fe793fd784810164d90c7c.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_portrait_cut/9d/06/9d06cd341e729573d23d8045a1af602e.jpg" },

			//},
			//Address = new Address
			//{
			//	Street = "Nydalavägen 16",
			//	PostalCode = "903 38",
			//	City = "Umeå",
			//},
			//	Muncipality = await ctx.Muncipalities.FirstAsync(m => m.Name == "Umeå"),
			//	RealEstateAgent = await ctx.Agents.FirstAsync(b => b.FirstName == "Teodor")
			//},
			//new Property
			//{
			//	ListingPrice = 1895000,
			//	LivingSpace = 70,
			//	SecondaryArea = 0,
			//	LotSize = 0,
			//	Description = "Välkommen till Kvarnvägen 8B – en ljus och välplanerad tvårummare med balkong!",
			//	NumberOfRooms = 2,
			//	MonthlyFee = 4709,
			//	OperationalCostPerYear = 0,
			//	YearBuilt = 1985,
			//	PropertyType = PropertyType.Condo,
			//Images = new List<PropertyImage>
			//{
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/77/1c/771cebc92bbe6cb7dc232b59b133d6ce.jpg"},
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/b0/a5/b0a5862cc39f0f6cde12fc140f3ca8cc.jpg"},
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/6c/52/6c52776a309c4f70a1bdd92ccfb592f4.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/d6/cf/d6cfcbd6f0f477ea45479068acaf9c77.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/82/65/8265e733b5cc0b66ff6f7feaf2fa096e.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/e2/7e/e27e0dd7a4cd69a84afdbcbdb3c3fb47.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/d6/38/d638966417ce3b5d31246cb33326e3ca.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/60/ea/60ea7f3df22b9b925f2d496a26ce9683.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/63/c2/63c203f86797c239206fec095e368c5b.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/7d/50/7d5052a1b8cbe8d372b25c5ade5ef1ae.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/64/f2/64f2c55fe648749e7952e51df0efdceb.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/a2/3f/a23f0ce306ee5e7b5d54cae9be539564.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/f8/71/f871fbac66bcb8cac5441659d5a0fb1f.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/f3/09/f309152a69a334ef95c47f361a19026f.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/f8/23/f823b6c49c17fcc1d385750085881cff.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_portrait_cut/73/06/73060683baf2c1ee0939236419f20d0c.jpg" },

			//},
			//Address = new Address
			//{
			//	Street = "Kvarnvägen 8B",
			//	PostalCode = "903 20",
			//	City = "Umeå",
			//},
			//	Muncipality = await ctx.Muncipalities.FirstAsync(m => m.Name == "Umeå"),
			//	RealEstateAgent = await ctx.Agents.FirstAsync(b => b.FirstName == "Teodor")
			//},
			//new Property
			//{
			//	ListingPrice = 1650000,
			//	LivingSpace = 78,
			//	SecondaryArea = 0,
			//	LotSize = 0,
			//	Description = "Välkommen till Tråggränd 42B – ett hemtrevligt och insynsskyddat boende med genomtänkt planlösning, belägen i en populär förening på lugna och barnvänliga Ersboda.",
			//	NumberOfRooms = 3,
			//	MonthlyFee = 4336,
			//	OperationalCostPerYear = 3492,
			//	YearBuilt = 1982,
			//	PropertyType = PropertyType.Condo,
			//Images = new List<PropertyImage>
			//{
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/44/c1/44c178dfe5b94d4a8bf843de6d7f1b1e.jpg"},
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/b0/a6/b0a6570a43d31f16465dcfded7502165.jpg"},
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/a6/2b/a62bb2a44040e772228af67012dfa87c.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_portrait_cut/92/5d/925dd3961d7d7e8eb1217320d6ddb00f.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/ea/0f/ea0fb2cbe6edc7bfda77a09c67ca19a5.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/ea/f9/eaf9c3313af337f7db7297c43493d5f1.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/4f/ac/4fac6eed6582bd5755624b018e6f42d6.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/bb/06/bb063d12f02a4136a8ab2e3437793001.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/34/7e/347e8dd5d491213f22a87fd2a8a78400.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/26/fc/26fc6460e2f5f46cd6ec3c6f55789e89.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/69/79/69795bfc9f565b8e54bf7a96e80fe907.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/5a/f0/5af0fdeddc529e7e9c3f88ff9c00a54b.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/4d/f1/4df1c2ed06d4554459e1af881eb02374.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_portrait_cut/07/31/0731f70647c61c85776d1ce03eb71842.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/3b/f3/3bf393c46394c67c60c68c527f6517c3.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_portrait_cut/83/27/832723ff841c7f5cf8930087e732a261.jpg" },

			//},
			//Address = new Address
			//{
			//	Street = "Tråggränd 42B",
			//	PostalCode = "906 26",
			//	City = "Umeå",
			//},
			//	Muncipality = await ctx.Muncipalities.FirstAsync(m => m.Name == "Umeå"),
			//	RealEstateAgent = await ctx.Agents.FirstAsync(b => b.FirstName == "Teodor")
			//},
			//new Property
			//{
			//	ListingPrice = 1795000,
			//	LivingSpace = 54,
			//	SecondaryArea = 0,
			//	LotSize = 0,
			//	Description = "Kålhagsvägen 29A erbjuder ett modernt och bekvämt boende i ett naturnära och populärt område.",
			//	NumberOfRooms = 2,
			//	MonthlyFee = 4790,
			//	OperationalCostPerYear = 0,
			//	YearBuilt = 2020,
			//	PropertyType = PropertyType.Condo,
			//Images = new List<PropertyImage>
			//{
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/6a/e4/6ae41c3c6ac3eb96206969f8dc77dba2.jpg"},
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/68/a6/68a6cdb664aecc525a0b34e8b076d92a.jpg"},
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/ba/96/ba967b3b9ba8be0b56f2c7b647ab3cce.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/1c/a3/1ca3c28927a75652c38827b459e5fc5b.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_portrait_cut/2f/11/2f1148557d17b7d71c44cc5eaa9d8209.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_portrait_cut/80/05/80051a969df19f60380b66d7e981e65b.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/1b/76/1b760f0902987f748675e2ee616a03ab.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/50/33/503376274f471f44b583f83a42ab278d.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/f9/39/f939c13e1e6c7eba96245d5480897482.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/5d/5e/5d5e85fdcf13720a8dbf60639e465f57.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/4f/91/4f91899b6b8b3dccbb78daa162c7d8e1.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/0f/9d/0f9d182b9a9462052567f1fe2831dcd8.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/5a/b5/5ab5682ab70ca7b031e2f1776816c136.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/e9/18/e918545363f11358193317222debb25c.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/58/5a/585a70afcc1340b833fbab8848e5f888.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_portrait_cut/49/2d/492dfb00f544c5cf36b6ab4411366a30.jpg" },

			//},
			//Address = new Address
			//{
			//	Street = "Kålhagsvägen 29A",
			//	PostalCode = "907 55",
			//	City = "Umeå",
			//},
			//	Muncipality = await ctx.Muncipalities.FirstAsync(m => m.Name == "Umeå"),
			//	RealEstateAgent = await ctx.Agents.FirstAsync(b => b.FirstName == "Teodor")
			//},
			//new Property
			//{
			//	ListingPrice = 2450000,
			//	LivingSpace = 79,
			//	SecondaryArea = 0,
			//	LotSize = 0,
			//	Description = "Välkommen till Hermelinvägen 113 – en smakfullt renoverad trea om 79 kvm med genomtänkta materialval, öppen planlösning och ett av föreningens bästa lägen!",
			//	NumberOfRooms = 3,
			//	MonthlyFee = 4238,
			//	OperationalCostPerYear = 0,
			//	YearBuilt = 1974,
			//	PropertyType = PropertyType.Condo,
			//Images = new List<PropertyImage>
			//{
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/d2/1f/d21f44536410b76cd58397ec929055f4.jpg"},
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/0b/4c/0b4cd4f7210ee898ea7741f28c493b63.jpg"},
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/ab/89/ab89a7339ea34584f8103a421151af94.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_portrait_cut/42/56/4256c4b9962506c32374871eaf1437e9.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/85/61/856116fb51793b97b15f8491ae27a660.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/bd/38/bd3871a02455b7da712966b214292ce6.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/35/90/35901a15ebeafe7f654983e10c1a24b7.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/3a/12/3a125845d9f456274d5ab3b246d3d576.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/d9/fe/d9fe612f5f7941df1b744f02270a9b8e.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/7b/bf/7bbf04ec15f9843683a64bd7b523ebfd.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/cb/b3/cbb34d099fae88720a45431aaf901687.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/df/78/df7896a9f9afa1d646616fc26cdab91e.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/3c/8b/3c8b163d2f3f72ea359c9e02b6e1897f.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/be/3e/be3e84ff9f1f906f55fa33a825bfaaf6.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/f2/5e/f25e0747ab6fb2241ffb79881525f0dc.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_portrait_cut/c0/37/c0378f0d79587516b63601eba930a369.jpg" },

			//},
			//Address = new Address
			//{
			//	Street = "Hermelinvägen 113",
			//	PostalCode = "906 42",
			//	City = "Umeå",
			//},
			//	Muncipality = await ctx.Muncipalities.FirstAsync(m => m.Name == "Umeå"),
			//	RealEstateAgent = await ctx.Agents.FirstAsync(b => b.FirstName == "Teodor")
			//},
			//new Property
			//{
			//	ListingPrice = 1950000,
			//	LivingSpace = 99,
			//	SecondaryArea = 0,
			//	LotSize = 0,
			//	Description = "Välkommen till denna trivsamma markplanslägenhet på hela 99 kvm i hjärtat av Tomtebo! ",
			//	NumberOfRooms = 4,
			//	MonthlyFee = 6949,
			//	OperationalCostPerYear = 0,
			//	YearBuilt = 0,
			//	PropertyType = PropertyType.Condo,
			//Images = new List<PropertyImage>
			//{
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/ff/2f/ff2f7ae18ecd58234fc95146498676b6.jpg"},
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/82/02/8202439f6c85fea19049f1cd8e3fefb6.jpg"},
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/5a/5b/5a5b834c8f93ecaa4100d062a72be0c6.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/93/21/9321e7c050cb93d49308859468ccc492.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/9a/63/9a63ead6627b589b11c45f3c5f2a238f.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/06/01/060195e5a45661b7513bd7f092563c81.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/d2/fa/d2fa4b6e83e81477805cccddb8deeebb.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/46/03/4603a7a6780ca346597f679ec663d93a.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/91/0c/910c901e5921f5b74d149394de99c868.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/f2/5e/f25e1bfcf65c89953bc6a12a19728881.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/17/52/175206d8a256e8c605ee2d22522cc292.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/e5/11/e5119991702db0151064ea18f53fcbc6.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/03/9a/039abdbe0b01e34596dfd1320c86dba3.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/87/04/87042825554d347bb2c818dd13b49c2d.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/2c/54/2c542b659be493cb939105d19cbe8187.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_portrait_cut/68/41/684156c4b847d790ad75d2d2bef24988.jpg" },

			//},
			//Address = new Address
			//{
			//	Street = "Älvans väg 80",
			//	PostalCode = "907 50",
			//	City = "Umeå",
			//},
			//	Muncipality = await ctx.Muncipalities.FirstAsync(m => m.Name == "Umeå"),
			//	RealEstateAgent = await ctx.Agents.FirstAsync(b => b.FirstName == "Sofie")
			//},
			//new Property
			//{
			//	ListingPrice = 1785000,
			//	LivingSpace = 53,4,
			//	SecondaryArea = 0,
			//	LotSize = 0,
			//	Description = "Varmt välkomna till Hissjövägen 44! Ett säkert kort bland Umeås föreningar. Här hittar du ett välskött hem med bekvämt avstånd till stadskärnan och gröna strövområden.",
			//	NumberOfRooms = 3,
			//	MonthlyFee = 3624,
			//	OperationalCostPerYear = 0,
			//	YearBuilt = 0,
			//	PropertyType = PropertyType.Condo,
			//Images = new List<PropertyImage>
			//{
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/f6/b2/f6b2cec8a22bf6fcd76dcb9793f59ff1.jpg"},
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/61/b6/61b653381c85fcd3894bb801a4703a1d.jpg"},
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/2a/8c/2a8cca0f6d93c74754d6c715026a2ed5.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/ef/6b/ef6bf1c44ce9cc1a724b72ea4d023d1d.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/e3/54/e3544b1fa0d2021c54984a7dcf4c053b.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/3f/4f/3f4fd8e357dd1e3836a13f3eada03fe7.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/7f/8c/7f8cbaf04ff2138b7e8b03cf2f904ccc.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/8d/84/8d8486332ec2aa72f7b54f62bd4647e5.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/48/50/4850949c9c05ba4827f01eed97c44abe.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/b7/09/b709897646b2481ed6464542cd8a364c.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/73/bd/73bd2b427ad7a7396d9981ce5ef07f12.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/36/62/366224003c0cc23fb5d121e25c0046da.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/8c/d9/8cd93bb573b87e7a5f737b27c798e386.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/78/8e/788e14c3e4b9c0b442cc7b9415365555.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/2d/8b/2d8b5ba1076eea48497b1496acba54f0.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_portrait_cut/e6/c1/e6c13fd4a2932450d6d0a0571eda6760.png" },
			//},
			//Address = new Address
			//{
			//	Street = "Hissjövägen 44",
			//	PostalCode = "903 45",
			//	City = "Umeå",
			//},
			//	Muncipality = await ctx.Muncipalities.FirstAsync(m => m.Name == "Umeå"),
			//	RealEstateAgent = await ctx.Agents.FirstAsync(b => b.FirstName == "Sofie")
			//},
			//new Property
			//{
			//	ListingPrice = 1395000,
			//	LivingSpace = 27,5,
			//	SecondaryArea = 0,
			//	LotSize = 0,
			//	Description = "Välkommen till denna kvadratsmarta etta på Teg! Denna ljusa och fräscha bostad erbjuder en smart planlösning med en mysig sovalkov.",
			//	NumberOfRooms = 1,
			//	MonthlyFee = 2032,
			//	OperationalCostPerYear = 0,
			//	YearBuilt = 1956,
			//	PropertyType = PropertyType.Condo,
			//Images = new List<PropertyImage>
			//{
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/9b/bb/9bbb5626c6f866dd48aea0d093f0f085.jpg"},
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/9f/a5/9fa5ab68d93838e36dd88bf2c3d46824.jpg"},
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/d2/9e/d29e85ed0058a0e0fce315c6df44dd6b.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/b8/89/b88995c068c716af6e0815f8a745529e.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/99/25/9925dca7aab773f8a9eb217ef60e1e60.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/03/60/0360e69f8cf2ecc2f2d8b18486ae79c5.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/95/ab/95ab3de450134c0e01181c393cfc4b04.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/31/90/3190cf5a79f768f33bf7f9b7e41f17d7.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/09/9c/099c8967f1a87b64784289320cd0d4ff.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/42/af/42af26dd8410d13ab1f325c22da27f79.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/2a/a3/2aa3f1cfd3dabe5e8b50b9ccb559dfeb.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/3f/e5/3fe5b2393dbf9f29ac355c369c36c965.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_portrait_cut/c6/68/c668d4267f944af43a1e9f317ba93277.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/4e/b1/4eb1dfff7507b394d10c859f555b7d09.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/dc/b9/dcb9809735a1cba7289503f03cdb59b0.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_portrait_cut/6c/28/6c28334bca4dabfccd51572d4bbe7e09.jpg" },
			//},
			//Address = new Address
			//{
			//	Street = "Riksvägen 50C",
			//	PostalCode = "904 33",
			//	City = "Umeå",
			//},
			//	Muncipality = await ctx.Muncipalities.FirstAsync(m => m.Name == "Umeå"),
			//	RealEstateAgent = await ctx.Agents.FirstAsync(b => b.FirstName == "Sofie")
			//},
			//new Property
			//{
			//	ListingPrice = 1 545 000,
			//	LivingSpace = 69,5,
			//	SecondaryArea = 0,
			//	LotSize = 0,
			//	Description = "Välkommen till denna rymliga tvåa på 69,5 kvm belägen på Kemigränd 14 i en stabil förening. Här möts du av ett fint renoverat kök och ett badrum med tvättmaskin för extra bekvämlighet.",
			//	NumberOfRooms = 2,
			//	MonthlyFee = 4424,
			//	OperationalCostPerYear = 0,
			//	YearBuilt = 1969,
			//	PropertyType = PropertyType.Condo,
			//Images = new List<PropertyImage>
			//{
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/b7/7b/b77b04444c7c1e741f896fbc7bbf4087.jpg"},
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/d9/64/d964cfec4d0d494b54c78ffa27b80782.jpg"},
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/c3/da/c3daaddb69a737c3366e51387515e9f3.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/0e/87/0e877545a344c9cb3b6697ec619e7521.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/f6/66/f666453874dde964cea354c5b6428e64.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/7a/af/7aaf1ec1ad3fe81a639c6a803135ff82.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/74/9a/749a07727b0d9920dafa1b1f323ebbd6.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/11/65/1165a2eb49fd16dd21b5c03a5eb888b4.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/b4/ab/b4abc29e4b8fbbbf3e943581738a9479.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/7d/45/7d455c3f56863eb0dcf1b1e5efae93c9.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/45/8b/458bdc38a6a4543e3e6e8eaa379eeb5a.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/01/99/01992cd5d2d4d6585142c9f3ca139c2c.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/d5/e6/d5e6b937b417b208213c8604a74689fc.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/48/d6/48d6b3a5f34fda3b0d5000fb91ec228a.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/1a/10/1a10f783a269549bb8216491ee89f8b2.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_portrait_cut/d2/52/d2527c794f2d914c02c541d1d8bfbe49.jpg" },

			//},
			//Address = new Address
			//{
			//	Street = "Kemigränd 14",
			//	PostalCode = "907 31",
			//	City = "Umeå",
			//},
			//	Muncipality = await ctx.Muncipalities.FirstAsync(m => m.Name == "Umeå"),
			//	RealEstateAgent = await ctx.Agents.FirstAsync(b => b.FirstName == "Sofie")
			//},
			//new Property
			//{
			//	ListingPrice = 4395000,
			//	LivingSpace = 119,
			//	SecondaryArea = 0,
			//	LotSize = 226,
			//	Description = "Välkommen till detta fantastiska radhus i ett populärt område på Tomtebo, bara ett stenkast från Nydalasjön! Detta hem erbjuder hela 119 kvm av välplanerad yta, byggt 2008, med fyra rymliga sovrum och två helkaklade badrum.",
			//	NumberOfRooms = 5,
			//	MonthlyFee = 0,
			//	OperationalCostPerYear = 49268,
			//	YearBuilt = 2008,
			//	PropertyType = PropertyType.TownHouse,
			//Images = new List<PropertyImage>
			//{
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/66/01/6601435259f1c23da0549afc302f0a0a.jpg"},
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/1b/08/1b08edd9e59a2fb753fe0bfe864b173c.jpg"},
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/3c/64/3c64e31bfd7b14bd6be6447c54e3662f.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/22/69/2269cfe6010a87a6d4ca3a246ed04a16.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/40/7f/407f6bff53fcc4ea6ac38e28fb3d801f.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/29/fd/29fd40941ec9e7b08801ec657b5a1e4b.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/82/6e/826ebd714ead54179cfd74ac5e50716f.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/39/32/3932c86fd292ab9d2cdf47aa1456eb82.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/71/82/71825f581f9f01e0131011b1f1aa605c.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/02/fd/02fdacc0cf8128c5e06f6ede0100cf9e.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/f1/fe/f1fec07a3f3f4d70a4ae30d2e85646af.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/e4/e6/e4e6d68c954a4a69f4062b7f0f88076a.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/53/5c/535c56a3ee3969939b858010e03cb19a.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/ab/5b/ab5b0db64206ebce1f99dfe236edb501.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_portrait_cut/2f/fb/2ffb8a66990b324d0b1c8767ec980586.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_portrait_cut/a2/c6/a2c63a40631f8b7ceed042f17f295075.jpg" },

			//},
			//Address = new Address
			//{
			//	Street = "Sagovägen 33B",
			//	PostalCode = "907 54",
			//	City = "Umeå",
			//},
			//	Muncipality = await ctx.Muncipalities.FirstAsync(m => m.Name == "Umeå"),
			//	RealEstateAgent = await ctx.Agents.FirstAsync(b => b.FirstName == "Sofie")
			//},
			//new Property
			//{
			//	ListingPrice = 1095000,
			//	LivingSpace = 69,5,
			//	SecondaryArea = 0,
			//	LotSize = 0,
			//	Description = "Välkommen till denna trivsamma tvåa på 69,5 kvm! Här finner du en inglasad balkong som förlänger sommaren och ger extra utrymme att njuta av. ",
			//	NumberOfRooms = 2,
			//	MonthlyFee = 6000,
			//	OperationalCostPerYear = 0,
			//	YearBuilt = 1968,
			//	PropertyType = PropertyType.Condo,
			//Images = new List<PropertyImage>
			//{
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/a0/cf/a0cf39d1a2929151ed4fe53ece5d46c7.jpg"},
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/2d/5a/2d5a5a09ae2f6ae7ac45ca8576299164.jpg"},
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/5d/68/5d68584a81211d42894ee2f3d19250b8.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/ed/00/ed00d232d713fa91c3b2a539f9b594c2.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/31/b9/31b9e50d67e9ebd97a346bd6cb739b85.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/fb/27/fb27db011e9ee8b87b1861ccf71b69e8.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/a5/1c/a51c341f7ac223a5b0361c3e881b439b.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/8c/9a/8c9aed444479400cc11ac3d5dfbf3ff7.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/e7/09/e7095b06f5b2f43a485cc180a4ed8721.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/a1/16/a116e7f9532e9f922283c5d166e89db8.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/8a/ae/8aaeb459e296c25bb6fe54fadc7b4169.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/13/93/13933578930d6a1bc1798c0927bf93ff.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/8c/b0/8cb07cc3d80916ea8c771c95de471082.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/5e/e7/5ee7d221da367b1c0f83e6900e91d110.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/a4/68/a4687e7786f2d37840460b7ed951eefb.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/88/65/8865708c9b47c825581eea874f6b78cd.jpg" },

			//},
			//Address = new Address
			//{
			//	Street = "Mariehemsvägen 33F",
			//	PostalCode = "906 52",
			//	City = "Umeå",
			//},
			//	Muncipality = await ctx.Muncipalities.FirstAsync(m => m.Name == "Umeå"),
			//	RealEstateAgent = await ctx.Agents.FirstAsync(b => b.FirstName == "Sofie")
			//},
			//new Property
			//{
			//	ListingPrice = 2995000,
			//	LivingSpace = 126,
			//	SecondaryArea = 48,
			//	LotSize = 738,
			//	Description = "",
			//	NumberOfRooms = 5,
			//	MonthlyFee = 0,
			//	OperationalCostPerYear = 32173,
			//	YearBuilt = 1982,
			//	PropertyType = PropertyType.House,
			//Images = new List<PropertyImage>
			//{
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/0c/a5/0ca5bf9028040470a0723034cc19631e.jpg"},
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/f9/c9/f9c96837336513c95394edb155faed51.jpg"},
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/40/56/40565c3d6128c48e84c3aff12b3ef2db.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/c6/3e/c63e62dd0248b3fbea49125f4c60b4eb.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/c8/fe/c8fe0494ea762732fdcb53bb8752cc6f.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/34/b0/34b0631245d771d78a6b79ec89bb6283.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/8c/28/8c2844ca77c1ae0ab2d036a88eaf17fa.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/90/b8/90b8719a0d12e557a699825eaf92b5bd.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/2f/9d/2f9da11b3c9151ddb53033a60913cafd.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/7d/9d/7d9dd5515502c1e26943acab6e57e768.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/69/b3/69b3d0a99d24c47d08fbe51e34dc3e49.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/73/5b/735b92a6eda516b1990366ce1270dcdc.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/ab/ce/abcede41b89bf61bf45238f206dc1821.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/18/a5/18a5ccec94621a42c8632dc50cfe5c26.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_portrait_cut/ec/40/ec400583e61dae85d0b77a419fc00161.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/42/32/423258583b6ac02667ec92f222b8f5f9.jpg" },

			//},
			//Address = new Address
			//{
			//	Street = "Kulgränd 1",
			//	PostalCode = "913 34",
			//	City = "Umeå",
			//},
			//	Muncipality = await ctx.Muncipalities.FirstAsync(m => m.Name == "Umeå"),
			//	RealEstateAgent = await ctx.Agents.FirstAsync(b => b.FirstName == "Sofie")
			//},
			//new Property
			//{
			//	ListingPrice = 2250000,
			//	LivingSpace = 76,5,
			//	SecondaryArea = 0,
			//	LotSize = 0,
			//	Description = "Söker du något utöver det vanliga? Då är du varmt välkommen till denna fantastiska trea med industriell stil och moderna inslag!",
			//	NumberOfRooms = 3,
			//	MonthlyFee = 3578,
			//	OperationalCostPerYear = 0,
			//	YearBuilt = 1966,
			//	PropertyType = PropertyType.Condo,
			//Images = new List<PropertyImage>
			//{
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/f6/2c/f62c6f022b837ab4f2a56025eebf07aa.jpg"},
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/98/b0/98b072ce1b9bd708f2370051226367ed.jpg"},
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/43/b5/43b5250bf0a2f9eebebf25617b0afe4f.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/44/a8/44a85c7e2230ee4cbafadd9bb63146ca.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/fa/8e/fa8e92ad49c1b58d8f2bce28e523a0d8.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/3b/f1/3bf15e09f3abd4b010cfd1e2279904cb.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/d7/64/d764b4abb088980fcbe5e516685db5ab.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/63/89/638918fdcb3431c2292e8f764893459a.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/e5/10/e51085192513d63389d7f26e15406b54.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/73/bc/73bc438ecca8a9779c7eeb14fe669274.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/58/a8/58a8678bb27efe79c91599bf92c4fe28.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/27/d2/27d25b3c4326c771496698485e8ab177.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/af/da/afda534ee5c3c01dc7a1fc5ad3f6f548.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/84/c3/84c399b7c2321e84a50b38ac8d2d683c.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/00/98/00989946f4cfa23a78a67191028cb741.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_portrait_cut/20/1e/201eb8022ca18cafeb4c9351bd85bb27.jpg" },

			//},
			//Address = new Address
			//{
			//	Street = "Mariehemsvägen 3H",
			//	PostalCode = "906 54",
			//	City = "Umeå",
			//},
			//	Muncipality = await ctx.Muncipalities.FirstAsync(m => m.Name == "Umeå"),
			//	RealEstateAgent = await ctx.Agents.FirstAsync(b => b.FirstName == "Sofie")
			//},
			//new Property
			//{
			//	ListingPrice = 3795000,
			//	LivingSpace = 108,
			//	SecondaryArea = 0,
			//	LotSize = 300,
			//	Description = "Välkommen till detta charmiga parhus på 108 kvm, som erbjuder en väl genomtänkt och rymlig planlösning.",
			//	NumberOfRooms = 4,
			//	MonthlyFee = 0,
			//	OperationalCostPerYear = 24899,
			//	YearBuilt = 2014,
			//	PropertyType = PropertyType.TownHouse,
			//Images = new List<PropertyImage>
			//{
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/bf/8c/bf8ccb7e137bb841c8fbcf33c4e107fd.jpg"},
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/8a/94/8a945fea57ac73a00c2a79e41fd117b8.jpg"},
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/d2/fa/d2fa185dc60bf78021ea25823f68e789.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/11/c7/11c7f73d2b641c2997167e7daca33381.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/dc/76/dc76606ed6362e6a7d33887345c6d28a.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/8a/b4/8ab4e8205a3d628cbc697d750a52d313.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/cd/48/cd481c6fc49f302198d8f8c6de094b9d.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/c3/5d/c35de34d819c5f426af0950f985a344a.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/f2/ea/f2eae7e1658814f05a57385b48b17732.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/1c/47/1c4737458a6a0ca3b6e672e242de8756.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/88/4c/884c26c192ad8008d6cf4a230841d584.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/22/86/2286e276acefddece387dec3aaf9cf2f.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/7c/30/7c301b8a426e5e29e6cd0d1b020ae222.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/90/7e/907ee363533bd6ba53469e6a786fea9f.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_portrait_cut/e1/cd/e1cd64165b44691dc31132b7fee04ecf.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_portrait_cut/39/57/3957c8788d2cb55fd192784945c2678f.jpg" },

			//},
			//Address = new Address
			//{
			//	Street = "Kavaljersvägen 2D",
			//	PostalCode = "903 64",
			//	City = "Umeå",
			//},
			//	Muncipality = await ctx.Muncipalities.FirstAsync(m => m.Name == "Umeå"),
			//	RealEstateAgent = await ctx.Agents.FirstAsync(b => b.FirstName == "Sofie")
			//},
			//new Property
			//{
			//	ListingPrice = 1300000,
			//	LivingSpace = 80,
			//	SecondaryArea = 0,
			//	LotSize = 0,
			//	Description = "Välkommen till denna välplanerade 3:a i en populär förening på Ersboda!",
			//	NumberOfRooms = 3,
			//	MonthlyFee = 6392,
			//	OperationalCostPerYear = 0,
			//	YearBuilt = 1991,
			//	PropertyType = PropertyType.Condo,
			//Images = new List<PropertyImage>
			//{
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/d8/59/d85969bddb98cfca96a6e9c1bbdb8c5c.jpg"},
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/74/13/741328483514627dfc51c9b7cc1b8989.jpg"},
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/61/f4/61f4420bc5800bba690a502ad6c71132.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/9c/42/9c423b6d497773909c532b3d5a5a4c45.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/45/b6/45b6bae07446dc49022dca8bf391ffff.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/04/61/04619cdba9c0d6c7d513133026cde0d1.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/b5/c8/b5c8d353ea7637dd64b8890e08573b61.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/b0/6c/b06cca94113103ad9f2a3b7da35f029d.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/c2/6f/c26fb0880ae8cc0d1bd35c5c5f90ddca.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/96/77/9677ce296a2a49391488941cc0cea023.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/e8/a1/e8a1de012d5f7428cd38986f86703d2e.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/10/1f/101fad70248ac7d8f03e777bcda341fa.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/a6/4f/a64ffd35fc059e8b72757ac6aa15a070.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/28/06/2806948984ff1ddc7c823775bd4a956b.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/ee/a0/eea004a529352f47f10e10dc8b5d0914.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_portrait_cut/88/0c/880c908ea004bb0b803e15edcb941776.jpg" },

			//},
			//Address = new Address
			//{
			//	Street = "Mjölkvägen 82",
			//	PostalCode = "906 28",
			//	City = "Umeå",
			//},
			//	Muncipality = await ctx.Muncipalities.FirstAsync(m => m.Name == "Umeå"),
			//	RealEstateAgent = await ctx.Agents.FirstAsync(b => b.FirstName == "Sofie")
			//},
			//new Property
			//{
			//	ListingPrice = 3395000,
			//	LivingSpace = 157,
			//	SecondaryArea = 0,
			//	LotSize = 1206,
			//	Description = "Varmt välkommen till Avaliden 39 och detta magiska hem i naturnära läge.",
			//	NumberOfRooms = 6,
			//	MonthlyFee = 0,
			//	OperationalCostPerYear = 23208,
			//	YearBuilt = 1980,
			//	PropertyType = PropertyType.House,
			//Images = new List<PropertyImage>
			//{
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/2c/6e/2c6ecf7e32efe9a9d8a599871e19e1f1.jpg"},
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/93/1f/931ffd69df61f241e27309526258bab8.jpg"},
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/86/7e/867e1ae9394abc0cf822f5c0b10c27a4.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/71/5f/715f420c6722468358715aec9bb95de1.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/09/46/0946d0c4fc9e6ee13e1011c02bfe9bbe.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/46/bf/46bf8695c0b0cd3226cbd195a95ce1c9.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/e1/76/e1761bfb5eabd1a547d988673ea1f6c5.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/d9/1b/d91bbfd5dbf1dbc6f922f7e5ba11dd6a.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/3b/23/3b23c8a255eddc23d2037c82ac479ed7.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/b4/53/b4539a6be1579d246191b8ba6fe328ff.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/96/96/96969dab8c13964bcfcb89d42c0d30d7.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/1a/7e/1a7ea201ecadea31f7e329db356836d8.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/60/85/608526c6c52479e06b578e592767f6bd.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/43/4d/434dea2c5cd464370adee028a768b04d.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/95/e5/95e592036193b06e9fe9051f09623d69.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/de/63/de63fb4ce2675f36f961b2106494a58e.jpg" },

			//},
			//Address = new Address
			//{
			//	Street = "Avaliden 39",
			//	PostalCode = "922 66",
			//	City = "Umeå",
			//},
			//	Muncipality = await ctx.Muncipalities.FirstAsync(m => m.Name == "Umeå"),
			//	RealEstateAgent = await ctx.Agents.FirstAsync(b => b.FirstName == "Sara")
			//},
			//new Property
			//{
			//	ListingPrice = 3195000,
			//	LivingSpace = 53,
			//	SecondaryArea = 40,
			//	LotSize = 2269,
			//	Description = "Magiskt läge vid havet – insynsskyddad oas med panoramautsikt.",
			//	NumberOfRooms = 2,
			//	MonthlyFee = 0,
			//	OperationalCostPerYear = 13184,
			//	YearBuilt = 1965,
			//	PropertyType = PropertyType.VacationHouse,
			//Images = new List<PropertyImage>
			//{
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/10/24/1024bcaa5933a9bff9153b8dc7dad407.jpg"},
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/a0/89/a089726a3c411bb413af40fbf560c7b4.jpg"},
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/99/29/9929d8006834e5ef2927321ec23e9af5.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/b3/88/b388cb2a86c81c9cb34125801c1682dd.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/db/db/dbdb0322beec7686458c3e170216e537.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/60/9b/609be30cf8196b25a8f2f509db9b3c26.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/02/da/02dadffbb2f68e8d0c9b89eaaf70ccd3.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/5d/09/5d09e8732da59a39313ccd9bce27978a.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/d6/41/d6411a98cf34ed2c09b5893277353c51.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/c6/1c/c61c73c5d017b10ab1da05decad6e9ce.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/9f/89/9f898f220dfa65584f1a3b485434508d.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/1b/50/1b504249f7f10bbdd88901c04646d0b4.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/ee/3a/ee3ab2bf4b51f672cee5f3db2738f1d7.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/b1/f2/b1f208b19b0aa5d2a810cd865f22d8c0.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/0e/c8/0ec81c335a1c00a198ad963341658168.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/48/a2/48a213f5b28c72a675692ad4663e447e.png" },

			//},
			//Address = new Address
			//{
			//	Street = "Sillviken 77",
			//	PostalCode = "905 96",
			//	City = "Umeå",
			//},
			//	Muncipality = await ctx.Muncipalities.FirstAsync(m => m.Name == "Umeå"),
			//	RealEstateAgent = await ctx.Agents.FirstAsync(b => b.FirstName == "Sara")
			//},
			//new Property
			//{
			//	ListingPrice = 3995000,
			//	LivingSpace = 62,
			//	SecondaryArea = 0,
			//	LotSize = 0,
			//	Description = "Välkommen till ett hem som sätter ribban för lyxigt boende! Här bor du i en modern oas byggd 2021, där hissen smidigt tar dig direkt från markplan upp till din egen fristad.",
			//	NumberOfRooms = 2,
			//	MonthlyFee = 4929,
			//	OperationalCostPerYear = 3192,
			//	YearBuilt = 2021,
			//	PropertyType = PropertyType.Condo,
			//Images = new List<PropertyImage>
			//{
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/cf/40/cf40a043f8c0bdfd58001536fac09563.jpg"},
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/51/d0/51d0affd8475af38e61ed5dc853e9a1d.jpg"},
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/ee/16/ee1639837df200b324e33078447770a8.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/30/99/3099decdc403b37b100d69474effba26.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/00/02/0002992780ef39a98ec521feb1d052af.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/3c/c4/3cc4166a4e8394f1e54438fec3a81fc2.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/4c/ca/4ccaccaff8168af4248ef87a7d2aa861.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/33/d7/33d74ef6b8393fd5dc5fee124743a877.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/34/12/3412148c8b50bc3b056d2203b2560e1a.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/55/fd/55fdcbb66eba211e37ed1c8688457c8f.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/d7/6d/d76d002526eb2e4b42595a5590a56605.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/e5/6c/e56cc47223592eb34fd5e172e2fd5c27.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/ea/9d/ea9d2c42294397a5d71553c9b8e917bb.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/64/f7/64f79fdd3cf1f31265b07f2cffd510da.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/e2/94/e294ee41316ff0bd9f80378deae8eff1.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_portrait_cut/bc/8e/bc8e55013e257de85dcc8af2c15fd455.jpg" },

			//},
			//Address = new Address
			//{
			//	Street = "Hoppets gränd 34",
			//	PostalCode = "903 34",
			//	City = "Umeå",
			//},
			//	Muncipality = await ctx.Muncipalities.FirstAsync(m => m.Name == "Umeå"),
			//	RealEstateAgent = await ctx.Agents.FirstAsync(b => b.FirstName == "Sara")
			//},
			//new Property
			//{
			//	ListingPrice = 1795000,
			//	LivingSpace = 120,
			//	SecondaryArea = 0,
			//	LotSize = 0,
			//	Description = "Välkommen till en stor och ljus 5:a i perfekt gavelläge på populära Mariehem! Separat uthyrningsrum ger lägre boendekostnad!",
			//	NumberOfRooms = 5,
			//	MonthlyFee = 7914,
			//	OperationalCostPerYear = 0,
			//	YearBuilt = 1968,
			//	PropertyType = PropertyType.Condo,
			//Images = new List<PropertyImage>
			//{
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/3d/d7/3dd78c8c0f32ae7a356ef20b17e032ad.jpg"},
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/43/af/43af7330ffaf0809800527f879172166.jpg"},
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/19/58/1958821024134bf7b6085c005eae760c.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/d1/c1/d1c1ffbf6caf6fc7addae808c7b19d94.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/e0/5a/e05a9b6d2a4c397b14d1571324c651a4.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/0c/fb/0cfb9b721b1c647e274c8509f4e94f81.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/75/d2/75d2d3505d0460357f4832ba841e08f0.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/ce/26/ce26c4220183ab1692f800e997f15257.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/af/fa/affa24df92a0e09ae2e3f9086f2feee9.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/71/4b/714b04d93132171d2be81a8a14bde17f.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/22/8f/228f8d04b461e2ac90893247c58b978b.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/19/c6/19c6fce14a69155354d3de9d9387f5f2.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/a2/14/a214004a18707b5caa0790f54e3111a8.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/d6/0d/d60dddc505146eddbcc859f80cbe3a77.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/29/4c/294cbcc7c4ed6e99ce9befeed01e4075.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_portrait_cut/e0/c3/e0c32e6499e02c21f6e359989baded69.jpg" },

			//},
			//Address = new Address
			//{
			//	Street = "Mariehemsvägen 33H",
			//	PostalCode = "906 52",
			//	City = "Umeå",
			//},
			//	Muncipality = await ctx.Muncipalities.FirstAsync(m => m.Name == "Umeå"),
			//	RealEstateAgent = await ctx.Agents.FirstAsync(b => b.FirstName == "Sara")
			//},
			//new Property
			//{
			//	ListingPrice = 1 995 000,
			//	LivingSpace = 87,
			//	SecondaryArea = 0,
			//	LotSize = 1900,
			//	Description = "Varmt välkomna till denna rymliga stuga med åretrunt standard. Stora härliga ytor perfekt för umgänge för familj och vänner men ändå gott om sovrum för övernattande gäster.",
			//	NumberOfRooms = 3,
			//	MonthlyFee = 0,
			//	OperationalCostPerYear = 23203,
			//	YearBuilt = 1975,
			//	PropertyType = PropertyType.House,
			//Images = new List<PropertyImage>
			//{
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/37/e9/37e909d3e062a24993ae429905a8ff50.jpg"},
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/b5/5e/b55ebad229e7e7016da477f54d74ece8.jpg"},
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/41/0d/410dd883aa8757ae8f8aff69c778772d.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/97/1e/971e57cf9b11eb79ef3730a5a405bd39.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/83/17/83175e818e9a42070d798581dec9b9a7.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_portrait_cut/db/d5/dbd53d2be71404b3dda1794164fbf119.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/9d/e1/9de1a57021a65dc23c60f9e4756e009a.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/e6/d7/e6d772a6d695addc33510482ea01dd8a.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/62/03/620308d24ba804cb87489930d01a19bc.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_portrait_cut/1b/3b/1b3b1f8c928296bd909a271517454394.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_portrait_cut/c1/00/c100b8d1c11fdd69a661e150621ccc4b.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/45/ba/45bab4838e10c0ef652a65d7b57cb860.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/ae/fd/aefd5282bbabcedcfd9f2ce6a60b08cf.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/f1/92/f1920e0441cfecec7399da1a78e4020c.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_portrait_cut/1c/fa/1cfa11c4579e2d767355d734365b06c6.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/a2/47/a247016ed06e7007e96de7ae292e1c7b.jpg" },
			//},
			//Address = new Address
			//{
			//	Street = "Ugglevägen 9",
			//	PostalCode = "905 83",
			//	City = "Umeå",
			//},
			//	Muncipality = await ctx.Muncipalities.FirstAsync(m => m.Name == "Umeå"),
			//	RealEstateAgent = await ctx.Agents.FirstAsync(b => b.FirstName == "Sara")
			//},
			//new Property
			//{
			//	ListingPrice = 1695000,
			//	LivingSpace = 100,
			//	SecondaryArea = 0,
			//	LotSize = 1429,
			//	Description = "",
			//	NumberOfRooms = 3,
			//	MonthlyFee = 0,
			//	OperationalCostPerYear = 24320,
			//	YearBuilt = 1977,
			//	PropertyType = PropertyType.House,
			//Images = new List<PropertyImage>
			//{
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/b4/48/b4488520b69334b8d4502c7a6397ee22.jpg"},
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/79/89/7989fd0cb0a531e650e59d99d8a07a07.jpg"},
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/fd/9a/fd9a82ed908b1b0c1370f6aae7b7f309.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/36/46/36462bb2ae0578ad59356559c3a8a9b6.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/51/4e/514e83ef4ea994a64030f30889d337a6.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/f9/00/f900f38c1d99a7fb62241c3a71c4b55c.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/31/25/312584cce0c6f8721882504472e771e5.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/96/38/96380233ebf3c571da8bc24ae0006e7b.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/97/55/975532e19f64675fccac33e8a604a17d.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/74/76/7476c107bb5237032ea5dcac1a1298be.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/cc/c2/ccc224f658e7306c4015c576281b13b6.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/1c/82/1c820e1caa55e94b0f279dff1dd7ac0a.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/07/04/0704c5f1e04175cca0ea1b415c797f45.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/89/88/8988070e89dce7ec76a08c1b1318925b.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_portrait_cut/b9/43/b9434558efc03451d002e80f9aaa596c.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/dc/8e/dc8ead1eb6a225eca41f1eb9f410b21e.png" },
			//},
			//Address = new Address
			//{
			//	Street = "Ängersjö 9",
			//	PostalCode = "910 20",
			//	City = "Umeå",
			//},
			//	Muncipality = await ctx.Muncipalities.FirstAsync(m => m.Name == "Umeå"),
			//	RealEstateAgent = await ctx.Agents.FirstAsync(b => b.FirstName == "Sara")
			//},
			//new Property
			//{
			//	ListingPrice = 4295000,
			//	LivingSpace = 155,
			//	SecondaryArea = 0,
			//	LotSize = 2955,
			//	Description = "Högt i tak, ljus i överflöd och havet runt hörnet – det här är inte bara ett hus, det är ett livsstilsboende. Med generösa ytor, en öppen planlösning och en stämningsfull kamin i hjärtat av vardagsrummet bjuder detta hem in till både stilla vardagar och stora middagar.",
			//	NumberOfRooms = 4,
			//	MonthlyFee = 0,
			//	OperationalCostPerYear = 26215,
			//	YearBuilt = 2006,
			//	PropertyType = PropertyType.House,
			//Images = new List<PropertyImage>
			//{
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/a3/8a/a38a36faea4d6a335d22ec6f1b7b9969.jpg"},
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/00/8e/008ed980b014e307b09debae4c98ddcf.jpg"},
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/f8/a7/f8a709ac9b760ec29cc43a923bad6f57.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/f0/91/f091c001c4cfa63ad8ad7b895292b2e1.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/66/38/66385fed2d584070b5593ce5fc894d29.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_portrait_cut/ee/96/ee96e9484df54d91f371a9443925e215.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/0a/a9/0aa9a900b4f92e95da50202307b032d8.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/7b/34/7b345c0499c9876973a48c8f7faba153.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_portrait_cut/0b/2c/0b2c8e1c969cd78c6ba76351d321a7af.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/a1/24/a12409cb9a2b586c728254836eec9f2a.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/03/67/036724a31ed2cd44d2b3942ec8891d5f.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/02/f7/02f7f2453ed3081585d2089de7494645.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/df/0a/df0a77f0799389ee8492815784f969e7.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/62/f6/62f6e58244a75af72434d70c0ecf0d8d.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/af/04/af04678f4a028a3e515d7ce0901bae66.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/2d/0e/2d0e3238d76a586e3cfd0d52e6e09fb8.png" },
			//},				
			//Address = new Address
			//{
			//	Street = "Verkholmen 48A",
			//	PostalCode = "907 88",
			//	City = "Umeå",				//},
			//	Muncipality = await ctx.Muncipalities.FirstAsync(m => m.Name == "Umeå"),
			//	RealEstateAgent = await ctx.Agents.FirstAsync(b => b.FirstName == "Sara")
			//},
			//new Property
			//{
			//	ListingPrice = 1995000,
			//	LivingSpace = 66,5,
			//	SecondaryArea = 0,
			//	LotSize = 0,
			//	Description = "Välkommen till 66,5 välplanerade kvadratmeter som inrymmer allt du behöver - för ett boende över lång tid!",
			//	NumberOfRooms = 2,
			//	MonthlyFee = 4551,
			//	OperationalCostPerYear = 2796,
			//	YearBuilt = 1988,
			//	PropertyType = PropertyType.Condo,
			//Images = new List<PropertyImage>
			//{
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/eb/6e/eb6e8cfbdffb5ceecbfaf88e314cee08.jpg"},
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/82/c3/82c3d0105f83903e1db98f24aa27fa1b.jpg"},
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/60/79/607938b2e7b760ed5589e1090183ef56.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/60/79/607938b2e7b760ed5589e1090183ef56.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/95/b8/95b84c3c771d69b6bc745e79875b69d5.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/15/c4/15c49f45a76badcb9e48f5a0b0987d0f.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/4e/21/4e215f9312ef906d925290ee78419c09.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/13/04/1304127f6a727ca74512947985e2bde1.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/c3/d9/c3d96494bfca7aa9ca6e79118516ec65.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/79/ed/79ed49d9d809099a144d731275654af6.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/f8/74/f8748bf4b9f42e82cf66c1980f548c59.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/8d/77/8d779e5a47e14a3558edea1860d9b27f.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/db/13/db13fe02f476bbcc228e3d68125aa40e.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/20/72/20727a7f02dbdd2e6cfb84c27572107d.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/87/9e/879e08b83724ebe1d28baca8e5361c29.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_portrait_cut/5a/be/5abe60b96223f89e03f707db28e220aa.jpg" },
			//},
			//Address = new Address
			//{
			//	Street = "Kuratorvägen 48",
			//	PostalCode = "907 36",
			//	City = "Umeå",
			//},
			//	Muncipality = await ctx.Muncipalities.FirstAsync(m => m.Name == "Umeå"),
			//	RealEstateAgent = await ctx.Agents.FirstAsync(b => b.FirstName == "Sara")
			//},
			//new Property
			//{
			//	ListingPrice = 5495000,
			//	LivingSpace = 252,
			//	SecondaryArea = 162,
			//	LotSize = 1143,
			//	Description = "Välkommen till en anrik och vacker fastighet som bjuder på något utöver det vanliga.",
			//	NumberOfRooms = 9,
			//	MonthlyFee = 0,
			//	OperationalCostPerYear = 52071,
			//	YearBuilt = 1800,
			//	PropertyType = PropertyType.House,
			//Images = new List<PropertyImage>
			//{
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/52/5a/525ae8702aeece91ad902d6fd4d74997.jpg"},
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/b7/5a/b75a0f58b9547b58a33ea930b33a5931.jpg"},
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/d9/d2/d9d2e7506ffc268293aa3b150c89c3dc.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/dd/12/dd127f9e500da91dbd84b3f251dd7745.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/79/3b/793b598fe64090597a30c3b22d02e5f5.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/bb/70/bb707fdbfc1802940e555edd7c4cf518.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/be/1b/be1b1e07f6b4af90dd858815aeb043dd.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/02/fc/02fcba67ea478c025faae3e979808e4f.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/2a/90/2a902dbdf0875d8b9b5db84acc5b509b.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/37/77/37772c46e1413d4024829e3c2f57564a.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/ba/a9/baa9bf6dacbbd42546b32129e0eaa797.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_portrait_cut/a9/87/a987fefb2047d767f25979b58b3a903c.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_portrait_cut/43/4c/434c12e64967e04c051655fb761a2fd4.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_portrait_cut/ad/19/ad19ec1117cfd0018416f517f43ad3ff.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_portrait_cut/65/e4/65e4a59b68be08ebdca912c4e7e71930.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_portrait_cut/09/2b/092b36c65b91591e0ac2e11002572a3c.jpg" },
			//},
			//Address = new Address
			//{
			//	Street = "Fiskebyvägen 39",
			//	PostalCode = "907 88",
			//	City = "Umeå",
			//},
			//	Muncipality = await ctx.Muncipalities.FirstAsync(m => m.Name == "Umeå"),
			//	RealEstateAgent = await ctx.Agents.FirstAsync(b => b.FirstName == "Sara")
			//},
			//new Property
			//{
			//	ListingPrice = 1625000,
			//	LivingSpace = 60,
			//	SecondaryArea = 0,
			//	LotSize = 0,
			//	Description = "I ett omtyckt område, bekvämt belägen på första våningen, hittar ni denna mycket trivsamma och helrenoverade tvåa.",
			//	NumberOfRooms = 2,
			//	MonthlyFee = 3914,
			//	OperationalCostPerYear = 0,
			//	YearBuilt = 0,
			//	PropertyType = PropertyType.Condo,
			//Images = new List<PropertyImage>
			//{
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/00/38/003822607fe2526eaadfde1a755045b4.jpg"},
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/eb/b3/ebb3a54c86a8f6b8fac636009ba7799c.jpg"},
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/ac/3a/ac3a1a9baf1d58a81ca8bdd9ce2e23ee.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/dc/d6/dcd626e612203de263eb0568bb060e55.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/d2/57/d2574f4ec35e2e4d2d6b6e8ccd9dee36.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/a4/25/a4258fbb45976a8cea0548de05b440f5.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/3d/51/3d51f647e590dba33cd628f616ab8b33.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/13/67/13676115ab9d0d50086b80c5ae7bc139.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/fa/2b/fa2bbd99a456096094d1913a06c059ca.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/17/d0/17d08a75ed0c61323c0e5848520e1f81.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/33/43/334344106244a7fb3ed27252e6404eca.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/fd/fc/fdfc6d357bac226c80e7dc7b174f36b1.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_portrait_cut/ab/ed/abed079033381b0adc9700e1898e7a1a.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/c9/1c/c91cb93f8131f19efa6842fef5222482.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/ab/7e/ab7eca5383dcf0a6e0b0a9c683da0e33.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_portrait_cut/59/ee/59ee6651fe152ba6f4fcae86d42e6bd3.jpg" },
			//},
			//Address = new Address
			//{
			//	Street = "Biologigränd 37",
			//	PostalCode = "907 32",
			//	City = "Umeå",
			//},
			//	Muncipality = await ctx.Muncipalities.FirstAsync(m => m.Name == "Umeå"),
			//	RealEstateAgent = await ctx.Agents.FirstAsync(b => b.FirstName == "Sara")
			//},
			//new Property
			//{
			//	ListingPrice = 4300000,
			//	LivingSpace = 125,
			//	SecondaryArea = 58,
			//	LotSize = 468,
			//	Description = "",
			//	NumberOfRooms = 5,
			//	MonthlyFee = 0,
			//	OperationalCostPerYear = 39962,
			//	YearBuilt = 1966,
			//	PropertyType = PropertyType.TownHouse,
			//Images = new List<PropertyImage>
			//{
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/43/0a/430ab1d4947d93fca49b094dea6f2dbe.jpg"},
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/91/9a/919a5bd92416b3ce917c888e488e4d58.jpg"},
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/96/92/9692cf13499b0d2379ab2abbebd11b1a.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/95/2b/952bc9786a826de9d4d12c4ada766bc8.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/7d/a6/7da634d03e9bf26e8de3693fc5a503a5.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/0f/3b/0f3bbc6942ef29f243b3c55b7e3ac4f9.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/89/09/8909a7a7a4c3e52e2528e6ab9bc79b0b.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/77/7b/777b487e976ea4bbbbca125a851ce986.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/ee/69/ee69e8fab84c47ce7390d8cdbe0b4b8c.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/a1/01/a101501d68d1708f44a084cfa442b8ba.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/86/f7/86f7d5cc3d1640be7c1b1787f5ad0fb2.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/9c/8a/9c8aa3f2660e77991e4f521d81cfa706.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/24/97/249713867f7f2051d8fb00f1b0cc9835.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/c2/8f/c28f72ada7c908ffe99f229f9e635238.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_portrait_cut/be/3b/be3b1024f928ef10190f7d9917b2a647.jpg" },
			//	new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_portrait_cut/7a/63/7a63dee87f4ddd5a70dfad3024df6db1.jpg" },
			//},
			//Address = new Address
			//{
			//	Street = "Snickargatan 10",
			//	PostalCode = "903 60",
			//	City = "Umeå",
			//},
			//	Muncipality = await ctx.Muncipalities.FirstAsync(m => m.Name == "Umeå"),
			//	RealEstateAgent = await ctx.Agents.FirstAsync(b => b.FirstName == "Sara")
			//},
			};
			ctx.Properties.AddRange(list);
			await ctx.SaveChangesAsync();
		}
	}
}