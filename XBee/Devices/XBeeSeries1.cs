﻿using System;
using System.Threading.Tasks;
using XBee.Frames.AtCommands;

namespace XBee.Devices
{
    internal class XBeeSeries1 : XBeeNode
    {
        internal XBeeSeries1(XBeeController controller,
            HardwareVersion hardwareVersion = HardwareVersion.XBeeSeries1, 
            NodeAddress address = null) : base(controller, hardwareVersion, address)
        {
        }

        public virtual async Task<bool> IsCoordinator()
        {
            CoordinatorEnableResponseData response =
                await ExecuteAtQueryAsync<CoordinatorEnableResponseData>(new CoordinatorEnableCommand());

            if(response.EnableState == null)
                throw new InvalidOperationException("No valid coordinator state returned.");

            return response.EnableState.Value == CoordinatorEnableState.Coordinator;
        }

        public virtual async Task SetCoordinator(bool enable)
        {
            await ExecuteAtCommandAsync(new CoordinatorEnableCommand(enable));
        }

        public async Task<SleepOptions> GetSleepOptions()
        {
            var response = await ExecuteAtQueryAsync<SleepOptionsResponseData>(new SleepOptionsCommand());

            if(response.Options == null)
                throw new InvalidOperationException("No valid sleep options returned.");

            return response.Options.Value;
        }

        public async Task SetSleepOptions(SleepOptions options)
        {
            await ExecuteAtCommandAsync(new SleepOptionsCommand(options));
        }
    }
}