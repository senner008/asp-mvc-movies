using Microsoft.AspNetCore.Mvc.Filters;

namespace MvcMovie.Models
{

    public class ScaffoldingOptions : System.Attribute
    {
        public string Name { get; set; }
        public string Prop { get; set; }

        public ScaffoldingOptions(string name, string prop)
        {
            this.Name = name;
            this.Prop = prop;

        }

        
    }

}
