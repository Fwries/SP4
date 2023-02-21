using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

	#region Singleton

	public static Inventory instance;

	void Awake()
	{
		if (instance != null)
		{
			Debug.LogWarning("More than one instance of Inventory found!");
			return;
		}

		instance = this;
	}

	#endregion

	// Callback which is triggered when
	// an item gets added/removed.
	public delegate void OnItemChanged();
	public OnItemChanged onItemChangedCallback;

	public int space = 9;  // Amount of slots in inventory

	// Current list of items in inventory
	public List<ScEquipment> items = new List<ScEquipment>();

	// Check if item is alr in inventory
	bool stack = false;

	// Add a new item. If there is enough room we
	// return true. Else we return false.
	public bool AddItem(ScEquipment item)
	{
		// Check if out of space
		if (items.Count >= space)
		{
			Debug.Log("Not enough room.");
			return false;
		}
		for (int i=0; i < items.Count; i++)
		{
			if (item.name == items[i].name)
			{
				items[i].stack++;
				Debug.Log("Add to stack");
				stack = true;
			}
		}
		if(stack==false)
		{
			// Add item to list
			items.Add(item);
        }
		// Trigger callback
		if (onItemChangedCallback != null)
			onItemChangedCallback.Invoke();
		stack = false;
		return true;
	}

}