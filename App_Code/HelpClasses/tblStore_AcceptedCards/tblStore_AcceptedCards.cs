using System;

namespace tblStore_AcceptedCards
{
    /// <summary>
    /// Summary description for tblStore_AcceptedCards.
    /// </summary>
    public class tblStore_AcceptedCards
    {
        public tblStore_AcceptedCards()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        #region Private variables Declaration
        private int _cardID;
        private string _cardName;
        private string _cardLogo;
        private int _cardOnline;
        #endregion

        #region Public Get Set Properties
        public int cardID
        {
            get { return _cardID; }
            set { _cardID = value; }
        }
        public string cardName
        {
            get { return _cardName; }
            set { _cardName = value; }
        }
        public string cardLogo
        {
            get { return _cardLogo; }
            set { _cardLogo = value; }
        }
        public int cardOnline
        {
            get { return _cardOnline; }
            set { _cardOnline = value; }
        }
        #endregion
    }
}
