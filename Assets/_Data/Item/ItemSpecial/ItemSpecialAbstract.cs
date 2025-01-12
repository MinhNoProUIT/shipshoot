using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemSpecialAbstract : BaseMonoBehaviour
{
	[Header("Item Special Abstract")]
	[SerializeField] protected ItemSpecialCtrl itemSpecialCtrl;
	public ItemSpecialCtrl ItemSpecialCtrl => itemSpecialCtrl;

	protected override void LoadComponents()
	{
		base.LoadComponents();
		this.LoadItemSpecialCtrl();
	}

	protected virtual void LoadItemSpecialCtrl()
	{
		if (this.itemSpecialCtrl != null) return;
		this.itemSpecialCtrl = transform.parent.GetComponent<ItemSpecialCtrl>();
		Debug.Log(transform.name + ": LoadItemCtrl", gameObject);
	}
}
