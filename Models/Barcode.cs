using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ENTBarCodeAPI.Models
{
    public class Barcode
    {
       [Key]
        public int BarcodeId { get; set; }
       [Required]
        [DisplayName("BarCodeName")]
        public string BarcodeInitial { get; set; }
       [Required]
        public string BarcodeDescription { get; set; }
       [Required]
        public int BarcodePackNo { get; set; }
       [Required]
       [DisplayName("BarcodeCreatedDate")]
        //[DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy hh:mm:ss tt}", ApplyFormatInEditMode = true)]
        public DateTime BarcodeCreatedDate { get; set; }

        //public string BarcodeCreatedDate { get; set; }
        public int BarcodeActive { get; set; } = 0;


    }
}