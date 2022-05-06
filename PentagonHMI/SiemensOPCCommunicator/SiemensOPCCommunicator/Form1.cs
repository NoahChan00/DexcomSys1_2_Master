using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OpcRcw.Comn;
using OpcRcw.Da;
using System.Runtime.InteropServices;
using System.Collections;
using System.Threading;


namespace SiemensOPCCommunicator
{
    public partial class Form1 : Form
    {

        #region Variable
        OPC Write;
        #endregion

        #region Constructor
        public Form1()
        {
            InitializeComponent();
        }
        #endregion
        
        #region FormEvent
        private void btnStart_Click(object sender, EventArgs e)
        {
            try
            {
                //ArrayList _arrAddress = new ArrayList();

                //_arrAddress.Add("S7:[S7 connection_1]DB1000,REAL0");
                //_arrAddress.Add("S7:[S7 connection_1]DB1000,REAL4");
                //_arrAddress.Add("S7:[S7 connection_1]DB1000,STRING16.250");

                //OPC item1 = new OPC(_arrAddress,"S7:[S7 connection_1]DB1000,STRING8.1");

                //item1.Connect();
                //item1.AsyncReading();
                //item1.OnUpdateEvent += new OPC.OnUpdateHandler(item1_OnUpdate);

                //if (_arrAddress != null)
                //{
                //    _arrAddress = null;
                //}

                ArrayList _arrAddress = new ArrayList();

                _arrAddress.Add("S7:[S7 connection_1]DB507,STRING104.15");
                _arrAddress.Add("S7:[S7 connection_1]DB507,STRING14.1");
                //_arrAddress.Add("S7:[S7 connection_1]DB507,STRING14.1");

                OPC item1 = new OPC(_arrAddress,"",false);

                item1.Connect();
                item1.AsyncReading();
                item1.OnUpdateEvent += new OPC.OnUpdateHandler(item1_OnUpdate);

                if (_arrAddress != null)
                {
                    _arrAddress = null;
                }

                ArrayList _arrWriteAddress = new ArrayList();

                _arrWriteAddress.Add("S7:[S7 connection_1]DB507,STRING104.15");
                //_arrWriteAddress.Add("S7:[S7 connection_1]DB507,STRING14.1");

                Write = new OPC(_arrWriteAddress, "S7:[S7 connection_1]DB507,STRING14.1", false);
                Write.Connect();
                Write.OnWriteCompleteEvent +=new OPC.OnWriteCompleteHandler(Write_OnWriteCompleteEvent);

                if (_arrWriteAddress != null)
                {
                    _arrWriteAddress = null;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

                

        private void btnWrite_Click(object sender, EventArgs e)
        {


            Write.Write("S7:[S7 connection_1]DB507,STRING104.15", textBox1.Text);
            Write.Write("S7:[S7 connection_1]DB507,STRING14.1", textBox2.Text);
            //OPC item1 = new OPC();            
            //item1.Write("S7:[S7 connection_1]DB1000,REAL0", textBox1.Text);
            //if (item1 != null)
            //{
            //    item1 = null;
            //}
            //OPC item2 = new OPC();
            //item2.Write("S7:[S7 connection_1]DB1000,REAL4", textBox1.Text);
            //if (item2 != null)
            //{
            //    item2 = null;
            //}

            //OPC item3 = new OPC();
            //item3.Write("S7:[S7 connection_1]DB1000,STRING16.250", textBox2.Text);
            //if (item3 != null)
            //{
            //    item3 = null;
            //}

            //OPC item4 = new OPC();
            ////item4.OnWriteCompleteEvent += new OPC.OnWriteCompleteHandler(item1_OnWriteCompleteEvent);
            //item4.Write("S7:[S7 connection_1]DB1000,STRING8.1", textBox3.Text);
            //if (item4 != null)
            //{
            //    item4 = null;
            //}
        }

        //void item1_OnWriteCompleteEvent(object o, OnWriteCompleteEventArgs e)
        //{
        //    label2.Text = e.WriteOutput;
        //}
        
        #endregion

        #region Method
        void item1_OnUpdate(object o, OnUpdateEventArgs e)
        {
            string _strResult = "";
            foreach (string str in e.OPCAddresses)
            {
                _strResult += str + Environment.NewLine;
            }
            ThreadExceptionDialog.CheckForIllegalCrossThreadCalls = false;
            this.label1.Text = _strResult;
            ThreadExceptionDialog.CheckForIllegalCrossThreadCalls = true;
            //throw new NotImplementedException();
        }

        void Write_OnWriteCompleteEvent(object o, OnWriteCompleteEventArgs e)
        {
            
            ThreadExceptionDialog.CheckForIllegalCrossThreadCalls = false;
            string str = e.WriteResult.ToString();
            //MessageBox.Show(str);
            ThreadExceptionDialog.CheckForIllegalCrossThreadCalls = true;
        }
        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            Write.Write("S7:[S7 connection_1]DB507,STRING104.15", textBox3.Text);
            Write.Write("S7:[S7 connection_1]DB507,STRING14.1", textBox4.Text);
        }

        

    }
}
