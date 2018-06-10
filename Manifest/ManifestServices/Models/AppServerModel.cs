using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManifestServices.Models
{
    public class AppServerModel
    {
        [Required]
        public string Name { get; set; }
        public AppServerModel(string name)
        {
            this.Name = name;
        }
    }
}
