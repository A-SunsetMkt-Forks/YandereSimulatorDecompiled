﻿using System;
using UnityEngine;

namespace UnityStandardAssets.Vehicles.Car
{
	// Token: 0x02000520 RID: 1312
	[RequireComponent(typeof(CarController))]
	public class CarAIControl : MonoBehaviour
	{
		// Token: 0x06002162 RID: 8546 RVA: 0x001E948A File Offset: 0x001E768A
		private void Awake()
		{
			this.m_CarController = base.GetComponent<CarController>();
			this.m_RandomPerlin = UnityEngine.Random.value * 100f;
			this.m_Rigidbody = base.GetComponent<Rigidbody>();
		}

		// Token: 0x06002163 RID: 8547 RVA: 0x001E94B8 File Offset: 0x001E76B8
		private void FixedUpdate()
		{
			if (this.m_Target == null || !this.m_Driving)
			{
				this.m_CarController.Move(0f, 0f, -1f, 1f);
				return;
			}
			Vector3 to = base.transform.forward;
			if (this.m_Rigidbody.velocity.magnitude > this.m_CarController.MaxSpeed * 0.1f)
			{
				to = this.m_Rigidbody.velocity;
			}
			float num = this.m_CarController.MaxSpeed;
			switch (this.m_BrakeCondition)
			{
			case CarAIControl.BrakeCondition.TargetDirectionDifference:
			{
				float b = Vector3.Angle(this.m_Target.forward, to);
				float a = this.m_Rigidbody.angularVelocity.magnitude * this.m_CautiousAngularVelocityFactor;
				float t = Mathf.InverseLerp(0f, this.m_CautiousMaxAngle, Mathf.Max(a, b));
				num = Mathf.Lerp(this.m_CarController.MaxSpeed, this.m_CarController.MaxSpeed * this.m_CautiousSpeedFactor, t);
				break;
			}
			case CarAIControl.BrakeCondition.TargetDistance:
			{
				Vector3 vector = this.m_Target.position - base.transform.position;
				float b2 = Mathf.InverseLerp(this.m_CautiousMaxDistance, 0f, vector.magnitude);
				float value = this.m_Rigidbody.angularVelocity.magnitude * this.m_CautiousAngularVelocityFactor;
				float t2 = Mathf.Max(Mathf.InverseLerp(0f, this.m_CautiousMaxAngle, value), b2);
				num = Mathf.Lerp(this.m_CarController.MaxSpeed, this.m_CarController.MaxSpeed * this.m_CautiousSpeedFactor, t2);
				break;
			}
			}
			Vector3 vector2 = this.m_Target.position;
			if (Time.time < this.m_AvoidOtherCarTime)
			{
				num *= this.m_AvoidOtherCarSlowdown;
				vector2 += this.m_Target.right * this.m_AvoidPathOffset;
			}
			else
			{
				vector2 += this.m_Target.right * (Mathf.PerlinNoise(Time.time * this.m_LateralWanderSpeed, this.m_RandomPerlin) * 2f - 1f) * this.m_LateralWanderDistance;
			}
			float num2 = (num < this.m_CarController.CurrentSpeed) ? this.m_BrakeSensitivity : this.m_AccelSensitivity;
			float num3 = Mathf.Clamp((num - this.m_CarController.CurrentSpeed) * num2, -1f, 1f);
			num3 *= 1f - this.m_AccelWanderAmount + Mathf.PerlinNoise(Time.time * this.m_AccelWanderSpeed, this.m_RandomPerlin) * this.m_AccelWanderAmount;
			Vector3 vector3 = base.transform.InverseTransformPoint(vector2);
			float steering = Mathf.Clamp(Mathf.Atan2(vector3.x, vector3.z) * 57.29578f * this.m_SteerSensitivity, -1f, 1f) * Mathf.Sign(this.m_CarController.CurrentSpeed);
			this.m_CarController.Move(steering, num3, num3, 0f);
			if (this.m_StopWhenTargetReached && vector3.magnitude < this.m_ReachTargetThreshold)
			{
				this.m_Driving = false;
			}
		}

		// Token: 0x06002164 RID: 8548 RVA: 0x001E97E8 File Offset: 0x001E79E8
		private void OnCollisionStay(Collision col)
		{
			if (col.rigidbody != null)
			{
				CarAIControl component = col.rigidbody.GetComponent<CarAIControl>();
				if (component != null)
				{
					this.m_AvoidOtherCarTime = Time.time + 1f;
					if (Vector3.Angle(base.transform.forward, component.transform.position - base.transform.position) < 90f)
					{
						this.m_AvoidOtherCarSlowdown = 0.5f;
					}
					else
					{
						this.m_AvoidOtherCarSlowdown = 1f;
					}
					Vector3 vector = base.transform.InverseTransformPoint(component.transform.position);
					float f = Mathf.Atan2(vector.x, vector.z);
					this.m_AvoidPathOffset = this.m_LateralWanderDistance * -Mathf.Sign(f);
				}
			}
		}

		// Token: 0x06002165 RID: 8549 RVA: 0x001E98B6 File Offset: 0x001E7AB6
		public void SetTarget(Transform target)
		{
			this.m_Target = target;
			this.m_Driving = true;
		}

		// Token: 0x04004943 RID: 18755
		[SerializeField]
		[Range(0f, 1f)]
		private float m_CautiousSpeedFactor = 0.05f;

		// Token: 0x04004944 RID: 18756
		[SerializeField]
		[Range(0f, 180f)]
		private float m_CautiousMaxAngle = 50f;

		// Token: 0x04004945 RID: 18757
		[SerializeField]
		private float m_CautiousMaxDistance = 100f;

		// Token: 0x04004946 RID: 18758
		[SerializeField]
		private float m_CautiousAngularVelocityFactor = 30f;

		// Token: 0x04004947 RID: 18759
		[SerializeField]
		private float m_SteerSensitivity = 0.05f;

		// Token: 0x04004948 RID: 18760
		[SerializeField]
		private float m_AccelSensitivity = 0.04f;

		// Token: 0x04004949 RID: 18761
		[SerializeField]
		private float m_BrakeSensitivity = 1f;

		// Token: 0x0400494A RID: 18762
		[SerializeField]
		private float m_LateralWanderDistance = 3f;

		// Token: 0x0400494B RID: 18763
		[SerializeField]
		private float m_LateralWanderSpeed = 0.1f;

		// Token: 0x0400494C RID: 18764
		[SerializeField]
		[Range(0f, 1f)]
		private float m_AccelWanderAmount = 0.1f;

		// Token: 0x0400494D RID: 18765
		[SerializeField]
		private float m_AccelWanderSpeed = 0.1f;

		// Token: 0x0400494E RID: 18766
		[SerializeField]
		private CarAIControl.BrakeCondition m_BrakeCondition = CarAIControl.BrakeCondition.TargetDistance;

		// Token: 0x0400494F RID: 18767
		[SerializeField]
		private bool m_Driving;

		// Token: 0x04004950 RID: 18768
		[SerializeField]
		private Transform m_Target;

		// Token: 0x04004951 RID: 18769
		[SerializeField]
		private bool m_StopWhenTargetReached;

		// Token: 0x04004952 RID: 18770
		[SerializeField]
		private float m_ReachTargetThreshold = 2f;

		// Token: 0x04004953 RID: 18771
		private float m_RandomPerlin;

		// Token: 0x04004954 RID: 18772
		private CarController m_CarController;

		// Token: 0x04004955 RID: 18773
		private float m_AvoidOtherCarTime;

		// Token: 0x04004956 RID: 18774
		private float m_AvoidOtherCarSlowdown;

		// Token: 0x04004957 RID: 18775
		private float m_AvoidPathOffset;

		// Token: 0x04004958 RID: 18776
		private Rigidbody m_Rigidbody;

		// Token: 0x02000681 RID: 1665
		public enum BrakeCondition
		{
			// Token: 0x04004FD0 RID: 20432
			NeverBrake,
			// Token: 0x04004FD1 RID: 20433
			TargetDirectionDifference,
			// Token: 0x04004FD2 RID: 20434
			TargetDistance
		}
	}
}
