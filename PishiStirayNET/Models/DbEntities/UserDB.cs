﻿using System.ComponentModel.DataAnnotations;

namespace PishiStirayNET.Models.DbEntities
{
    internal class UserDB
    {
        [Key]
        public int UserID { get; set; }
        public string UserSurname { get; set; }
        public string UserName { get; set; }
        public string UserPatronymic { get; set; }
        public string UserLogin { get; set; }
        public string UserPassword { get; set; }
        public int UserRole { get; set; }
    }
}
