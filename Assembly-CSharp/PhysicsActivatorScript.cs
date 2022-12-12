using UnityEngine;

public class PhysicsActivatorScript : MonoBehaviour
{
	public int Frame;

	private void Start()
	{
		Debug.Log("Spawned a PhysicsActivator.");
	}

	private void Update()
	{
		if (Frame > 0)
		{
			Debug.Log("Destroyed a PhysicsActivator.");
			Object.Destroy(base.gameObject);
		}
		Frame++;
	}

	private void OnTriggerEnter(Collider other)
	{
		Debug.Log("Collided with something.");
		if (other.gameObject.layer != 15)
		{
			return;
		}
		Debug.Log("Collided with something on the PickUp layer.");
		PickUpScript component = other.gameObject.GetComponent<PickUpScript>();
		if (component != null && component.enabled)
		{
			Debug.Log("Found a PickUpScript attached.");
			if (component.Yandere.PickUp != component)
			{
				Debug.Log("It's not in Ayano's hands. Telling it to Drop().");
				component.Drop();
			}
			else
			{
				Debug.Log("It's in Ayano's hands. Ignoring it.");
			}
		}
		else
		{
			Debug.Log("Didn't find a PickUpScript attached.");
		}
		WeaponScript component2 = other.gameObject.GetComponent<WeaponScript>();
		if (component2 != null && component2.Yandere.EquippedWeapon != component2)
		{
			component2.Drop();
		}
	}
}