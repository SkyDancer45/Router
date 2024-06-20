using System.Net;
using System.Text.RegularExpressions;
public static class IPRoutingSimulations

{
    public static bool ValidateIp(string ip)
    {
        // Regex pattern for a valid IP address (excluding leading/trailing whitespace)
        string pattern = @"^(?:[0-9]{1,3}\.){3}[0-9]{1,3}$";

        return Regex.IsMatch(ip, pattern);
    }

    public static string IpToBinary(string ip)
    {
        if (!ValidateIp(ip))
        {
            return "Invalid IP address format.";
        }
        string[] ipParts = ip.Split('.');
        string binary = "";
        foreach (string part in ipParts)
        {

            int octetValue = int.Parse(part);
            string octetBinary = Convert.ToString(octetValue, 2).PadLeft(8, '0');
            binary += octetBinary;

        }
        return binary;
    }
    public static string BinaryToIp(string binary)
    {
        if (binary.Length != 32)
        {
            return "Invalud binary IP addr format must be 32 bits.";

        }
        string ip = "";
        for (int i = 0; i < binary.Length; i += 8)
        {
            string octetBinary = binary.Substring(i, 8);
            int octetValue = Convert.ToInt32(octetBinary, 2);
            ip += octetValue.ToString() + ".";
        }
        return ip.Substring(0, ip.Length - 1);

    }
    public static (IPAddress networkAddress, IPAddress brodcastAddress) calculatedSubnet(IPAddress ip, IPAddress subnetMask)
    {
        byte[] ipBytes = ip.GetAddressBytes();
        byte[] maskBytes = subnetMask.GetAddressBytes();
        byte[] networkAddressBytes = new byte[ipBytes.Length];

        for (int i = 0; i < ipBytes.Length; i++)
        {
            networkAddressBytes[i] = (byte)(networkAddressBytes[i] & maskBytes[i]);

        }
        IPAddress networkAddress = new IPAddress(networkAddressBytes);
        byte[] invertedMaskBytes = maskBytes.Select(b => (byte)~b).ToArray();
        byte[] brodcastAddressBytes = new byte[ipBytes.Length];
        for (int i = 0; i < ipBytes.Length; i++)
        {
            brodcastAddressBytes[i] = (byte)(networkAddressBytes[i] | invertedMaskBytes[i]);
        }
        IPAddress brodcastAddress = new IPAddress(brodcastAddressBytes);
        return (networkAddress, brodcastAddress);


    }
    public static string ArpRequest(Dictionary<IPAddress, string> arpTable, IPAddress senderIp, IPAddress targetIp)
    {
        if (arpTable.ContainsKey(targetIp))
        {
            return arpTable[targetIp];
        }
        else
        {
            return "MAC address not found in ARP table";
        }
    }

    public static string RarpReply(Dictionary<string, (IPAddress, string)> deviceTable, string senderMac)
    {
        if (deviceTable.ContainsKey(senderMac))
        {
            return deviceTable[senderMac].Item1.ToString();
        }
        else
        {
            return "IP address not found in device table";
        }
    }
}
