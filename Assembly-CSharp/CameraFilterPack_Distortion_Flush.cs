﻿using System;
using UnityEngine;

// Token: 0x0200017A RID: 378
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Distortion/Flush")]
public class CameraFilterPack_Distortion_Flush : MonoBehaviour
{
	// Token: 0x1700027F RID: 639
	// (get) Token: 0x06000D9D RID: 3485 RVA: 0x00076B05 File Offset: 0x00074D05
	private Material material
	{
		get
		{
			if (this.SCMaterial == null)
			{
				this.SCMaterial = new Material(this.SCShader);
				this.SCMaterial.hideFlags = HideFlags.HideAndDontSave;
			}
			return this.SCMaterial;
		}
	}

	// Token: 0x06000D9E RID: 3486 RVA: 0x00076B39 File Offset: 0x00074D39
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Distortion_Flush");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000D9F RID: 3487 RVA: 0x00076B5C File Offset: 0x00074D5C
	private void OnRenderImage(RenderTexture sourceTexture, RenderTexture destTexture)
	{
		if (this.SCShader != null)
		{
			this.TimeX += Time.deltaTime;
			if (this.TimeX > 100f)
			{
				this.TimeX = 0f;
			}
			this.material.SetFloat("_TimeX", this.TimeX);
			this.material.SetFloat("Value", this.Value);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000DA0 RID: 3488 RVA: 0x00076C12 File Offset: 0x00074E12
	private void Update()
	{
	}

	// Token: 0x06000DA1 RID: 3489 RVA: 0x00076C14 File Offset: 0x00074E14
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x040011DE RID: 4574
	public Shader SCShader;

	// Token: 0x040011DF RID: 4575
	private float TimeX = 1f;

	// Token: 0x040011E0 RID: 4576
	private Material SCMaterial;

	// Token: 0x040011E1 RID: 4577
	[Range(-10f, 50f)]
	public float Value = 5f;
}
