using System.Collections.Generic;

namespace FT.src.packets
{
    public class ConfigResponse : Response
    {
        private int Response;
        private int Num;

        public ConfigResponse(int Num)
        {
            this.Num = Num;
        }

        public override int GetLength()
        {
            return 27 + 4 * Num;
        }

        protected override void LoadPayload(List<byte> bytes)
        {
            Response = header.CommandCode;
        }

        public bool Success()
        {
            return Response == 0x69;
        }
    }
}