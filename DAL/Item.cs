using System;

namespace DAL.JecaestevezApp
{
    public class Item
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Expiration { get; set; }
    }
}
