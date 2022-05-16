using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModelsCore.VMEcom.VMList
{
    public class VMUserList
    {
        public VMUserList()
        {

        }
        public long Id { get; set; }
        public string Name { get; set; }
        public string ProfileImage { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Apartment { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Dob { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedDate { get; set; }

        public VMUserList(DataRow objectRow)
        {
            this.Id = (objectRow["Id"] != DBNull.Value) ? long.Parse(objectRow["Id"].ToString()) : 0;
            this.Name = objectRow["Name"].ToString();
            this.ProfileImage = objectRow["ProfileImage"].ToString();
            this.Mobile = objectRow["Mobile"].ToString();
            this.Email = objectRow["Email"].ToString();
            this.Apartment = objectRow["Apartment"].ToString();
            this.State = objectRow["State"].ToString();
            this.Country = objectRow["Country"].ToString();
            this.City = objectRow["City"].ToString();
            this.PostalCode = objectRow["PostalCode"].ToString();
            this.Dob = objectRow["Dob"].ToString();
            //this.CreatedBy = objectRow["CreatedBy"].ToString();
            //this.CreatedDate = objectRow["CreatedDate"].ToString();
        }
    }
}
