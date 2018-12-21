using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static api.Models.Enums;

namespace api.Models
{
    public class Response
    {
        public bool success { get; set; }
        public string message { get; set; }
    }

    public class Cities
    {
        public int id { get; set; }
        public string name { get; set; }
        public string slug { get; set; }
    }

    public class Category
    {
        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cat_id">Kategori numarası</param>
        /// <param name="main_category_id">Ana kategori numarası</param>
        /// <param name="cat_order">Kategori sırası</param>
        /// <param name="cat_name">Kategori adı</param>
        /// <param name="slug">Kategori seo url</param>
        /// <param name="icon">Kategori ikonu</param>
        /// <param name="picture">Kategori resmi</param>
        public Category(int cat_id, int? main_category_id, int cat_order, string cat_name, string icon, string picture)
        {
            this.cat_id = cat_id;
            this.main_cat_id = main_cat_id;
            this.cat_order = cat_order;
            this.cat_name = cat_name;
            //this.slug = slug;
            this.icon = icon;
            this.picture = picture;
        }

        public int cat_id { get; set; }
        public int? main_cat_id { get; set; }
        public int cat_order { get; set; }
        public string cat_name { get; set; }
        //public string slug { get; set; }
        public string slug { get => UrlManager.ToSeoUrl(cat_name); set => cat_name = value; }
        public string icon { get; set; }
        public string picture { get; set; }
        public isActive photo_show { get; set; }
        public isActive price_show { get; set; }

    }

    public class CategoryTranslation
    {
        public int id { get; set; }
        public int translation_id { get; set; }
        public string lang_code { get; set; }
        public string category_type { get; set; }
        public string title { get; set; }
        public string slug { get; set; }
    }

    public class Languages
    {
        public int id { get; set; }
        public string code { get; set; }
        public string direction { get; set; }
        public string name { get; set; }
        public string file_name { get; set; }
        public bool active { get; set; }
        public bool isDefault { get; set; }
    }

    public static class UrlManager
    {
        public static string ToSeoUrl(string IncomingText)
        {
            IncomingText = IncomingText.Replace("ş", "s");
            IncomingText = IncomingText.Replace("Ş", "s");
            IncomingText = IncomingText.Replace("İ", "i");
            IncomingText = IncomingText.Replace("I", "i");
            IncomingText = IncomingText.Replace("ı", "i");
            IncomingText = IncomingText.Replace("ö", "o");
            IncomingText = IncomingText.Replace("Ö", "o");
            IncomingText = IncomingText.Replace("ü", "u");
            IncomingText = IncomingText.Replace("Ü", "u");
            IncomingText = IncomingText.Replace("Ç", "c");
            IncomingText = IncomingText.Replace("ç", "c");
            IncomingText = IncomingText.Replace("ğ", "g");
            IncomingText = IncomingText.Replace("Ğ", "g");
            IncomingText = IncomingText.Replace(" ", "-");
            IncomingText = IncomingText.Replace("---", "-");
            IncomingText = IncomingText.Replace("?", "");
            IncomingText = IncomingText.Replace("/", "");
            IncomingText = IncomingText.Replace(".", "");
            IncomingText = IncomingText.Replace("'", "");
            IncomingText = IncomingText.Replace("#", "");
            IncomingText = IncomingText.Replace("%", "");
            IncomingText = IncomingText.Replace("&", "");
            IncomingText = IncomingText.Replace("*", "");
            IncomingText = IncomingText.Replace("!", "");
            IncomingText = IncomingText.Replace("@", "");
            IncomingText = IncomingText.Replace("+", "");

            IncomingText = IncomingText.ToLower();
            IncomingText = IncomingText.Trim();

            // tüm harfleri küçült
            string encodedUrl = (IncomingText ?? "").ToLower();

            // & ile " " yer değiştirme
            encodedUrl = Regex.Replace(encodedUrl, @"&+", "and");

            // " " karakterlerini silme
            encodedUrl = encodedUrl.Replace("'", "");

            // geçersiz karakterleri sil
            encodedUrl = Regex.Replace(encodedUrl, @"[^a-z0-9]", "-");

            // tekrar edenleri sil
            encodedUrl = Regex.Replace(encodedUrl, @"-+", "-");

            // karakterlerin arasına tire koy
            encodedUrl = encodedUrl.Trim('-');

            return encodedUrl;
        }
    }
}
