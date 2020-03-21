using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Askianoor.AdminPanel.Data.Models
{
    public class DashboardSetting
    {
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "The Website Title field is required.")]
        [StringLength(50, ErrorMessage = "Website Title is too long!")]
        public string WebsiteTitle { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "The Website Header field is required.")]
        public string WebsiteHeader { get; set; }

        //[Required(AllowEmptyStrings = false, ErrorMessage = "The Owner Name field is required.")]
        //[StringLength(50, ErrorMessage = "Owner Name is too long!")]
        public string OwnerName { get; set; }
        public string OwnerPictureSrc { get; set; }

        //[Required(AllowEmptyStrings = false, ErrorMessage = "The About Me Description field is required.")]
        public string AboutMeDescription { get; set; }

        public string AboutMeImage { get; set; }

        //[Required(AllowEmptyStrings = false, ErrorMessage = "The Home Page Image field is required.")]
        public string HomePageImage { get; set; }

        public string HomePageText { get; set; }

        //[Required(AllowEmptyStrings = false, ErrorMessage = "The Footer field is required.")]
        public string FooterText { get; set; }

        public DashboardSetting()
        {
            Id = 0;
            WebsiteTitle = "";
            WebsiteHeader = "";
            OwnerName = "";
            OwnerPictureSrc = "";
            AboutMeDescription = "";
            AboutMeImage = "";
            HomePageImage = "";
            HomePageText = "";
            FooterText = "";
        }

        public DashboardSetting(DashboardSetting dash)
        {
            Id = dash.Id;
            WebsiteTitle = dash.WebsiteTitle;
            WebsiteHeader = dash.WebsiteHeader;
            OwnerName = dash.OwnerName;
            OwnerPictureSrc = dash.OwnerPictureSrc;
            AboutMeDescription = dash.AboutMeDescription;
            AboutMeImage = dash.AboutMeImage;
            HomePageImage = dash.HomePageImage;
            HomePageText = dash.HomePageText;
            FooterText = dash.FooterText;
        }
    }
}
