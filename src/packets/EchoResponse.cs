using System.Collections.Generic;

namespace FT.src.packets
{
    public class EchoResponse : Response
    {
        private int Response;
        protected override void LoadPayload(List<byte> bytes)
        {
            Response = header.CommandCode;
        }

        public bool Success()
        {
            return Response == 0x65;
        }
    }
}