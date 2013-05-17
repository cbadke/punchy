module private CommandBuilder

    open System

    let MessageStart = seq [ 0xF0uy; 0x7Euy; 0x00uy; 0x07uy; 0x02uy; 0x01uy; 0x00uy; 0x03uy ]
    let MessageEnd = seq [ 0xF7uy; ]

    type Command =
        | Brightness = 0x02uy
        | Trigger = 0x08uy
        | Color = 0x40uy

    type ColourChannel =
        | Red = 0x00uy
        | Green = 0x10uy
        | Blue = 0x20uy

    type LightIndex =
        | Record = 0x00uy
        | Ready = 0x01uy
        | Cue = 0x02uy
        | Note1 = 0x03uy
        | Note2 = 0x04uy

    type TriggerIndex =
        | Off = 0x00uy
        | Ready = 0x01uy
        | Record = 0x02uy
        | Cue = 0x03uy

    let ConvertToHalfByte (value : byte) =
        let valueToConvert = if value > 0xFDuy then 0xFDuy else value
        0x80uy - Convert.ToByte(Math.Ceiling((float)(valueToConvert + 0x01uy)/2.0))
