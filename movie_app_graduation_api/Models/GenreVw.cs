using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MoviesApi.Models
{
    [Keyless]
    public partial class GenreVw
    {
        public byte Id { get; set; }
        [StringLength(100)]
        public string Name { get; set; } = null!;
    }
}
