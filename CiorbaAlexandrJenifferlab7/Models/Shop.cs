using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CiorbaAlexandrJenifferlab7.Models
{

    public class Shop
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public string ShopName { get; set; }

        public string Adress { get; set; }

        [Ignore] // prevent sqlite-net from trying to map this computed property
        public string ShopDetails => ShopName + " " + Adress;

        [Ignore] // prevent sqlite-net from trying to map List<ShopList>
        public List<ShopList> ShopLists { get; set; }
    }
}