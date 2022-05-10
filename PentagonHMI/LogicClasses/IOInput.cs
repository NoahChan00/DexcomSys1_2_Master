using System;
using Logix;
using System.Linq;
using System.Collections.Generic;
using System.Data;

namespace PentagonHMI.LogicClasses
{
    public class IOInput : IDisposable
    {
        #region Variables
        LogicClasses.Main _Main;
        public Dictionary<int, Tag> Dic_ITag = new Dictionary<int, Tag>();
        public Dictionary<int, Tag> Dic_OTag = new Dictionary<int, Tag>();

        public delegate void onUpdateHandler();
        public event onUpdateHandler OnUpdate;

        public delegate void IOUpdateHandler();
        public event IOUpdateHandler UpdateIO;

        private string SQL_Select_IOMode = string.Format(Info.SQL.Select.Config, "IOReadMode");

        //1Array
        Tag TagAllInput;
        Tag TagAllOutput;
        private string SQL_Select_1ArrayIn = string.Format(Info.SQL.Select.TagName, "IOArrayInput");
        private string SQL_Select_1ArrayOut = string.Format(Info.SQL.Select.TagName, "IOArrayOutput");

        //8Array
        TagGroup TgrpIn;
        TagGroup TgrpOut;

        public string Locnow { get; set; } = "DEFAULT";
        string IOReadMode;

        Controller MainIOPLC = new Controller
        {
            IPAddress = Classes.GlobalFunctions.PLC_IPAddress,
            Path = Classes.GlobalFunctions.PLC_Path,
            Timeout = Convert.ToInt16(Classes.GlobalFunctions.PLC_Timeout.ToString())
        };

        #endregion
        #region Constructor
        public IOInput(ref Main MainConnection)
        {
            _Main = MainConnection;
            _Main.OnIOUpdate += new Main.onIOUpdateHandler(TcpIOInput_OnUpdate);
            Initialization();
        }
        #endregion
        #region Methods
        public void Initialization()
        {
            if (MainIOPLC.Connect() != ResultCode.E_SUCCESS)
                Utilities.FileLogger.logError("Plc1 Connection Failed", "IO-Page");

            IOReadMode = _Main.SQLer.Exec_Scalar<string>(SQL_Select_IOMode);

            if (string.IsNullOrWhiteSpace(IOReadMode))
            {
                //One By One
            }
            else if (IOReadMode.ToUpper().Equals("1ARRAY"))
            {
                TagAllInput = new Tag { Name = _Main.SQLer.Exec_Scalar<string>(SQL_Select_1ArrayIn), NetType = typeof(System.Single), Length = 50 };
                TagAllOutput = new Tag { Name = _Main.SQLer.Exec_Scalar<string>(SQL_Select_1ArrayOut), NetType = typeof(System.Single), Length = 50 };

                UpdateIO += new IOUpdateHandler(Array1_UpdateIO);
            }
            else if (IOReadMode.ToUpper().Equals("8ARRAY"))
            {
                TgrpIn = new TagGroup();
                TgrpOut = new TagGroup();
                string Select = "SELECT LEFT([TAGNAME], LEN([TAGNAME]) - 2) AS 'DATA' FROM [gdb_DexcomSystem1].[DBO].[IO] " +
                      "WHERE [STATIONID] = 1 AND [IO] = '{0}' AND [TAGINDEX]%8 = 0 GROUP BY LEFT([TAGNAME], LEN([TAGNAME]) -2)," +
                      "[TAGINDEX] ORDER BY CAST([TAGINDEX] AS INT)";
                DataTable DTI = _Main.SQLer.Exec_DTSelect(string.Format(Select, 'I'));
                DataTable DTO = _Main.SQLer.Exec_DTSelect(string.Format(Select, 'O'));

                foreach (DataRow dr_tagname in DTI.Rows)
                    TgrpIn.AddTag(new Tag { Name = dr_tagname[0].ToString(), DataType = Tag.ATOMIC.SINT });
                foreach (DataRow dr_tagname in DTO.Rows)
                    TgrpOut.AddTag(new Tag { Name = dr_tagname[0].ToString(), DataType = Tag.ATOMIC.SINT });
                UpdateIO += new IOUpdateHandler(Array8_UpdateIO);
            }
        }

        public void Dispose()
        {
            _Main.IOPageON = false;
        }

        #endregion
        #region Event
        void TcpIOInput_OnUpdate()
        {
            try
            {
                UpdateIO();
                if (OnUpdate != null)
                    OnUpdate();
            }
            catch (Exception ex)
            {
                Utilities.FileLogger.logError(ex.Message, "IO List threading");
            }
        }

        private void Array8_UpdateIO()
        {
            try
            {
                if (MainIOPLC.GroupRead(TgrpIn, TgrpOut) == ResultCode.E_SUCCESS)
                {
                    _Main.IValue.Clear();
                    foreach (Tag _tag in TgrpIn.Tags)
                    {
                        SByte Value = Convert.ToSByte(_tag.Value);
                        _Main.IValue.AddRange(Convert.ToString(Value, 2).PadLeft(16, Value < 0 ? '1' : '0')
                            .Substring(8).ToCharArray().OfType<object>().Select(o => o.Equals('1') ? true : false).ToArray().Reverse());
                    }

                    _Main.OValue.Clear();
                    foreach (Tag _tag in TgrpOut.Tags)
                    {
                        SByte Value = Convert.ToSByte(_tag.Value);
                        _Main.OValue.AddRange(Convert.ToString(Value, 2).PadLeft(16, Value < 0 ? '1' : '0')
                            .Substring(8).ToCharArray().OfType<object>().Select(o => o.Equals('1') ? true : false).ToArray().Reverse());
                    }
                }
            }
            catch (Exception ex)
            {
                Utilities.FileLogger.logError(ex.Message, "UpdateIO Failed");
            }
        }

        private void Array1_UpdateIO()
        {
            try
            {
                if (MainIOPLC.ReadTag(TagAllInput) == ResultCode.E_SUCCESS)
                {
                    _Main.IValue.Clear();
                    foreach (SByte Value in ((Array)TagAllInput.Value).OfType<object>().Select(o => Convert.ToSByte(o)).ToArray())
                        _Main.IValue.AddRange(Convert.ToString(Value, 2).PadLeft(16, Value < 0 ? '1' : '0').Substring(8).ToCharArray().OfType<object>().Select(o => o.Equals('1') ? true : false).ToArray().Reverse());
                }

                if (MainIOPLC.ReadTag(TagAllOutput) == ResultCode.E_SUCCESS)
                {
                    _Main.OValue.Clear();
                    foreach (SByte Value in ((Array)TagAllOutput.Value).OfType<object>().Select(o => Convert.ToSByte(o)).ToArray())
                        _Main.OValue.AddRange(Convert.ToString(Value, 2).PadLeft(16, Value < 0 ? '1' : '0').Substring(8).ToCharArray().OfType<object>().Select(o => o.Equals('1') ? true : false).ToArray().Reverse());
                }
            }
            catch (Exception ex)
            {
                Utilities.FileLogger.logError(ex.Message, "UpdateIO Failed");
            }
        }

        public bool WorkTag(ref Tag tag, bool? Value = null)
        {
            try
            {
                if (Value == null)
                {
                    if (MainIOPLC.ReadTag(tag) == ResultCode.E_SUCCESS)
                        return true;
                }
                else
                {
                    tag.Value = Value;
                    if (MainIOPLC.WriteTag(tag) == ResultCode.E_SUCCESS)
                        return true;
                }
            }
            catch (Exception ex)
            {
                Utilities.FileLogger.logError(ex.Message, "WorkTag Failed");
            }

            return false;
        }
        #endregion

        #region Destructor
        ~IOInput()
        {
            Dispose();
        }
        #endregion
    }
}
