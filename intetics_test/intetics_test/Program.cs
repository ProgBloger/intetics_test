using System;
using System.IO;

namespace intetics_test
{
    class Program
    {
        const string inputFilename = "input.txt";
        const string outputFilename = "output.txt";
        const int MAX_BUFFER_SIZE = 512;

        private static int readFileCoursor = 0;
        private static int writeFileCoursor = 0;

        private static StreamReader sr;
        private static StreamWriter sw;

        static void Main(string[] args)
        {

            char[] bufferPutData = new char[MAX_BUFFER_SIZE];
            char[] chunkGetData;

            int bufferPutDataCoursor = 0;
            int chunkGetDataCoursor = 0;
            int chunkGetDataCount = 0;

            bool readingGetDataFinished = false;

            do
            {
                int upperBorder = bufferPutDataCoursor;
                bool maxBufferSizeReached = false;
                readingGetDataFinished = false;

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
                while (!maxBufferSizeReached && !readingGetDataFinished);

                // If after bufferPutData filled 512 chars
                // And not all chars from chunkGetData stored in bufferPutData
                // Put it to bufferPutData and set bufferPutDataCoursor

                // If buffer holds a 512 char array 
                // Call PutData
                // Else if buffer holds less than 512 
                // And next GetData returned 0
                // Call PutData and exit loop
                PutData(bufferPutData, bufferPutDataCoursor);

                bufferPutDataCoursor = 0;

                bool lastChunkNotSaved = chunkGetDataCoursor < chunkGetDataCount;
                if(lastChunkNotSaved)
                {
                    while (chunkGetDataCoursor < chunkGetDataCount)
                    {
                        bufferPutData[bufferPutDataCoursor++] = chunkGetData[chunkGetDataCoursor++];
                    }
                }
            }
            while (!readingGetDataFinished);

            if(sr != null)
            {
                sr.Dispose();
            }

            if (sw != null)
            {
                sw.Close();
                sw.Dispose();
            }
        }

        public static int GetData(out char[] buf)
        {
            return Read(out buf);
        }

        public static void PutData(char[] buf, int count)
        {
            Write(buf, count);
        }

        private static int Read(out char[] buf)
        {
            var random = new Random();
            int fileLength = (int) (new FileInfo(inputFilename).Length);

            if(sr == null)
                sr = new StreamReader(inputFilename);
            
            int readChars = random.Next(0, MAX_BUFFER_SIZE);

            if(fileLength < readFileCoursor + readChars)
            {
                readChars = fileLength - readFileCoursor;
            }

            buf = new char[readChars];

            sr.Read(buf, 0, readChars);

            return buf.Length;
        }

        private static void Write(char[] buf, int count)
        {
            if (sw == null)
                sw = new StreamWriter(outputFilename);
            
            sw.Write(buf, 0, count);
        }
    }
}
