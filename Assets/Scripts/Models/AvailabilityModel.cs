using Newtonsoft.Json;
using System.Diagnostics;
using Unity.VisualScripting;

public class AvailabilityModel : BaseModel
    {
        [JsonProperty("NH Jan")]
        public string NHJan { get; set; }

        [JsonProperty("NH Feb")]
        public string NHFeb { get; set; }

        [JsonProperty("NH Mar")]
        public string NHMar { get; set; }

        [JsonProperty("NH Apr")]
        public string NHApr { get; set; }

        [JsonProperty("NH May")]
        public string NHMay { get; set; }

        [JsonProperty("NH Jun")]
        public string NHJun { get; set; }

        [JsonProperty("NH Jul")]
        public string NHJul { get; set; }

        [JsonProperty("NH Aug")]
        public string NHAug { get; set; }

        [JsonProperty("NH Sep")]
        public string NHSep { get; set; }

        [JsonProperty("NH Oct")]
        public string NHOct { get; set; }

        [JsonProperty("NH Nov")]
        public string NHNov { get; set; }

        [JsonProperty("NH Dec")]
        public string NHDec { get; set; }

        [JsonProperty("SH Jan")]
        public string SHJan { get; set; }

        [JsonProperty("SH Feb")]
        public string SHFeb { get; set; }

        [JsonProperty("SH Mar")]
        public string SHMar { get; set; }

        [JsonProperty("SH Apr")]
        public string SHApr { get; set; }

        [JsonProperty("SH May")]
        public string SHMay { get; set; }

        [JsonProperty("SH Jun")]
        public string SHJun { get; set; }

        [JsonProperty("SH Jul")]
        public string SHJul { get; set; }

        [JsonProperty("SH Aug")]
        public string SHAug { get; set; }

        [JsonProperty("SH Sep")]
        public string SHSep { get; set; }

        [JsonProperty("SH Oct")]
        public string SHOct { get; set; }

        [JsonProperty("SH Nov")]
        public string SHNov { get; set; }

        [JsonProperty("SH Dec")]
        public string SHDec { get; set; }
    
        public string TimeAvailability
        {
            get
            {
                var properties = GetType().GetProperties();
                foreach (var property in properties)
                {
                    if (property.Name.Contains("NH") ||
                        property.Name.Contains("SH"))
                    {
                        if (property.GetValue(this).ToString() != "NA")
                        {
                            return property.GetValue(this).ToString();
                            break;
                        }
                    }
                }
                return null;
            }
        }

    public string NHAvailability
    {
        get
        {
            string monthAvailabilty = "";

            if (NHJan != "NA")
                monthAvailabilty += "Jan | ";
            if (NHFeb != "NA")
                monthAvailabilty += "Feb | ";
            if (NHMar != "NA")
                monthAvailabilty += "Mar | ";
            if (NHApr != "NA")
                monthAvailabilty += "Apr | ";
            if (NHMay != "NA")
                monthAvailabilty += "May | ";
            if (NHJun != "NA")
                monthAvailabilty += "Jun | ";
            if (NHJul != "NA")
                monthAvailabilty += "Jul | ";
            if (NHAug != "NA")
                monthAvailabilty += "Aug | ";
            if (NHSep != "NA")
                monthAvailabilty += "Sep | ";
            if (NHOct != "NA")
                monthAvailabilty += "Oct | ";
            if (NHNov != "NA")
                monthAvailabilty += "Nov | ";
            if (NHDec != "NA")
                monthAvailabilty += "Dec | ";


            if (monthAvailabilty == "Jan | Feb | Mar | Apr | May | Jun | Jul | Aug | Sep | Oct | Nov | Dec | ")
                monthAvailabilty = "All Months";

            else 
                monthAvailabilty = monthAvailabilty.Substring(0, monthAvailabilty.Length - 2);

            return monthAvailabilty;
        }
    }

    public string SHAvailability
    {
        get
        {
            string monthAvailabilty = "";

            if (SHJan != "NA")
                monthAvailabilty += "Jan | ";
            if (SHFeb != "NA")
                monthAvailabilty += "Feb | ";
            if (SHMar != "NA")
                monthAvailabilty += "Mar | ";
            if (SHApr != "NA")
                monthAvailabilty += "Apr | ";
            if (SHMay != "NA")
                monthAvailabilty += "May | ";
            if (SHJun != "NA")
                monthAvailabilty += "Jun | ";
            if (SHJul != "NA")
                monthAvailabilty += "Jul | ";
            if (SHAug != "NA")
                monthAvailabilty += "Aug | ";
            if (SHSep != "NA")
                monthAvailabilty += "Sep | ";
            if (SHOct != "NA")
                monthAvailabilty += "Oct | ";
            if (SHNov != "NA")
                monthAvailabilty += "Nov | ";
            if (SHDec != "NA")
                monthAvailabilty += "Dec | ";


            if (monthAvailabilty == "Jan | Feb | Mar | Apr | May | Jun | Jul | Aug | Sep | Oct | Nov | Dec | ")
                monthAvailabilty = "All Months";    

            return monthAvailabilty;
        }
    }
}