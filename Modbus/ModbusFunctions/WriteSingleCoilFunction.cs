using Common;
using Modbus.FunctionParameters;
using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;

namespace Modbus.ModbusFunctions
{
    /// <summary>
    /// Class containing logic for parsing and packing modbus write coil functions/requests.
    /// </summary>
    public class WriteSingleCoilFunction : ModbusFunction
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WriteSingleCoilFunction"/> class.
        /// </summary>
        /// <param name="commandParameters">The modbus command parameters.</param>
        public WriteSingleCoilFunction(ModbusCommandParameters commandParameters) : base(commandParameters)
        {
            CheckArguments(MethodBase.GetCurrentMethod(), typeof(ModbusWriteCommandParameters));
        }

        /// <inheritdoc />
        public override byte[] PackRequest()
        {
            ModbusWriteCommandParameters parameters = CommandParameters as ModbusWriteCommandParameters;

            if (parameters == null)
            {
                throw new ArgumentException("Command parameters are not of type ModbusWriteCommandParameters");
            }

            byte[] request = new byte[12];

            // Head part (7 bytes)
            ushort tidNetworkOrder = (ushort)IPAddress.HostToNetworkOrder((short)parameters.TransactionId);
            ushort pidNetworkOrder = (ushort)IPAddress.HostToNetworkOrder((short)parameters.ProtocolId);
            ushort lenNetworkOrder = (ushort)IPAddress.HostToNetworkOrder((short)parameters.Length);

            // Transaction ID (TID)
            request[0] = (byte)(tidNetworkOrder >> 8); // High byte
            request[1] = (byte)(tidNetworkOrder & 0xFF); // Low byte

            // Protocol ID (PID)
            request[2] = (byte)(pidNetworkOrder >> 8); // High byte
            request[3] = (byte)(pidNetworkOrder & 0xFF); // Low byte

            // Length (LEN)
            request[4] = (byte)(lenNetworkOrder >> 8); // High byte
            request[5] = (byte)(lenNetworkOrder & 0xFF); // Low byte

            // Unit ID
            request[6] = parameters.UnitId;

            // Data part (5 bytes)
            request[7] = parameters.FunctionCode;

            // Output Address
            ushort outputAddressNetworkOrder = (ushort)IPAddress.HostToNetworkOrder((short)parameters.OutputAddress);
            request[8] = (byte)(outputAddressNetworkOrder >> 8); // High byte
            request[9] = (byte)(outputAddressNetworkOrder & 0xFF); // Low byte

            // Quantity
            ushort valueNetworkOrder = (ushort)IPAddress.HostToNetworkOrder((short)parameters.Value);
            request[10] = (byte)(valueNetworkOrder >> 8); // High byte
            request[11] = (byte)(valueNetworkOrder & 0xFF); // Low byte

            return request;
        }

        /// <inheritdoc />
        public override Dictionary<Tuple<PointType, ushort>, ushort> ParseResponse(byte[] response)
        {
            //TO DO: IMPLEMENT

            Console.WriteLine();

            Dictionary<Tuple<PointType, ushort>, ushort> responseDict = new Dictionary<Tuple<PointType, ushort>, ushort>();

            ushort address = (ushort)IPAddress.NetworkToHostOrder(BitConverter.ToUInt16(response, 7));
            Console.WriteLine("address: " + address);
            ushort value = (ushort)IPAddress.NetworkToHostOrder(BitConverter.ToUInt16(response, 8));

            responseDict.Add(new Tuple<PointType,ushort>(PointType.DIGITAL_OUTPUT, address), value);

            return responseDict;

            throw new NotImplementedException();
        }
    }
}