using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blood.Models
{
    [MetadataType(typeof(usermetadata))]
    public partial class user
    {

    }

    public class usermetadata
    {
        [HiddenInput]
        public int Id { get; set; }
        [Required(ErrorMessage = "Please provide First name", AllowEmptyStrings = false)]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Please provide Last Name", AllowEmptyStrings = false)]
        public string LastName { get; set; }
         [RegularExpression(@"^([0-9a-zA-Z]([\+\-_\.][0-9a-zA-Z]+)*)+@(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]*\.)+[a-zA-Z0-9]{2,3})$",
                    ErrorMessage = "Please provide valid email id")]
         [Required(ErrorMessage = "Please provide Email Id", AllowEmptyStrings = false)]
        public string EmailId { get; set; }
         [Required(ErrorMessage = "Please provide Address", AllowEmptyStrings = false)]
        public string Address { get; set; }
        [Required(ErrorMessage = "Please provide state", AllowEmptyStrings = false)]
        public string State { get; set; }
        [Required(ErrorMessage = "Please provide Zipcode", AllowEmptyStrings = false)]
        public string Zipcode { get; set; }
        [Required(ErrorMessage = "Please provide Blood Type", AllowEmptyStrings = false)]
        public string BloodType { get; set; }
    }
}