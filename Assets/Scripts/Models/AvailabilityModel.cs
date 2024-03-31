using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

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

    private bool[] SHMonthAvailability = new bool[12];
    private bool[] NHMonthAvailability = new bool[12];

    public string TimeAvailability
        {
            get
            {
                string toReturn = "";
                
                var properties = GetType().GetProperties();
                foreach (var property in properties)
                {
                    if ((property.Name.Contains("NH") ||
                        property.Name.Contains("SH")) && !property.Name.Contains("Availability"))
                    {
                        if (property.GetValue(this).ToString() != "NA")
                        {
                            if (!toReturn.Contains(property.GetValue(this).ToString()))
                            {
                                toReturn += property.GetValue(this).ToString() + " | ";
                            }
                            
                        }
                    }
                }

                toReturn = toReturn.Substring(0, toReturn.Length - 3);

               return toReturn;
            }
        }

    public string NHAvailability
    {
        get
        {
            string monthAvailabilty = "";

            if (NHJan != "NA")
            {
                monthAvailabilty += "Jan | ";
                NHMonthAvailability[0] = true;
            }
            if (NHFeb != "NA")
            {
                monthAvailabilty += "Feb | ";
                NHMonthAvailability[1] = true;
            }
            if (NHMar != "NA")
            {
                monthAvailabilty += "Mar | ";
                NHMonthAvailability[2] = true;
            }
            if (NHApr != "NA")
            {
                monthAvailabilty += "Apr | ";
                NHMonthAvailability[3] = true;
            }
            if (NHMay != "NA")
            {
                monthAvailabilty += "May | ";
                NHMonthAvailability[4] = true;
            }
            if (NHJun != "NA")
            {
                monthAvailabilty += "Jun | ";
                NHMonthAvailability[5] = true;
            }
            if (NHJul != "NA")
            {
                monthAvailabilty += "Jul | ";
                NHMonthAvailability[6] = true;
            }
            if (NHAug != "NA")
            {
                monthAvailabilty += "Aug | ";
                NHMonthAvailability[7] = true;
            }
            if (NHSep != "NA")
            {
                monthAvailabilty += "Sep | ";
                NHMonthAvailability[8] = true;
            }
            if (NHOct != "NA")
            {
                monthAvailabilty += "Oct | ";
                NHMonthAvailability[9] = true;
            }
            if (NHNov != "NA")
            {
                monthAvailabilty += "Nov | ";
                NHMonthAvailability[10] = true;
            }
            if (NHDec != "NA")
            {
                monthAvailabilty += "Dec | ";
                NHMonthAvailability[11] = true;
            }


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
            {
                monthAvailabilty += "Jan | ";
                SHMonthAvailability[0] = true;
            }
            if (SHFeb != "NA")
            {
                monthAvailabilty += "Feb | ";
                SHMonthAvailability[1] = true;
            }
            if (SHMar != "NA")
            {
                monthAvailabilty += "Mar | ";
                SHMonthAvailability[2] = true;
            }
            if (SHApr != "NA")
            {
                monthAvailabilty += "Apr | ";
                SHMonthAvailability[3] = true;
            }
            if (SHMay != "NA")
            {
                monthAvailabilty += "May | ";
                SHMonthAvailability[4] = true;
            }
            if (SHJun != "NA")
            {
                monthAvailabilty += "Jun | ";
                SHMonthAvailability[5] = true;
            }
            if (SHJul != "NA")
            {
                monthAvailabilty += "Jul | ";
                SHMonthAvailability[6] = true;
            }
            if (SHAug != "NA")
            {
                monthAvailabilty += "Aug | ";
                SHMonthAvailability[7] = true;
            }
            if (SHSep != "NA")
            {
                monthAvailabilty += "Sep | ";
                SHMonthAvailability[8] = true;
            }
            if (SHOct != "NA")
            {
                monthAvailabilty += "Oct | ";
                SHMonthAvailability[9] = true;
            }
            if (SHNov != "NA")
            {
                monthAvailabilty += "Nov | ";
                SHMonthAvailability[10] = true;
            }
            if (SHDec != "NA")
            {
                monthAvailabilty += "Dec | ";
                SHMonthAvailability[11] = true;
            }


            if (monthAvailabilty == "Jan | Feb | Mar | Apr | May | Jun | Jul | Aug | Sep | Oct | Nov | Dec | ")
                monthAvailabilty = "All Months";
            else
                monthAvailabilty = monthAvailabilty.Substring(0, monthAvailabilty.Length - 2);

            return monthAvailabilty;
        }

    }

    public bool isAvailableNow(string timeAvailability)
    {
        System.DateTime playerTime = TimeManager.GetInstance().PlayerTime;
            
        if (timeAvailability == "All day")
        {
            return true;
        }
        else
        {
            timeAvailability = timeAvailability.Replace("-", "");
            string[] splitTimeAvailability = timeAvailability.Split(" ", StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < splitTimeAvailability.Length; i++) 
            {
                splitTimeAvailability[i] = splitTimeAvailability[i].Trim(); 
            }

            int startHour = Int32.Parse(splitTimeAvailability[0]);
            if (splitTimeAvailability[1].Contains("PM"))
            {
                if (startHour != 12)
                    startHour += 12;
            }
            if (splitTimeAvailability[1].Contains("AM"))
            {
                if (startHour == 12)
                    startHour = 0;
            }

            int endHour = Int32.Parse(splitTimeAvailability[2]);
            if (splitTimeAvailability[3].Contains("PM"))
            {
                if (endHour != 12)
                    endHour += 12;
            }
            if (splitTimeAvailability[3].Contains("AM"))
            {
                if (endHour == 12)
                    endHour = 0;
            }

            System.DateTime startTime = new DateTime(playerTime.Year, playerTime.Month, playerTime.Day,
                                                     startHour, 0, 0);
            System.DateTime endTime = new DateTime(playerTime.Year, playerTime.Month, playerTime.Day,
                                                   endHour, 0, 0);

            if (TimeManager.IsBetweenTwoDates(playerTime, startTime, endTime))
            {
                return true;
            }

        }
        return false;
    }
}

