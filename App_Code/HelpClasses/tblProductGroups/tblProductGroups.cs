using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace tblProductGroups
{
    /// <summary>
    /// Summary description for tblCompanies
    /// </summary>
    public class tblProductGroups
    {
        // Default Constructor
        public tblProductGroups() { }

        # region Private Data Member Declaration


        private System.Guid _ProductsGroupID;
        private string _Description = null;
        private string _ThumbnailDescription = null;
        //////---------Code Added in 30 dec---------//////////////////////

        private string _Description_Anjolee = null;
        private string _ThumbDescription_Anjolee = null;
        private string _ThumbAnjoleeTitle = null;
        /////////////////////////////////////////////////////////////////
        

        private string _ProductsGroupName = null;
        private System.Guid _ImageID;
        private System.Guid _thumbImageID;
        private int _VisualOrderIndex;
        private System.Guid _ParentProductGroupID;
        private float _MinimumPrice;
        //private float? _MaximumPrice=null;
        private float _MaximumPrice;
        private string _DefaultSize = null;
        private string _GroupDimension = null;
        private string _AudioLink = null;
        private int _Size = 0;
        private int _Length = 0;
        private int _Enable = 0;



        #endregion

        # region Public Properties Implementation

        public System.Guid ProductsGroupID
        {
            get { return _ProductsGroupID; }
            set { _ProductsGroupID = value; }
        }


        public System.Guid ParentProductGroupID
        {
            get { return _ParentProductGroupID; }
            set { _ParentProductGroupID = value; }
        }

        public int VisualOrderIndex
        {
            get { return _VisualOrderIndex; }
            set { _VisualOrderIndex = value; }
        }

        public System.Guid ImageID
        {
            get { return _ImageID; }
            set { _ImageID = value; }
        }
        public System.Guid thumbImageID
        {
            get { return _thumbImageID; }
            set { _thumbImageID = value; }
        }



        public string ProductsGroupName
        {
            get { return _ProductsGroupName; }
            set { _ProductsGroupName = value; }
        }
        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }
        public string ThumbnailDescription
        {
            get { return _ThumbnailDescription; }
            set { _ThumbnailDescription = value; }
        }

        //////---------Code Added in 30 dec---------//////////////////////

        public string Description_Anjolee
        {
            get { return _Description_Anjolee; }
            set { _Description_Anjolee = value; }
        }
        public string ThumbDescription_Anjolee
        {
            get { return _ThumbDescription_Anjolee; }
            set { _ThumbDescription_Anjolee = value; }
        }

        public string ThumbAnjoleeTitle
        {
            get { return _ThumbAnjoleeTitle; }
            set { _ThumbAnjoleeTitle = value; }
        }
        
        /////////////////////////////////////////////////////////////////////
        

        public float MinimumPrice
        {
            get { return _MinimumPrice; }
            set { _MinimumPrice = value; }
        }

        public float MaximumPrice
        {
            get { return _MaximumPrice; }
            set { _MaximumPrice = value; }
        }

        public string DefaultSize
        {
            get { return _DefaultSize; }
            set { _DefaultSize = value; }
        }

        public string GroupDimension
        {
            get { return _GroupDimension; }
            set { _GroupDimension = value; }
        }

        public string AudioLink
        {
            get { return _AudioLink; }
            set { _AudioLink = value; }
        }
        public int Size
        {
            get { return _Size; }
            set { _Size = value; }
        }
        public int Length
        {
            get { return _Length; }
            set { _Length = value; }
        }
        public int Enable
        {
            get { return _Enable; }
            set { _Enable = value; }
        }

        #endregion

    }
}