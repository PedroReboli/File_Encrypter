using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using static System.Math;
namespace File_Crypto
{
    class Encrypt_File
    {
        public static void cript(string Path,string Password)
        {
            byte[] Key = Hash_Generation.Hash(Password);
            FileStream File = new FileStream(Path, (FileMode)FileAccess.ReadWrite);
            long FileSize = File.Length;
            int Position = 0;
            int Pointer = 0;
            int BufferSize = 1048576;
            byte[] Buffer = new byte[BufferSize+1];
            bool End = false;
            int KeyLength = Key.Length -1;
            if (BufferSize > FileSize) { BufferSize = (int)FileSize; End = true; }
            Form1 frm = (Form1)Application.OpenForms["Form1"];

            frm.progressBar1.Maximum = (int)FileSize;
            while (true){
                File.Seek(Position, SeekOrigin.Begin);
                File.Read(Buffer,0, BufferSize);
                
                for (int sp = 0; sp <= BufferSize; sp++)
                {
                    Buffer[sp] = (byte)(Key[(int)Abs(Round(Sin(KeyLength + Pointer + Key[Pointer]) * 1000) % KeyLength)] ^ Buffer[sp]); //criptografia
                    Pointer++; //aumenta a posicao da senha para deixar mais aleatorio
                    if (Pointer > KeyLength)
                    {
                        Pointer = 0;
                    }
                }
                File.Seek(Position, SeekOrigin.Begin);
                File.Write(Buffer,0, BufferSize);

                frm.progressBar1.Value = Position;
                if (End == true | Position == FileSize) //se for a ultima rodada fecha o arquivo e sai o loop
                {
                    File.Flush();
                    File.SetLength(FileSize);
                    File.Close();
                    break;
                }
                if (Position + BufferSize > FileSize) //se a posicao do arquivo for maior do que o tamanho do arquivo
                {
                    BufferSize = (int)(FileSize - Position)-1;
                    Position += BufferSize; //restante de bytes restantes
                    End = true;
                }
                else
                {
                    Position += BufferSize;
                }
            }

        }
    }
}
