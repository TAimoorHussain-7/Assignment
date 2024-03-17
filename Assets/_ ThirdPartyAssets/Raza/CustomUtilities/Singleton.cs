using System.Globalization;
using System;
using UnityEngine;

namespace CustomUtilities
{
	public abstract class SingletonInternal<T> : MonoBehaviour where T : Component
	{
		/// <summary>
		///     The instance.
		/// </summary>
		protected static T instance;

		protected virtual void OnValidate()
		{
			gameObject.name = typeof(T).Name;
		}
	}

	public abstract class Singleton<T> : SingletonInternal<T> where T : Component
	{
		public static T Instance
		{
			get
			{
				if (instance == null)
				{
					instance = FindObjectOfType<T>();
					if (instance == null) instance = new GameObject(typeof(T).Name, typeof(T)).GetComponent<T>();
				}

				return instance;
			}
		}

		protected virtual void Awake()
		{
			if (instance == null)
				instance = this as T;
		}
	}

	public abstract class SingletonPersistent<T> : Singleton<T> where T : Component
	{
		protected override void Awake()
		{
			if (instance == null)
			{
				instance = this as T;
				DontDestroyOnLoad(gameObject);
			}
			else
			{
				Destroy(gameObject);
			}
		}
	}

    public static class Utilities
    {
        /// <summary>
        /// Converts the anchoredPosition of the first RectTransform to the second RectTransform,
        /// taking into consideration offset, anchors and pivot, and returns the new anchoredPosition
        /// </summary>
        public static Vector2 SwitchToRectTransform(RectTransform from, RectTransform to)
        {
            Vector2 localPoint;
            Vector2 fromPivotDerivedOffset = new Vector2(from.rect.width * 0.5f + from.rect.xMin, from.rect.height * 0.5f + from.rect.yMin);
            Vector2 screenP = RectTransformUtility.WorldToScreenPoint(null, from.position);
            screenP += fromPivotDerivedOffset;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(to, screenP, null, out localPoint);
            Vector2 pivotDerivedOffset = new Vector2(to.rect.width * 0.5f + to.rect.xMin, to.rect.height * 0.5f + to.rect.yMin);
            return to.anchoredPosition + localPoint - pivotDerivedOffset;
        }
        public static DateTime TryParseDateTime(int epochTime)
        {
            DateTime start = new DateTime(1970, 1, 1, 0, 0, 0, 0); //from start epoch time
            start = start.AddSeconds(epochTime); //add the seconds to the start DateTime
            return start;
        }

        public static int ConvertDateToEpoch(DateTime dateTime)
        {
            TimeSpan timeSpan = dateTime - new DateTime(1970, 1, 1);
            int secondsSinceEpoch = (int)timeSpan.TotalSeconds;
            //Console.WriteLine(secondsSinceEpoch);
            return secondsSinceEpoch;
        }

        public static string TimeFromSeconds(int seconds)
        {
            TimeSpan time = TimeSpan.FromSeconds(seconds);
            //here backslash is must to tell that colon is
            //not the part of format, it just a character that we want in output
            return time.ToString(@"mm\:ss");
        }


        public static double ToDouble(string val)
        {
            return val.Length == 0 || val == "-" ? 0.0f : double.Parse(val, CultureInfo.InvariantCulture);
        }

        public static float ToFloat(string val)
        {
            return val.Length == 0 || val == "-" ? 0f : float.Parse(val, CultureInfo.InvariantCulture);
        }

        public static int ToInt(string val)
        {
            /*Debug.LogError(val);*/
            return val.Length == 0 || val == "-" || string.IsNullOrEmpty(val) ? 0 : int.Parse(val.Split('.')[0], CultureInfo.InvariantCulture);
        }

        public static bool ToBool(string booleanString)
        {
            booleanString = booleanString.ToUpper();
            switch (booleanString)
            {
                case "TRUE":
                    return true;
                case "FALSE":
                    return false;
                case "1":
                    return true;
                case "0":
                    return false;
                default:
                    return false;
            }
        }

        public static string GetDeviceId()
        {
#if UNITY_EDITOR || UNITY_ANDROID
            const string deviceIdPrefName = "deviceid";
            var deviceId = PlayerPrefs.GetString(deviceIdPrefName, SystemInfo.deviceUniqueIdentifier);
            PlayerPrefs.SetString(deviceIdPrefName, deviceId);
            return deviceId;
#elif UNITY_IOS
            string userId = KeyChain.BindGetKeyChainUUID ();
            if (string.IsNullOrEmpty(userId))
            {
                userId = SystemInfo.deviceUniqueIdentifier;
                KeyChain.BindSetKeyChainUUID(userId);
            }
            return userId;
#endif
        }
        public static string ParseImageName(string val)
        {
            val = val.Replace(":", "_");
            val = val.Replace(" ", "");
            return val;
        }

        public static string ParseDialogues(string val)
        {
            val = val.Trim();
            val = val.Replace("Ö", ",");
            return val;
        }

    }
}