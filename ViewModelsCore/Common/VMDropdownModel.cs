using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModelsCore.Common
{
    public class VMDropdownModel
    {
        public VMDropdownModel()
        {

        }
        public long Id { get; set; }
        public string Name { get; set; }

        public VMDropdownModel(DataRow objectRow)
        {
            this.Id = long.Parse(objectRow["Id"].ToString());
            this.Name = objectRow["Name"].ToString();
        }   
    }
}
