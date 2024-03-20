using UnityEngine;

namespace CustomUtilities.Gyro
{
	[CreateAssetMenu(fileName = "GyroInputSystem", menuName = "ProjectCore/InputSystems/GyroInput")]
	public class GyroInput : ScriptableObject
	{
		public Vector3Shared Vector3Shared;
		
		public void UpdateGyroInput()
		{
			Vector3 acceleration = Input.acceleration;
			//may need to normalize vector accelerometer
			acceleration.Normalize();
			Vector3Shared.SetValue(acceleration);
		}

	}
}