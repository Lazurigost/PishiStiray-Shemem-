﻿using PishiStiray.Models;
using PishiStiray.Models.DbEntities;

namespace PishiStiray.Infrastructure
{
    internal static class CurrentUser
    {
        public static User User { get; set; }
    }
}
