using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Notissimus_Task.Data;
using Notissimus_Task.Models;
using System;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Runtime.ConstrainedExecution;
using System.Security.AccessControl;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using static System.Net.Mime.MediaTypeNames;

namespace Notissimus_Task.Controllers
{
    public class HomeController : Controller
    {
        

        private readonly IHttpClientFactory _httpClientFactory = null!;
        private readonly ApplicationDbContext _db;


        public HomeController(IHttpClientFactory httpClientFactory, ApplicationDbContext db)
        {
            _httpClientFactory = httpClientFactory;
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            int offerId = 12344;

            using HttpClient client = _httpClientFactory.CreateClient();

            var response = await client.GetByteArrayAsync("http://partner.market.yandex.ru/pages/help/YML.xml");

            var responseString = Encoding.GetEncoding("windows-1251").GetString(response, 0, response.Length);

            XmlDocument xmlDoc = new();
            xmlDoc.LoadXml(responseString);

            XmlElement? xRoot = xmlDoc.DocumentElement;

            MusicOffer? musicOffer = null;

            if (xRoot != null)
            {
                var shopNode = xRoot.SelectSingleNode("shop");

                if (shopNode != null)
                {

                    var offersNode = shopNode.SelectSingleNode("offers");

                    if (offersNode != null)
                    {
                        foreach (XmlNode offer in offersNode.ChildNodes)
                        {
                            if (offer.Attributes != null && offer.Attributes.Count > 0)
                            {
                                if (offer.Attributes.GetNamedItem("id")?.Value == offerId.ToString())
                                {
                                    XmlSerializer xmlSer = new(typeof(MusicOffer));

                                    using var xmlNodeReader = new XmlNodeReader(offer);
                                    musicOffer = xmlSer.Deserialize(xmlNodeReader) as MusicOffer;

                                    var dbItem = await _db.musicOffers.FindAsync(offerId);

                                    if (dbItem == null)
                                    {
                                        using var transaction = _db.Database.BeginTransaction();
                                        await _db.AddAsync(musicOffer!);
                                        _db.Database.ExecuteSqlRaw("SET IDENTITY_INSERT TaskDb.dbo.musicOffers ON;");
                                        _db.SaveChanges();
                                        _db.Database.ExecuteSqlRaw("SET IDENTITY_INSERT TaskDb.dbo.musicOffers OFF");
                                        transaction.Commit();
                                    }

                                    break;
                                }
                            }
                        }
                    }
                }
            }

            return View(musicOffer);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}