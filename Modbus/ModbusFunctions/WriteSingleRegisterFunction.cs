using Common;
using Modbus.FunctionParameters;
using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;

namespace Modbus.ModbusFunctions
{
    /// <summary>
    /// Class containing logic for parsing and packing modbus write single register functions/requests.
    /// </summary>
    public class WriteSingleRegisterFunction : ModbusFunction
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WriteSingleRegisterFunction"/> class.
        /// </summary>
        /// <param name="commandParameters">The modbus command parameters.</param>
        public WriteSingleRegisterFunction(ModbusCommandParameters commandParameters) : base(commandParameters)
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

            // Start Address
            ushort startAddressNetworkOrder = (ushort)IPAddress.HostToNetworkOrder((short)parameters.OutputAddress);
            request[8] = (byte)(startAddressNetworkOrder >> 8); // High byte
            request[9] = (byte)(startAddressNetworkOrder & 0xFF); // Low byte

            // Quantity
            ushort quantityNetworkOrder = (ushort)IPAddress.HostToNetworkOrder((short)parameters.Value);
            request[10] = (byte)(quantityNetworkOrder >> 8); // High byte
            request[11] = (byte)(quantityNetworkOrder & 0xFF); // Low byte

            return request;
        }

        /// <inheritdoc />
        public override Dictionary<Tuple<PointType, ushort>, ushort> ParseResponse(byte[] response)
        {
            //TO DO: IMPLEMENT
            throw new NotImplementedException();
        }
    }
}