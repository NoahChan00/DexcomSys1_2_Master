using Logix;
using SimpleOPC.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SimpleOPC
{
    public class INGEAR_Opc : IDisposable
    {
        public struct TagInfo
        {
            public string Name;
            public Type DataType;
            public object MonitorValue;
        }

        private static Dictionary<Type, Tag.ATOMIC> Dic_Type_IngearType = new Dictionary<Type, Tag.ATOMIC>
        {
            [typeof(bool)] = Tag.ATOMIC.BOOL,
            [typeof(double)] = Tag.ATOMIC.REAL,
            [typeof(short)] = Tag.ATOMIC.INT,
            // Actual ATOMIC.INT is 16 bit, but previous code utilize as int
            //[typeof(int)] = Tag.ATOMIC.INT,
            [typeof(int)] = Tag.ATOMIC.DINT,
            [typeof(long)] = Tag.ATOMIC.LINT,
            [typeof(object)] = Tag.ATOMIC.OBJECT,
            [typeof(sbyte)] = Tag.ATOMIC.SINT,
            [typeof(float)] = Tag.ATOMIC.REAL,
            [typeof(double)] = Tag.ATOMIC.REAL,
            [typeof(string)] = Tag.ATOMIC.STRING,
        };

        public EventHandler OPCUpdate;
        public Func<bool> IsConnected => () => Ctr_OPC?.IsConnected ?? false;

        private object Locker = new object();
        private Dictionary<string, Tag> Dic_TagName_Tag = new Dictionary<string, Tag>();
        private List<Controller> lst_ScanControllers = new List<Controller>();
        private List<TagGroup> lst_ScanGroups = new List<TagGroup>();
        private Controller Ctr_OPC = new Controller();
        private TagGroup IndieTagGroup;

        #region Disposer

        private void DisposeMainBody()
        {
            try
            {
                Ctr_OPC?.Dispose();
                foreach(TagGroup tagGroup in lst_ScanGroups)
                    tagGroup?.Dispose();
                foreach(Controller Ctr in lst_ScanControllers)
                    Ctr?.Dispose();
                Dic_TagName_Tag = null;
                OPCUpdate = null;
            }
            catch(Exception) { throw; }
        }

        public void Dispose()
        {
            try
            {
                DisposeMainBody();
                GC.SuppressFinalize(this);
            }
            catch(Exception) { throw; }
        }

        #endregion Disposer

        public INGEAR_Opc(string IP)
        {
            Logger.SetPath();
            Connect(IP);
        }

        public bool Initialize(List<TagInfo> lst_tags, string ip, string path = "0", int timeout = 3000, object controller = null)
        {
            try
            {
                if(controller == null)
                    Connect(ip, path, timeout);
                else
                    Ctr_OPC = controller as Controller;
                Dic_TagName_Tag.Clear();
                foreach(TagInfo tagInfo in lst_tags)
                {
                    Tag.ATOMIC _type;
                    Tag _tag = new Tag
                    {
                        Name = tagInfo.Name,
                        DataType = Dic_Type_IngearType.TryGetValue(tagInfo.DataType, out _type) ? _type : Tag.ATOMIC.OBJECT,
                        Controller = Ctr_OPC,
                        MyObject = tagInfo.MonitorValue
                    };
                    Dic_TagName_Tag.Add(tagInfo.Name, _tag);
                }
                return IsConnected();
            }
            catch(Exception) { throw; }
        }

        public T Read<T>(string TagName, Type TagType = null, int length = 1)
        {
            try
            {
                Tag _tag;
                if(!Dic_TagName_Tag.TryGetValue(TagName, out _tag))
                {
                    _tag = new Tag { Name = TagName, DataType = Dic_Type_IngearType[TagType ?? typeof(T)], Length = length };
                    Dic_TagName_Tag.Add(TagName, _tag);
                }

                Ctr_OPC.ReadTag(_tag); //== ResultCode.E_SUCCESS &&

                if(_tag.QualityCode == ResultCode.QUAL_GOOD)
                    return (T)Convert.ChangeType(_tag.Value, typeof(T));

                Logger.Warn(string.Format(Logger.Msg.ReadFail, _tag.Name, _tag.Value));
                return default;
            }
            catch(Exception) { throw; }
        }

        public object Read(string TagName, Type TagType = null, int length = 1)
        {
            try
            {
                Tag _tag;
                if(!Dic_TagName_Tag.TryGetValue(TagName, out _tag))
                {
                    _tag = new Tag { Name = TagName, DataType = Dic_Type_IngearType[TagType], Length = length };
                    Dic_TagName_Tag.Add(TagName, _tag);
                }

                Ctr_OPC.ReadTag(_tag); //== ResultCode.E_SUCCESS &&
                if(_tag.QualityCode == ResultCode.QUAL_GOOD)
                    return _tag.Value;

                Logger.Warn(string.Format(Logger.Msg.ReadFail, _tag.Name, _tag.Value));
                return default;
            }
            catch(Exception) { throw; }
        }

        public bool Write(string TagName, object Value, Type TagType = null)
        {
            try
            {
                Logger.Info(string.Format(Logger.Msg.Write, TagName, Value?.ToString() ?? "NULL"));
                Tag _tag;
                if(!Dic_TagName_Tag.TryGetValue(TagName, out _tag) || _tag.Length > 1)
                {
                    _tag = new Tag { Name = TagName, DataType = TagType == null ? Tag.ATOMIC.BOOL : Dic_Type_IngearType[TagType] };
                    Dic_TagName_Tag.Add(TagName, _tag);
                }

                _tag.Value = Value;
                if(Ctr_OPC.WriteTag(_tag) == ResultCode.E_SUCCESS && _tag.QualityCode == ResultCode.QUAL_GOOD)
                    return true;

                Logger.Warn(string.Format(Logger.Msg.WriteFail, _tag.Name, _tag.Value));
                return false;
            }
            catch(Exception) { throw; }
        }

        public bool WriteStringTag(string TagName, object Value)
        {
            Tag _tag = new Tag { Name = TagName, DataType = Tag.ATOMIC.STRING };
            Ctr_OPC.ReadTag(_tag);
            try
            {
                _tag.Value = (Value ?? string.Empty).ToString();
                if(Ctr_OPC.WriteTag(_tag) == ResultCode.E_SUCCESS && _tag.QualityCode == ResultCode.QUAL_GOOD)
                    return true;

                Logger.Warn(string.Format(Logger.Msg.WriteFail, _tag.Name, _tag.Value));
                return false;
            }
            catch(Exception) { throw; }
            finally { _tag.Dispose(); _tag = null; }
        }

        #region Read/Write By Tag

        public bool ReadTag(ref Tag _tag, Tag.ATOMIC tagType, Type type = null)
        {
            try
            {
                if(_tag.Controller == null)
                {
                    _tag.Controller = Ctr_OPC;
                };
                if(type != null)
                {
                    _tag.NetType = type;
                };
                _tag.DataType = tagType;
                if(Ctr_OPC.ReadTag(_tag) == ResultCode.E_SUCCESS && _tag.QualityCode == ResultCode.QUAL_GOOD)
                    return true;
                Logger.Warn(string.Format(Logger.Msg.ReadFail, _tag.Name, _tag.Value));
                return false;
            }
            catch(Exception) { throw; }
        }

        public bool WriteTag(ref Tag _tag, object value = null)
        {
            try
            {
                if(_tag.Controller == null)
                {
                    _tag.Controller = Ctr_OPC;
                };
                if(value != null)
                    _tag.Value = value;
                if(Ctr_OPC.WriteTag(_tag) == ResultCode.E_SUCCESS && _tag.QualityCode == ResultCode.QUAL_GOOD)
                    return true;
                Logger.Warn(string.Format(Logger.Msg.WriteFail, _tag.Name, _tag.Value));
                return false;
            }
            catch(Exception) { throw; }
        }

        #endregion Read/Write By Tag

        public bool Connect(string IP, string Path = "0", int Timeout = 3000)
        {
#if !DEBUG
            try
            {
                if(IsConnected())
                    return true;
                lock(Locker)
                {
                    if(!string.IsNullOrWhiteSpace(IP) && !string.IsNullOrWhiteSpace(Path))
                    {
                        Ctr_OPC = new Controller
                        {
                            CPUType = Controller.CPU.LOGIX,
                            Simulate = false,
                            IPAddress = IP,
                            Path = Path,
                            Timeout = Timeout
                        };

                        if(Ctr_OPC.Connect() == ResultCode.E_SUCCESS && Ctr_OPC.IsConnected)
                            return true;
                    }
                }
                return false;
            }
            catch(Exception) { throw; }
#else
            return true;
#endif
        }

        public bool Disconnect()
        {
            Ctr_OPC?.Disconnect();
            return IsConnected();
        }

        #region Scanner

        public void SetupIndividualScanGroup(int interval = 10)
        {
            IndieTagGroup = new TagGroup
            {
                Controller = Ctr_OPC,
                Interval = interval,
                ScanningMode = TagGroup.SCANMODE.ReadWrite
            };
            foreach(Tag tag in Dic_TagName_Tag.Values)
            {
                if(tag.MyObject != null)
                    IndieTagGroup.AddTag(tag);
            }
        }

        public List<string> MonitorScanGroup_Lst_TagName()
        {
            Ctr_OPC.GroupRead(IndieTagGroup);
            return IndieTagGroup.Tags.ToArray()
                .Where(tag => ((Tag)tag).Value == ((Tag)tag).MyObject)
                .Select(tag => ((Tag)tag).Name).ToList();
        }

        public List<KeyValuePair<string, object>> ManualReadIndieTagGroup_Lst_KeyValuePair()
        {
            Ctr_OPC.GroupRead(IndieTagGroup);
            return IndieTagGroup.Tags.ToArray()
                .Select(tag =>
                new KeyValuePair<string, object>(((Tag)tag).Name, ((Tag)tag).Value)).ToList();
        }

        public List<Tag> ManualReadIndieTagGroup_Lst_Tag()
        {
            Ctr_OPC.GroupRead(IndieTagGroup);
            return IndieTagGroup.Tags.ToArray().Select(tag => tag as Tag).ToList();
        }

        public void SetupMaxPerformanceScanner(bool StartScanning = true, int interval = 30, int SizePerTagGroup = 15)
        {
            try
            {
                if(Dic_TagName_Tag.Count <= 0)
                    throw new Ex.CustomException(string.Format(Ex.Msg.TagListEmpty, nameof(Initialize)));
                if(Ctr_OPC == null)
                    throw new Ex.CustomException(string.Format(Ex.Msg.ControllerNull, nameof(Initialize), nameof(Connect)));

                if(lst_ScanControllers.Count > 0)
                {
                    foreach(Controller Ctrl in lst_ScanControllers)
                    {
                        Ctrl.Disconnect();
                        Ctrl.Dispose();
                    }
                    lst_ScanControllers.Clear();
                }
                if(lst_ScanGroups.Count > 0)
                {
                    foreach(TagGroup Tg in lst_ScanGroups)
                    {
                        Tg.ScanStop();
                        Tg.Dispose();
                    }
                    lst_ScanGroups.Clear();
                }

                lst_ScanControllers.Add(GetController(DeepClone: true));
                TagGroup tagGroup = new TagGroup { Controller = lst_ScanControllers.Last(), Interval = interval, ScanningMode = TagGroup.SCANMODE.ReadWrite };
                int nCount = 0;
                foreach(Tag tag in Dic_TagName_Tag.Values)
                {
                    tag.MyObject = tag.Name;
                    tag.Changed += new EventHandler(Tag_Updated);
                    tagGroup.AddTag(tag);
                    bool IsLast = ++nCount == Dic_TagName_Tag.Values.Count;
                    if(tagGroup.Tags.Count >= SizePerTagGroup || IsLast)
                    {
                        lst_ScanGroups.Add(tagGroup);
                        ///////////////////////////////////////////////////////////////////////////
                        /// Ingear official support recommends us to hold not more than 50 tags per taggroup,
                        /// in order to mitigate changed event miss rate
                        lst_ScanControllers.Add(GetController(DeepClone: true));
                        if(!IsLast)
                            tagGroup = new TagGroup
                            {
                                Controller = lst_ScanControllers.Last(),
                                Interval = interval,
                                ScanningMode = TagGroup.SCANMODE.ReadWrite
                            };
                    }
                }
                if(StartScanning)
                    StartScan();
            }
            catch(Exception) { throw; }
        }

        public void StartScan()
        {
            foreach(TagGroup tagGroup in lst_ScanGroups)
                tagGroup?.ScanStart();
        }

        public void StopScan()
        {
            foreach(TagGroup tagGroup in lst_ScanGroups)
                tagGroup?.ScanStop();
        }

        public void ReadAllTagFromScanGroup()
        {
            foreach(TagGroup tagGroup in lst_ScanGroups)
                Ctr_OPC.GroupRead(tagGroup);
        }

        private void Tag_Updated(object sender, EventArgs e)
        {
            DataChangeEventArgs args = (DataChangeEventArgs)e;
            if(OPCUpdate != null)
                OPCUpdate(new KeyValuePair<string, object>(args.MyObject.ToString(), args.Value), e);
        }

        public Controller GetController(bool DeepClone = false)
        {
            try
            {
                if(DeepClone)
                {
                    Controller Ctrler = new Controller { Timeout = Ctr_OPC.Timeout, Path = Ctr_OPC.Path, IPAddress = Ctr_OPC.IPAddress };
                    Ctrler.Connect();
                    if(!Ctrler.IsConnected)
                        Logger.Warn(Logger.Msg.ControllerConnectionFail);
                    return Ctrler;
                }
                else
                    return Ctr_OPC;
            }
            catch(Exception) { throw; }
        }

        #endregion Scanner
    }
}