using System;

namespace intetics_test
{
    class Program
    {
        static void Main(string[] args)
        {
            const int MAX_BUFFER_SIZE = 512;

            char[] bufferPutData = new char[MAX_BUFFER_SIZE];
            char[] chunkGetData;

            int bufferPutDataCoursor = 0;
            int chunkGetDataCoursor = 0;
            int chunkGetDataCount = 0;

            do
            {
                int upperBorder = 0;
                bufferPutDataCoursor = 0;
                bool maxBufferSizeReached = false;
                bool readingGetDataFinished = false;

                // Call GetData while buffer doesn't holds all 512 chars
                do
                {
                    // Getting new chunk
                    chunkGetDataCount = GetData(out chunkGetData);
                    chunkGetDataCoursor = 0;

                    upperBorder += chunkGetDataCount;

                    readingGetDataFinished = chunkGetDataCount == 0;
                    maxBufferSizeReached = upperBorder >= MAX_BUFFER_SIZE;

                    if (maxBufferSizeReached)
                    {
                        upperBorder = MAX_BUFFER_SIZE;
                    }

                    while (bufferPutDataCoursor < upperBorder)
                    {
                        bufferPutData[bufferPutDataCoursor++] = chunkGetData[chunkGetDataCoursor++];
                    }
                }
                while (!maxBufferSizeReached || readingGetDataFinished);

                // If after buffer filled 512 chars
                // And not all chars from GetData stored in buffer
                // Memoise them by holding coursor to temp array

                // If buffer holds a 512 char array 
                // Call PutData
                // Else if buffer holds less than 512 
                // And next GetData returned 0
                // Call PutData and exit loop
                PutData(bufferPutData, bufferPutDataCoursor);


            }
            while (chunkGetDataCount != 0);
        }

        public static int GetData(out char[] buf)
        {
            buf = new char[5];

            // The function provides input
            // It returns the number of chars placed into buf array by each call
            // A value can be less than or equal to 512
            // Number varies from one call to the next
            // If it's 0 then input is exhausted
            
            return buf.Length;
        }

        public static void PutData(char[] buf, int count)
        {
            // Accepts output
            // Writes "count" chars to output from buf
            // Count must represent the number of characters
            // contained in byf and be 512 for every call, except the last call
            // It may be less than 512 (even 0) for the last call only
        }
    }
}
