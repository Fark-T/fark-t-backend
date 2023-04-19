﻿using System.ComponentModel.DataAnnotations;

namespace fark_t_backend.Models
{
    public class Orders
    {
        public Orders()
        {
            // Constructor logic here (if any)
        }
        [Key]
        public Guid Order_Id { get; set; }
        public string Restaurant { get; set; }
        public string Category { get; set; }
        public int Limit { get; set; }
        public int Count { get; set; }

        // Navigation property
        public Users User { get; set; } = null!;
    }
}