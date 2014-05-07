namespace Grid.Options
{

    #region << Using >>

    #endregion

    public class GridOptions
    {
        #region Static Fields

        public static readonly GridOptions Default = new GridOptions();

        #endregion

        #region Properties

        public virtual string Arrows { get; set; }

        public virtual string NoRecordsText { get; set; }

        #endregion

        #region Api Methods

        public virtual bool IsArrowsBootstrap()
        {
            return Arrows == "bootstrap";
        }

        #endregion
    }
}