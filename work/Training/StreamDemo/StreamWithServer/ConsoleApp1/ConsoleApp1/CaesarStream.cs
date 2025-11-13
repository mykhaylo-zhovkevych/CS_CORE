using ConsoleApp1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace StreamDemo.StreamWithServer.ConsoleApp1.ConsoleApp1
{
    public class CaesarStream : Stream, IDisposable
    {
        private readonly Stream _baseStream;

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


        public async Task<string> WriteFromServerAsync(string data)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(data + "\n");

            await WriteAsync(bytes, 0, data.Length);
            await FlushAsync();
            // await Task.Delay(1000);

            return Encoding.UTF8.GetString(bytes); ;
            //finally
            //{
            //    Dispose();
            //}   
        }

        public async Task ReadFromClientAsync()
        {
            byte[] buffer = new byte[1024];
            int length = await ReadAsync(buffer, 0, buffer.Length);

            string response = Encoding.UTF8.GetString(buffer, 0, length);
            Console.WriteLine("Response from Server: " + response);
        //    finally
        //    {
        //        Dispose();
        //    }
        //}


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


            Console.WriteLine("[DEBUG] Before encryption: " + encText);
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
