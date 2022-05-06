using System;
using System.Collections.Generic;
using System.Text;

namespace Utilities
{
    public static class Converter
    {
        public static bool ToBoolean(object Value)
        {
            bool _bReturn = false;
            if (Value == null)
            {
                return false;
            }
            else if (!Boolean.TryParse(Value.ToString(), out _bReturn))
            {
                return false;
            }
            else
            {
                return _bReturn;
            }
        }

        public static Int32 ToInt32(object Value)
        {
            Int32 _iReturn = 0;
            if (Value == null)
            {

                return 0;
            }
            else
            {
                try
                {
                    _iReturn = Convert.ToInt32(Value);
                    return _iReturn;
                }
                catch
                {
                    if (!Int32.TryParse(Value.ToString(), out _iReturn))
                    {
                        return 0;
                    }
                    else
                    {
                        return _iReturn;
                    }
                }
            }
        }

        public static Int16 ToInt16(object Value)
        {
            Int16 _iReturn = 0;
            if (Value == null)
            {
                return 0;
            }
            else
            {
                try
                {
                    _iReturn = Convert.ToInt16(Value);
                    return _iReturn;
                }
                catch
                {
                    if (!Int16.TryParse(Value.ToString(), out _iReturn))
                    {
                        return 0;
                    }
                    else
                    {
                        return _iReturn;
                    }
                }
            }
        }

        public static String ToString(object Value)
        {
            if (Value == null)
            {
                return "";
            }
            else if (Value is DBNull)
            {
                return "";
            }
            else
            {
                return Value.ToString();
            }
        }

        public static Decimal ToDecimal(object Value)
        {
            Decimal _dReturn = 0;
            if (Value == null)
            {
                return 0;
            }

            if (!Decimal.TryParse(Value.ToString(), out _dReturn))
            {
                return 0;
            }
            else
            {
                return _dReturn;
            }
        }

        public static DateTime ToDateTime(object Value, String DateFormat)
        {
            DateTime _dtReturn;
            if (DateTime.TryParseExact(Value.ToString(), DateFormat, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out _dtReturn))
            {
                return _dtReturn;
            }
            else
            {
                return new DateTime(1999, 1, 1);
            }
        }

        public static DateTime ToDateTime(object Value)
        {
            DateTime _dtReturn;
            if (DateTime.TryParse(Value.ToString(), out _dtReturn))
            {
                return _dtReturn;
            }
            else
            {
                return new DateTime(1999, 1, 1);
            }
        }

        public static double ToDouble(object Value)
        {
            double _dtReturn;
            if (double.TryParse(Value.ToString(), out _dtReturn))
            {
                return _dtReturn;
            }
            else
            {
                return 0;
            }

        }

        public static TimeSpan ToTimeSpan(object Value)
        {
            //TimeSpan _dtReturn;            
            if (Value != null)
            {
                return (TimeSpan)Value;
            }
            else
            {
                return new TimeSpan(0, 0, 0);
            }

        }

        public static TimeSpan ToTimeSpan(object Value, String TimeFormat)
        {
            DateTime _dtReturn;
            if (DateTime.TryParseExact(Value.ToString(), TimeFormat, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out _dtReturn))
            {

                return _dtReturn.TimeOfDay;
            }
            else
            {
                return new TimeSpan(0, 0, 0);
            }

        }

        public static Int32 ToInt32FromStyle(object Value, System.Globalization.NumberStyles NumberStyles)
        {
            Int32 _iReturn = 0;
            if (Value == null)
            {
                return 0;
            }
            else
            {
                if (!Int32.TryParse(Value.ToString(),NumberStyles, System.Globalization.CultureInfo.InvariantCulture, out _iReturn))
                {
                    return 0;
                }
                else
                {
                    return _iReturn;
                }
            }            
        }

        public static Int64 ToInt64FromStyle(object Value, System.Globalization.NumberStyles NumberStyles)
        {
            Int64 _iReturn = 0;
            if (Value == null)
            {
                return 0;
            }
            else
            {
                if (!Int64.TryParse(Value.ToString(), NumberStyles, System.Globalization.CultureInfo.InvariantCulture, out _iReturn))
                {
                    return 0;
                }
                else
                {
                    return _iReturn;
                }
            }
        }

        public static char[] ConvertIntToBoolean(string Integer)
        {
            string strByte = string.Empty;

            int convertedInt = Convert.ToInt32(int.Parse(Integer, System.Globalization.NumberStyles.Integer));
            strByte += Convert.ToString(convertedInt,2).PadLeft(16, '0');

            string reverseBinary = string.Empty;
            char[] tempChar = strByte.ToCharArray();

            for (int j = 0; j <= tempChar.Length - 1; j += 16)
            {
                string temp8 = string.Empty;
                for (int i = j + 15; i >= j; i--)
                {
                    temp8 += tempChar[i];
                }
                reverseBinary += temp8;
            }

            char[] strChar = reverseBinary.ToCharArray();
            return strChar;

        }

        public static char[] ConvertIntToBoolean1(string Integer)
        {
            string strByte = string.Empty;

            int convertedInt = Convert.ToInt32(int.Parse(Integer, System.Globalization.NumberStyles.Integer));
            strByte += Convert.ToString(convertedInt, 2).PadLeft(24, '0');

            string reverseBinary = string.Empty;
            char[] tempChar = strByte.ToCharArray();

            for (int j = 0; j <= tempChar.Length - 1; j += 24)
            {
                string temp8 = string.Empty;
                for (int i = j + 23; i >= j; i--)
                {
                    temp8 += tempChar[i];
                }
                reverseBinary += temp8;
            }

            char[] strChar = reverseBinary.ToCharArray();
            return strChar;

        }
    }
}
