using System;
using System.Net;
class Program
{
  static void Main(string[] args)
  {
    while (true)
    {
      Console.WriteLine("IP Routing Simulation Menu:");
      Console.WriteLine("1. IP to Binary Conversion");
      Console.WriteLine("2. Binary to IP Conversion");
      Console.WriteLine("3. Subnet Calculation");
      Console.WriteLine("0. Exit");
      string choice = Console.ReadLine();
      if (choice == "1")
      {
        Console.Write("Enter IP address");
        string ip = Console.ReadLine();
        if (IPRoutingSimulations.ValidateIp(ip))
        {
          string binary = IPRoutingSimulations.IpToBinary(ip);
          Console.WriteLine("Binary representation: {0}", binary);
        }
        else
        {
          Console.WriteLine("IPAdress invalid Format");

        }
      }
      else if (choice == "2")
      {
        Console.Write("Enter binary IP address: ");
        string binary = Console.ReadLine();
        string ip = IPRoutingSimulations.BinaryToIp(binary);
        Console.WriteLine("IP address representation: {0}", ip);

      }
      else if (choice == "3")
      {
        Console.Write("Enter IP address: ");
        string subnetIp = Console.ReadLine();
        Console.Write("Enter subnet mask (e.g., 255.255.255.0): ");
        string subnetMaskString = Console.ReadLine();

        // Convert subnet mask string to IPAddress object
        IPAddress subnetMask = IPAddress.Parse(subnetMaskString);

        if (IPRoutingSimulations.ValidateIp(subnetIp) && subnetMask != null) // Check for valid formats
        {
          IPAddress ipAddress = IPAddress.Parse(subnetIp);
          var (networkAddress, broadcastAddress) = IPRoutingSimulations.calculatedSubnet(ipAddress, subnetMask);
          Console.WriteLine("Network address: {0}", networkAddress);
          Console.WriteLine("Broadcast address: {0}", broadcastAddress);
        }
        else
        {
          Console.WriteLine("Invalid IP address or subnet mask format.");
        }

      }


    }
  }
}
