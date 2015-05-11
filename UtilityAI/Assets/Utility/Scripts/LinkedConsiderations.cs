using System;
using System.Collections;

[Serializable]
public class LinkedConsideration{
	public string name;
	public enum Scope
	{
		Local,
		Global
	}
	public Scope scope;
	public float weight;
	private Consideration consideration;

	public void SetConsideration(Consideration con){
		consideration = con;
	}
	public Consideration GetConsideration(){
		return consideration;
	}
}
