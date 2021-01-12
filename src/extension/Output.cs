namespace FT.src.extension
{
    public class Output
    {
        public ushort[] CounterResetCommandId { private set; get; } = new ushort[Extension.MOTORS];
        public byte[] MotorMasterValues { private set; get; } = new byte[Extension.MOTORS];
        public short[] PwmOutputValues { private set; get; } = new short[Extension.MOTORS * 2];
        public ushort[] MotorDistanceValues { private set; get; } = new ushort[Extension.MOTORS];
        public ushort[] MotorCommandId { private set; get; } = new ushort[Extension.MOTORS];

    }
}