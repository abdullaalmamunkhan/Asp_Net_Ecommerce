using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModelsCore.Test
{
    public class VMTestTableList
    {
        public long ID { get; set; }
        public string Name { get; set; }

        public VMTestTableList()
        {

        }

        public VMTestTableList(DataRow objectRow)
        {
            if (objectRow["ID"] != DBNull.Value) this.ID = Convert.ToInt32(objectRow["ID"]);
            this.Name = objectRow["Name"] as System.String;
        }
    }
}
