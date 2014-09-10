namespace Grid.Components
{
    using System;
    using System.ComponentModel;

    [Flags]
    public enum BootstrapTable
    {
        [Description("table-striped")]
        Striped = 1,
        [Description("table-bordered")]
        Bordered = 2,
        [Description("table-hover")]
        Hover = 4,
        [Description("table-condensed")]
        Condensed = 8
    }
}
