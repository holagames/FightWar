using System;
using UnityEngine;

namespace CodeStage.AntiCheat.ObscuredTypes
{
	/// <summary>
	/// Use it instead of regular <c>Quaternion</c> for any cheating-sensitive variables.
	/// </summary>
	/// <strong><em>Regular type is faster and memory wiser comparing to the obscured one!</em></strong><br/>
	/// <strong>\htmlonly<font color="FF4040">WARNING:</font>\endhtmlonly Doesn't mimic regular type API, thus should be used with extra caution.</strong> Cast it to regular, not obscured type to work with regular APIs.<br/>
	/// <strong>\htmlonly<font color="7030A0">IMPORTANT:</font>\endhtmlonly Not as accurate in Flash Player as on other platforms and may produce wrong regular type comparison results.</strong>
	public struct ObscuredQuaternion
	{
		/// <summary>
		/// Set it to any method you wish to be called in case of any ObscuredQuaternion variable cheating detection.<br/>
		/// Fires only once.
		/// </summary>
		public static Action onCheatingDetected;

		private static int cryptoKey = 120205;

		private int currentCryptoKey;
		private Quaternion hiddenValue;
		public Quaternion fakeValue;
		private bool inited;

		private ObscuredQuaternion(Quaternion value)
		{
			currentCryptoKey = cryptoKey;
			hiddenValue = value;
			fakeValue = Quaternion.identity;
			inited = true;
		}

		/// <summary>
		/// Allows to change default crypto key.
		/// </summary>
		public static void SetNewCryptoKey(int newKey)
		{
			cryptoKey = newKey;
		}

		/// <summary>
		/// Allows to pick current obscured value as is.
		/// </summary>
		/// Use it in conjunction with SetEncrypted().<br/>
		/// Useful for saving data in obscured state.
		public Quaternion GetEncrypted()
		{
			if (currentCryptoKey != cryptoKey)
			{
				Quaternion temp = InternalDecrypt();
				hiddenValue = Encrypt(temp, cryptoKey);
				currentCryptoKey = cryptoKey;
			}

			return hiddenValue;
		}

		/// <summary>
		/// Allows to explicitly set current obscured value.
		/// </summary>
		/// Use it in conjunction with GetEncrypted().<br/>
		/// Useful for loading data stored in obscured state.
		public void SetEncrypted(Quaternion encrypted)
		{
			hiddenValue = encrypted;

			if (onCheatingDetected != null)
			{
				fakeValue = InternalDecrypt();
			}
		}

		/// <summary>
		/// Use this simple encryption method to encrypt any Quaternion value, uses default crypto key.
		/// </summary>
		public static Quaternion Encrypt(Quaternion value)
		{
			return Encrypt(value, 0);
		}

		/// <summary>
		/// Use this simple encryption method to encrypt any Quaternion value, uses passed crypto key.
		/// </summary>
		public static Quaternion Encrypt(Quaternion value, int key)
		{
			if (key == 0)
			{
				key = cryptoKey;
			}

#if !UNITY_FLASH
			value.x = ObscuredDouble.Encrypt(value.x, key);
			value.y = ObscuredDouble.Encrypt(value.y, key);
			value.z = ObscuredDouble.Encrypt(value.z, key);
			value.w = ObscuredDouble.Encrypt(value.w, key);
#else
			value.x = ObscuredFloat.Encrypt(value.x, key);
			value.y = ObscuredFloat.Encrypt(value.y, key);
			value.z = ObscuredFloat.Encrypt(value.z, key);
			value.w = ObscuredFloat.Encrypt(value.w, key);
#endif
			return value;
		}

		/// <summary>
		/// Use it to decrypt Quaternion you got from Encrypt(Quaternion), uses default crypto key.
		/// </summary>
		public static Quaternion Decrypt(Quaternion value)
		{
			return Decrypt(value, 0);
		}

		/// <summary>
		/// Use it to decrypt Quaternion you got from Encrypt(Quaternion), uses passed crypto key.
		/// </summary>
		public static Quaternion Decrypt(Quaternion value, int key)
		{
			if (key == 0)
			{
				key = cryptoKey;
			}

#if !UNITY_FLASH
			value.x = (float)ObscuredDouble.Decrypt((long)value.x, key);
			value.y = (float)ObscuredDouble.Decrypt((long)value.y, key);
			value.z = (float)ObscuredDouble.Decrypt((long)value.z, key);
			value.w = (float)ObscuredDouble.Decrypt((long)value.w, key);
#else
			value.x = ObscuredFloat.Decrypt((int)value.x, key);
			value.y = ObscuredFloat.Decrypt((int)value.y, key);
			value.z = ObscuredFloat.Decrypt((int)value.z, key);
			value.w = ObscuredFloat.Decrypt((int)value.w, key);
#endif
			return value;
		}

		private Quaternion InternalDecrypt()
		{
			if (!inited)
			{
				currentCryptoKey = cryptoKey;
				hiddenValue = Encrypt(Quaternion.identity);
				fakeValue = Quaternion.identity;
				inited = true;
			}

			int key = cryptoKey;

			if (currentCryptoKey != cryptoKey)
			{
				key = currentCryptoKey;
			}

			Quaternion value;

#if !UNITY_FLASH
			value.x = (float)ObscuredDouble.Decrypt((long)hiddenValue.x, key);
			value.y = (float)ObscuredDouble.Decrypt((long)hiddenValue.y, key);
			value.z = (float)ObscuredDouble.Decrypt((long)hiddenValue.z, key);
			value.w = (float)ObscuredDouble.Decrypt((long)hiddenValue.w, key);
#else
			value.x = ObscuredFloat.Decrypt((int)hiddenValue.x, key);
			value.y = ObscuredFloat.Decrypt((int)hiddenValue.y, key);
			value.z = ObscuredFloat.Decrypt((int)hiddenValue.z, key);
			value.w = ObscuredFloat.Decrypt((int)hiddenValue.w, key);
#endif

			if (onCheatingDetected != null && !fakeValue.Equals(Quaternion.identity) && !value.Equals(fakeValue))
			{
				onCheatingDetected();
				onCheatingDetected = null;
			}

			return value;
		}

		#region operators, overrides, interface implementations
		//! @cond
		public static implicit operator ObscuredQuaternion(Quaternion value)
		{
			ObscuredQuaternion obscured = new ObscuredQuaternion(Encrypt(value));
			if (onCheatingDetected != null)
			{
				obscured.fakeValue = value;
			}
			return obscured;
		}

		public static implicit operator Quaternion(ObscuredQuaternion value)
		{
			return value.InternalDecrypt();
		}

		/// <summary>
		/// Returns the hash code for this instance.
		/// </summary>
		/// 
		/// <returns>
		/// A 32-bit signed integer hash code.
		/// </returns>
		/// <filterpriority>2</filterpriority>
		public override int GetHashCode()
		{
			return InternalDecrypt().GetHashCode();
		}

		/// <summary>
		/// Returns a nicely formatted string of the Quaternion.
		/// </summary>
		public override string ToString()
		{
			return InternalDecrypt().ToString();
		}

		/// <summary>
		/// Returns a nicely formatted string of the Quaternion.
		/// </summary>
		public string ToString(string format)
		{
			return InternalDecrypt().ToString(format);
		}

		//! @endcond
		#endregion
	}
}