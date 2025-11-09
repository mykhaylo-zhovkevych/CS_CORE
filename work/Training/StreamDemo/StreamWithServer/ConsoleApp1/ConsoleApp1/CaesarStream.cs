using ConsoleApp1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamDemo.StreamWithServer.ConsoleApp1.ConsoleApp1
{
    public class CaesarStream : Stream
    {
        private readonly Stream _baseStream;
        public bool IsClient { get; set; } = false;

        public CaesarStream(Stream baseStream)
        {
            _baseStream = baseStream;
        }

        public override bool CanRead => _baseStream.CanRead;

        public override bool CanSeek => false;

        public override bool CanWrite => _baseStream.CanWrite;

        public override long Length => _baseStream.Length;

        public override long Position 
        { 
            get => throw new NotImplementedException(); 
            set => throw new NotImplementedException(); 
        }

        public override void Flush()
        {
            _baseStream.Flush();
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            int bytesRead = _baseStream.Read(buffer, offset, count);
            if (bytesRead <= 0)
                return bytesRead;

            string encText = Encoding.UTF8.GetString(buffer, offset, bytesRead);
            string decText = Helper.Decrypt(encText);

            byte[] outBytes = Encoding.UTF8.GetBytes(decText);
            Array.Copy(outBytes, 0, buffer, offset, outBytes.Length);

            return outBytes.Length;


        }
        public override void Write(byte[] buffer, int offset, int count)
        {
            string plainText = Encoding.UTF8.GetString(buffer, offset, count);
            string encText = Helper.Encrypt(plainText);
            byte[] outBytes = Encoding.UTF8.GetBytes(encText);

            if (IsClient)
            {
                Console.WriteLine("[DEBUG] Before encryption: " + encText);
            }
            _baseStream.Write(outBytes, 0, outBytes.Length);
            
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotImplementedException();
        }

        public override void SetLength(long value)
        {
            throw new NotImplementedException();
        }

    }
}
