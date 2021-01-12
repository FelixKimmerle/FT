namespace FT.src.extension
{
    public class Input
    {
        private ushort[] UniversalInputs = new ushort[Extension.UNIVERSALINPUTS];

        public ushort GetUniversalInput(int index)
        {
            return UniversalInputs[index];
        }

        public void SetUniversalInput(int index, ushort value)
        {
            Changed |= UniversalInputs[index] != value;
            UniversalInputs[index] = value;
        }

        private byte[] CounterInput = new byte[Extension.MOTORS];

        public byte GetCounterInput(int index)
        {
            return CounterInput[index];
        }

        public void SetCounterInput(int index, byte value)
        {
            Changed |= CounterInput[index] != value;
            CounterInput[index] = value;
        }

        private ushort[] CounterValue = new ushort[Extension.MOTORS];
        public ushort GetCounterValue(int index)
        {
            return CounterValue[index];
        }

        public void SetCounterValue(int index, ushort value)
        {
            Changed |= CounterValue[index] != value;
            CounterValue[index] = value;
        }

        private ushort DisplayButtonLeft = 0;
        public ushort GetDisplayButtonLeft()
        {
            return DisplayButtonLeft;
        }

        public void SetDisplayButtonLeft(ushort value)
        {
            Changed |= DisplayButtonLeft != value;
            DisplayButtonLeft = value;
        }

        private ushort DisplayButtonRight { set; get; }
        public ushort GetDisplayButtonRight()
        {
            return DisplayButtonRight;
        }

        public void SetDisplayButtonRight(ushort value)
        {
            Changed |= DisplayButtonRight != value;
            DisplayButtonRight = value;
        }

        private ushort[] MotorReachedDestination = new ushort[Extension.MOTORS];
        public ushort GetMotorReachedDestination(int index)
        {
            return MotorReachedDestination[index];
        }

        public void SetMotorReachedDestination(int index, ushort value)
        {
            Changed |= MotorReachedDestination[index] != value;
            MotorReachedDestination[index] = value;
        }

        private ushort[] ResetCounter = new ushort[Extension.MOTORS];
        public ushort GetResetCounter(int index)
        {
            return ResetCounter[index];
        }

        public void SetResetCounter(int index, ushort value)
        {
            Changed |= ResetCounter[index] != value;
            ResetCounter[index] = value;
        }

        public bool Changed { set; get; } = true;
    }
}