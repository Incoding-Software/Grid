using Incoding.CQRS;
using Incoding.Data;

namespace GridUI.Setups
{
    #region << Using >>

    

    #endregion

    public class DataBaseSetup : ISetUp
    {
        #region Fields

        readonly IManagerDataBase dataBase;

        #endregion

        #region Constructors

        public DataBaseSetup(IManagerDataBase _dataBase)
        {
            this.dataBase = _dataBase;
        }

        #endregion

        #region ISetUp Members

        public int GetOrder()
        {
            return 0;
        }

        public void Execute()
        {
            if (!this.dataBase.IsExist())
                this.dataBase.Create();
        }

        #endregion

        #region Disposable

        public void Dispose() { }

        #endregion
    }
}