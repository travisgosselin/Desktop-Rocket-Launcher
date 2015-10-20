namespace RocketLauncher.Api
{
    public static class ByteCommands
    {
        public static byte[] Up
        {
            get
            {
                var byteArray = new byte[10];
                byteArray[1] = 2;
                byteArray[2] = 2;

                return byteArray;
            }
        }

        public static byte[] Down
        {
            get
            {
                var byteArray = new byte[10];
                byteArray[1] = 2;
                byteArray[2] = 1;

                return byteArray;
            }
        }

        public static byte[] Left
        {
            get
            {
                var byteArray = new byte[10];
                byteArray[1] = 2;
                byteArray[2] = 4;

                return byteArray;
            }
        }

        public static byte[] Right
        {
            get
            {
                var byteArray = new byte[10];
                byteArray[1] = 2;
                byteArray[2] = 8;

                return byteArray;
            }
        }

        public static byte[] Fire
        {
            get
            {
                var byteArray = new byte[10];
                byteArray[1] = 2;
                byteArray[2] = 16;

                return byteArray;
            }
        }

        public static byte[] Stop
        {
            get
            {
                var byteArray = new byte[10];
                byteArray[1] = 2;
                byteArray[2] = 32;

                return byteArray;
            }
        }

        public static byte[] GetStatus
        {
            get
            {
                var byteArray = new byte[9];
                byteArray[1] = 1;

                return byteArray;
            }
        }

        public static byte[] LedOn
        {
            get
            {
                var byteArray = new byte[9];
                byteArray[1] = 3;
                byteArray[2] = 1;

                return byteArray;
            }
        }

        public static byte[] LedOff
        {
            get
            {
                var byteArray = new byte[9];
                byteArray[1] = 3;

                return byteArray;
            }
        }
    }
}
