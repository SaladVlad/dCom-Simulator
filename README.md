# Mini Drone Control System

This project implements a control system for a mini drone with the following components:

- **Four Motor Outputs**: Control power to the four motors of the drone.
- **Battery Charger**: Manages the charging of the drone's battery.
- **Battery Indicator**: Monitors the battery level.
- **Two Distance Sensors**: Measure horizontal and vertical distances.
- **Power Signal Digital Input**: Indicates the power signal.

## Communication Details

- **RTU Slave Address**: 105
- **Transport Protocol**: TCP
- **Port**: 59860

## Inputs and Outputs

| Size | Type         | Address | Description                            |
|------|--------------|---------|----------------------------------------|
| M1   | Analog Output| 4000    | Power for Motor 1                      |
| M2   | Analog Output| 4001    | Power for Motor 2                      |
| M3   | Analog Output| 4002    | Power for Motor 3                      |
| M4   | Analog Output| 4003    | Power for Motor 4                      |
| B1   | Analog Input | 3800    | Battery Capacity Indicator             |
| S1   | Analog Input | 3801    | First Distance Sensor Value            |
| S2   | Analog Input | 3802    | Second Distance Sensor Value           |
| D1   | Digital Output| 2000    | Power Signal ON/OFF                    |

## Functionality

- Periodically reads all digital inputs/outputs and updates UI values every 2 seconds.
- Periodically reads all analog inputs/outputs and updates UI values every 4 seconds.
- Allows control through a control window for all defined digital outputs (coils) and updates values on the UI after successful write.
- Enables control through a control window for all defined analog outputs (holding registers) and updates values on the UI after successful write.

## Configuration

Configure communication parameters in the dCom application and simulator to establish a TCP connection. Properly configure the "RtuCfg.txt" file according to the specified sizes and values in the system.
