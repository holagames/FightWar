using System;

namespace CodeStage.AntiCheat.ObscuredTypes
{
	/// <summary>
	/// Use it instead of regular <c>bool</c> for any cheating-sensitive variables.
	/// </summary>
	/// <strong><em>Regular type is faster and memory wiser comparing to the obscured one!</em></strong>
	public struct ObscuredBool : IEquatable<ObscuredBool>
	{
#if !UNITY_FLASH
		/// <summary>
		/// Set it to any method you wish to be called in case of any ObscuredBool variable cheating detection.<br/>
		/// Fires only once.
		/// </summary>
		/// <strong>\htmlonly<font color="FF4040">WARNING:</font>\endhtmlonly Flash Player is not supported!</strong>
		public static Action onCheatingDetected;
#endif

		private static byte cryptoKey = 215;

		private byte currentCryptoKey;
		private int hiddenValue;

#if !UNITY_FLASH
		private bool? fakeValue;
#endif

		private bool inited;

		private ObscuredBool(int value)
		{
			currentCryptoKey = cryptoKey;
			hiddenValue = value;
#if !UNITY_FLASH
			fakeValue = null;
#endif
			inited = true;
		}

		/// <summary>
		/// Allows to change default crypto key.
		/// </summary>
		public static void SetNewCryptoKey(byte newKey)
		{
			cryptoKey = newKey;
		}

		/// <summary>
		/// Allows to pick current obscured value as is.
		/// </summary>
		/// Use it in conjunction with SetEncrypted().<br/>
		/// Useful for saving data in obscured state.
		public int GetEncrypted()
		{
			if (currentCryptoKey != cryptoKey)
			{
				bool temp = InternalDecrypt();
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
		public void SetEncrypted(int encrypted)
		{
			hiddenValue = encrypted;
#if !UNITY_FLASH
			if (onCheatingDetected != null)
			{
				fakeValue = InternalDecrypt();
			}
#endif
		}

		/// <summary>
		/// Use this simple encryption method to encrypt any bool value, uses default crypto key.
		/// </summary>
		public static int Encrypt(bool value)
		{
			return Encrypt(value, 0);
		}

		/// <summary>
		/// Use this simple encryption method to encrypt any bool value, uses passed crypto key.
		/// </summary>
		public static int Encrypt(bool value, byte key)
		{
			if (key == 0)
			{
				key = cryptoKey;
			}

			int encryptedValue = value ? 213 : 181;

			encryptedValue ^= key;

			return encryptedValue;
		}

		/// <summary>
		/// Use it to decrypt int you got from Encrypt(bool), uses default crypto key.
		/// </summary>
		public static bool Decrypt(int value)
		{
			return Decrypt(value, 0);
		}

		/// <summary>
		/// Use it to decrypt int you got from Encrypt(bool), uses passed crypto key.
		/// </summary>
		public static bool Decrypt(int value, byte key)
		{
			if (key == 0)
			{
				key = cryptoKey;
			}

			value ^= key;

			return value != 181;
		}

		private bool InternalDecrypt()
		{
			if (!inited)
			{
				currentCryptoKey = cryptoKey;
				hiddenValue = Encrypt(false);
#if !UNITY_FLASH
				fakeValue = false;
#endif
				inited = true;
			}

			byte key = cryptoKey;

			if (currentCryptoKey != cryptoKey)
			{
				key = currentCryptoKey;
			}

			int value = hiddenValue;
			value ^= key;

			bool decrypted = value != 181;

#if !UNITY_FLASH
			if (onCheatingDetected != null && fakeValue != null && decrypted != fakeValue)
			{
				onCheatingDetected();
				onCheatingDetected = null;
			}
#endif

			return decrypted;
		}

		#region operators, overrides, interface implementations
		//! @cond
		public static implicit operator ObscuredBool(bool value)
		{
			ObscuredBool obscured = new ObscuredBool(Encrypt(value));

#if !UNITY_FLASH
			if (onCheatingDetected != null)
			{
				obscured.fakeValue = value;
			}
#endif

			return obscured;
		}

		public static implicit operator bool(ObscuredBool value)
		{
			return value.InternalDecrypt();
		}

		/// <summary>
		/// Returns a value indicating whether this instance is equal to a specified object.
		/// </summary>
		/// 
		/// <returns>
		/// true if <paramref name="obj"/> is an instance of ObscuredBool and equals the value of this instance; otherwise, false.
		/// </returns>
		/// <param name="obj">An object to compare with this instance. </param><filterpriority>2</filterpriority>
		public override bool Equals(object obj)
		{
			if (!(obj is ObscuredBool))
				return false;

			ObscuredBool oi = (ObscuredBool)obj;
			return (hiddenValue == oi.hiddenValue);
		}

		/// <summary>
		/// Returns a value indicating whether this instance is equal to a specified ObscuredBool value.
		/// </summary>
		/// 
		/// <returns>
		/// true if <paramref name="obj"/> has the same value as this instance; otherwise, false.
		/// </returns>
		/// <param name="obj">An ObscuredVector3 value to compare to this instance.</param><filterpriority>2</filterpriority>
		public bool Equals(ObscuredBool obj)
		{
			return hiddenValue == obj.hiddenValue;
		}

#if !UNITY_FLASH
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
#else
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
			return !InternalDecrypt() ? 0 : 1;
		}
#endif

		/// <summary>
		/// Converts the numeric value of this instance to its equivalent string representation.
		/// </summary>
		/// 
		/// <returns>
		/// The string representation of the value of this instance, consisting of a negative sign if the value is negative, and a sequence of digits ranging from 0 to 9 with no leading zeroes.
		/// </returns>
		/// <filterpriority>1</filterpriority>
		public override string ToString()
		{
			return InternalDecrypt().ToString();
		}

		//! @endcond
		#endregion

	}
}