using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.InternalContracts.Application.Models
{
    public class ApplicationModel
    {

        [Column("id")]
        public Guid Id { get; set; }

        [Column("author")]
        public string Author { get; set; }

        [Column("activity")]
        public string Activity { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("description")]
        public string Description { get; set; }

        [Column("plan")]
        public string Plan { get; set; }


    }

    internal enum ActivityKind
    {
        None,
        Report,
        Tutorial,
        Discussion
    }
}
