using UnityEngine;
using System.Collections;

public class Singleton<T> where T : new() {

	private static T _instance = default(T);

	public static T instance 
	{
		get 
		{
			if (_instance == null)
			{
				_instance = new T();
			}

			return _instance;
		}
	}
}