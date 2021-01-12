namespace FT.src.extension
{
    public class Config
    {
        private UniversalInputConfig[] UniversalInputConfigs = new UniversalInputConfig[Extension.UNIVERSALINPUTS];
        public UniversalInputConfig GetUniversalInputConfig(int index)
        {
            return UniversalInputConfigs[index];
        }

        public void SetUniversalInputConfig(int index, UniversalInputConfig value)
        {
            UniversalInputConfigs[index] = value;
            Changed = true;
        }



        private byte[] MotorConfig = new byte[Extension.MOTORS];

        public void SetMotorConfig(int index, byte value)
        {
            MotorConfig[index] = value;
            Changed = true;
        }

        public byte GetMotorConfig(int index)
        {
            return MotorConfig[index];
        }

        private byte[] CounterConfig = new byte[Extension.MOTORS];

        public void SetCounterConfig(int index, byte value)
        {
            CounterConfig[index] = value;
            Changed = true;
        }

        public byte GetCounterConfig(int index)
        {
            return CounterConfig[index];
        }

        public bool Changed { set; get; } = true;


        public Config()
        {
            for (int i = 0; i < Extension.UNIVERSALINPUTS; i++)
            {
                UniversalInputConfigs[i] = new UniversalInputConfig();
            }

            for (int i = 0; i < Extension.MOTORS; i++)
            {
                MotorConfig[i] = 1;
                CounterConfig[i] = 1;
            }
        }
    }
}