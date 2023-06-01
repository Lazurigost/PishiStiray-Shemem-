using PishiStiray.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PishiStiray.Infrastructure
{
    public static class ChangableOrder
    {
        public static Order? order { get; set; }
    }
}
