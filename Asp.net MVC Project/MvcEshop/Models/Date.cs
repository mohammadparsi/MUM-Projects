using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcEshop.Models
{
    public class Date
    {
        public Date()
        {
            Days = new List<Day>();
            Monthes = new List<Month>();
            Years = new List<Year>();

            for (int i = 1; i <= 31; i++)
            {
                Day oDay=new Day();
                oDay.DayId=i;
                oDay.Name = i.ToString();
                Days.Add(oDay);
            }

            Day nullDay = new Day();
            nullDay.DayId = 0;
            nullDay.Name = "";
            Days.Add(nullDay);


            for (int i = 1; i <= 12; i++)
            {
                Month oMonth = new Month();
                oMonth.MonthId = i;
                oMonth.Name = i.ToString();
                Monthes.Add(oMonth);
            }

            Month nullMonth = new Month();
            nullMonth.MonthId = 0;
            nullMonth.Name = "";
            Monthes.Add(nullMonth);

            
            for (int i = 1393; i <= 1493; i++)
            {
                Year oYear = new Year();
                oYear.YearId = i;
                oYear.Name = i.ToString();
                Years.Add(oYear);
            }

            Year nullYear = new Year();
            nullYear.YearId = 0;
            nullYear.Name = "";
            Years.Add(nullYear);


        }
        public List<Day> Days { get; set; }
        public List<Year> Years { get; set; }
        public List<Month> Monthes { get; set; }
    }
}