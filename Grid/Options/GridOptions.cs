using System;
using System.Linq;
using Grid.Components;
using Incoding.Extensions;

namespace Grid.Options
{
    using Incoding.MvcContrib;

    public class GridOptions
    {
        public static readonly GridOptions Default = new GridOptions();

        private string _styling = null;

        public virtual string NoRecordsText { get; set; }
        
        public virtual void AddStyling(string @class)
        {
            _styling = @class;
        }

        public virtual void AddStyling(BootstrapTable @class)
        {
            var classes = Enum.GetValues(typeof(BootstrapTable))
                .Cast<BootstrapTable>()
                .Where(r => @class.HasFlag(r))
                .Select(r => r.ToLocalization())
                .AsString(" ");

            AddStyling(classes);
        }

        public virtual string GetStyling()
        {
            return _styling;
        }
    }
}