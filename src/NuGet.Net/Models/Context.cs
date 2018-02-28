namespace NuGet.Net.Models
{
    public class Context
    {
        public string Vocab { get; set; }
        public string Xsd { get; set; }
        public CatalogEntry CatalogEntry { get; set; }
        public Registration Registration { get; set; }
        public PackageContent PackageContent { get; set; }
        public Published Published { get; set; }
    }
}