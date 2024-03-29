using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.InternalContracts.Application.Models
{
    internal class ApplicationModel
    {

        [Column("id")]
        public Guid Id { get; set; }

        [Column("activity")]
        public ActivityKind Activity { get; set; }

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
