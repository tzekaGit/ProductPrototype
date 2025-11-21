using ProductPrototype.DbAccess.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductPrototype.DbAccess.Data.ModelDto
{
    public class SpecificationAttributeOptionDto
    {
        public int SpecificationId { get; set; }
        public string SpecificationName { get; set; }
        public string SpecificationKey { get; set; }
        public int SpecificationAttributeId { get; set; }
        
     
    }
}
