namespace HealthCare.Client.Shared
{
    public class NavItems
    {
        public string Name { get; set; }
        public bool Updated { get; set; }
        public bool New { get; set; }
        public string Path { get; set; }
        public string Title { get; set; }
        public string Icon { get; set; }
        public string[] Tags { get; set; }
        public bool Expanded { get; set; }=false;
        public NavItems[] Children { get; set; }
    }
}
