using System.Text.Json;
using FribergHomeAPI.Models;
using Microsoft.EntityFrameworkCore;
using static FribergHomeAPI.Models.PropertyTypes;
using Microsoft.AspNetCore.Identity;
using FribergHomeAPI.Constants;

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

            new Property
            {
                ListingPrice = 1160000,
                LivingSpace = 47,
                SecondaryArea = 0,
                LotSize = 0,
                Description = "För dig som vill bo billigt i en stor etta ges nu möjlighet att förvärva denna kvadratsmarta bostad med modernt kök. Planlösningen är optimal med stort kök, rymlig hall och luftigt allrum. Allrummet ligger i gavel med fint ljusinsläpp och med möjlighet att skärma av rummet för att skapa ett separat sovrum. Bra förvaringsmöjligheter med fastbyggda garderober och källarförråd. Tack vare läget intill E45:an är det smidigt att ta sig vidare in till stan, till Hisingen eller norr ut.  Här bor du i en skuldfri förening med låg månadsavgift, låg parkeringsavgift (100 kr/mån), renoverat gym, bastu och stort förråd i källaren. Lägenheten har ett tillgängligt läge på andra våning med härlig vy från berget ner mot älven. \r\nOmrådet i Agnesberg präglas av vacker natur och passar utmärkt för er som uppskattar lugna gröna omgivningar men samtidigt vill ha nära in till city! Kommunikationsförbindelser via buss nås alldeles i anslutning till föreningen. Närmsta hållplats är Steken som servar området  med linje 173 (ca 20 min in till stan) samt linje 401 mot Angered centrum på bara några minuter. Angered centrum erbjuder stort utbud på matbutiker och övrig service. Härifrån kan vi även ta spårvagnar vidare till andra delar av stan.",
                NumberOfRooms = 2,
                MonthlyFee = 2142,
                OperationalCostPerYear = 3696,
                YearBuilt = 1965,
                PropertyType = PropertyType.Condo,
                Images = new List<PropertyImage>
                {
                    new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/5e/3c/5e3ca433bb5a28d841ce55523969ae98.jpg"},
                    new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/bc/41/bc411ddc7ae1b8e32b0683c140caa412.jpg"},
                    new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/d8/3f/d83fa443d40fc2c5b797e50498a3e582.jpg"},
                    new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/34/92/34928181ac8e56ac3c2db5b3067655de.jpg"},
                    new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/b3/f5/b3f5c8c670460f156368c474d85fb26d.jpg"},
                    new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/f5/46/f546628585e0eab3aa2c88550e68064d.jpg"},
                    new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/b4/01/b401c14a30d2f7d0d23072f22a651564.jpg"},
                    new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/f0/11/f011550b645b13a073b7dc2a2f2d9f18.jpg"}
                },
                Address = new Address
                {
                    Street = "Steken 3C",
                    PostalCode = "424 38",
                    City = "Angered",
                },
                Muncipality = await ctx.Muncipalities.FirstAsync(m => m.Name == "Göteborg"),
                RealEstateAgent = await ctx.Agents.FirstAsync(b => b.FirstName == "Bengt")

            },

            new Property
            {
                ListingPrice = 2565000,
                LivingSpace = 59,
                SecondaryArea = 0,
                LotSize = 0,
                Description = "Stilren tvåa med inglasad balkong i soligt läge!\r\nVälkommen till denna fina tvårummare om ca 58,5 kvm – en bostad som kombinerar modern komfort med ett utmärkt läge. Här bor du på fjärde våningen med fint ljusinsläpp och många soltimmar. De ljusa golven och den genomtänkta planlösningen skapar en trivsam och luftig känsla, samtidigt som bostaden är lättmöblerad och anpassningsbar efter din personliga stil.\r\nKöket är praktiskt avskilt från vardagsrummet, vilket ger en tydlig rumsindelning och möjlighet till en lugn matlagningsmiljö. Intill köket finns en naturlig plats för matbordet, perfekt för trevliga middagsbjudningar. I vardagsrummet finns gott om plats för både soffgrupp och tillhörande möblemang. Härifrån når du även den rymliga inglasade balkongen i soligt läge – en perfekt plats för morgonkaffet, kvällens bok eller umgänge med vänner.\r\nSom boende i föreningen har du dessutom tillgång till en härlig gemensam takterrass – perfekt för att njuta av soliga dagar och vacker utsikt över närområdet. Du kan boka glashuset, inklusive pentry, för att bjuda vänner och familj på en extra trevlig kväll med god mat och härlig vy. \r\nLäget är svårslaget. Här bor du nära både matbutiker, caféer och annan service, samtidigt som du har utmärkta kommunikationer med kollektivtrafiken bara ett stenkast bort. Området är en knutpunkt i ständig utveckling, där stadspulsen möter ett avslappnat kvartersliv.\r\nVälkommen till Borstbindaregatan 1!",
                NumberOfRooms = 4,
                MonthlyFee = 4450,
                OperationalCostPerYear = 6600,
                YearBuilt = 2015,
                PropertyType = PropertyType.Condo,
                Images = new List<PropertyImage>
                {
                    new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/1d/19/1d19ca836eaab75cd7b182734b48b526.jpg"},
                    new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/98/74/9874c6b580131ce8e0d27a6346ef9a8a.jpg"},
                    new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/04/25/04254d2f2c65a7a0171c4f89a3fc5c35.jpg"},
                    new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/0a/4d/0a4d18d1610a5b64beb44f477188153f.jpg"},
                    new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/c3/3d/c33d5712978e523b4c3e67724b8ebb63.jpg"},
                    new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/19/fc/19fc7ca35f80ca9e0852eb350a29316c.jpg"},
                    new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/02/3f/023f6824a93d0bb65647839c2d227830.jpg"},
                    new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/4d/35/4d3568bda649f18daddc2ff352a7172e.jpg"},
                    new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/57/49/5749bec1156629d98d149c99cfa34630.jpg"},
                    new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/e0/a3/e0a370a43734b52a1e0adc3f8633adbb.jpg"},
                    new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/2b/78/2b781ad5f65df5174514190dfa9f82bb.jpg"},
                    new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/cb/38/cb389e352664fc48b60a6c3c31651c7f.jpg"},
                    new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/bf/da/bfda01389817cb984a9eb7bb176056cb.jpg"},
                    new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/d9/59/d959aab0e64faf0d77c7914d988917f7.jpg"},
                    new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/f8/98/f8983b1e5dad82651c401f6dcd576452.jpg"},
                    new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/d0/64/d0649056a29a0b5a58f4b5b495fe8919.jpg"},
                    new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/2c/a4/2ca4bef843b73eb4d910a5afe99ea578.jpg"},
                    new PropertyImage { ImgURL = "https://bilder.hemnet.se/images/itemgallery_cut/e0/33/e03340ccd4d0ce09fd086ba6e4158a6b.jpg"}

                },
                Address = new Address
                {
                    Street = "Borstbindaregatan 1",
                    PostalCode = "417 22",
                    City = "Göteborg",
                },
                Muncipality = await ctx.Muncipalities.FirstAsync(m => m.Name == "Göteborg"),
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

			}};
			ctx.Properties.AddRange(list);
			await ctx.SaveChangesAsync();
		}
	}
}