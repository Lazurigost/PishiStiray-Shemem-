using PishiStirayNET.Models;
using PishiStirayNET.Models.DbEntities;

namespace PishiStirayNET.Infrastructure
{
    internal static class CurrentUser
    {
        public static User User { get; set; }
    }
}
