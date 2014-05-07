using System;

namespace Grid.Attributes
{
    [AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = false)]
    public class AutoBindAttribute : Attribute
    {
        public bool SortDefault { get; set; }
        public object SortBy { get; set; }
        public bool Hide { get; set; }
        public bool Raw { get; set; }
        public string Title { get; set; }
        public int Width { get; set; }
        public int WidthPct { get; set; }
    }
}