using System.Collections.Generic;

namespace TestSkyori
{
    public class DataForChecks
    {
        public ScreenTypes ScreenResult { get; set; } = ScreenTypes.Menu;
        public  string Email { get; set; }
        public List<string> ConnectionDbStringList { get; set; } = new List<string>();
        public List<string> SiteList { get; set; } = new List<string>();

    }
}