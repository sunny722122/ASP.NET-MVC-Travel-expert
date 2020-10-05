using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using MVCTravelExpert00.Models;

namespace MVCTravelExpert00.Models
{
    public class MappingOrderhistory:Profile
    {
        public MappingOrderhistory()
        {
            CreateMap<OrderHistoryViewModel,OrderHistory>();
        }

        //public static void run()
        //{
        //    Mapper.Initialize(a => { a.AddProfile<MappingOrderhistory>(); });
        //}
    }
}