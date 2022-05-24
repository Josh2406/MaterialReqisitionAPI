using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace MaterialRequisition.Application.DTOs.Request
{
    public class InventoryItemPostRequest
    {
        public int CategoryId { get; set; }

        [Required, StringLength(50)]
        public string ItemName { get; set; }

        [StringLength(150)]
        public string Description { get; set; }

        [Required, StringLength(50)]
        public string StandardUnit { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
