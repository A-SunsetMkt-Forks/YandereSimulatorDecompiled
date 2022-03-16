﻿using System;
using UnityEngine;

// Token: 0x02000384 RID: 900
public class ObstacleDetectorScript : MonoBehaviour
{
	// Token: 0x06001A33 RID: 6707 RVA: 0x001146B4 File Offset: 0x001128B4
	private void Update()
	{
		this.Frame++;
		if (this.Frame == 3)
		{
			this.Frame = 0;
			this.Obstacles = 0;
			base.gameObject.SetActive(false);
			return;
		}
		if (this.Frame != 2)
		{
			int frame = this.Frame;
			return;
		}
		if (this.Obstacles > 0)
		{
			this.Yandere.NotificationManager.CustomText = "Something's in the way.";
			this.Yandere.NotificationManager.DisplayNotification(NotificationType.Custom);
			this.Yandere.NotificationManager.CustomText = "You can't set the cello case down here.";
			this.Yandere.NotificationManager.DisplayNotification(NotificationType.Custom);
			return;
		}
		this.Frame = 0;
		this.Yandere.Container.Drop();
		this.Yandere.WeaponMenu.UpdateSprites();
		base.gameObject.SetActive(false);
	}

	// Token: 0x06001A34 RID: 6708 RVA: 0x00114794 File Offset: 0x00112994
	private void OnTriggerEnter(Collider other)
	{
		if (this.Yandere.Container.CelloCase && other.gameObject.layer != 1 && other.gameObject.layer != 2 && other.gameObject.layer != 8 && other.gameObject.layer != 13 && other.gameObject.layer != 14)
		{
			Debug.Log("Obstacle detected: " + other.gameObject.name + ". It's on Layer: " + other.gameObject.layer.ToString());
			this.Obstacles++;
		}
	}

	// Token: 0x04002AD5 RID: 10965
	public YandereScript Yandere;

	// Token: 0x04002AD6 RID: 10966
	public int Obstacles;

	// Token: 0x04002AD7 RID: 10967
	public int Frame;

	// Token: 0x04002AD8 RID: 10968
	public int ID;
}
