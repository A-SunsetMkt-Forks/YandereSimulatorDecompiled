﻿using System;
using UnityEngine;

// Token: 0x02000134 RID: 308
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Blend 2 Camera/Hue")]
public class CameraFilterPack_Blend2Camera_Hue : MonoBehaviour
{
	// Token: 0x17000238 RID: 568
	// (get) Token: 0x06000BD2 RID: 3026 RVA: 0x0006F0DF File Offset: 0x0006D2DF
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

	// Token: 0x06000BD3 RID: 3027 RVA: 0x0006F114 File Offset: 0x0006D314
	private void Start()
	{
		if (this.Camera2 != null)
		{
			this.Camera2tex = new RenderTexture(Screen.width, Screen.height, 24);
			this.Camera2.targetTexture = this.Camera2tex;
		}
		this.SCShader = Shader.Find(this.ShaderName);
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000BD4 RID: 3028 RVA: 0x0006F178 File Offset: 0x0006D378
	private void OnRenderImage(RenderTexture sourceTexture, RenderTexture destTexture)
	{
		if (this.SCShader != null)
		{
			this.TimeX += Time.deltaTime;
			if (this.TimeX > 100f)
			{
				this.TimeX = 0f;
			}
			if (this.Camera2 != null)
			{
				this.material.SetTexture("_MainTex2", this.Camera2tex);
			}
			this.material.SetFloat("_TimeX", this.TimeX);
			this.material.SetFloat("_Value", this.BlendFX);
			this.material.SetFloat("_Value2", this.SwitchCameraToCamera2);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000BD5 RID: 3029 RVA: 0x0006F268 File Offset: 0x0006D468
	private void OnValidate()
	{
		if (this.Camera2 != null)
		{
			this.Camera2tex = new RenderTexture(Screen.width, Screen.height, 24);
			this.Camera2.targetTexture = this.Camera2tex;
		}
	}

	// Token: 0x06000BD6 RID: 3030 RVA: 0x0006F2A0 File Offset: 0x0006D4A0
	private void Update()
	{
	}

	// Token: 0x06000BD7 RID: 3031 RVA: 0x0006F2A2 File Offset: 0x0006D4A2
	private void OnEnable()
	{
		if (this.Camera2 != null)
		{
			this.Camera2tex = new RenderTexture(Screen.width, Screen.height, 24);
			this.Camera2.targetTexture = this.Camera2tex;
		}
	}

	// Token: 0x06000BD8 RID: 3032 RVA: 0x0006F2DA File Offset: 0x0006D4DA
	private void OnDisable()
	{
		if (this.Camera2 != null)
		{
			this.Camera2.targetTexture = null;
		}
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04001017 RID: 4119
	private string ShaderName = "CameraFilterPack/Blend2Camera_Hue";

	// Token: 0x04001018 RID: 4120
	public Shader SCShader;

	// Token: 0x04001019 RID: 4121
	public Camera Camera2;

	// Token: 0x0400101A RID: 4122
	private float TimeX = 1f;

	// Token: 0x0400101B RID: 4123
	private Material SCMaterial;

	// Token: 0x0400101C RID: 4124
	[Range(0f, 1f)]
	public float SwitchCameraToCamera2;

	// Token: 0x0400101D RID: 4125
	[Range(0f, 1f)]
	public float BlendFX = 0.5f;

	// Token: 0x0400101E RID: 4126
	private RenderTexture Camera2tex;
}
