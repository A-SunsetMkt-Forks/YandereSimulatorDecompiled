﻿using System;
using UnityEngine;

// Token: 0x02000369 RID: 873
public class MiyukiEnemyScript : MonoBehaviour
{
	// Token: 0x060019A4 RID: 6564 RVA: 0x00105E26 File Offset: 0x00104026
	private void Start()
	{
		base.transform.position = this.SpawnPoints[this.ID].position;
		base.transform.rotation = this.SpawnPoints[this.ID].rotation;
	}

	// Token: 0x060019A5 RID: 6565 RVA: 0x00105E64 File Offset: 0x00104064
	private void Update()
	{
		if (this.Enemy.activeInHierarchy)
		{
			if (!this.Down)
			{
				this.Float += Time.deltaTime * this.Speed;
				if (this.Float > this.Limit)
				{
					this.Down = true;
				}
			}
			else
			{
				this.Float -= Time.deltaTime * this.Speed;
				if (this.Float < -1f * this.Limit)
				{
					this.Down = false;
				}
			}
			this.Enemy.transform.position += new Vector3(0f, this.Float * Time.deltaTime, 0f);
			if (this.Enemy.transform.position.y > this.SpawnPoints[this.ID].position.y + 1.5f)
			{
				this.Enemy.transform.position = new Vector3(this.Enemy.transform.position.x, this.SpawnPoints[this.ID].position.y + 1.5f, this.Enemy.transform.position.z);
			}
			if (this.Enemy.transform.position.y < this.SpawnPoints[this.ID].position.y + 0.5f)
			{
				this.Enemy.transform.position = new Vector3(this.Enemy.transform.position.x, this.SpawnPoints[this.ID].position.y + 0.5f, this.Enemy.transform.position.z);
				return;
			}
		}
		else
		{
			this.RespawnTimer += Time.deltaTime;
			if (this.RespawnTimer > 5f)
			{
				base.transform.position = this.SpawnPoints[this.ID].position;
				base.transform.rotation = this.SpawnPoints[this.ID].rotation;
				this.Enemy.SetActive(true);
				this.RespawnTimer = 0f;
			}
		}
	}

	// Token: 0x060019A6 RID: 6566 RVA: 0x001060B8 File Offset: 0x001042B8
	private void OnTriggerEnter(Collider other)
	{
		if (this.Enemy.activeInHierarchy && other.gameObject.tag == "missile")
		{
			UnityEngine.Object.Instantiate<GameObject>(this.HitEffect, other.transform.position, Quaternion.identity);
			UnityEngine.Object.Destroy(other.gameObject);
			this.Health -= 1f;
			if (this.Health == 0f)
			{
				UnityEngine.Object.Instantiate<GameObject>(this.DeathEffect, other.transform.position, Quaternion.identity);
				this.Enemy.SetActive(false);
				this.Health = 50f;
				this.ID++;
				if (this.ID >= this.SpawnPoints.Length)
				{
					this.ID = 0;
				}
			}
		}
	}

	// Token: 0x04002909 RID: 10505
	public float Float;

	// Token: 0x0400290A RID: 10506
	public float Limit;

	// Token: 0x0400290B RID: 10507
	public float Speed;

	// Token: 0x0400290C RID: 10508
	public bool Dead;

	// Token: 0x0400290D RID: 10509
	public bool Down;

	// Token: 0x0400290E RID: 10510
	public GameObject DeathEffect;

	// Token: 0x0400290F RID: 10511
	public GameObject HitEffect;

	// Token: 0x04002910 RID: 10512
	public GameObject Enemy;

	// Token: 0x04002911 RID: 10513
	public Transform[] SpawnPoints;

	// Token: 0x04002912 RID: 10514
	public float RespawnTimer;

	// Token: 0x04002913 RID: 10515
	public float Health;

	// Token: 0x04002914 RID: 10516
	public int ID;
}
