using Core.Entities;
using Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Helpers
{
    public class Service
    {
        public int ID { get; set; }

        public string OwnerID { get; set; }
        public Owner Owner { get; set; }

        //public String ServiceType { get; }

        //public string Discriminator { get; protected set; }
    }
}
