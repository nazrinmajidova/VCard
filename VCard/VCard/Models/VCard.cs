using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static VCardProject.Program;

namespace VCardProject.Models
{
    public class VCard
    {
        public Name Name { get; set; }
        public Location Location { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Country { get; set; }
        public string City { get; set; }


        // VCard-ı string kimi formatlaşdıran method
        public string VCardFormat()
        {
            return $"BEGIN         :VCARD\r\n" +
                   $"VERSION       :3.0\r\n" +
                   $"FN            :{Name.First} {Name.Last}\r\n" +
                   $"EMAIL         :{Email}\r\n" +
                   $"TEL           :{Phone}\r\n" +
                   $"ADR;TYPE=HOME :;;{City};;{Country}\r\n" +
                   $"END:VCARD";

        }

        // VCard stringi file`a vcf file şəklində save etmək üçün static method
        public static void SaveMethod(string VCardString, string fileName, string directory)
        {
            var Directory = $@"{directory}";
            var Filename = $"{fileName}.vcf";
            var fullPath = Path.Combine(Directory, Filename);
            File.WriteAllText(fullPath, VCardString);
        }
    }

    // APİ- dan alınmış JSON responseları göstərən class
    public class RandomUserResponse
    {
        [JsonProperty("results")]
        public VCard[] Results { get; set; }
    }


    public class Name
    {
        public string First { get; set; }
        public string Last { get; set; }
    }

    public class Location
    {
        public string Country { get; set; }
        public string City { get; set; }
    }

}

