﻿using System;
using UnityEngine;

// Token: 0x0200047C RID: 1148
public class TimeStopKnifeScript : MonoBehaviour
{
	// Token: 0x06001EF8 RID: 7928 RVA: 0x001B66CC File Offset: 0x001B48CC
	private void Start()
	{
		base.transform.localScale = new Vector3(0f, 0f, 0f);
	}

	// Token: 0x06001EF9 RID: 7929 RVA: 0x001B66F0 File Offset: 0x001B48F0
	private void Update()
	{
		if (!this.Unfreeze)
		{
			this.Speed = Mathf.MoveTowards(this.Speed, 0f, Time.deltaTime);
			if (base.transform.localScale.x < 0.99f)
			{
				base.transform.localScale = Vector3.Lerp(base.transform.localScale, new Vector3(1f, 1f, 1f), Time.deltaTime * 10f);
			}
		}
		else
		{
			this.Speed = 10f;
			this.Timer += Time.deltaTime;
			if (this.Timer > 5f)
			{
				UnityEngine.Object.Destroy(base.gameObject);
			}
		}
		base.transform.Translate(Vector3.forward * this.Speed * Time.deltaTime, Space.Self);
	}

	// Token: 0x06001EFA RID: 7930 RVA: 0x001B67D0 File Offset: 0x001B49D0
	private void OnTriggerEnter(Collider other)
	{
		if (this.Unfreeze && other.gameObject.layer == 9)
		{
			StudentScript component = other.gameObject.GetComponent<StudentScript>();
			if (component != null && component.StudentID > 1)
			{
				if (component.Male)
				{
					UnityEngine.Object.Instantiate<GameObject>(this.MaleScream, base.transform.position, Quaternion.identity);
				}
				else
				{
					UnityEngine.Object.Instantiate<GameObject>(this.FemaleScream, base.transform.position, Quaternion.identity);
				}
				component.DeathType = DeathType.EasterEgg;
				component.BecomeRagdoll();
			}
		}
	}

	// Token: 0x04004058 RID: 16472
	public GameObject FemaleScream;

	// Token: 0x04004059 RID: 16473
	public GameObject MaleScream;

	// Token: 0x0400405A RID: 16474
	public bool Unfreeze;

	// Token: 0x0400405B RID: 16475
	public float Speed = 0.1f;

	// Token: 0x0400405C RID: 16476
	private float Timer;
}
