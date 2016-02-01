using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using System.IO;
using System.Net.Mail;
using System.Windows.Forms;
using System.Text;

namespace KitchenGeeks
{
    public class SimpleAES
    {
        // Change these keys
        private byte[] Key = { 22, 217, 45, 11, 24, 26, 99, 45, 114, 8, 27, 75, 37, 112, 124, 209, 241, 1, 175, 127, 173, 53, 196, 29, 24, 26, 17, 218, 131, 236, 53, 209 };
        private byte[] Vector = { 146, 64, 191, 215, 23, 3, 113, 119, 55, 254, 252, 112, 79, 32, 114, 80 };


        private ICryptoTransform EncryptorTransform, DecryptorTransform;
        private System.Text.UTF8Encoding UTFEncoder;

        public SimpleAES()
        {
            //This is our encryption method
            RijndaelManaged rm = new RijndaelManaged();

            //Create an encryptor and a decryptor using our encryption method, key, and vector.
            EncryptorTransform = rm.CreateEncryptor(this.Key, this.Vector);
            DecryptorTransform = rm.CreateDecryptor(this.Key, this.Vector);

            //Used to translate bytes to text and vice versa
            UTFEncoder = new System.Text.UTF8Encoding();
        }

        public string EncryptToString(string TextValue)
        {
            if (TextValue == null || TextValue.Length == 0) return TextValue;
            return Convert.ToBase64String(Encrypt(TextValue));
        }

        public byte[] Encrypt(string TextValue)
        {
            //Translates our text value into a byte array.
            Byte[] bytes = UTFEncoder.GetBytes(TextValue);

            //Used to stream the data in and out of the CryptoStream.
            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(memoryStream, EncryptorTransform, CryptoStreamMode.Write))
                {
                    cs.Write(bytes, 0, bytes.Length);
                    cs.FlushFinalBlock();

                    memoryStream.Position = 0;
                    byte[] encrypted = new byte[memoryStream.Length];
                    memoryStream.Read(encrypted, 0, encrypted.Length);
                    return encrypted;
                }
            }
        }

        public string DecryptString(string EncryptedString)
        {
            if (EncryptedString == null || EncryptedString.Length == 0) return EncryptedString;
            return Decrypt(Convert.FromBase64String(EncryptedString));
        }

        public string Decrypt(byte[] EncryptedValue)
        {
            using (MemoryStream encryptedStream = new MemoryStream())
            {
                using (CryptoStream decryptStream = new CryptoStream(encryptedStream, DecryptorTransform,
                    CryptoStreamMode.Write))
                {
                    decryptStream.Write(EncryptedValue, 0, EncryptedValue.Length);
                    decryptStream.FlushFinalBlock();

                    encryptedStream.Position = 0;
                    Byte[] decryptedBytes = new Byte[encryptedStream.Length];
                    encryptedStream.Read(decryptedBytes, 0, decryptedBytes.Length);
                    encryptedStream.Close();
                    return UTFEncoder.GetString(decryptedBytes);
                }
            }
        }

        public byte[] StrToByteArray(string str)
        {
            if (str.Length == 0)
                throw new Exception("Invalid string value in StrToByteArray");

            byte val;
            byte[] byteArr = new byte[str.Length / 3];
            int i = 0;
            int j = 0;
            do
            {
                val = byte.Parse(str.Substring(i, 3));
                byteArr[j++] = val;
                i += 3;
            }
            while (i < str.Length);
            return byteArr;
        }

        public string ByteArrToString(byte[] byteArr)
        {
            byte val;
            string tempStr = "";
            for (int i = 0; i <= byteArr.GetUpperBound(0); i++)
            {
                val = byteArr[i];
                if (val < (byte)10)
                    tempStr += "00" + val.ToString();
                else if (val < (byte)100)
                    tempStr += "0" + val.ToString();
                else
                    tempStr += val.ToString();
            }
            return tempStr;
        }
    }

    public struct SMTPTestResults
    {
        public bool Connected;
        public bool Acknowledged;
        public bool AuthenticationRequired;
        public bool AuthenticationSucceeded;
    }

    public class SMTPFunctions
    {
        public static SMTPTestResults TestSMTP(string server, int port, string user = null, string pass = null)
        {
            SMTPTestResults results = new SMTPTestResults();
            results.Connected = false;
            results.Acknowledged = false;
            results.AuthenticationRequired = false;
            results.AuthenticationSucceeded = false;

            using (TcpClient client = new TcpClient())
            {
                try
                {
                    client.Connect(server, port);
                }
                catch
                {
                    return results;
                }
                results.Connected = true;
                using (NetworkStream ns = client.GetStream())
                {
                    try
                    {
                        string line = ReadLine(ns);
                        if (!line.StartsWith("220"))
                        {
                            return results;
                        }
                        byte[] message = Encoding.ASCII.GetBytes("EHLO " + Environment.MachineName + "\n");
                        ns.Write(message, 0, message.Length);
                        string code = "";
                        while (code == "")
                        {
                            line = ReadLine(ns);
                            if (line.StartsWith("250-AUTH") && line.Contains("PLAIN"))
                                results.AuthenticationRequired = true;
                            if (line.Length > 3 && line.Substring(3, 1) == " ")
                                code = line.Substring(0, 3);
                        }
                        results.Acknowledged = (code == "250");
                        if (user != null && pass != null)
                        {
                            string authMessage = GetAuthCommand(user, pass);
                            message = Encoding.ASCII.GetBytes(authMessage + "\n");
                            ns.Write(message, 0, message.Length);
                            line = ReadLine(ns);
                            results.AuthenticationSucceeded = line.StartsWith("235");
                        }
                    }
                    finally
                    {
                        try
                        {
                            byte[] message = Encoding.ASCII.GetBytes("QUIT\n");
                            ns.Write(message, 0, message.Length);
                        }
                        catch { }
                    }
                }
            }
            return results;
        }

        public static string GetAuthCommand(string username, string password)
        {
            string data = "\0" + username + "\0" + password;
            return "AUTH PLAIN " + Convert.ToBase64String(Encoding.ASCII.GetBytes(data));
        }


        private static string ReadLine(NetworkStream ns)
        {
            string output = "";
            DateTime timeout = DateTime.Now.AddSeconds(10);
            while (timeout > DateTime.Now)
            {
                if (ns.DataAvailable)
                {
                    byte[] inData = new byte[1];
                    inData[0] = (byte)ns.ReadByte();
                    output += Encoding.ASCII.GetString(inData, 0, 1);
                    timeout = DateTime.Now.AddSeconds(10);

                    if (output.Contains("\n"))
                    {
                        output = output.Replace("\r", "").Replace("\n", "");
                        return output;
                    }
                }
            }
            return output;
        }

        public static bool SendMessage(MailAddress to, string subject, string body)
        {
            // Use the first part of the email address if we didn't get provided a name.
            string fromName = Config.Settings.SMTPFromName;
            if (fromName.Length == 0)
                fromName = Config.Settings.SMTPFromAddress.Substring(0, Config.Settings.SMTPFromAddress.IndexOf("@"));
            while (true)
            {
                try
                {
                    MailMessage message = new MailMessage();
                    message.From = new MailAddress(Config.Settings.SMTPFromAddress, fromName);
                    message.Subject = subject;
                    message.To.Add(to);
                    message.Body = body;

                    SmtpClient client = new SmtpClient(Config.Settings.SMTPServer, Config.Settings.SMTPPort);
                    client.Send(message);

                    MessageBox.Show("Your message has been successfully sent.", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return true;
                }
                catch (SmtpException ex)
                {
                    string msg = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                    if (MessageBox.Show("Email failure while trying to send the email: " + msg + " Retry?",
                        "Error While Sending", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error,
                        MessageBoxDefaultButton.Button1) == DialogResult.Cancel)
                        return false;
                }
                catch (Exception ex)
                {
                    string msg = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                    if (MessageBox.Show("General failure trying to send the email: " + msg + " Retry?",
                        "Error While Sending", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error,
                        MessageBoxDefaultButton.Button1) == DialogResult.Cancel)
                        return false;
                }
            }

        }
    }
}
