namespace Punchy
    module internal CommandBuilder =

        open System
        open Constants

        let private MessageStart = [| 0xF0uy; 0x7Euy; 0x00uy; 0x07uy; 0x02uy; 0x01uy; 0x00uy; 0x03uy |]
        let private MessageEnd = [| 0xF7uy; |]

        type private Command =
            static member Brightness = 0x02uy
            static member Trigger = 0x08uy
            static member Color = 0x40uy

        type private ColourChannel =
            static member Red = 0x00uy
            static member Green = 0x10uy
            static member Blue = 0x20uy

        type private LightIndex =
            static member Record = 0x00uy
            static member Ready = 0x01uy
            static member Cue = 0x02uy
            static member Note1 = 0x03uy
            static member Note2 = 0x04uy

        type private TriggerIndex =
            static member Off = 0x00uy
            static member Ready = 0x01uy
            static member Record = 0x02uy
            static member Cue = 0x03uy

        let private ConvertToHalfByte (value : byte) =
            let valueToConvert = if value > 0xFDuy then 0xFDuy else value
            0x80uy - Convert.ToByte(Math.Ceiling((float)(valueToConvert + 0x01uy)/2.0))

        let private BuildCommand command argument =
            Array.concat [MessageStart; [| command; argument |]; MessageEnd]

        let DeactivateLight() = BuildCommand Command.Trigger TriggerIndex.Off

        let ActivateLight color =
            let light = match color with
                        | Color1 -> TriggerIndex.Ready
                        | Color2 -> TriggerIndex.Record
                        | Flash -> TriggerIndex.Cue
            BuildCommand Command.Trigger light