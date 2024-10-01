using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebApi.Models
{
    public class Product
    {
        //====================================================
        //建立資料模型
        //====================================================
        //指定屬性(Id,Name,Price)的資料類型(int,string,decimal)
        //get:允許讀取其屬性的值
        //set:允許設定其屬性的值
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
