using UnityEngine;
using System;
using System.Text;
using System.Collections;
using System.IO;  
using Common;


public class SFJSONObject
{
	public Hashtable nameValuePairs = new Hashtable(); 
	private string ins;
	private char[] cins;
	private int pos;
    public SFJSONObject() {
    }
    public SFJSONObject(String json) {
		Debug.Log ("------------SFJSONObject");
        JSONTokener readFrom = new JSONTokener(json);
		Debug.Log ("------------SFJSONObject 2222");
		System.Object obj = readFrom.nextValue();

		if(obj is SFJSONObject){
			 this.nameValuePairs = ((SFJSONObject)obj).nameValuePairs;
		}
        

    }
	public object get(string name){
		Debug.Log ("------------SFJSONObject get name = "+name+" value = "+nameValuePairs[name]);
		return nameValuePairs[name];
	}
	public void put(string key, object value){
		nameValuePairs.Add(key,value);
	}
      
	
	public string toString()
	{
		string svalue = "{";
		foreach (DictionaryEntry de in nameValuePairs)
		{
			svalue += "\""+de.Key+"\""+":"+"\""+de.Value+"\""+",";
		}

		svalue = svalue.Remove(svalue.Length-1);
		svalue += "}";
		
		return svalue;
	}

	public string toInlineString()
	{
		string svalue = "{";
		foreach (DictionaryEntry de in nameValuePairs)
		{
			svalue += "\\\""+de.Key+"\\\""+":"+"\\\""+de.Value+"\\\""+",";
		}
		
		svalue = svalue.Remove(svalue.Length-1);
		svalue += "}";
		
		return svalue;
	}



}