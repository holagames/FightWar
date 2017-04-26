using System;
using System.Text;

namespace CodeStage.AntiCheat.ObscuredTypes
{
	/// <summary>
	/// Use it instead of regular <c>string</c> for any cheating-sensitive variables.
	/// </summary>
	/// <strong><em>Regular type is faster and memory wiser comparing to the obscured one!</em></strong>
	public sealed class ObscuredString
	{
		/// <summary>
		/// Set it to any method you wish to be called in case of any ObscuredVector2 variable cheating detection.<br/>
		/// Fires only once.
		/// </summary>
		public static Action onCheatingDetected;

		private static string cryptoKey = "4441";

		private string currentCryptoKey;
		private string hiddenValue;
		private string fakeValue;
		private bool inited;

		private ObscuredString(string value)
		{
			currentCryptoKey = cryptoKey;
			hiddenValue = value;
			fakeValue = null;
			inited = true;
		}

		/// <summary>
		/// Allows to change default crypto key.
		/// </summary>
		public static void SetNewCryptoKey(string newKey)
		{
			cryptoKey = newKey;
		}

		/// <summary>
		/// Allows to pick current obscured value as is.
		/// </summary>
		/// Use it in conjunction with SetEncrypted().<br/>
		/// Useful for saving data in obscured state.
		public string GetEncrypted()
		{
			return hiddenValue;
		}

		/// <summary>
		/// Allows to explicitly set current obscured value.
		/// </summary>
		/// Use it in conjunction with GetEncrypted().<br/>
		/// Useful for loading data stored in obscured state.
		public void SetEncrypted(string encrypted)
		{
			hiddenValue = encrypted;
			if (onCheatingDetected != null)
			{
				fakeValue = InternalDecrypt();
			}
		}

		/// <summary>
		/// Simple symmetric encryption, uses default crypto key.
		/// </summary>
		/// <returns>Encrypted or decrypted <c>string</c> (depending on what <c>string</c> was passed to the function)</returns>
		public static string EncryptDecrypt(string value)
		{
			return EncryptDecrypt(value, "");
		}

		/// <summary>
		/// Simple symmetric encryption, uses passed crypto key.
		/// </summary>
		/// <returns>Encrypted or decrypted <c>string</c> (depending on what <c>string</c> was passed to the function)</returns>
		public static string EncryptDecrypt(string value, string key)
		{
			if (string.IsNullOrEmpty(value))
			{
				return "";
			}

			if (string.IsNullOrEmpty(key))
			{
				key = cryptoKey;
			}

			StringBuilder result = new StringBuilder();
			int keyLength = key.Length;
			int valueLength = value.Length;

			for (int i = 0; i < valueLength; i++)
			{
				result.Append((char)(value[i] ^ key[i % keyLength]));
			}

			return result.ToString();
		}

		private string InternalDecrypt()
		{
			if (!inited)
			{
				currentCryptoKey = cryptoKey;
				hiddenValue = EncryptDecrypt("");
				fakeValue = "";
				inited = true;
			}

			string key = cryptoKey;

			if (currentCryptoKey != cryptoKey)
			{
				key = currentCryptoKey;
			}

			string result = EncryptDecrypt(hiddenValue, key);

			if (onCheatingDetected != null && fakeValue != null && result != fakeValue)
			{
				onCheatingDetected();
				onCheatingDetected = null;
			}

			return result;
		}

		#region operators and overrides
		//! @cond
		public static implicit operator ObscuredString(string value)
		{
			if (value == null)
			{
				return null;
			}

			ObscuredString obscured = new ObscuredString(EncryptDecrypt(value));
			if (onCheatingDetected != null)
			{
				obscured.fakeValue = value;
			}
			return obscured;
		}

		public static implicit operator string(ObscuredString value)
		{
			if (value == null)
			{
				return null;
			}
			return value.InternalDecrypt();
		}

		/// <summary>
		/// Overrides default ToString to provide easy implicit conversion to the <c>string</c>.
		/// </summary>
		public override string ToString()
		{
			return InternalDecrypt();
		}

		/// <summary>
		/// Determines whether two specified ObscuredStrings have the same value.
		/// </summary>
		/// 
		/// <returns>
		/// true if the value of <paramref name="a"/> is the same as the value of <paramref name="b"/>; otherwise, false.
		/// </returns>
		/// <param name="a">An ObscuredString or null. </param><param name="b">An ObscuredString or null. </param><filterpriority>3</filterpriority>
		public static bool operator ==(ObscuredString a, ObscuredString b)
		{
			if (ReferenceEquals(a, b))
			{
				return true;
			}

			if (((object)a == null) || ((object)b == null))
			{
				return false;
			}

			string strA = a.hiddenValue;
			string strB = b.hiddenValue;

			return string.Equals(strA, strB);
		}

		/// <summary>
		/// Determines whether two specified ObscuredStrings have different values.
		/// </summary>
		/// 
		/// <returns>
		/// true if the value of <paramref name="a"/> is different from the value of <paramref name="b"/>; otherwise, false.
		/// </returns>
		/// <param name="a">An ObscuredString or null. </param><param name="b">An ObscuredString or null. </param><filterpriority>3</filterpriority>
		public static bool operator !=(ObscuredString a, ObscuredString b)
		{
			return !(a == b);
		}

		/// <summary>
		/// Determines whether this instance of ObscuredString and a specified object, which must also be a ObscuredString object, have the same value.
		/// </summary>
		/// 
		/// <returns>
		/// true if <paramref name="obj"/> is a ObscuredString and its value is the same as this instance; otherwise, false.
		/// </returns>
		/// <param name="obj">An <see cref="T:System.Object"/>. </param><filterpriority>2</filterpriority>
		public override bool Equals(object obj)
		{
			ObscuredString strA = obj as ObscuredString;
			string strB = null;
			if (strA != null) strB = strA.hiddenValue;

			return string.Equals(hiddenValue, strB);
		}

		/// <summary>
		/// Determines whether this instance and another specified ObscuredString object have the same value.
		/// </summary>
		/// 
		/// <returns>
		/// true if the value of the <paramref name="value"/> parameter is the same as this instance; otherwise, false.
		/// </returns>
		/// <param name="value">A ObscuredString. </param><filterpriority>2</filterpriority>
		public bool Equals(ObscuredString value)
		{
			string strA = null;
			if (value != null) strA = value.hiddenValue;

			return string.Equals(hiddenValue, strA);
		}

		/// <summary>
		/// Determines whether this string and a specified ObscuredString object have the same value. A parameter specifies the culture, case, and sort rules used in the comparison.
		/// </summary>
		/// 
		/// <returns>
		/// true if the value of the <paramref name="value"/> parameter is the same as this string; otherwise, false.
		/// </returns>
		/// <param name="value">An ObscuredString to compare.</param><param name="comparisonType">A value that defines the type of comparison. </param><exception cref="T:System.ArgumentException"><paramref name="comparisonType"/> is not a <see cref="T:System.StringComparison"/> value. </exception><filterpriority>2</filterpriority>
		public bool Equals(ObscuredString value, StringComparison comparisonType)
		{
			string strA = null;
			if (value != null) strA = value.InternalDecrypt();

			return string.Equals(InternalDecrypt(), strA, comparisonType);
		}

		/// <summary>
		/// Returns the hash code for this ObscuredString.
		/// </summary>
		/// 
		/// <returns>
		/// A 32-bit signed integer hash code.
		/// </returns>
		public override int GetHashCode()
		{
			return InternalDecrypt().GetHashCode();
		}
		//! @endcond
		#endregion
	}
}