﻿using System;
using UnityEngine;

// Token: 0x02000139 RID: 313
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Blend 2 Camera/LinearLight")]
public class CameraFilterPack_Blend2Camera_LinearLight : MonoBehaviour
{
	// Token: 0x1700023D RID: 573
	// (get) Token: 0x06000BFA RID: 3066 RVA: 0x0006FC97 File Offset: 0x0006DE97
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

	// Token: 0x06000BFB RID: 3067 RVA: 0x0006FCCC File Offset: 0x0006DECC
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

	// Token: 0x06000BFC RID: 3068 RVA: 0x0006FD30 File Offset: 0x0006DF30
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

	// Token: 0x06000BFD RID: 3069 RVA: 0x0006FE20 File Offset: 0x0006E020
	private void OnValidate()
	{
		if (this.Camera2 != null)
		{
			this.Camera2tex = new RenderTexture(Screen.width, Screen.height, 24);
			this.Camera2.targetTexture = this.Camera2tex;
		}
	}

	// Token: 0x06000BFE RID: 3070 RVA: 0x0006FE58 File Offset: 0x0006E058
	private void Update()
	{
	}

	// Token: 0x06000BFF RID: 3071 RVA: 0x0006FE5A File Offset: 0x0006E05A
	private void OnEnable()
	{
		if (this.Camera2 != null)
		{
			this.Camera2tex = new RenderTexture(Screen.width, Screen.height, 24);
			this.Camera2.targetTexture = this.Camera2tex;
		}
	}

	// Token: 0x06000C00 RID: 3072 RVA: 0x0006FE92 File Offset: 0x0006E092
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

	// Token: 0x0400103F RID: 4159
	private string ShaderName = "CameraFilterPack/Blend2Camera_LinearLight";

	// Token: 0x04001040 RID: 4160
	public Shader SCShader;

	// Token: 0x04001041 RID: 4161
	public Camera Camera2;

	// Token: 0x04001042 RID: 4162
	private float TimeX = 1f;

	// Token: 0x04001043 RID: 4163
	private Material SCMaterial;

	// Token: 0x04001044 RID: 4164
	[Range(0f, 1f)]
	public float SwitchCameraToCamera2;

	// Token: 0x04001045 RID: 4165
	[Range(0f, 1f)]
	public float BlendFX = 0.5f;

	// Token: 0x04001046 RID: 4166
	private RenderTexture Camera2tex;
}
