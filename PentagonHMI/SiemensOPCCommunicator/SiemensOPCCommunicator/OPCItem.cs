using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace SiemensOPCCommunicator
{
    public class OPCItem
    {
        #region Constructor
        public OPCItem()
        {
        }

        /// <summary>
        /// Constructor for opc item
        /// </summary>
        /// <param name="Address">PLC Address</param>
        /// <param name="Type">CanonicalDataType</param>
        public OPCItem(string Address, Enumeration.CanonicalDataType Type)
        {
            this.Address = Address;
            this.Type = Type;
        }

        /// <summary>
        /// Constructor for opc item
        /// </summary>
        /// <param name="Address">PLC Address</param>
        /// <param name="Type">CanonicalDataType</param>
        /// <param name="ACK">Is ACK, Note: only one ack per list</param>
        public OPCItem(string Address, Enumeration.CanonicalDataType Type, bool ACK)
        {
            this.Address = Address;
            this.Type = Type;
            this.boolACK = ACK;
        }        
        #endregion

        #region Properties
        private string _strAddress;
        public string Address
        {
            get { return _strAddress; }
            set 
            { 
                _strAddress = value;
                
            }
        }

        private object _objOPCValue;
        private object objOPCValue
        {
            get { return _objOPCValue; }
            set { _objOPCValue = value; }
        }

        private string _strOPCValue;
        private string strOPCValue
        {
            get { return _strOPCValue; }
            set { _strOPCValue = value; }
        }

        private bool _boolOPCValue;
        private bool boolOPCValue
        {
            get { return _boolOPCValue; }
            set { _boolOPCValue = value; }
        }

        private double _dblOPCValue;
        private double dblOPCValue
        {
            get { return _dblOPCValue; }
            set { _dblOPCValue = value; }
        }

        private long _lngOPCValue;
        private long lngOPCValue
        {
            get { return _lngOPCValue; }
            set { _lngOPCValue = value; }
        }

        private int _intOPCValue;
        private int intOPCValue
        {
            get { return _intOPCValue; }
            set { _intOPCValue = value; }
        }

        private TimeSpan _tmOPCValue;
        private TimeSpan tmOPCValue
        {
            get { return _tmOPCValue; }
            set { _tmOPCValue = value; }
        }

        public object OPCValue
        {
            get 
            {
                if (this.Type == Enumeration.CanonicalDataType.ctBoolean)
                {
                    return boolOPCValue;
                }
                else if (this.Type == Enumeration.CanonicalDataType.ctString)
                {
                    return strOPCValue;
                }
                else if (this.Type == Enumeration.CanonicalDataType.ctReal)
                {
                    return dblOPCValue;
                }
                else if (this.Type == Enumeration.CanonicalDataType.ctLong)
                {
                    return lngOPCValue;
                }
                else if (this.Type == Enumeration.CanonicalDataType.ctInt)
                {
                    return intOPCValue;
                }
                else if (this.Type == Enumeration.CanonicalDataType.ctTime)
                {
                    return tmOPCValue;
                }
                else
                {
                    return objOPCValue;
                }
            }
            set 
            {
                if (this.Type == Enumeration.CanonicalDataType.ctBoolean)
                {
                    boolOPCValue = Convert.ToBoolean(value);
                }
                else if (this.Type == Enumeration.CanonicalDataType.ctString)
                {
                    strOPCValue = Convert.ToString(value);
                }
                else if (this.Type == Enumeration.CanonicalDataType.ctReal)
                {
                    dblOPCValue = Convert.ToDouble(value);
                }
                else if (this.Type == Enumeration.CanonicalDataType.ctLong)
                {
                    lngOPCValue = Convert.ToInt64(value);
                }
                else if (this.Type == Enumeration.CanonicalDataType.ctInt)
                {
                    intOPCValue = Convert.ToInt32(value);
                }
                else if (this.Type == Enumeration.CanonicalDataType.ctTime)
                {
                    tmOPCValue = TimeSpan.Zero;
                    tmOPCValue = TimeSpan.FromMilliseconds(Convert.ToDouble(value));
                }
                else
                {
                    objOPCValue = value;
                }
            }
        }

        private string _strQuality;
        public string Quality
        {
            get { return _strQuality; }
            set { _strQuality = value; }
        }

        private Enumeration.CanonicalDataType _Type;
        public Enumeration.CanonicalDataType Type
        {
            get { return _Type; }
            set { _Type = value; }
        }

        private bool _boolACK = false;
        public bool boolACK
        {
            get { return _boolACK; }
            set { _boolACK = value; }
        }
        #endregion
    }
}
