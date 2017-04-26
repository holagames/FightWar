using System;
using UnityEngine;

namespace CodeStage.AntiCheat.ObscuredTypes
{
	/// <summary>
	/// Use it instead of regular <c>Vector3</c> for any cheating-sensitive variables.
	/// </summary>
	/// <strong><em>Regular type is faster and memory wiser comparing to the obscured one!</em></strong><br/>
	/// <strong>\htmlonly<font color="FF4040">WARNING:</font>\endhtmlonly Doesn't mimic regular type API, thus should be used with extra caution.</strong> Cast it to regular, not obscured type to work with regular APIs.<br/>
	/// <strong>\htmlonly<font color="7030A0">IMPORTANT:</font>\endhtmlonly Not as accurate in Flash Player as on other platforms and may produce wrong regular type comparison results.</strong>
	public struct ObscuredVector3
	{
		/// <summary>
		/// Set it to any method you wish to be called in case of any ObscuredVector2 variable cheating detection.<br/>
		/// Fires only once.
		/// </summary>
		public static Action onCheatingDetected;

		private static int cryptoKey = 120207;

		private int currentCryptoKey;
		private Vector3 hiddenValue;
		private Vector3 fakeValue;
		private bool inited;

		private ObscuredVector3(Vector3 value)
		{
			currentCryptoKey = cryptoKey;
			hiddenValue = value;
			fakeValue = new Vector3(0, 0);
			inited = true;
		}

		public float x
		{
			get
			{
				float decrypted = InternalDecryptField(hiddenValue.x);
				if (onCheatingDetected != null && fakeValue != new Vector3(0, 0) && Math.Abs(decrypted - fakeValue.x) > 0.0005f)
				{
					onCheatingDetected();
					onCheatingDetected = null;
				}
				return decrypted;
			}

			set
			{
				hiddenValue.x = InternalEncryptField(value);
				if (onCheatingDetected != null)
				{
					fakeValue.x = value;
				}
			}
		}

		public float y
		{
			get
			{
				float decrypted = InternalDecryptField(hiddenValue.y);
				if (onCheatingDetected != null && fakeValue != new Vector3(0, 0) && Math.Abs(decrypted - fakeValue.y) > 0.0005f)
				{
					onCheatingDetected();
					onCheatingDetected = null;
				}
				return decrypted;
			}

			set
			{
				hiddenValue.y = InternalEncryptField(value);
				if (onCheatingDetected != null)
				{
					fakeValue.y = value;
				}
			}
		}

		public float z
		{
			get
			{
				float decrypted = InternalDecryptField(hiddenValue.z);
				if (onCheatingDetected != null && fakeValue != new Vector3(0, 0) && Math.Abs(decrypted - fakeValue.z) > 0.0005f)
				{
					onCheatingDetected();
					onCheatingDetected = null;
				}
				return decrypted;
			}

			set
			{
				hiddenValue.z = InternalEncryptField(value);
				if (onCheatingDetected != null)
				{
					fakeValue.z = value;
				}
			}
		}

		public float this[int index]
		{
			get
			{
				switch (index)
				{
					case 0:
						return x;
					case 1:
						return y;
					case 2:
						return z;
					default:
						throw new IndexOutOfRangeException("Invalid ObscuredVector3 index!");
				}
			}
			set
			{
				switch (index)
				{
					case 0:
						x = value;
						break;
					case 1:
						y = value;
						break;
					case 2:
						z = value;
						break;
					default:
						throw new IndexOutOfRangeException("Invalid ObscuredVector3 index!");
				}
			}
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
		public Vector3 GetEncrypted()
		{
			if (currentCryptoKey != cryptoKey)
			{
				Vector3 temp = InternalDecrypt();
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
		public void SetEncrypted(Vector3 encrypted)
		{
			hiddenValue = encrypted;
			if (onCheatingDetected != null)
			{
				fakeValue = InternalDecrypt();
			}
		}

		/// <summary>
		/// Use this simple encryption method to encrypt any Vector3 value, uses default crypto key.
		/// </summary>
		public static Vector3 Encrypt(Vector3 value)
		{
			return Encrypt(value, 0);
		}

		/// <summary>
		/// Use this simple encryption method to encrypt any Vector3 value, uses passed crypto key.
		/// </summary>
		public static Vector3 Encrypt(Vector3 value, int key)
		{
			if (key == 0)
			{
				key = cryptoKey;
			}

#if !UNITY_FLASH
			value.x = ObscuredDouble.Encrypt(value.x, key);
			value.y = ObscuredDouble.Encrypt(value.y, key);
			value.z = ObscuredDouble.Encrypt(value.z, key);
#else
			value.x = ObscuredFloat.Encrypt(value.x, key);
			value.y = ObscuredFloat.Encrypt(value.y, key);
			value.z = ObscuredFloat.Encrypt(value.z, key);
#endif
			return value;
		}

		/// <summary>
		/// Use it to decrypt Vector3 you got from Encrypt(Vector3), uses default crypto key.
		/// </summary>
		public static Vector3 Decrypt(Vector3 value)
		{
			return Decrypt(value, 0);
		}

		/// <summary>
		/// Use it to decrypt Vector3 you got from Encrypt(Vector3), uses passed crypto key.
		/// </summary>
		public static Vector3 Decrypt(Vector3 value, int key)
		{
			if (key == 0)
			{
				key = cryptoKey;
			}

#if !UNITY_FLASH
			value.x = (float)ObscuredDouble.Decrypt((long)value.x, key);
			value.y = (float)ObscuredDouble.Decrypt((long)value.y, key);
			value.z = (float)ObscuredDouble.Decrypt((long)value.z, key);
#else
			value.x = ObscuredFloat.Decrypt((int)value.x, key);
			value.y = ObscuredFloat.Decrypt((int)value.y, key);
			value.z = ObscuredFloat.Decrypt((int)value.z, key);
#endif


			return value;
		}

		private Vector3 InternalDecrypt()
		{
			if (!inited)
			{
				currentCryptoKey = cryptoKey;
				hiddenValue = Encrypt(new Vector3(0, 0));
				fakeValue = new Vector3(0, 0);
				inited = true;
			}

			int key = cryptoKey;

			if (currentCryptoKey != cryptoKey)
			{
				key = currentCryptoKey;
			}

			Vector3 value;

#if !UNITY_FLASH
			value.x = (float)ObscuredDouble.Decrypt((long)hiddenValue.x, key);
			value.y = (float)ObscuredDouble.Decrypt((long)hiddenValue.y, key);
			value.z = (float)ObscuredDouble.Decrypt((long)hiddenValue.z, key);
#else
			value.x = ObscuredFloat.Decrypt((int)hiddenValue.x, key);
			value.y = ObscuredFloat.Decrypt((int)hiddenValue.y, key);
			value.z = ObscuredFloat.Decrypt((int)hiddenValue.z, key);
#endif
			if (onCheatingDetected != null && !fakeValue.Equals(new Vector3(0, 0)) && !value.Equals(fakeValue))
			{
				onCheatingDetected();
				onCheatingDetected = null;
			}

			return value;
		}

		private float InternalDecryptField(float encrypted)
		{
			int key = cryptoKey;

			if (currentCryptoKey != cryptoKey)
			{
				key = currentCryptoKey;
			}

			float result;

#if !UNITY_FLASH
			result = (float)ObscuredDouble.Decrypt((long)encrypted, key);
#else
			result = ObscuredFloat.Decrypt((int)encrypted, key);
#endif

			return result;
		}

		private float InternalEncryptField(float encrypted)
		{
			float result;

#if !UNITY_FLASH
			result = ObscuredDouble.Encrypt(encrypted, cryptoKey);
#else
			result = ObscuredFloat.Encrypt(encrypted, cryptoKey);
#endif

			return result;
		}

		#region operators, overrides, interface implementations
		//! @cond
		public static implicit operator ObscuredVector3(Vector3 value)
		{
			ObscuredVector3 obscured = new ObscuredVector3(Encrypt(value));
			if (onCheatingDetected != null)
			{
				obscured.fakeValue = value;
			}
			return obscured;
		}

		public static implicit operator Vector3(ObscuredVector3 value)
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
		/// Returns a nicely formatted string for this vector.
		/// </summary>
		public override string ToString()
		{
			return InternalDecrypt().ToString();
		}

		/// <summary>
		/// Returns a nicely formatted string for this vector.
		/// </summary>
		public string ToString(string format)
		{
			return InternalDecrypt().ToString(format);
		}

		//! @endcond
		#endregion
	}
}