using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

public class LevelInfo : XmlInfo {

	public const string TYPE_SIMPLE = "simple";
	public const string TYPE_ITEMS = "items";
	public const string TYPE_TILES = "tiles";

	/**
	 * Id уровня
	 */
	private int _id = 0;
	public int id {
		get {return _id; }
	}
	/**
	 * Тип уровня
	 */
	private string _type = TYPE_SIMPLE;
	public string type {
		get {return _type; }
	}

	private int _types = 0;
	public int types {
		get {return _types; }
	}

	private int _repeats = 0;
	public int repeats {
		get {return _repeats; }
	}

	private int _width = 0;
	public int width {
		get {return _width; }
	}

	private int _height = 0;
	public int height {
		get {return _height; }
	}
	/**
	 * Список игровых объектов, если тип уровня TYPE_ITEMS
	 */
	private List<ItemInfo> _items;
	public List<ItemInfo> items{
		get {return _items;}
	}
	/**
	 * Тайловая карта уровня tiles[row][col], если тип уровня TYPE_TILES
	 */
	private List<List<int>> _tiles;
	public List<List<int>> tiles{
		get {return _tiles;}
	}
	/**
	 * Тайловая карта уровня tilesXY[x][y], если тип уровня TYPE_TILES
	 */
	private List<List<int>> _tilesXY;
	public List<List<int>> tilesXY{
		get {return _tilesXY;}
	}

	public LevelInfo() { }
	
	public LevelInfo(string assetName) : base(assetName) {	}
	
	public LevelInfo(XmlDocument xml) : base(xml){	}
	
	public override void Parse (XmlNode xml)
	{
		base.Parse (xml);
	
	 	_id = ParseInt(xml.Attributes.GetNamedItem ("id"));
		_type = ParseString(xml.Attributes.GetNamedItem ("type"));
		_types = ParseInt(xml.Attributes.GetNamedItem ("types"));
		_repeats = ParseInt(xml.Attributes.GetNamedItem ("repeats"));
		_width = ParseInt(xml.Attributes.GetNamedItem ("width"));
		_height = ParseInt(xml.Attributes.GetNamedItem ("height"));

		switch (_type) {
			case TYPE_ITEMS : ParseItems(xml); break;
			case TYPE_TILES : ParseRows(xml); break;
		}
	}

	private void ParseItems(XmlNode xml){
		_items = new List<ItemInfo>();
       
		XmlNodeList children = xml.ChildNodes;

		int i = 0;
		int pos = -1;
		while (i < children.Count) {
			if (children[i].NodeType == XmlNodeType.Element){
				pos = i;
				break;
			}
			i++;
		}

		if (pos >= 0) {
			XmlNodeList itms = children[pos].ChildNodes;
			foreach (XmlNode node in itms) {
				if (node.NodeType == XmlNodeType.Element) {
					ItemInfo item = new ItemInfo ();
					item.Parse (node);
					_items.Add (item);
				}
			}
		} else {
			Debug.Log("Warning! items in level not found. Check levels.xml");
		}
	}

	private void ParseRows(XmlNode xml){
		_tiles = new List<List<int>> ();
		_tilesXY = new List<List<int>> ();
        XmlNodeList rws = xml.ChildNodes;
        foreach (XmlNode node in rws)
        {
			if (node.NodeType == XmlNodeType.Element){
				_tiles.Add(ParseListInt(node.Attributes.GetNamedItem("data")));
			}
		}
		for (int i = 0; i < _tiles.Count; i++) {
			for (int j = 0; j < _tiles[i].Count; j++) {
				if (i == 0){
					_tilesXY.Add(new List<int>());
				}
				_tilesXY[j].Add(_tiles[i][j]);
			}
		}
	}

}
