using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

public class LevelsInfo : XmlInfo {

	public List<LevelInfo> levels;

	public List<List<int>> transitions;


	public LevelsInfo() { }

	public LevelsInfo(string assetName) : base(assetName) {	}

	public LevelsInfo(XmlDocument xml) : base(xml){	}

	public override void Parse (XmlNode xml)
	{
		base.Parse (xml);

		levels = new List<LevelInfo>();

       
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
			XmlNodeList lvlsPos = children[pos].ChildNodes;

			i = 0;
			pos = -1;

			bool levelsFound = false;
			bool transitionsFound = false;
			XmlNodeList lvls;
			while (i < lvlsPos.Count) {
				if (lvlsPos[i].NodeType == XmlNodeType.Element){
					lvls = lvlsPos[i].ChildNodes;
					if (lvlsPos[i].Name ==  "transitions"){
						parseTransitions(lvls);
						transitionsFound = true;
					} 
					if (lvlsPos[i].Name ==  "levels"){
						parseLevels(lvls);
						levelsFound = true;
					} 
				}
				i++;
			}
			if (!levelsFound){
				Debug.Log("Warning! levels not found. Check levels.xml");
			}
			if (!transitionsFound){
				Debug.Log("Warning! transitions not found. Check levels.xml");
			}
		} else {
			Debug.Log("Warning! root not found. Check levels.xml");
		}
	}

	void parseLevels (XmlNodeList lvls)
	{
		foreach (XmlNode node in lvls)
		{
			if (node.NodeType == XmlNodeType.Element){
				LevelInfo level = new LevelInfo();
				level.Parse(node);
				levels.Add(level);
			}
		}
	}

	void parseTransitions (XmlNodeList trns)
	{
		transitions = new List<List<int>> ();
		foreach (XmlNode node in trns)
		{
			if (node.NodeType == XmlNodeType.Element){
				transitions.Add(ParseListInt(node.Attributes.GetNamedItem("transitions")));
			}
		}
	}
}
