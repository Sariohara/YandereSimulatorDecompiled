﻿using System;
using UnityEngine;

namespace UnityStandardAssets.Vehicles.Car
{
	// Token: 0x0200051B RID: 1307
	[RequireComponent(typeof(CarController))]
	public class CarAIControl : MonoBehaviour
	{
		// Token: 0x06002141 RID: 8513 RVA: 0x001E60F6 File Offset: 0x001E42F6
		private void Awake()
		{
			this.m_CarController = base.GetComponent<CarController>();
			this.m_RandomPerlin = UnityEngine.Random.value * 100f;
			this.m_Rigidbody = base.GetComponent<Rigidbody>();
		}

		// Token: 0x06002142 RID: 8514 RVA: 0x001E6124 File Offset: 0x001E4324
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

		// Token: 0x06002143 RID: 8515 RVA: 0x001E6454 File Offset: 0x001E4654
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

		// Token: 0x06002144 RID: 8516 RVA: 0x001E6522 File Offset: 0x001E4722
		public void SetTarget(Transform target)
		{
			this.m_Target = target;
			this.m_Driving = true;
		}

		// Token: 0x040048E0 RID: 18656
		[SerializeField]
		[Range(0f, 1f)]
		private float m_CautiousSpeedFactor = 0.05f;

		// Token: 0x040048E1 RID: 18657
		[SerializeField]
		[Range(0f, 180f)]
		private float m_CautiousMaxAngle = 50f;

		// Token: 0x040048E2 RID: 18658
		[SerializeField]
		private float m_CautiousMaxDistance = 100f;

		// Token: 0x040048E3 RID: 18659
		[SerializeField]
		private float m_CautiousAngularVelocityFactor = 30f;

		// Token: 0x040048E4 RID: 18660
		[SerializeField]
		private float m_SteerSensitivity = 0.05f;

		// Token: 0x040048E5 RID: 18661
		[SerializeField]
		private float m_AccelSensitivity = 0.04f;

		// Token: 0x040048E6 RID: 18662
		[SerializeField]
		private float m_BrakeSensitivity = 1f;

		// Token: 0x040048E7 RID: 18663
		[SerializeField]
		private float m_LateralWanderDistance = 3f;

		// Token: 0x040048E8 RID: 18664
		[SerializeField]
		private float m_LateralWanderSpeed = 0.1f;

		// Token: 0x040048E9 RID: 18665
		[SerializeField]
		[Range(0f, 1f)]
		private float m_AccelWanderAmount = 0.1f;

		// Token: 0x040048EA RID: 18666
		[SerializeField]
		private float m_AccelWanderSpeed = 0.1f;

		// Token: 0x040048EB RID: 18667
		[SerializeField]
		private CarAIControl.BrakeCondition m_BrakeCondition = CarAIControl.BrakeCondition.TargetDistance;

		// Token: 0x040048EC RID: 18668
		[SerializeField]
		private bool m_Driving;

		// Token: 0x040048ED RID: 18669
		[SerializeField]
		private Transform m_Target;

		// Token: 0x040048EE RID: 18670
		[SerializeField]
		private bool m_StopWhenTargetReached;

		// Token: 0x040048EF RID: 18671
		[SerializeField]
		private float m_ReachTargetThreshold = 2f;

		// Token: 0x040048F0 RID: 18672
		private float m_RandomPerlin;

		// Token: 0x040048F1 RID: 18673
		private CarController m_CarController;

		// Token: 0x040048F2 RID: 18674
		private float m_AvoidOtherCarTime;

		// Token: 0x040048F3 RID: 18675
		private float m_AvoidOtherCarSlowdown;

		// Token: 0x040048F4 RID: 18676
		private float m_AvoidPathOffset;

		// Token: 0x040048F5 RID: 18677
		private Rigidbody m_Rigidbody;

		// Token: 0x0200067B RID: 1659
		public enum BrakeCondition
		{
			// Token: 0x04004F61 RID: 20321
			NeverBrake,
			// Token: 0x04004F62 RID: 20322
			TargetDirectionDifference,
			// Token: 0x04004F63 RID: 20323
			TargetDistance
		}
	}
}