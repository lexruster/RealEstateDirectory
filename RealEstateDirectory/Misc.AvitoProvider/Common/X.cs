﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18052
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System.Xml.Serialization;

// 
// This source code was auto-generated by xsd, Version=4.0.30319.17929.
// 


/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
[System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
public partial class Ads
{

    private AdsAD[] adField;

    private string targetField;

    private byte formatVersionField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("Ad")]
    public AdsAD[] Ad
    {
        get
        {
            return this.adField;
        }
        set
        {
            this.adField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string target
    {
        get
        {
            return this.targetField;
        }
        set
        {
            this.targetField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public byte formatVersion
    {
        get
        {
            return this.formatVersionField;
        }
        set
        {
            this.formatVersionField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class AdsAD
{

    private string idField;

    private string categoryField;

    private string marketTypeField;

    private string operationTypeField;

    private string objectTypeField;

    private System.DateTime dateBeginField;

    private bool dateBeginFieldSpecified;

    private System.DateTime dateEndField;

    private bool dateEndFieldSpecified;

    private string regionField;

    private int saleRoomsField;

    private bool saleRoomsFieldSpecified;

    private string cityField;

    private string localityField;

    private string streetField;

    private int roomsField;

    private bool roomsFieldSpecified;

    private decimal squareField;

    private bool squareFieldSpecified;

    private int floorField;

    private bool floorFieldSpecified;

    private int floorsField;

    private bool floorsFieldSpecified;

    private string houseTypeField;

    private string wallsTypeField;

    private decimal landAreaField;

    private bool landAreaFieldSpecified;

    private string descriptionField;

    private string contactPhoneField;

    private decimal priceField;

    private bool priceFieldSpecified;

    private AdsADImage[] imagesField;

    private string adStatusField;

    /// <remarks/>
    public string Id
    {
        get
        {
            return this.idField;
        }
        set
        {
            this.idField = value;
        }
    }

    /// <remarks/>
    public string Category
    {
        get
        {
            return this.categoryField;
        }
        set
        {
            this.categoryField = value;
        }
    }

    /// <remarks/>
    public string MarketType
    {
        get
        {
            return this.marketTypeField;
        }
        set
        {
            this.marketTypeField = value;
        }
    }

    /// <remarks/>
    public string OperationType
    {
        get
        {
            return this.operationTypeField;
        }
        set
        {
            this.operationTypeField = value;
        }
    }

    /// <remarks/>
    public string ObjectType
    {
        get
        {
            return this.objectTypeField;
        }
        set
        {
            this.objectTypeField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
    public System.DateTime DateBegin
    {
        get
        {
            return this.dateBeginField;
        }
        set
        {
            this.dateBeginField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool DateBeginSpecified
    {
        get
        {
            return this.dateBeginFieldSpecified;
        }
        set
        {
            this.dateBeginFieldSpecified = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
    public System.DateTime DateEnd
    {
        get
        {
            return this.dateEndField;
        }
        set
        {
            this.dateEndField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool DateEndSpecified
    {
        get
        {
            return this.dateEndFieldSpecified;
        }
        set
        {
            this.dateEndFieldSpecified = value;
        }
    }

    /// <remarks/>
    public string Region
    {
        get
        {
            return this.regionField;
        }
        set
        {
            this.regionField = value;
        }
    }

    /// <remarks/>
    public int SaleRooms
    {
        get
        {
            return this.saleRoomsField;
        }
        set
        {
            this.saleRoomsField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool SaleRoomsSpecified
    {
        get
        {
            return this.saleRoomsFieldSpecified;
        }
        set
        {
            this.saleRoomsFieldSpecified = value;
        }
    }

    /// <remarks/>
    public string City
    {
        get
        {
            return this.cityField;
        }
        set
        {
            this.cityField = value;
        }
    }

    /// <remarks/>
    public string Locality
    {
        get
        {
            return this.localityField;
        }
        set
        {
            this.localityField = value;
        }
    }

    /// <remarks/>
    public string Street
    {
        get
        {
            return this.streetField;
        }
        set
        {
            this.streetField = value;
        }
    }

    /// <remarks/>
    public int Rooms
    {
        get
        {
            return this.roomsField;
        }
        set
        {
            this.roomsField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool RoomsSpecified
    {
        get
        {
            return this.roomsFieldSpecified;
        }
        set
        {
            this.roomsFieldSpecified = value;
        }
    }

    /// <remarks/>
    public decimal Square
    {
        get
        {
            return this.squareField;
        }
        set
        {
            this.squareField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool SquareSpecified
    {
        get
        {
            return this.squareFieldSpecified;
        }
        set
        {
            this.squareFieldSpecified = value;
        }
    }

    /// <remarks/>
    public int Floor
    {
        get
        {
            return this.floorField;
        }
        set
        {
            this.floorField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool FloorSpecified
    {
        get
        {
            return this.floorFieldSpecified;
        }
        set
        {
            this.floorFieldSpecified = value;
        }
    }

    /// <remarks/>
    public int Floors
    {
        get
        {
            return this.floorsField;
        }
        set
        {
            this.floorsField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool FloorsSpecified
    {
        get
        {
            return this.floorsFieldSpecified;
        }
        set
        {
            this.floorsFieldSpecified = value;
        }
    }

    /// <remarks/>
    public string HouseType
    {
        get
        {
            return this.houseTypeField;
        }
        set
        {
            this.houseTypeField = value;
        }
    }

    /// <remarks/>
    public string WallsType
    {
        get
        {
            return this.wallsTypeField;
        }
        set
        {
            this.wallsTypeField = value;
        }
    }

    /// <remarks/>
    public decimal LandArea
    {
        get
        {
            return this.landAreaField;
        }
        set
        {
            this.landAreaField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool LandAreaSpecified
    {
        get
        {
            return this.landAreaFieldSpecified;
        }
        set
        {
            this.landAreaFieldSpecified = value;
        }
    }

    /// <remarks/>
    public string Description
    {
        get
        {
            return this.descriptionField;
        }
        set
        {
            this.descriptionField = value;
        }
    }

    /// <remarks/>
    public string ContactPhone
    {
        get
        {
            return this.contactPhoneField;
        }
        set
        {
            this.contactPhoneField = value;
        }
    }

    /// <remarks/>
    public decimal Price
    {
        get
        {
            return this.priceField;
        }
        set
        {
            this.priceField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool PriceSpecified
    {
        get
        {
            return this.priceFieldSpecified;
        }
        set
        {
            this.priceFieldSpecified = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlArrayItemAttribute("Image", IsNullable = false)]
    public AdsADImage[] Images
    {
        get
        {
            return this.imagesField;
        }
        set
        {
            this.imagesField = value;
        }
    }

    /// <remarks/>
    public string AdStatus
    {
        get
        {
            return this.adStatusField;
        }
        set
        {
            this.adStatusField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class AdsADImage
{

    private string nameField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string name
    {
        get
        {
            return this.nameField;
        }
        set
        {
            this.nameField = value;
        }
    }
}
