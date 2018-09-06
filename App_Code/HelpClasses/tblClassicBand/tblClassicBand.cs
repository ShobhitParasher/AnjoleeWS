using System;
using System.Collections.Generic;
//using System.Linq;
using System.Web;

/// <summary>
/// Summary description for tblClassicBand
/// </summary>
public class tblClassicBand
{

    private string _Index;
    private string _StyleNumber = null;
    private string _ProductId = null;
    private string _ProductName = null;
    private string _LayoutImage = null;
    private List<KeyValuePair<string, string>> _ImageInfo = null;
   // private List<KeyValuePair<string, string>> _YellowImageColl = null;

   
    public string Index
    {
        get { return _Index; }
        set { _Index = value; }
    }


    public string StyleNumbe
    {
        get { return _StyleNumber; }
        set { _StyleNumber = value; }
    }
    public string ProductId
    {
        get { return _ProductId; }
        set { _ProductId = value; }
    }
    public string ProductName
    {
        get { return _ProductName; }
        set { _ProductName = value; }
    }
    public string LayoutImage
    {
        get { return _LayoutImage; }
        set { _LayoutImage = value; }
    }
    public List<KeyValuePair<string, string>> ImageInfo
    {
        get { return _ImageInfo; }
        set { _ImageInfo = value; }
    }

    //public List<KeyValuePair<string, string>> YellowImageColl
    //{
    //    get { return _YellowImageColl; }
    //    set { _YellowImageColl = value; }
    //}
}