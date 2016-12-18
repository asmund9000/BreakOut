using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

public class ItemInfo : XmlInfo {

	private int _row = 0;
	public int row{
		get {return _row;}
	}
	private int _col = 0;
	public int col{
		get {return _col;}
	}
	private int _type = 0;
	public int type{
		get {return _type;}
	}

	public ItemInfo() { }
	
	public ItemInfo(string assetName) : base(assetName) {	}
	
	public ItemInfo(XmlDocument xml) : base(xml){	}
	
	public override void Parse (XmlNode xml)
	{
		base.Parse (xml);

		_row = ParseInt (xml.Attributes.GetNamedItem ("row"));
		_col = ParseInt (xml.Attributes.GetNamedItem ("col"));
		_type = ParseInt (xml.Attributes.GetNamedItem ("type"));

	 
	}


}
