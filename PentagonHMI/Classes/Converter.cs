using System;

namespace PentagonHMI.Classes
{
    internal class Converter
    {
        public static bool ToBoolean(object Value)
        {
            bool _bReturn = false;
            if(Value == null)
            {
                return false;
            }
            else if(!Boolean.TryParse(Value.ToString(), out _bReturn))
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
            if(Value == null)
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
                    if(!Int32.TryParse(Value.ToString(), out _iReturn))
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
            if(Value == null)
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
                    if(!Int16.TryParse(Value.ToString(), out _iReturn))
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
            if(Value == null)
            {
                return "";
            }
            else if(Value is DBNull)
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
            if(Value == null)
            {
                return 0;
            }

            if(!Decimal.TryParse(Value.ToString(), out _dReturn))
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
            if(DateTime.TryParseExact(Value.ToString(), DateFormat, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out _dtReturn))
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
            if(DateTime.TryParse(Value.ToString(), out _dtReturn))
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
            if(double.TryParse(Value.ToString(), out _dtReturn))
            {
                return _dtReturn;
            }
            else
            {
                return 0;
            }
        }

        public static TimeSpan ToTimeSpan(object Value, String TimeFormat)
        {
            DateTime _dtReturn;
            if(DateTime.TryParseExact(Value.ToString(), TimeFormat, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out _dtReturn))
            {
                return _dtReturn.TimeOfDay;
            }
            else
            {
                return new TimeSpan(0, 0, 0);
            }
        }
    }
}