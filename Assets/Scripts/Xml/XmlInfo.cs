using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Globalization;

public class XmlInfo {

	public XmlInfo(){}

	public XmlInfo(string assetName){
		if (assetName != ""){
			TextAsset asset = Resources.Load<TextAsset>(assetName);
			if (asset != null){
				XmlDocument xml = new XmlDocument();
				xml.LoadXml(asset.text);
				Parse(xml);
			} else {
				Debug.Log("Asset "+assetName+" not found!");

			}
		}
	}
	public XmlInfo(XmlDocument xml){
		if (xml != null){
			Parse(xml as XmlNode);
		}
	}
	public virtual void Parse(XmlNode xml){

	}

	protected int ParseInt(XmlNode attr){
		if (attr != null) {
			return int.Parse (attr.Value);
		}
		return 0;
	}
	protected string ParseString(XmlNode attr){
		if (attr != null) {
			return attr.Value;
		}
		return "";
	}
	protected float ParseFloat(XmlNode attr){
		if (attr != null) {
			return float.Parse(attr.Value, CultureInfo.InvariantCulture);
		}
		return 0;
	}
	protected bool ParseBool(XmlNode attr){
		bool res = false;
		if (attr != null) {
			res = bool.Parse(attr.Value);
		}
		return res;
	}
	protected Vector2 ParseVector2(XmlNode attr, string[] separator){
		Vector2 res = new Vector2();
		if (attr != null) {
			string str = attr.Value;
			string[] terms = str.Split(separator, 2, System.StringSplitOptions.RemoveEmptyEntries);
			if (terms.Length > 0){
				res.x = float.Parse(terms[0], CultureInfo.InvariantCulture);
			}
			if (terms.Length > 1){
				res.y = float.Parse(terms[1], CultureInfo.InvariantCulture);
			}
		}
		return res;
	}
	protected Vector2 ParseVector2(XmlNode attr){
		return ParseVector2(attr, new string[] {",", " "});
	}

	protected Vector3 ParseVector3(XmlNode attr, string[] separator){
		Vector3 res = new Vector3();
		if (attr != null) {
			string str = attr.Value;
			string[] terms = str.Split(separator, 3, System.StringSplitOptions.RemoveEmptyEntries);
			if (terms.Length > 0){
				res.x = float.Parse(terms[0], CultureInfo.InvariantCulture);
			}
			if (terms.Length > 1){
				res.y = float.Parse(terms[1], CultureInfo.InvariantCulture);
			}
			if (terms.Length > 2){
				res.z = float.Parse(terms[2], CultureInfo.InvariantCulture);
			}
		}
		return res;
	}
	protected Vector3 ParseVector3(XmlNode attr){
		return ParseVector3(attr, new string[] {",", " "});
	}
	protected Rect ParseRect(XmlNode attr, string[] separator){
		Rect res = new Rect();
		if (attr != null) {
			string str = attr.Value;
			string[] terms = str.Split(separator, 4, System.StringSplitOptions.RemoveEmptyEntries);
			if (terms.Length > 0){
				res.x = float.Parse(terms[0], CultureInfo.InvariantCulture);
			}
			if (terms.Length > 1){
				res.y = float.Parse(terms[1], CultureInfo.InvariantCulture);
			}
			if (terms.Length > 2){
				res.width = float.Parse(terms[2], CultureInfo.InvariantCulture);
			}
			if (terms.Length > 3){
				res.height = float.Parse(terms[3], CultureInfo.InvariantCulture);
			}
		}
		return res;
	}
	protected Rect ParseRect(XmlNode attr){
		return ParseRect(attr, new string[] {",", " "});
	}
	protected List<int> ParseListInt(XmlNode attr, string[] separator){
		List<int> res = new List<int>();
		if (attr != null) {
			string str = attr.Value;
			string[] terms = str.Split(separator, System.StringSplitOptions.RemoveEmptyEntries);
			for(int i = 0; i < terms.Length; i++){
				terms[i] = terms[i].Trim(new char[]{' '});
				res.Add(int.Parse(terms[i]));
			}
		}
		return res;
	}
	protected List<int> ParseListInt(XmlNode attr){
		return ParseListInt(attr, new string[] {",", " "});
	}
}
