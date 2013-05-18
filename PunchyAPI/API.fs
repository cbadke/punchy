namespace Punchy
    module API =

        type Light(id) =
            member this.TurnOn() =
                let data = [| 0xF0uy; 0x7Euy; 0x00uy;
                              0x07uy; 0x02uy; 0x01uy;
                              0x00uy; 0x03uy; 0x08uy;
                              0x02uy; 0xF7uy; |]
                Native.SendData data id

            member this.TurnOff() =
                let data = [| 0xF0uy; 0x7Euy; 0x00uy;
                              0x07uy; 0x02uy; 0x01uy;
                              0x00uy; 0x03uy; 0x08uy;
                              0x00uy; 0xF7uy; |]
                Native.SendData data id

        let GetLights() =
            Native.AvailableDevices() |> List.map (fun x -> new Light(x))