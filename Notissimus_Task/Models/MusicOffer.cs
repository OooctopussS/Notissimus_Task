using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Notissimus_Task.Models
{
    [XmlRoot("offer")]
    public class MusicOffer
    {
        
        [XmlAttribute("id")]
        public int Id { get; set; }

        [XmlElement("url")]
        public string? Url { get; set; }

        [XmlElement("price")]
        public decimal Price;

        [XmlElement("currencyId")]
        public string? Currency { get; set; }

        [XmlElement("categoryId")]
        public string? Category { get; set; }

        [XmlElement("picture")]
        public string? Picture { get; set; }

        [XmlElement("delivery")]
        public bool Delivery { get; set; }

        [XmlElement("artist")]
        public string? Artist { get; set; }

        [XmlElement("title")]
        public string? Title { get; set; }

        [XmlElement("year")]
        public int Year { get; set; }

        [XmlElement("media")]
        public string? Media { get; set; }

        [XmlElement("description")]
        public string? Description { get; set; }

        public MusicOffer() { }

        public MusicOffer(decimal price, string? currency, string? category, string? picture,
                          bool delivery, string? artist, string? title, int year,
                          string? media, string? description, string? url)
        {
            Price = price;
            Currency = currency;
            Category = category;
            Picture = picture;
            Delivery = delivery;
            Artist = artist;
            Title = title;
            Year = year;
            Media = media;
            Description = description;
            Url = url;
        }
    }
}
