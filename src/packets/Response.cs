using System;
using System.Collections.Generic;

namespace FT.src.packets
{
    public class Response
    {
        protected Header header = new Header();
        public ushort SessionId 
        {
            get
            {
                return header.SessionId;
            }
        }

        public ushort TransactionId
        {
            get
            {
                return header.TrancactionId;
            }
        }
        public void Load(List<byte> bytes)
        {
            header.Load(bytes);
            int n = BitConverter.ToInt32(Utils.GetByteArray(bytes, 20, 4));
            if (bytes.Count > 27)
            {
                LoadPayload(bytes.GetRange(24, header.Length - 20));
            }
            else
            {
                LoadPayload(new List<byte>());
            }
        }

        protected virtual void LoadPayload(List<byte> list) { }
        public virtual int GetLength() { return 27; }
    }
}