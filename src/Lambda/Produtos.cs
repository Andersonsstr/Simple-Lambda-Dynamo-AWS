using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon.DynamoDBv2.DataModel;

namespace Lambda
{

    [DynamoDBTable("produtos")]
    public class Produtos
    {
        [DynamoDBHashKey("pk")]
        public string Pk { get; set; }

        [DynamoDBRangeKey("sk")]
        public string Sk { get; set; } 

        public string Descricao { get; set; }
        public string Preco { get; set; }
        public string Tipo { get; set; }
    }
}
